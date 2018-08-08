using System.Collections.Generic;
using System.Linq;

using SharpDX;

namespace RageLib.Resources.GTA5.PC.Meta.ExtendedTypes
{
	public class CMapTypes : MetaStructureWrapper<PC.Meta.CMapTypes>
	{
		public MetaFile Meta;
		public Array_StructurePointer Extensions;
		public Array_StructurePointer Archetypes;
		public uint Name;
		public Array_uint Dependencies;
		public List<CCompositeEntityType> CompositeEntityTypes;

		public CMapTypes(MetaName metaName) : base(metaName)
		{
			this.MetaStructure = new PC.Meta.CMapTypes();
		}

		public void Parse(MetaFile meta, PC.Meta.CMapTypes CMapTypes)
		{
			this.Meta = meta;
			this.MetaStructure = CMapTypes;

			// this.Extensions = CMapTypes.extensions;
			// this.Archetypes = CMapTypes.archetypes;
			this.Name = CMapTypes.name;
			// this.Dependencies = CMapTypes.dependencies;
			var compositeEntityTypes = MetaUtils.ConvertArray_Structure<PC.Meta.CCompositeEntityType>(meta, CMapTypes.compositeEntityTypes);
			if(compositeEntityTypes != null)
				this.CompositeEntityTypes = (List<CCompositeEntityType>) (compositeEntityTypes.ToList().Select(e => { var msw = new CCompositeEntityType(MetaName.CCompositeEntityType); msw.Parse(meta, e); return msw; }));

		}

		public void Build(MetaBuilder mb, bool isRoot = false)
		{
			// this.MetaStructure.extensions = this.Extensions;
			// this.MetaStructure.archetypes = this.Archetypes;
			this.MetaStructure.name = this.Name;
			// this.MetaStructure.dependencies = this.Dependencies;
			if(this.CompositeEntityTypes != null)
				this.MetaStructure.compositeEntityTypes = mb.AddItemArrayPtr(MetaName.CCompositeEntityType, this.CompositeEntityTypes.Select(e => e.MetaStructure).ToArray());

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
