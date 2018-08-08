using System.Collections.Generic;
using System.Linq;

using SharpDX;

namespace RageLib.Resources.GTA5.PC.Meta.ExtendedTypes
{
	public class CLODLight : MetaStructureWrapper<PC.Meta.CLODLight>
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

		public CLODLight(MetaName metaName) : base(metaName)
		{
			this.MetaStructure = new PC.Meta.CLODLight();
		}

		public void Parse(MetaFile meta, PC.Meta.CLODLight CLODLight)
		{
			this.Meta = meta;
			this.MetaStructure = CLODLight;

			var direction = MetaUtils.ConvertArray_Structure<PC.Meta.VECTOR3>(meta, CLODLight.direction);
			if(direction != null)
				this.Direction = (List<VECTOR3>) (direction.ToList().Select(e => { var msw = new VECTOR3((MetaName) (-489959468)); msw.Parse(meta, e); return msw; }));

			// this.Falloff = CLODLight.falloff;
			// this.FalloffExponent = CLODLight.falloffExponent;
			// this.TimeAndStateFlags = CLODLight.timeAndStateFlags;
			// this.Hash = CLODLight.hash;
			// this.ConeInnerAngle = CLODLight.coneInnerAngle;
			// this.ConeOuterAngleOrCapExt = CLODLight.coneOuterAngleOrCapExt;
			// this.CoronaIntensity = CLODLight.coronaIntensity;
		}

		public void Build(MetaBuilder mb, bool isRoot = false)
		{
			if(this.Direction != null)
				this.MetaStructure.direction = mb.AddItemArrayPtr((MetaName) (-489959468), this.Direction.Select(e => e.MetaStructure).ToArray());
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
