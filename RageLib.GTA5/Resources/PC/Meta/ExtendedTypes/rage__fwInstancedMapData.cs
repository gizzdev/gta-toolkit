using System.Collections.Generic;
using System.Linq;

using SharpDX;

namespace RageLib.Resources.GTA5.PC.Meta.ExtendedTypes
{
	public class rage__fwInstancedMapData : MetaStructureWrapper<PC.Meta.rage__fwInstancedMapData>
	{
		public MetaFile Meta;
		public uint ImapLink;
		public Array_Structure PropInstanceList;
		public List<rage__fwGrassInstanceListDef> GrassInstanceList;

		public rage__fwInstancedMapData(MetaName metaName) : base(metaName)
		{
			this.MetaStructure = new PC.Meta.rage__fwInstancedMapData();
		}

		public void Parse(MetaFile meta, PC.Meta.rage__fwInstancedMapData rage__fwInstancedMapData)
		{
			this.Meta = meta;
			this.MetaStructure = rage__fwInstancedMapData;

			this.ImapLink = rage__fwInstancedMapData.ImapLink;
			// this.PropInstanceList = rage__fwInstancedMapData.PropInstanceList;
			var GrassInstanceList = MetaUtils.ConvertArray_Structure<PC.Meta.rage__fwGrassInstanceListDef>(meta, rage__fwInstancedMapData.GrassInstanceList);
			if(GrassInstanceList != null)
				this.GrassInstanceList = (List<rage__fwGrassInstanceListDef>) (GrassInstanceList.ToList().Select(e => { var msw = new rage__fwGrassInstanceListDef(MetaName.rage__fwGrassInstanceListDef); msw.Parse(meta, e); return msw; }));

		}

		public void Build(MetaBuilder mb, bool isRoot = false)
		{
			this.MetaStructure.ImapLink = this.ImapLink;
			// this.MetaStructure.PropInstanceList = this.PropInstanceList;
			if(this.GrassInstanceList != null)
				this.MetaStructure.GrassInstanceList = mb.AddItemArrayPtr(MetaName.rage__fwGrassInstanceListDef, this.GrassInstanceList.Select(e => e.MetaStructure).ToArray());

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
