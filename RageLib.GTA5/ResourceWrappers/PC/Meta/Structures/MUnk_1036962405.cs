using System.Collections.Generic;
using System.Linq;
using SharpDX;
using RageLib.Resources.GTA5.PC.Meta;

namespace RageLib.GTA5.ResourceWrappers.PC.Meta.Structures
{
	public class MUnk_1036962405 : MetaStructureWrapper<Unk_1036962405>
	{
		public static MetaName _MetaName = (MetaName) (1036962405);
		public MetaFile Meta;
		public byte TexId = 0;
		public byte Distribution = 255;

		public MUnk_1036962405()
		{
			this.MetaName = (MetaName) (1036962405);
			this.MetaStructure = new Unk_1036962405();
		}

		public static void AddEnumAndStructureInfo(MetaBuilder mb)
		{
			var enumInfos = MetaInfo.GetStructureEnumInfo(MUnk_1036962405._MetaName);

			for (int i = 0; i < enumInfos.Length; i++)
				mb.AddEnumInfo((MetaName) enumInfos[i].EnumNameHash);

			mb.AddStructureInfo(MUnk_1036962405._MetaName);
		}


		public override void Parse(MetaFile meta, Unk_1036962405 Unk_1036962405)
		{
			this.Meta = meta;
			this.MetaStructure = Unk_1036962405;

			this.TexId = Unk_1036962405.texId;
			this.Distribution = Unk_1036962405.distribution;
		}

		public override void Build(MetaBuilder mb, bool isRoot = false)
		{
			this.MetaStructure.texId = this.TexId;
			this.MetaStructure.distribution = this.Distribution;

 			MUnk_1036962405.AddEnumAndStructureInfo(mb);          

			if(isRoot)
			{
				mb.AddItem(this.MetaName, this.MetaStructure);

				this.Meta = mb.GetMeta();
			}
		}
	}
}
