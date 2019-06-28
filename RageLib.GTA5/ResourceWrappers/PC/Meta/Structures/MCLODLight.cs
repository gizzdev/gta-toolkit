using System.Collections.Generic;
using System.Linq;
using SharpDX;
using RageLib.Resources.GTA5.PC.Meta;

namespace RageLib.GTA5.ResourceWrappers.PC.Meta.Structures
{
	public class MCLODLight : MetaStructureWrapper<CLODLight>
	{
		public static MetaName _MetaName = MetaName.CLODLight;
		public MetaFile Meta;
		public List<MVECTOR3> Direction { get { return this.Entries.Select(e => e.Direction).ToList(); } }
        public List<float> Falloff { get { return this.Entries.Select(e => e.FallOff).ToList(); } }
		public List<float> FalloffExponent { get { return this.Entries.Select(e => e.FalloffExponent).ToList(); } }
        public List<uint> TimeAndStateFlags { get { return this.Entries.Select(e => e.TimeAndStateFlags).ToList(); } }
        public List<uint> Hash { get { return this.Entries.Select(e => e.Hash).ToList(); } }
        public List<byte> ConeInnerAngle { get { return this.Entries.Select(e => e.ConeInnerAngle).ToList(); } }
        public List<byte> ConeOuterAngleOrCapExt { get { return this.Entries.Select(e => e.ConeOuterAngleOrCapExt).ToList(); } }
        public List<byte> CoronaIntensity { get { return this.Entries.Select(e => e.CoronaIntensity).ToList(); } }

        public List<LODLightEntry> Entries = new List<LODLightEntry>();

		public MCLODLight()
		{
			this.MetaName = MetaName.CLODLight;
			this.MetaStructure = new CLODLight();
		}

		public static void AddEnumAndStructureInfo(MetaBuilder mb)
		{
			var enumInfos = MetaInfo.GetStructureEnumInfo(MCLODLight._MetaName);

			for (int i = 0; i < enumInfos.Length; i++)
				mb.AddEnumInfo((MetaName) enumInfos[i].EnumNameHash);

			mb.AddStructureInfo(MCLODLight._MetaName);
			mb.AddStructureInfo(MetaName.VECTOR3);
		}


		public override void Parse(MetaFile meta, CLODLight CLODLight)
		{
			this.Meta = meta;
			this.MetaStructure = CLODLight;

			var _direction = MetaUtils.ConvertDataArray<VECTOR3>(meta, CLODLight.direction);
			List<MVECTOR3> direction = _direction?.Select(e => { var msw = new MVECTOR3(); msw.Parse(meta, e); return msw; }).ToList();

            float[] falloff = MetaUtils.ConvertDataArray<float>(meta, CLODLight.falloff.Pointer, CLODLight.falloff.Count1).ToArray();
            float[] falloffExponent = MetaUtils.ConvertDataArray<float>(meta, CLODLight.falloffExponent.Pointer, CLODLight.falloffExponent.Count1).ToArray();
            uint[] timeAndStateFlags = MetaUtils.ConvertDataArray<uint>(meta, CLODLight.timeAndStateFlags.Pointer, CLODLight.timeAndStateFlags.Count1).ToArray();
            uint[] hash = MetaUtils.ConvertDataArray<uint>(meta, CLODLight.hash.Pointer, CLODLight.hash.Count1).ToArray();
            byte[] coneInnerAngle =  MetaUtils.ConvertDataArray<byte>(meta, CLODLight.coneInnerAngle.Pointer, CLODLight.coneInnerAngle.Count1).ToArray();
            byte[] coneOuterAngleOrCapExt = MetaUtils.ConvertDataArray<byte>(meta, CLODLight.coneOuterAngleOrCapExt.Pointer, CLODLight.coneOuterAngleOrCapExt.Count1).ToArray();
            byte[] coronaIntensity = MetaUtils.ConvertDataArray<byte>(meta, CLODLight.coronaIntensity.Pointer, CLODLight.coronaIntensity.Count1).ToArray();

            Entries.Clear();

            for(int i = 0; i < hash.Length; i++)
            {
                Entries.Add(new LODLightEntry()
                {
                    Direction = direction[i],
                    FallOff = falloff[i],
                    FalloffExponent = falloffExponent[i],
                    TimeAndStateFlags = timeAndStateFlags[i],
                    Hash = hash[i],
                    ConeInnerAngle = coneInnerAngle[i],
                    ConeOuterAngleOrCapExt = coneOuterAngleOrCapExt[i],
                    CoronaIntensity = coronaIntensity[i]
                });
            }
        }

		public override void Build(MetaBuilder mb, bool isRoot = false)
		{
			if(this.Direction != null)
				this.MetaStructure.direction = mb.AddItemArrayPtr(MetaName.VECTOR3, this.Direction.Select(e => { e.Build(mb); return e.MetaStructure; }).ToArray());

            MVECTOR3.AddEnumAndStructureInfo(mb);

            this.MetaStructure.falloff = mb.AddFloatArrayPtr(this.Falloff.ToArray());
			this.MetaStructure.falloffExponent = mb.AddFloatArrayPtr(this.FalloffExponent.ToArray());
			this.MetaStructure.timeAndStateFlags = mb.AddUintArrayPtr(this.TimeAndStateFlags.ToArray());
			this.MetaStructure.hash = mb.AddUintArrayPtr(this.Hash.ToArray());
			this.MetaStructure.coneInnerAngle = mb.AddByteArrayPtr(this.ConeInnerAngle.ToArray());
			this.MetaStructure.coneOuterAngleOrCapExt = mb.AddByteArrayPtr(this.ConeOuterAngleOrCapExt.ToArray());
            this.MetaStructure.coronaIntensity = mb.AddByteArrayPtr(this.CoronaIntensity.ToArray());

            MCLODLight.AddEnumAndStructureInfo(mb);                    

			if(isRoot)
			{
				mb.AddItem(this.MetaName, this.MetaStructure);

				this.Meta = mb.GetMeta();
			}
		}
	}

    public struct LODLightEntry
    {
        public MVECTOR3 Direction;
        public float FallOff;
        public float FalloffExponent;
        public uint TimeAndStateFlags;
        public uint Hash;
        public byte ConeInnerAngle;
        public byte ConeOuterAngleOrCapExt;
        public byte CoronaIntensity;
    }
}
