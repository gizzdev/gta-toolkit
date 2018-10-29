using System.Collections.Generic;
using System.Linq;
using SharpDX;
using RageLib.Resources.GTA5.PC.Meta;

namespace RageLib.GTA5.ResourceWrappers.PC.Meta.Structures
{
	public class MCComponentInfo : MetaStructureWrapper<CComponentInfo>
	{
		public static MetaName _MetaName = MetaName.CComponentInfo;
		public MetaFile Meta;
		public uint Unk_802196719;
		public uint Unk_4233133352;
		public ArrayOfBytes5 Unk_128864925 = new ArrayOfBytes5();
		public uint Flags;
		public int Inclusions;
		public int Exclusions;
		public short Unk_1613922652; // Unk_884254308 flags
        public ushort Unk_2114993291;
		public byte Unk_3509540765;
		public byte Unk_4196345791;

		public MCComponentInfo()
		{
			this.MetaName = MetaName.CComponentInfo;
			this.MetaStructure = new CComponentInfo();
		}

		public static void AddEnumAndStructureInfo(MetaBuilder mb)
		{
			var enumInfos = MetaInfo.GetStructureEnumInfo(MCComponentInfo._MetaName);

			for (int i = 0; i < enumInfos.Length; i++)
				mb.AddEnumInfo((MetaName) enumInfos[i].EnumNameHash);

			mb.AddStructureInfo(MCComponentInfo._MetaName);
		}


		public override void Parse(MetaFile meta, CComponentInfo CComponentInfo)
		{
			this.Meta = meta;
			this.MetaStructure = CComponentInfo;

			this.Unk_802196719 = CComponentInfo.Unk_802196719;
			this.Unk_4233133352 = CComponentInfo.Unk_4233133352;
			this.Unk_128864925 = CComponentInfo.Unk_128864925;
			this.Flags = CComponentInfo.flags;
			this.Inclusions = CComponentInfo.inclusions;
			this.Exclusions = CComponentInfo.exclusions;
			this.Unk_1613922652 = CComponentInfo.Unk_1613922652;
			this.Unk_2114993291 = CComponentInfo.Unk_2114993291;
			this.Unk_3509540765 = CComponentInfo.Unk_3509540765;
			this.Unk_4196345791 = CComponentInfo.Unk_4196345791;
		}

		public override void Build(MetaBuilder mb, bool isRoot = false)
		{
			this.MetaStructure.Unk_802196719 = this.Unk_802196719;
			this.MetaStructure.Unk_4233133352 = this.Unk_4233133352;
			this.MetaStructure.Unk_128864925 = this.Unk_128864925;
			this.MetaStructure.flags = this.Flags;
			this.MetaStructure.inclusions = this.Inclusions;
			this.MetaStructure.exclusions = this.Exclusions;
			this.MetaStructure.Unk_1613922652 = this.Unk_1613922652;
			this.MetaStructure.Unk_2114993291 = this.Unk_2114993291;
			this.MetaStructure.Unk_3509540765 = this.Unk_3509540765;
			this.MetaStructure.Unk_4196345791 = this.Unk_4196345791;

 			MCComponentInfo.AddEnumAndStructureInfo(mb);          

			if(isRoot)
			{
				mb.AddItem(this.MetaName, this.MetaStructure);

				this.Meta = mb.GetMeta();
			}
		}
	}
}
