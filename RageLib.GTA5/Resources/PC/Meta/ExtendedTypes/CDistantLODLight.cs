using System.Collections.Generic;
using System.Linq;

using SharpDX;

namespace RageLib.Resources.GTA5.PC.Meta.ExtendedTypes
{
	public class CDistantLODLight : MetaStructureWrapper<PC.Meta.CDistantLODLight>
	{
		public MetaFile Meta;
		public List<VECTOR3> Position;
		public Array_uint RGBI;
		public ushort NumStreetLights;
		public ushort Category;

		public CDistantLODLight(MetaName metaName) : base(metaName)
		{
			this.MetaStructure = new PC.Meta.CDistantLODLight();
		}

		public void Parse(MetaFile meta, PC.Meta.CDistantLODLight CDistantLODLight)
		{
			this.Meta = meta;
			this.MetaStructure = CDistantLODLight;

			var position = MetaUtils.ConvertArray_Structure<PC.Meta.VECTOR3>(meta, CDistantLODLight.position);
			if(position != null)
				this.Position = (List<VECTOR3>) (position.ToList().Select(e => { var msw = new VECTOR3((MetaName) (-489959468)); msw.Parse(meta, e); return msw; }));

			// this.RGBI = CDistantLODLight.RGBI;
			this.NumStreetLights = CDistantLODLight.numStreetLights;
			this.Category = CDistantLODLight.category;
		}

		public void Build(MetaBuilder mb, bool isRoot = false)
		{
			if(this.Position != null)
				this.MetaStructure.position = mb.AddItemArrayPtr((MetaName) (-489959468), this.Position.Select(e => e.MetaStructure).ToArray());
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
