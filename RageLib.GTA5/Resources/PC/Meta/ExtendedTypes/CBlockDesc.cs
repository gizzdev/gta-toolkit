using System;
using System.Collections.Generic;
using System.Linq;

using SharpDX;

namespace RageLib.Resources.GTA5.PC.Meta.ExtendedTypes
{
	public class CBlockDesc : MetaStructureWrapper<PC.Meta.CBlockDesc>
	{
		public MetaFile Meta;
		public uint Version;
		public uint Flags;
		public string Name = "";
		public string ExportedBy = "RageLib";
		public string Owner = "";
		public string Time = DateTime.UtcNow.ToString("dd MMMM yyyy HH:mm");

		public CBlockDesc(MetaName metaName) : base(metaName)
		{
			this.MetaStructure = new PC.Meta.CBlockDesc();
		}

		public void Parse(MetaFile meta, PC.Meta.CBlockDesc CBlockDesc)
		{
			this.Meta = meta;
			this.MetaStructure = CBlockDesc;

			this.Version = CBlockDesc.version;
			this.Flags = CBlockDesc.flags;
			this.Name = MetaUtils.GetString(Meta, CBlockDesc.name);
			this.ExportedBy = MetaUtils.GetString(Meta, CBlockDesc.exportedBy);
			this.Owner = MetaUtils.GetString(Meta, CBlockDesc.owner);
			this.Time = MetaUtils.GetString(Meta, CBlockDesc.time);
		}

		public void Build(MetaBuilder mb, bool isRoot = false)
		{
			this.MetaStructure.version = this.Version;
			this.MetaStructure.flags = this.Flags;
			this.MetaStructure.name = mb.AddStringPtr(this.Name);
			this.MetaStructure.exportedBy = mb.AddStringPtr(this.ExportedBy);
			this.MetaStructure.owner = mb.AddStringPtr(this.Owner);
			this.MetaStructure.time = mb.AddStringPtr(this.Time);

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
