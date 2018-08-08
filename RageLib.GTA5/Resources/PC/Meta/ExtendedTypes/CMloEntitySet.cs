using System.Collections.Generic;
using System.Linq;

using SharpDX;

namespace RageLib.Resources.GTA5.PC.Meta.ExtendedTypes
{
	public class CMloEntitySet : MetaStructureWrapper<PC.Meta.CMloEntitySet>
	{
		public MetaFile Meta;
		public uint Name;
		public Array_uint Locations;
		public Array_StructurePointer Entities;

		public CMloEntitySet(MetaName metaName) : base(metaName)
		{
			this.MetaStructure = new PC.Meta.CMloEntitySet();
		}

		public void Parse(MetaFile meta, PC.Meta.CMloEntitySet CMloEntitySet)
		{
			this.Meta = meta;
			this.MetaStructure = CMloEntitySet;

			this.Name = CMloEntitySet.name;
			// this.Locations = CMloEntitySet.locations;
			// this.Entities = CMloEntitySet.entities;
		}

		public void Build(MetaBuilder mb, bool isRoot = false)
		{
			this.MetaStructure.name = this.Name;
			// this.MetaStructure.locations = this.Locations;
			// this.MetaStructure.entities = this.Entities;

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
