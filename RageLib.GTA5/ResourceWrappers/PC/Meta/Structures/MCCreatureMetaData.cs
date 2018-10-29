using System.Collections.Generic;
using System.Linq;
using SharpDX;
using RageLib.Resources.GTA5.PC.Meta;

namespace RageLib.GTA5.ResourceWrappers.PC.Meta.Structures
{
	public class MCCreatureMetaData : MetaStructureWrapper<CCreatureMetaData>
	{
		public static MetaName _MetaName = MetaName.CCreatureMetaData;
		public MetaFile Meta;
		public List<MCShaderVariableComponent> ShaderVariableComponents;
		public List<MCPedPropExpressionData> PedPropExpressions;
		public List<MCPedCompExpressionData> PedCompExpressions;

		public MCCreatureMetaData()
		{
			this.MetaName = MetaName.CCreatureMetaData;
			this.MetaStructure = new CCreatureMetaData();
		}

		public static void AddEnumAndStructureInfo(MetaBuilder mb)
		{
			var enumInfos = MetaInfo.GetStructureEnumInfo(MCCreatureMetaData._MetaName);

			for (int i = 0; i < enumInfos.Length; i++)
				mb.AddEnumInfo((MetaName) enumInfos[i].EnumNameHash);

			mb.AddStructureInfo(MCCreatureMetaData._MetaName);
			mb.AddStructureInfo(MetaName.CShaderVariableComponent);
			mb.AddStructureInfo(MetaName.CPedPropExpressionData);
			mb.AddStructureInfo(MetaName.CPedCompExpressionData);
		}


		public override void Parse(MetaFile meta, CCreatureMetaData CCreatureMetaData)
		{
			this.Meta = meta;
			this.MetaStructure = CCreatureMetaData;

			var shaderVariableComponents = MetaUtils.ConvertDataArray<CShaderVariableComponent>(meta, CCreatureMetaData.shaderVariableComponents);
			this.ShaderVariableComponents = shaderVariableComponents?.Select(e => { var msw = new MCShaderVariableComponent(); msw.Parse(meta, e); return msw; }).ToList();

			var pedPropExpressions = MetaUtils.ConvertDataArray<CPedPropExpressionData>(meta, CCreatureMetaData.pedPropExpressions);
			this.PedPropExpressions = pedPropExpressions?.Select(e => { var msw = new MCPedPropExpressionData(); msw.Parse(meta, e); return msw; }).ToList();

			var pedCompExpressions = MetaUtils.ConvertDataArray<CPedCompExpressionData>(meta, CCreatureMetaData.pedCompExpressions);
			this.PedCompExpressions = pedCompExpressions?.Select(e => { var msw = new MCPedCompExpressionData(); msw.Parse(meta, e); return msw; }).ToList();

		}

		public override void Build(MetaBuilder mb, bool isRoot = false)
		{
			if(this.ShaderVariableComponents != null)
				this.MetaStructure.shaderVariableComponents = mb.AddItemArrayPtr(MetaName.CShaderVariableComponent, this.ShaderVariableComponents.Select(e => { e.Build(mb); return e.MetaStructure; }).ToArray());
 			MCShaderVariableComponent.AddEnumAndStructureInfo(mb);          

			if(this.PedPropExpressions != null)
				this.MetaStructure.pedPropExpressions = mb.AddItemArrayPtr(MetaName.CPedPropExpressionData, this.PedPropExpressions.Select(e => { e.Build(mb); return e.MetaStructure; }).ToArray());
 			MCPedPropExpressionData.AddEnumAndStructureInfo(mb);          

			if(this.PedCompExpressions != null)
				this.MetaStructure.pedCompExpressions = mb.AddItemArrayPtr(MetaName.CPedCompExpressionData, this.PedCompExpressions.Select(e => { e.Build(mb); return e.MetaStructure; }).ToArray());
 			MCPedCompExpressionData.AddEnumAndStructureInfo(mb);          


 			MCCreatureMetaData.AddEnumAndStructureInfo(mb);          

			if(isRoot)
			{
				mb.AddItem(this.MetaName, this.MetaStructure);

				this.Meta = mb.GetMeta();
			}
		}
	}
}
