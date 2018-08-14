using System.Collections.Generic;
using System.Linq;
using SharpDX;
using RageLib.Resources.GTA5.PC.Meta;

namespace RageLib.GTA5.ResourceWrappers.PC.Meta.Structures
{
	public class CExtensionDefLadder : MetaStructureWrapper<RageLib.Resources.GTA5.PC.Meta.CExtensionDefLadder>
	{
		public MetaFile Meta;
		public uint Name;
		public Vector3 OffsetPosition;
		public Vector3 Bottom;
		public Vector3 Top;
		public Vector3 Normal;
		public Unk_1294270217 MaterialType;
		public uint Template;
		public bool CanGetOffAtTop;
		public bool CanGetOffAtBottom;

		public CExtensionDefLadder()
		{
			this.MetaName = MetaName.CExtensionDefLadder;
			this.MetaStructure = new RageLib.Resources.GTA5.PC.Meta.CExtensionDefLadder();
		}

		public override void Parse(MetaFile meta, RageLib.Resources.GTA5.PC.Meta.CExtensionDefLadder CExtensionDefLadder)
		{
			this.Meta = meta;
			this.MetaStructure = CExtensionDefLadder;

			this.Name = CExtensionDefLadder.name;
			this.OffsetPosition = CExtensionDefLadder.offsetPosition;
			this.Bottom = CExtensionDefLadder.bottom;
			this.Top = CExtensionDefLadder.top;
			this.Normal = CExtensionDefLadder.normal;
			this.MaterialType = CExtensionDefLadder.materialType;
			this.Template = CExtensionDefLadder.template;
			this.CanGetOffAtTop = CExtensionDefLadder.canGetOffAtTop;
			this.CanGetOffAtBottom = CExtensionDefLadder.canGetOffAtBottom;
		}

		public override void Build(MetaBuilder mb, bool isRoot = false)
		{
			this.MetaStructure.name = this.Name;
			this.MetaStructure.offsetPosition = this.OffsetPosition;
			this.MetaStructure.bottom = this.Bottom;
			this.MetaStructure.top = this.Top;
			this.MetaStructure.normal = this.Normal;
			this.MetaStructure.materialType = this.MaterialType;
			this.MetaStructure.template = this.Template;
			this.MetaStructure.canGetOffAtTop = this.CanGetOffAtTop;
			this.MetaStructure.canGetOffAtBottom = this.CanGetOffAtBottom;

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
