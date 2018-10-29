using System.Collections.Generic;
using System.Linq;
using SharpDX;
using RageLib.Resources.GTA5.PC.Meta;

namespace RageLib.GTA5.ResourceWrappers.PC.Meta.Structures
{
	public class MCScenarioChainingEdge : MetaStructureWrapper<CScenarioChainingEdge>
	{
		public static MetaName _MetaName = MetaName.CScenarioChainingEdge;
		public MetaFile Meta;
		public ushort NodeIndexFrom;
		public ushort NodeIndexTo;
		public Unk_3609807418 Action;
		public Unk_3971773454 NavMode;
		public Unk_941086046 NavSpeed;

		public MCScenarioChainingEdge()
		{
			this.MetaName = MetaName.CScenarioChainingEdge;
			this.MetaStructure = new CScenarioChainingEdge();
		}

		public static void AddEnumAndStructureInfo(MetaBuilder mb)
		{
			var enumInfos = MetaInfo.GetStructureEnumInfo(MCScenarioChainingEdge._MetaName);

			for (int i = 0; i < enumInfos.Length; i++)
				mb.AddEnumInfo((MetaName) enumInfos[i].EnumNameHash);

			mb.AddStructureInfo(MCScenarioChainingEdge._MetaName);
		}


		public override void Parse(MetaFile meta, CScenarioChainingEdge CScenarioChainingEdge)
		{
			this.Meta = meta;
			this.MetaStructure = CScenarioChainingEdge;

			this.NodeIndexFrom = CScenarioChainingEdge.NodeIndexFrom;
			this.NodeIndexTo = CScenarioChainingEdge.NodeIndexTo;
			this.Action = CScenarioChainingEdge.Action;
			this.NavMode = CScenarioChainingEdge.NavMode;
			this.NavSpeed = CScenarioChainingEdge.NavSpeed;
		}

		public override void Build(MetaBuilder mb, bool isRoot = false)
		{
			this.MetaStructure.NodeIndexFrom = this.NodeIndexFrom;
			this.MetaStructure.NodeIndexTo = this.NodeIndexTo;
			this.MetaStructure.Action = this.Action;
			this.MetaStructure.NavMode = this.NavMode;
			this.MetaStructure.NavSpeed = this.NavSpeed;

 			MCScenarioChainingEdge.AddEnumAndStructureInfo(mb);          

			if(isRoot)
			{
				mb.AddItem(this.MetaName, this.MetaStructure);

				this.Meta = mb.GetMeta();
			}
		}
	}
}
