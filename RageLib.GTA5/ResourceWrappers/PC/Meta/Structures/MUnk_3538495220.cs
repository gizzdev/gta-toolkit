using System.Collections.Generic;
using System.Linq;
using SharpDX;
using RageLib.Resources.GTA5.PC.Meta;

namespace RageLib.GTA5.ResourceWrappers.PC.Meta.Structures
{
	public class MUnk_3538495220 : MetaStructureWrapper<Unk_3538495220>
	{
		public static MetaName _MetaName = (MetaName) (-756472076);
		public MetaFile Meta;

        public byte Unk_3371516811 { get { int count = 0; for (int i = 0; i < Unk_1756136273.Count; i++) count += Unk_1756136273[i].ATexData.Count; return (byte) count; } }

		public List<MUnk_1535046754> Unk_1756136273 = new List<MUnk_1535046754>();

		public MUnk_3538495220()
		{
			this.MetaName = (MetaName) (-756472076);
			this.MetaStructure = new Unk_3538495220();
		}

		public static void AddEnumAndStructureInfo(MetaBuilder mb)
		{
			var enumInfos = MetaInfo.GetStructureEnumInfo(MUnk_3538495220._MetaName);

			for (int i = 0; i < enumInfos.Length; i++)
				mb.AddEnumInfo((MetaName) enumInfos[i].EnumNameHash);

			mb.AddStructureInfo(MUnk_3538495220._MetaName);
			mb.AddStructureInfo((MetaName) (1535046754));
		}


		public override void Parse(MetaFile meta, Unk_3538495220 Unk_3538495220)
		{
			this.Meta = meta;
			this.MetaStructure = Unk_3538495220;

			//this.Unk_3371516811 = Unk_3538495220.Unk_3371516811;
			var Unk_1756136273 = MetaUtils.ConvertDataArray<Unk_1535046754>(meta, Unk_3538495220.Unk_1756136273);
			this.Unk_1756136273 = Unk_1756136273?.Select(e => { var msw = new MUnk_1535046754(); msw.Parse(meta, e); return msw; }).ToList();

		}

		public override void Build(MetaBuilder mb, bool isRoot = false)
		{
			this.MetaStructure.Unk_3371516811 = this.Unk_3371516811;

            if (this.Unk_1756136273 != null)
				this.MetaStructure.Unk_1756136273 = mb.AddItemArrayPtr((MetaName) (1535046754), this.Unk_1756136273.Select(e => {e.Build(mb); return e.MetaStructure; }).ToArray());
 			MUnk_1535046754.AddEnumAndStructureInfo(mb);


            MUnk_3538495220.AddEnumAndStructureInfo(mb);          

			if(isRoot)
			{
				mb.AddItem(this.MetaName, this.MetaStructure);

				this.Meta = mb.GetMeta();
			}
		}
	}
}
