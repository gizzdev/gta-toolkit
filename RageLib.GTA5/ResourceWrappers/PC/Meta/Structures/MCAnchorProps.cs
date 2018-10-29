using System;
using System.Collections.Generic;
using System.Linq;
using SharpDX;
using RageLib.Resources.GTA5.PC.Meta;


namespace RageLib.GTA5.ResourceWrappers.PC.Meta.Structures
{
    public class MCAnchorProps : MetaStructureWrapper<CAnchorProps>
    {
        public static MetaName _MetaName = MetaName.CAnchorProps;
        public MetaFile Meta;

        public List<byte> Props {
            get {
                var list = PropsMap.ToList();
                list.Sort((a,b) => Parent.Unk_3902803273.IndexOf(a.Key) - Parent.Unk_3902803273.IndexOf(b.Key));
                return list.Select(e => e.Value).ToList();
            }
        }

        public Unk_2834549053 Anchor;
        public MUnk_2858946626 Parent;

        public Dictionary<MUnk_94549140, byte> PropsMap = new Dictionary<MUnk_94549140, byte>();

        public MCAnchorProps(MUnk_2858946626 parent)
		{
			this.MetaName = MetaName.CAnchorProps;
			this.MetaStructure = new CAnchorProps();
            this.Parent = parent;
        }

        public static void AddEnumAndStructureInfo(MetaBuilder mb)
        {
            var enumInfos = MetaInfo.GetStructureEnumInfo(MCAnchorProps._MetaName);

            for (int i = 0; i < enumInfos.Length; i++)
                mb.AddEnumInfo((MetaName)enumInfos[i].EnumNameHash);

            mb.AddStructureInfo(MCAnchorProps._MetaName);
        }


        public override void Parse(MetaFile meta, CAnchorProps CAnchorProps)
		{
			this.Meta = meta;
			this.MetaStructure = CAnchorProps;

            this.Anchor = CAnchorProps.anchor;
            var props = MetaUtils.ConvertDataArray<byte>(meta, CAnchorProps.props.Pointer, CAnchorProps.props.Count1)?.ToList();
            var linkedProps = Parent.Unk_3902803273.Where(e => e.AnchorId == (byte) Anchor).ToList();

            for (int i = 0; i < linkedProps.Count; i++)
            {
                PropsMap[linkedProps[i]] = props[i];
            }
		}

		public override void Build(MetaBuilder mb, bool isRoot = false)
		{
			this.MetaStructure.props = mb.AddByteArrayPtr(this.Props.ToArray());
			this.MetaStructure.anchor = this.Anchor;

            MCAnchorProps.AddEnumAndStructureInfo(mb);                    

            if (isRoot)
			{
				mb.AddItem(this.MetaName, this.MetaStructure);

				this.Meta = mb.GetMeta();
			}
		}
	}
}
