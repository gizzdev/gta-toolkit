using System.Collections.Generic;
using System.Linq;
using SharpDX;
using RageLib.Resources.GTA5.PC.Meta;

namespace RageLib.GTA5.ResourceWrappers.PC.Meta.Structures
{
	public class MUnk_2236980467 : MetaStructureWrapper<Unk_2236980467>
	{
		public static MetaName _MetaName = (MetaName) (-2057986829);
		public MetaFile Meta;
		public byte Unk_2828247905;

		public MUnk_2236980467()
		{
			this.MetaName = (MetaName) (-2057986829);
			this.MetaStructure = new Unk_2236980467();
		}

		public static void AddEnumAndStructureInfo(MetaBuilder mb)
		{
			var enumInfos = MetaInfo.GetStructureEnumInfo(MUnk_2236980467._MetaName);

			for (int i = 0; i < enumInfos.Length; i++)
				mb.AddEnumInfo((MetaName) enumInfos[i].EnumNameHash);

			mb.AddStructureInfo(MUnk_2236980467._MetaName);
		}


		public override void Parse(MetaFile meta, Unk_2236980467 Unk_2236980467)
		{
			this.Meta = meta;
			this.MetaStructure = Unk_2236980467;

			this.Unk_2828247905 = Unk_2236980467.Unk_2828247905;
		}

		public override void Build(MetaBuilder mb, bool isRoot = false)
		{
			this.MetaStructure.Unk_2828247905 = this.Unk_2828247905;

            MUnk_2236980467.AddEnumAndStructureInfo(mb);          

			if(isRoot)
			{
				mb.AddItem(this.MetaName, this.MetaStructure);

				this.Meta = mb.GetMeta();
			}
		}
	}
}
