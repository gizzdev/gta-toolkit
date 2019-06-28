using System.Collections.Generic;
using System.Linq;
using SharpDX;
using RageLib.Resources.GTA5.PC.Meta;

namespace RageLib.GTA5.ResourceWrappers.PC.Meta.Structures
{
	public class MCDistantLODLight : MetaStructureWrapper<CDistantLODLight>
	{
		public static MetaName _MetaName = MetaName.CDistantLODLight;
		public MetaFile Meta;
		public List<MVECTOR3> Position { get { return this.Entries.Select(e => e.Position).ToList(); }}
		public List<uint> RGBI { get { return this.Entries.Select(e => e.RGBI).ToList(); } }
        public ushort NumStreetLights;
		public ushort Category;

        public List<DistantLODLightEntry> Entries = new List<DistantLODLightEntry>();

        public MCDistantLODLight()
		{
			this.MetaName = MetaName.CDistantLODLight;
			this.MetaStructure = new CDistantLODLight();
		}

		public static void AddEnumAndStructureInfo(MetaBuilder mb)
		{
			var enumInfos = MetaInfo.GetStructureEnumInfo(MCDistantLODLight._MetaName);

			for (int i = 0; i < enumInfos.Length; i++)
				mb.AddEnumInfo((MetaName) enumInfos[i].EnumNameHash);

			mb.AddStructureInfo(MCDistantLODLight._MetaName);
			mb.AddStructureInfo(MetaName.VECTOR3);
		}


		public override void Parse(MetaFile meta, CDistantLODLight CDistantLODLight)
		{
			this.Meta = meta;
			this.MetaStructure = CDistantLODLight;

			var _position = MetaUtils.ConvertDataArray<VECTOR3>(meta, CDistantLODLight.position);
            List<MVECTOR3> position = _position?.Select(e => { var msw = new MVECTOR3(); msw.Parse(meta, e); return msw; }).ToList();

            uint[] rgbi = MetaUtils.ConvertDataArray<uint>(meta, CDistantLODLight.RGBI.Pointer, CDistantLODLight.RGBI.Count1).ToArray();

            Entries.Clear();

            for (int i = 0; i < position.Count; i++)
            {
                Entries.Add(new DistantLODLightEntry()
                {
                    Position = position[i],
                    RGBI     = rgbi[i],
                });
            }

            this.NumStreetLights = CDistantLODLight.numStreetLights;
			this.Category = CDistantLODLight.category;
		}

		public override void Build(MetaBuilder mb, bool isRoot = false)
		{
			if(this.Position != null)
				this.MetaStructure.position = mb.AddItemArrayPtr(MetaName.VECTOR3, this.Position.Select(e => { e.Build(mb); return e.MetaStructure; }).ToArray());
 			MVECTOR3.AddEnumAndStructureInfo(mb);                    

			this.MetaStructure.RGBI = mb.AddUintArrayPtr(this.RGBI.ToArray());
            this.MetaStructure.numStreetLights = this.NumStreetLights;
			this.MetaStructure.category = this.Category;

 			MCDistantLODLight.AddEnumAndStructureInfo(mb);          

			if(isRoot)
			{
				mb.AddItem(this.MetaName, this.MetaStructure);

				this.Meta = mb.GetMeta();
			}
		}
	}

    public struct DistantLODLightEntry
    {
        public MVECTOR3 Position;
        public uint RGBI;
    }
}
