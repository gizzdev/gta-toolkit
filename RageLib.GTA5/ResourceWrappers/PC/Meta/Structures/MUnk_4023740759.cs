using System.Collections.Generic;
using System.Linq;
using SharpDX;
using RageLib.Resources.GTA5.PC.Meta;

namespace RageLib.GTA5.ResourceWrappers.PC.Meta.Structures
{
	public class MUnk_4023740759 : MetaStructureWrapper<Unk_4023740759>
	{
		public static MetaName _MetaName = (MetaName) (-271226537);
		public MetaFile Meta;
		public List<MCScenarioChainingNode> Nodes;
		public List<MCScenarioChainingEdge> Edges;
		public List<MCScenarioChain> Chains;

		public MUnk_4023740759()
		{
			this.MetaName = (MetaName) (-271226537);
			this.MetaStructure = new Unk_4023740759();
		}

		public static void AddEnumAndStructureInfo(MetaBuilder mb)
		{
			var enumInfos = MetaInfo.GetStructureEnumInfo(MUnk_4023740759._MetaName);

			for (int i = 0; i < enumInfos.Length; i++)
				mb.AddEnumInfo((MetaName) enumInfos[i].EnumNameHash);

			mb.AddStructureInfo(MUnk_4023740759._MetaName);
			mb.AddStructureInfo(MetaName.CScenarioChainingNode);
			mb.AddStructureInfo(MetaName.CScenarioChainingEdge);
			mb.AddStructureInfo(MetaName.CScenarioChain);
		}


		public override void Parse(MetaFile meta, Unk_4023740759 Unk_4023740759)
		{
			this.Meta = meta;
			this.MetaStructure = Unk_4023740759;

			var Nodes = MetaUtils.ConvertDataArray<CScenarioChainingNode>(meta, Unk_4023740759.Nodes);
			this.Nodes = Nodes?.Select(e => { var msw = new MCScenarioChainingNode(); msw.Parse(meta, e); return msw; }).ToList();

			var Edges = MetaUtils.ConvertDataArray<CScenarioChainingEdge>(meta, Unk_4023740759.Edges);
			this.Edges = Edges?.Select(e => { var msw = new MCScenarioChainingEdge(); msw.Parse(meta, e); return msw; }).ToList();

			var Chains = MetaUtils.ConvertDataArray<CScenarioChain>(meta, Unk_4023740759.Chains);
			this.Chains = Chains?.Select(e => { var msw = new MCScenarioChain(); msw.Parse(meta, e); return msw; }).ToList();

		}

		public override void Build(MetaBuilder mb, bool isRoot = false)
		{
			if(this.Nodes != null)
				this.MetaStructure.Nodes = mb.AddItemArrayPtr(MetaName.CScenarioChainingNode, this.Nodes.Select(e => { e.Build(mb); return e.MetaStructure; }).ToArray());
 			MCScenarioChainingNode.AddEnumAndStructureInfo(mb);                    

			if(this.Edges != null)
				this.MetaStructure.Edges = mb.AddItemArrayPtr(MetaName.CScenarioChainingEdge, this.Edges.Select(e => { e.Build(mb); return e.MetaStructure; }).ToArray());
 			MCScenarioChainingEdge.AddEnumAndStructureInfo(mb);                    

			if(this.Chains != null)
				this.MetaStructure.Chains = mb.AddItemArrayPtr(MetaName.CScenarioChain, this.Chains.Select(e => { e.Build(mb); return e.MetaStructure; }).ToArray());
 			MCScenarioChain.AddEnumAndStructureInfo(mb);                    


 			MUnk_4023740759.AddEnumAndStructureInfo(mb);          

			if(isRoot)
			{
				mb.AddItem(this.MetaName, this.MetaStructure);

				this.Meta = mb.GetMeta();
			}
		}
	}
}
