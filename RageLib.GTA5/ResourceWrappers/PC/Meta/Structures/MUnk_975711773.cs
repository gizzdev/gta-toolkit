using System.Collections.Generic;
using System.Linq;
using SharpDX;
using RageLib.Resources.GTA5.PC.Meta;

namespace RageLib.GTA5.ResourceWrappers.PC.Meta.Structures
{
	public class MUnk_975711773 : MetaStructureWrapper<Unk_975711773>
	{
		public static MetaName _MetaName = (MetaName) (975711773);
		public MetaFile Meta;
		public short ICenterX;
		public short ICenterY;
		public short ICenterZ;
		public short ICosZ;
		public short ILength;
		public short IWidth;
		public short IHeight;
		public short ISinZ;

		public MUnk_975711773()
		{
			this.MetaName = (MetaName) (975711773);
			this.MetaStructure = new Unk_975711773();
		}

		public static void AddEnumAndStructureInfo(MetaBuilder mb)
		{
			var enumInfos = MetaInfo.GetStructureEnumInfo(MUnk_975711773._MetaName);

			for (int i = 0; i < enumInfos.Length; i++)
				mb.AddEnumInfo((MetaName) enumInfos[i].EnumNameHash);

			mb.AddStructureInfo(MUnk_975711773._MetaName);
		}


		public override void Parse(MetaFile meta, Unk_975711773 Unk_975711773)
		{
			this.Meta = meta;
			this.MetaStructure = Unk_975711773;

			this.ICenterX = Unk_975711773.iCenterX;
			this.ICenterY = Unk_975711773.iCenterY;
			this.ICenterZ = Unk_975711773.iCenterZ;
			this.ICosZ = Unk_975711773.iCosZ;
			this.ILength = Unk_975711773.iLength;
			this.IWidth = Unk_975711773.iWidth;
			this.IHeight = Unk_975711773.iHeight;
			this.ISinZ = Unk_975711773.iSinZ;
		}

		public override void Build(MetaBuilder mb, bool isRoot = false)
		{
			this.MetaStructure.iCenterX = this.ICenterX;
			this.MetaStructure.iCenterY = this.ICenterY;
			this.MetaStructure.iCenterZ = this.ICenterZ;
			this.MetaStructure.iCosZ = this.ICosZ;
			this.MetaStructure.iLength = this.ILength;
			this.MetaStructure.iWidth = this.IWidth;
			this.MetaStructure.iHeight = this.IHeight;
			this.MetaStructure.iSinZ = this.ISinZ;

 			MUnk_975711773.AddEnumAndStructureInfo(mb);          

			if(isRoot)
			{
				mb.AddItem(this.MetaName, this.MetaStructure);

				this.Meta = mb.GetMeta();
			}
		}
	}
}
