using System.Collections.Generic;
using System.Linq;

using SharpDX;

namespace RageLib.Resources.GTA5.PC.Meta.ExtendedTypes
{
	public class CExtensionDefExpression : MetaStructureWrapper<PC.Meta.CExtensionDefExpression>
	{
		public MetaFile Meta;
		public uint Name;
		public Vector3 OffsetPosition;
		public uint Unk_1095612811;
		public uint ExpressionName;
		public uint Unk_2766477159;
		public bool Unk_1562817888;

		public CExtensionDefExpression(MetaName metaName) : base(metaName)
		{
			this.MetaStructure = new PC.Meta.CExtensionDefExpression();
		}

		public void Parse(MetaFile meta, PC.Meta.CExtensionDefExpression CExtensionDefExpression)
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

		public void Build(MetaBuilder mb, bool isRoot = false)
		{
			this.MetaStructure.name = this.Name;
			this.MetaStructure.offsetPosition = this.OffsetPosition;
			this.MetaStructure.Unk_1095612811 = this.Unk_1095612811;
			this.MetaStructure.expressionName = this.ExpressionName;
			this.MetaStructure.Unk_2766477159 = this.Unk_2766477159;
			this.MetaStructure.Unk_1562817888 = this.Unk_1562817888;

			var enumInfos = MetaInfo.GetStructureEnumInfo(this.MetaName);
			var structureInfo = MetaInfo.GetStructureInfo(this.MetaName);
			var childStructureInfos = MetaInfo.GetStructureChildInfo(this.MetaName);

			for (int i = 0; i < enumInfos.Length; i++)
				mb.AddEnumInfo((MetaName) enumInfos[i].EnumNameHash);

			mb.AddStructureInfo((MetaName) structureInfo.StructureNameHash);


			for (int i = 0; i < childStructureInfos.Length; i++)
				mb.AddStructureInfo((MetaName) childStructureInfos[i].StructureNameHash);

			if(isRoot)
			{
				mb.AddItem(this.MetaName, this.MetaStructure);

				this.Meta = mb.GetMeta();
			}
		}
	}
}
