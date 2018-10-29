using System;
using System.Collections.Generic;
using System.Linq;
using SharpDX;
using RageLib.Resources.GTA5.PC.Meta;


namespace RageLib.GTA5.ResourceWrappers.PC.Meta.Structures
{
	public class MUnk_2858946626 : MetaStructureWrapper<Unk_2858946626>
	{
		public static MetaName _MetaName = (MetaName) (-1436020670);
		public MetaFile Meta;
		public byte Unk_2598445407 { get { return (byte) Unk_3902803273.Count; } }

        public List<MUnk_94549140> Unk_3902803273
        {
            get
            {
                var unk_3902803273 = new List<MUnk_94549140>();
                var values = Enum.GetValues(typeof(Unk_2834549053));

                foreach(Unk_2834549053 value in values)
                    if(value != Unk_2834549053.NUM_ANCHORS)
                        unk_3902803273.AddRange(Props[value]);

                return unk_3902803273;
            }
        }

		public List<MCAnchorProps> AAnchors = new List<MCAnchorProps>();

        public Dictionary<Unk_2834549053, List<MUnk_94549140>> Props = new Dictionary<Unk_2834549053, List<MUnk_94549140>>()
        {
            { Unk_2834549053.ANCHOR_HEAD, new List<MUnk_94549140>()},
            { Unk_2834549053.ANCHOR_EYES, new List<MUnk_94549140>()},
            { Unk_2834549053.ANCHOR_EARS, new List<MUnk_94549140>()},
            { Unk_2834549053.ANCHOR_MOUTH, new List<MUnk_94549140>()},
            { Unk_2834549053.ANCHOR_LEFT_HAND, new List<MUnk_94549140>()},
            { Unk_2834549053.ANCHOR_RIGHT_HAND, new List<MUnk_94549140>()},
            { Unk_2834549053.ANCHOR_LEFT_WRIST, new List<MUnk_94549140>()},
            { Unk_2834549053.ANCHOR_RIGHT_WRIST, new List<MUnk_94549140>()},
            { Unk_2834549053.ANCHOR_HIP, new List<MUnk_94549140>()},
            { Unk_2834549053.ANCHOR_LEFT_FOOT, new List<MUnk_94549140>()},
            { Unk_2834549053.ANCHOR_RIGHT_FOOT, new List<MUnk_94549140>()},
            { Unk_2834549053.Unk_604819740, new List<MUnk_94549140>()},
            { Unk_2834549053.Unk_2358626934, new List<MUnk_94549140>()},
        };

        public MUnk_2858946626()
		{
			this.MetaName = (MetaName) (-1436020670);
			this.MetaStructure = new Unk_2858946626();
		}

		public static void AddEnumAndStructureInfo(MetaBuilder mb)
		{
			var enumInfos = MetaInfo.GetStructureEnumInfo(MUnk_2858946626._MetaName);

			for (int i = 0; i < enumInfos.Length; i++)
				mb.AddEnumInfo((MetaName) enumInfos[i].EnumNameHash);

			mb.AddStructureInfo(MUnk_2858946626._MetaName);
			mb.AddStructureInfo((MetaName) (94549140));
			mb.AddStructureInfo(MetaName.CAnchorProps);
		}


		public override void Parse(MetaFile meta, Unk_2858946626 Unk_2858946626)
		{
			this.Meta = meta;
			this.MetaStructure = Unk_2858946626;

			// this.Unk_2598445407 = Unk_2858946626.Unk_2598445407;
			var Unk_3902803273 = MetaUtils.ConvertDataArray<Unk_94549140>(meta, Unk_2858946626.Unk_3902803273);

            var values = Enum.GetValues(typeof(Unk_2834549053));

            foreach(Unk_2834549053 value in values)
                if(value != Unk_2834549053.NUM_ANCHORS)
                    Props[value] = Unk_3902803273?.Where(e => e.anchorId == (byte) value).Select(e => { var msw = new MUnk_94549140(this); msw.Parse(meta, e); return msw; }).ToList() ?? new List<MUnk_94549140>();

			var aAnchors = MetaUtils.ConvertDataArray<CAnchorProps>(meta, Unk_2858946626.aAnchors);
			this.AAnchors = (aAnchors?.Select(e => { var msw = new MCAnchorProps(this); msw.Parse(meta, e); return msw; }).ToList()) ?? new List<MCAnchorProps>();
        }

		public override void Build(MetaBuilder mb, bool isRoot = false)
		{
			this.MetaStructure.Unk_2598445407 = this.Unk_2598445407;

			if(this.Unk_3902803273 != null)
				this.MetaStructure.Unk_3902803273 = mb.AddItemArrayPtr((MetaName) (94549140), this.Unk_3902803273.Select(e => { e.Build(mb); return e.MetaStructure; }).ToArray());

            MUnk_94549140.AddEnumAndStructureInfo(mb);          

			if(this.AAnchors != null)
				this.MetaStructure.aAnchors = mb.AddItemArrayPtr(MetaName.CAnchorProps, this.AAnchors.Select(e => { e.Build(mb); return e.MetaStructure; }).ToArray());

            MCAnchorProps.AddEnumAndStructureInfo(mb);                    


 			MUnk_2858946626.AddEnumAndStructureInfo(mb);          

			if(isRoot)
			{
				mb.AddItem(this.MetaName, this.MetaStructure);

				this.Meta = mb.GetMeta();
			}
		}
	}
}
