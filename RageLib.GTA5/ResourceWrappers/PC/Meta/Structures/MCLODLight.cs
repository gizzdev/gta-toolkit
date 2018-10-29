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
		public List<MVECTOR3> Direction = new List<MVECTOR3>();
		public Array_float Falloff;
		public Array_float FalloffExponent;
		public Array_uint TimeAndStateFlags;
		public Array_uint Hash;
		public Array_byte ConeInnerAngle;
		public Array_byte ConeOuterAngleOrCapExt;
		public Array_byte CoronaIntensity;

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

			var direction = MetaUtils.ConvertDataArray<VECTOR3>(meta, CLODLight.direction);
			this.Direction = direction?.Select(e => { var msw = new MVECTOR3(); msw.Parse(meta, e); return msw; }).ToList();

			// this.Falloff = CLODLight.falloff;
			// this.FalloffExponent = CLODLight.falloffExponent;
			// this.TimeAndStateFlags = CLODLight.timeAndStateFlags;
			// this.Hash = CLODLight.hash;
			// this.ConeInnerAngle = CLODLight.coneInnerAngle;
			// this.ConeOuterAngleOrCapExt = CLODLight.coneOuterAngleOrCapExt;
			// this.CoronaIntensity = CLODLight.coronaIntensity;
		}

		public override void Build(MetaBuilder mb, bool isRoot = false)
		{
			if(this.Direction != null)
				this.MetaStructure.direction = mb.AddItemArrayPtr(MetaName.VECTOR3, this.Direction.Select(e => { e.Build(mb); return e.MetaStructure; }).ToArray());
 			MVECTOR3.AddEnumAndStructureInfo(mb);                    

			// this.MetaStructure.falloff = this.Falloff;
			// this.MetaStructure.falloffExponent = this.FalloffExponent;
			// this.MetaStructure.timeAndStateFlags = this.TimeAndStateFlags;
			// this.MetaStructure.hash = this.Hash;
			// this.MetaStructure.coneInnerAngle = this.ConeInnerAngle;
			// this.MetaStructure.coneOuterAngleOrCapExt = this.ConeOuterAngleOrCapExt;
			// this.MetaStructure.coronaIntensity = this.CoronaIntensity;

 			MCLODLight.AddEnumAndStructureInfo(mb);                    

			if(isRoot)
			{
				mb.AddItem(this.MetaName, this.MetaStructure);

				this.Meta = mb.GetMeta();
			}
		}
	}
}
