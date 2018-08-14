using System.Collections.Generic;
using System.Linq;
using SharpDX;
using RageLib.Resources.GTA5.PC.Meta;

namespace RageLib.GTA5.ResourceWrappers.PC.Meta.Structures
{
	public class CLODLight : MetaStructureWrapper<RageLib.Resources.GTA5.PC.Meta.CLODLight>
	{
		public MetaFile Meta;
		public List<VECTOR3> Direction;
		public Array_float Falloff;
		public Array_float FalloffExponent;
		public Array_uint TimeAndStateFlags;
		public Array_uint Hash;
		public Array_byte ConeInnerAngle;
		public Array_byte ConeOuterAngleOrCapExt;
		public Array_byte CoronaIntensity;

		public CLODLight()
		{
			this.MetaName = MetaName.CLODLight;
			this.MetaStructure = new RageLib.Resources.GTA5.PC.Meta.CLODLight();
		}

		public override void Parse(MetaFile meta, RageLib.Resources.GTA5.PC.Meta.CLODLight CLODLight)
		{
			this.Meta = meta;
			this.MetaStructure = CLODLight;

			var direction = MetaUtils.ConvertDataArray<RageLib.Resources.GTA5.PC.Meta.VECTOR3>(meta, CLODLight.direction);
			this.Direction = direction?.Select(e => { var msw = new VECTOR3(); msw.Parse(meta, e); return msw; }).ToList();

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
				this.MetaStructure.direction = mb.AddItemArrayPtr(MetaName.VECTOR3, this.Direction.Select(e => e.MetaStructure).ToArray());
			// this.MetaStructure.falloff = this.Falloff;
			// this.MetaStructure.falloffExponent = this.FalloffExponent;
			// this.MetaStructure.timeAndStateFlags = this.TimeAndStateFlags;
			// this.MetaStructure.hash = this.Hash;
			// this.MetaStructure.coneInnerAngle = this.ConeInnerAngle;
			// this.MetaStructure.coneOuterAngleOrCapExt = this.ConeOuterAngleOrCapExt;
			// this.MetaStructure.coronaIntensity = this.CoronaIntensity;

			var enumInfos = MetaInfo.GetStructureEnumInfo(this.MetaName);
			var structureInfo = MetaInfo.GetStructureInfo(this.MetaName);
			var childStructureInfos = MetaInfo.GetStructureChildInfo(this.MetaName);

			for (int i = 0; i < enumInfos.Length; i++)
				mb.AddEnumInfo((MetaName) enumInfos[i].EnumNameHash);

			mb.AddStructureInfo((MetaName) structureInfo.StructureNameHash);


			for (int i = 0; i < childStructureInfos.Length; i++)
				mb.AddStructureInfo((MetaName) childStructureInfos[i].StructureNameHash);

			if(isRoot)
			{
				mb.AddItem(this.MetaName, this.MetaStructure);

				this.Meta = mb.GetMeta();
			}
		}
	}
}
