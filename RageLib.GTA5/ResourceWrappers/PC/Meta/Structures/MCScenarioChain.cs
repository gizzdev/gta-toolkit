using System.Collections.Generic;
using System.Linq;
using SharpDX;
using RageLib.Resources.GTA5.PC.Meta;

namespace RageLib.GTA5.ResourceWrappers.PC.Meta.Structures
{
	public class MCScenarioChain : MetaStructureWrapper<CScenarioChain>
	{
		public static MetaName _MetaName = MetaName.CScenarioChain;
		public MetaFile Meta;
		public byte Unk_1156691834;
		public Array_ushort EdgeIds;

		public MCScenarioChain()
		{
			this.MetaName = MetaName.CScenarioChain;
			this.MetaStructure = new CScenarioChain();
		}

		public static void AddEnumAndStructureInfo(MetaBuilder mb)
		{
			var enumInfos = MetaInfo.GetStructureEnumInfo(MCScenarioChain._MetaName);

			for (int i = 0; i < enumInfos.Length; i++)
				mb.AddEnumInfo((MetaName) enumInfos[i].EnumNameHash);

			mb.AddStructureInfo(MCScenarioChain._MetaName);
		}


		public override void Parse(MetaFile meta, CScenarioChain CScenarioChain)
		{
			this.Meta = meta;
			this.MetaStructure = CScenarioChain;

			this.Unk_1156691834 = CScenarioChain.Unk_1156691834;
			// this.EdgeIds = CScenarioChain.EdgeIds;
		}

		public override void Build(MetaBuilder mb, bool isRoot = false)
		{
			this.MetaStructure.Unk_1156691834 = this.Unk_1156691834;
			// this.MetaStructure.EdgeIds = this.EdgeIds;

 			MCScenarioChain.AddEnumAndStructureInfo(mb);          

			if(isRoot)
			{
				mb.AddItem(this.MetaName, this.MetaStructure);

				this.Meta = mb.GetMeta();
			}
		}
	}
}
