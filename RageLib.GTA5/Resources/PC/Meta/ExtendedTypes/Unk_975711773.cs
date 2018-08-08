using System.Collections.Generic;
using System.Linq;

using SharpDX;

namespace RageLib.Resources.GTA5.PC.Meta.ExtendedTypes
{
	public class Unk_975711773 : MetaStructureWrapper<PC.Meta.Unk_975711773>
	{
		public MetaFile Meta;
		public short ICenterX;
		public short ICenterY;
		public short ICenterZ;
		public short ICosZ;
		public short ILength;
		public short IWidth;
		public short IHeight;
		public short ISinZ;

		public Unk_975711773(MetaName metaName) : base(metaName)
		{
			this.MetaStructure = new PC.Meta.Unk_975711773();
		}

		public void Parse(MetaFile meta, PC.Meta.Unk_975711773 Unk_975711773)
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

		public void Build(MetaBuilder mb, bool isRoot = false)
		{
			this.MetaStructure.iCenterX = this.ICenterX;
			this.MetaStructure.iCenterY = this.ICenterY;
			this.MetaStructure.iCenterZ = this.ICenterZ;
			this.MetaStructure.iCosZ = this.ICosZ;
			this.MetaStructure.iLength = this.ILength;
			this.MetaStructure.iWidth = this.IWidth;
			this.MetaStructure.iHeight = this.IHeight;
			this.MetaStructure.iSinZ = this.ISinZ;

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
