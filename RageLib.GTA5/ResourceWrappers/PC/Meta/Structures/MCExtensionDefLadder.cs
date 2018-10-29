using System.Collections.Generic;
using System.Linq;
using SharpDX;
using RageLib.Resources.GTA5.PC.Meta;

namespace RageLib.GTA5.ResourceWrappers.PC.Meta.Structures
{
	public class MCExtensionDefLadder : MetaStructureWrapper<CExtensionDefLadder>
	{
		public static MetaName _MetaName = MetaName.CExtensionDefLadder;
		public MetaFile Meta;
		public uint Name;
		public Vector3 OffsetPosition;
		public Vector3 Bottom;
		public Vector3 Top;
		public Vector3 Normal;
		public Unk_1294270217 MaterialType;
		public uint Template;
		public byte CanGetOffAtTop;
		public byte CanGetOffAtBottom;

		public MCExtensionDefLadder()
		{
			this.MetaName = MetaName.CExtensionDefLadder;
			this.MetaStructure = new CExtensionDefLadder();
		}

		public static void AddEnumAndStructureInfo(MetaBuilder mb)
		{
			var enumInfos = MetaInfo.GetStructureEnumInfo(MCExtensionDefLadder._MetaName);

			for (int i = 0; i < enumInfos.Length; i++)
				mb.AddEnumInfo((MetaName) enumInfos[i].EnumNameHash);

			mb.AddStructureInfo(MCExtensionDefLadder._MetaName);
		}


		public override void Parse(MetaFile meta, CExtensionDefLadder CExtensionDefLadder)
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

 			MCExtensionDefLadder.AddEnumAndStructureInfo(mb);          

			if(isRoot)
			{
				mb.AddItem(this.MetaName, this.MetaStructure);

				this.Meta = mb.GetMeta();
			}
		}
	}
}
