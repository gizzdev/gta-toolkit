using System.Collections.Generic;
using System.Linq;
using SharpDX;
using RageLib.Resources.GTA5.PC.Meta;

namespace RageLib.GTA5.ResourceWrappers.PC.Meta.Structures
{
	public class CDistantLODLight : MetaStructureWrapper<RageLib.Resources.GTA5.PC.Meta.CDistantLODLight>
	{
		public MetaFile Meta;
		public List<VECTOR3> Position;
		public Array_uint RGBI;
		public ushort NumStreetLights;
		public ushort Category;

		public CDistantLODLight()
		{
			this.MetaName = MetaName.CDistantLODLight;
			this.MetaStructure = new RageLib.Resources.GTA5.PC.Meta.CDistantLODLight();
		}

		public override void Parse(MetaFile meta, RageLib.Resources.GTA5.PC.Meta.CDistantLODLight CDistantLODLight)
		{
			this.Meta = meta;
			this.MetaStructure = CDistantLODLight;

			var position = MetaUtils.ConvertDataArray<RageLib.Resources.GTA5.PC.Meta.VECTOR3>(meta, CDistantLODLight.position);
			this.Position = position?.Select(e => { var msw = new VECTOR3(); msw.Parse(meta, e); return msw; }).ToList();

			// this.RGBI = CDistantLODLight.RGBI;
			this.NumStreetLights = CDistantLODLight.numStreetLights;
			this.Category = CDistantLODLight.category;
		}

		public override void Build(MetaBuilder mb, bool isRoot = false)
		{
			if(this.Position != null)
				this.MetaStructure.position = mb.AddItemArrayPtr(MetaName.VECTOR3, this.Position.Select(e => e.MetaStructure).ToArray());
			// this.MetaStructure.RGBI = this.RGBI;
			this.MetaStructure.numStreetLights = this.NumStreetLights;
			this.MetaStructure.category = this.Category;

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
