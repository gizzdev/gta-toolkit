using System.Collections.Generic;
using System.Linq;
using SharpDX;
using RageLib.Resources.GTA5.PC.Meta;

namespace RageLib.GTA5.ResourceWrappers.PC.Meta.Structures
{
	public class MCExtensionDefExpression : MetaStructureWrapper<CExtensionDefExpression>
	{
		public static MetaName _MetaName = MetaName.CExtensionDefExpression;
		public MetaFile Meta;
		public uint Name;
		public Vector3 OffsetPosition;
		public uint Unk_1095612811;
		public uint ExpressionName;
		public uint Unk_2766477159;
		public byte Unk_1562817888;

		public MCExtensionDefExpression()
		{
			this.MetaName = MetaName.CExtensionDefExpression;
			this.MetaStructure = new CExtensionDefExpression();
		}

		public static void AddEnumAndStructureInfo(MetaBuilder mb)
		{
			var enumInfos = MetaInfo.GetStructureEnumInfo(MCExtensionDefExpression._MetaName);

			for (int i = 0; i < enumInfos.Length; i++)
				mb.AddEnumInfo((MetaName) enumInfos[i].EnumNameHash);

			mb.AddStructureInfo(MCExtensionDefExpression._MetaName);
		}


		public override void Parse(MetaFile meta, CExtensionDefExpression CExtensionDefExpression)
		{
			this.Meta = meta;
			this.MetaStructure = CExtensionDefExpression;

			this.Name = CExtensionDefExpression.name;
			this.OffsetPosition = CExtensionDefExpression.offsetPosition;
			this.Unk_1095612811 = CExtensionDefExpression.Unk_1095612811;
			this.ExpressionName = CExtensionDefExpression.expressionName;
			this.Unk_2766477159 = CExtensionDefExpression.Unk_2766477159;
			this.Unk_1562817888 = CExtensionDefExpression.Unk_1562817888;
		}

		public override void Build(MetaBuilder mb, bool isRoot = false)
		{
			this.MetaStructure.name = this.Name;
			this.MetaStructure.offsetPosition = this.OffsetPosition;
			this.MetaStructure.Unk_1095612811 = this.Unk_1095612811;
			this.MetaStructure.expressionName = this.ExpressionName;
			this.MetaStructure.Unk_2766477159 = this.Unk_2766477159;
			this.MetaStructure.Unk_1562817888 = this.Unk_1562817888;

 			MCExtensionDefExpression.AddEnumAndStructureInfo(mb);          

			if(isRoot)
			{
				mb.AddItem(this.MetaName, this.MetaStructure);

				this.Meta = mb.GetMeta();
			}
		}
	}
}
