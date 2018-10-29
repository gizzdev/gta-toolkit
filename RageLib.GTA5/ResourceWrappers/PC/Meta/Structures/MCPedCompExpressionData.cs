using System.Collections.Generic;
using System.Linq;
using SharpDX;
using RageLib.Resources.GTA5.PC.Meta;

namespace RageLib.GTA5.ResourceWrappers.PC.Meta.Structures
{
	public class MCPedCompExpressionData : MetaStructureWrapper<CPedCompExpressionData>
	{
		public static MetaName _MetaName = MetaName.CPedCompExpressionData;
		public MetaFile Meta;
		public uint PedCompID;
		public int PedCompVarIndex;
		public uint PedCompExpressionIndex;
		public Array_byte Tracks;
		public Array_ushort Ids;
		public Array_byte Types;
		public Array_byte Components;

		public MCPedCompExpressionData()
		{
			this.MetaName = MetaName.CPedCompExpressionData;
			this.MetaStructure = new CPedCompExpressionData();
		}

		public static void AddEnumAndStructureInfo(MetaBuilder mb)
		{
			var enumInfos = MetaInfo.GetStructureEnumInfo(MCPedCompExpressionData._MetaName);

			for (int i = 0; i < enumInfos.Length; i++)
				mb.AddEnumInfo((MetaName) enumInfos[i].EnumNameHash);

			mb.AddStructureInfo(MCPedCompExpressionData._MetaName);
		}


		public override void Parse(MetaFile meta, CPedCompExpressionData CPedCompExpressionData)
		{
			this.Meta = meta;
			this.MetaStructure = CPedCompExpressionData;

			this.PedCompID = CPedCompExpressionData.pedCompID;
			this.PedCompVarIndex = CPedCompExpressionData.pedCompVarIndex;
			this.PedCompExpressionIndex = CPedCompExpressionData.pedCompExpressionIndex;
			// this.Tracks = CPedCompExpressionData.tracks;
			// this.Ids = CPedCompExpressionData.ids;
			// this.Types = CPedCompExpressionData.types;
			// this.Components = CPedCompExpressionData.components;
		}

		public override void Build(MetaBuilder mb, bool isRoot = false)
		{
			this.MetaStructure.pedCompID = this.PedCompID;
			this.MetaStructure.pedCompVarIndex = this.PedCompVarIndex;
			this.MetaStructure.pedCompExpressionIndex = this.PedCompExpressionIndex;
			// this.MetaStructure.tracks = this.Tracks;
			// this.MetaStructure.ids = this.Ids;
			// this.MetaStructure.types = this.Types;
			// this.MetaStructure.components = this.Components;

 			MCPedCompExpressionData.AddEnumAndStructureInfo(mb);                    

			if(isRoot)
			{
				mb.AddItem(this.MetaName, this.MetaStructure);

				this.Meta = mb.GetMeta();
			}
		}
	}
}
