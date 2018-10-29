using System.Collections.Generic;
using System.Linq;
using SharpDX;
using RageLib.Resources.GTA5.PC.Meta;

namespace RageLib.GTA5.ResourceWrappers.PC.Meta.Structures
{
	public class MCPedPropExpressionData : MetaStructureWrapper<CPedPropExpressionData>
	{
		public static MetaName _MetaName = MetaName.CPedPropExpressionData;
		public MetaFile Meta;
		public uint PedPropID;
		public int PedPropVarIndex;
		public uint PedPropExpressionIndex;
		public Array_byte Tracks;
		public Array_ushort Ids;
		public Array_byte Types;
		public Array_byte Components;

		public MCPedPropExpressionData()
		{
			this.MetaName = MetaName.CPedPropExpressionData;
			this.MetaStructure = new CPedPropExpressionData();
		}

		public static void AddEnumAndStructureInfo(MetaBuilder mb)
		{
			var enumInfos = MetaInfo.GetStructureEnumInfo(MCPedPropExpressionData._MetaName);

			for (int i = 0; i < enumInfos.Length; i++)
				mb.AddEnumInfo((MetaName) enumInfos[i].EnumNameHash);

			mb.AddStructureInfo(MCPedPropExpressionData._MetaName);
		}


		public override void Parse(MetaFile meta, CPedPropExpressionData CPedPropExpressionData)
		{
			this.Meta = meta;
			this.MetaStructure = CPedPropExpressionData;

			this.PedPropID = CPedPropExpressionData.pedPropID;
			this.PedPropVarIndex = CPedPropExpressionData.pedPropVarIndex;
			this.PedPropExpressionIndex = CPedPropExpressionData.pedPropExpressionIndex;
			// this.Tracks = CPedPropExpressionData.tracks;
			// this.Ids = CPedPropExpressionData.ids;
			// this.Types = CPedPropExpressionData.types;
			// this.Components = CPedPropExpressionData.components;
		}

		public override void Build(MetaBuilder mb, bool isRoot = false)
		{
			this.MetaStructure.pedPropID = this.PedPropID;
			this.MetaStructure.pedPropVarIndex = this.PedPropVarIndex;
			this.MetaStructure.pedPropExpressionIndex = this.PedPropExpressionIndex;
			// this.MetaStructure.tracks = this.Tracks;
			// this.MetaStructure.ids = this.Ids;
			// this.MetaStructure.types = this.Types;
			// this.MetaStructure.components = this.Components;

 			MCPedPropExpressionData.AddEnumAndStructureInfo(mb);          

			if(isRoot)
			{
				mb.AddItem(this.MetaName, this.MetaStructure);

				this.Meta = mb.GetMeta();
			}
		}
	}
}
