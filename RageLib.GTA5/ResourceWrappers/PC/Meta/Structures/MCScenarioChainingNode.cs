using System.Collections.Generic;
using System.Linq;
using SharpDX;
using RageLib.Resources.GTA5.PC.Meta;

namespace RageLib.GTA5.ResourceWrappers.PC.Meta.Structures
{
	public class MCScenarioChainingNode : MetaStructureWrapper<CScenarioChainingNode>
	{
		public static MetaName _MetaName = MetaName.CScenarioChainingNode;
		public MetaFile Meta;
		public Vector3 Position;
		public uint Unk_2602393771;
		public uint ScenarioType;
		public byte Unk_407126079;
		public byte Unk_1308720135;

		public MCScenarioChainingNode()
		{
			this.MetaName = MetaName.CScenarioChainingNode;
			this.MetaStructure = new CScenarioChainingNode();
		}

		public static void AddEnumAndStructureInfo(MetaBuilder mb)
		{
			var enumInfos = MetaInfo.GetStructureEnumInfo(MCScenarioChainingNode._MetaName);

			for (int i = 0; i < enumInfos.Length; i++)
				mb.AddEnumInfo((MetaName) enumInfos[i].EnumNameHash);

			mb.AddStructureInfo(MCScenarioChainingNode._MetaName);
		}


		public override void Parse(MetaFile meta, CScenarioChainingNode CScenarioChainingNode)
		{
			this.Meta = meta;
			this.MetaStructure = CScenarioChainingNode;

			this.Position = CScenarioChainingNode.Position;
			this.Unk_2602393771 = CScenarioChainingNode.Unk_2602393771;
			this.ScenarioType = CScenarioChainingNode.ScenarioType;
			this.Unk_407126079 = CScenarioChainingNode.Unk_407126079;
			this.Unk_1308720135 = CScenarioChainingNode.Unk_1308720135;
		}

		public override void Build(MetaBuilder mb, bool isRoot = false)
		{
			this.MetaStructure.Position = this.Position;
			this.MetaStructure.Unk_2602393771 = this.Unk_2602393771;
			this.MetaStructure.ScenarioType = this.ScenarioType;
			this.MetaStructure.Unk_407126079 = this.Unk_407126079;
			this.MetaStructure.Unk_1308720135 = this.Unk_1308720135;

 			MCScenarioChainingNode.AddEnumAndStructureInfo(mb);                    

			if(isRoot)
			{
				mb.AddItem(this.MetaName, this.MetaStructure);

				this.Meta = mb.GetMeta();
			}
		}
	}
}
