using System.Collections.Generic;
using System.Linq;
using SharpDX;
using RageLib.Resources.GTA5.PC.Meta;

namespace RageLib.GTA5.ResourceWrappers.PC.Meta.Structures
{
	public class MUnk_1535046754 : MetaStructureWrapper<Unk_1535046754>
	{
		public static MetaName _MetaName = (MetaName) (1535046754);
		public MetaFile Meta;
		public byte PropMask = 1;
		public byte Unk_2806194106;
		public List<MUnk_1036962405> ATexData = new List<MUnk_1036962405>();
		public MUnk_2236980467 ClothData = new MUnk_2236980467();

		public MUnk_1535046754()
		{
			this.MetaName = (MetaName) (1535046754);
			this.MetaStructure = new Unk_1535046754();
		}

		public static void AddEnumAndStructureInfo(MetaBuilder mb)
		{
			var enumInfos = MetaInfo.GetStructureEnumInfo(MUnk_1535046754._MetaName);

			for (int i = 0; i < enumInfos.Length; i++)
				mb.AddEnumInfo((MetaName) enumInfos[i].EnumNameHash);

			mb.AddStructureInfo(MUnk_1535046754._MetaName);
			mb.AddStructureInfo((MetaName) (1036962405));
			mb.AddStructureInfo((MetaName) (-2057986829));
		}


		public override void Parse(MetaFile meta, Unk_1535046754 Unk_1535046754)
		{
			this.Meta = meta;
			this.MetaStructure = Unk_1535046754;

			this.PropMask = Unk_1535046754.propMask;
			this.Unk_2806194106 = Unk_1535046754.Unk_2806194106;
			var aTexData = MetaUtils.ConvertDataArray<Unk_1036962405>(meta, Unk_1535046754.aTexData);
			this.ATexData = aTexData?.Select(e => { var msw = new MUnk_1036962405(); msw.Parse(meta, e); return msw; }).ToList();

			this.ClothData = new MUnk_2236980467();
			this.ClothData.Parse(meta, Unk_1535046754.clothData);
		}

		public override void Build(MetaBuilder mb, bool isRoot = false)
		{
			this.MetaStructure.propMask = this.PropMask;
			this.MetaStructure.Unk_2806194106 = this.Unk_2806194106;
			if(this.ATexData != null)
				this.MetaStructure.aTexData = mb.AddItemArrayPtr((MetaName) (1036962405), this.ATexData.Select(e => {e.Build(mb); return e.MetaStructure; }).ToArray());
 			MUnk_1036962405.AddEnumAndStructureInfo(mb);          

			this.ClothData.Build(mb);
			this.MetaStructure.clothData = this.ClothData.MetaStructure;
 			MUnk_2236980467.AddEnumAndStructureInfo(mb);


            MUnk_1535046754.AddEnumAndStructureInfo(mb);          

			if(isRoot)
			{
				mb.AddItem(this.MetaName, this.MetaStructure);

				this.Meta = mb.GetMeta();
			}
		}
	}
}
