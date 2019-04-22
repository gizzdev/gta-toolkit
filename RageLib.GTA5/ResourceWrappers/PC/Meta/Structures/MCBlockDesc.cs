using System.Collections.Generic;
using System.Linq;
using SharpDX;
using RageLib.Resources.GTA5.PC.Meta;
using System;

namespace RageLib.GTA5.ResourceWrappers.PC.Meta.Structures
{
	public class MCBlockDesc : MetaStructureWrapper<CBlockDesc>
	{
		public static MetaName _MetaName = MetaName.CBlockDesc;
		public MetaFile Meta;
		public uint Version;
		public uint Flags;
		public string Name = "GTAUtil";
		public string ExportedBy = "GTAUtil";
		public string Owner = "GTAUtil";
		public string Time = DateTime.UtcNow.ToString("dd MMMM yyyy HH:mm");

		public MCBlockDesc()
		{
			this.MetaName = MetaName.CBlockDesc;
			this.MetaStructure = new CBlockDesc();
		}

		public static void AddEnumAndStructureInfo(MetaBuilder mb)
		{
			var enumInfos = MetaInfo.GetStructureEnumInfo(MCBlockDesc._MetaName);

			for (int i = 0; i < enumInfos.Length; i++)
				mb.AddEnumInfo((MetaName) enumInfos[i].EnumNameHash);

			mb.AddStructureInfo(MCBlockDesc._MetaName);
		}


		public override void Parse(MetaFile meta, CBlockDesc CBlockDesc)
		{
			this.Meta = meta;
			this.MetaStructure = CBlockDesc;

			this.Version = CBlockDesc.version;
			this.Flags = CBlockDesc.flags;
			this.Name = MetaUtils.GetString(Meta, CBlockDesc.name) ?? "GTAUtil";
			this.ExportedBy = MetaUtils.GetString(Meta, CBlockDesc.exportedBy) ?? "GTAUtil";
            this.Owner = MetaUtils.GetString(Meta, CBlockDesc.owner) ?? "GTAUtil";
			this.Time = MetaUtils.GetString(Meta, CBlockDesc.time) ?? DateTime.UtcNow.ToString("dd MMMM yyyy HH:mm");
		}

		public override void Build(MetaBuilder mb, bool isRoot = false)
		{
			this.MetaStructure.version = this.Version;
			this.MetaStructure.flags = this.Flags;
			this.MetaStructure.name = mb.AddStringPtr(this.Name);
			this.MetaStructure.exportedBy = mb.AddStringPtr(this.ExportedBy);
			this.MetaStructure.owner = mb.AddStringPtr(this.Owner);
			this.MetaStructure.time = mb.AddStringPtr(this.Time);

 			MCBlockDesc.AddEnumAndStructureInfo(mb);                    

			if(isRoot)
			{
				mb.AddItem(this.MetaName, this.MetaStructure);

				this.Meta = mb.GetMeta();
			}
		}
	}
}
