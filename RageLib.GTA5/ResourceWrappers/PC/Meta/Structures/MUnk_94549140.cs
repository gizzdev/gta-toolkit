using System.Collections.Generic;
using System.Linq;
using SharpDX;
using RageLib.Resources.GTA5.PC.Meta;

namespace RageLib.GTA5.ResourceWrappers.PC.Meta.Structures
{
	public class MUnk_94549140 : MetaStructureWrapper<Unk_94549140>
	{
		public static MetaName _MetaName = (MetaName) (94549140);
		public MetaFile Meta;
		public uint AudioId;
		public ArrayOfBytes5 ExpressionMods = new ArrayOfBytes5();
		public List<MUnk_254518642> TexData = new List<MUnk_254518642>();
		public Unk_4212977111 RenderFlags;
		public uint PropFlags = 0;
		public ushort Flags = 0;
		public byte AnchorId;
		public byte PropId { get { return (byte) Parent.Props[(Unk_2834549053) AnchorId].IndexOf(this); } }
		public byte Unk_2894625425;
        MUnk_2858946626 Parent;

        public MUnk_94549140(MUnk_2858946626 parent)
		{
			this.MetaName = (MetaName) (94549140);
			this.MetaStructure = new Unk_94549140();
            this.Parent = parent;
		}

		public static void AddEnumAndStructureInfo(MetaBuilder mb)
		{
			var enumInfos = MetaInfo.GetStructureEnumInfo(MUnk_94549140._MetaName);

			for (int i = 0; i < enumInfos.Length; i++)
				mb.AddEnumInfo((MetaName) enumInfos[i].EnumNameHash);

			mb.AddStructureInfo(MUnk_94549140._MetaName);
			mb.AddStructureInfo((MetaName) (254518642));
		}


		public override void Parse(MetaFile meta, Unk_94549140 Unk_94549140)
		{
			this.Meta = meta;
			this.MetaStructure = Unk_94549140;

			this.AudioId = Unk_94549140.audioId;
			this.ExpressionMods = Unk_94549140.expressionMods;
			var texData = MetaUtils.ConvertDataArray<Unk_254518642>(meta, Unk_94549140.texData);
			this.TexData = texData?.Select(e => { var msw = new MUnk_254518642(); msw.Parse(meta, e); return msw; }).ToList();

			this.RenderFlags = Unk_94549140.renderFlags;
			this.PropFlags = Unk_94549140.propFlags;
			this.Flags = Unk_94549140.flags;
			this.AnchorId = Unk_94549140.anchorId;
			//this.PropId = Unk_94549140.propId;
			this.Unk_2894625425 = Unk_94549140.Unk_2894625425;
		}

		public override void Build(MetaBuilder mb, bool isRoot = false)
		{
			this.MetaStructure.audioId = this.AudioId;
			this.MetaStructure.expressionMods = this.ExpressionMods;
			if(this.TexData != null)
				this.MetaStructure.texData = mb.AddItemArrayPtr((MetaName) (254518642), this.TexData.Select(e => { e.Build(mb); return e.MetaStructure; }).ToArray());
 			MUnk_254518642.AddEnumAndStructureInfo(mb);          

			this.MetaStructure.renderFlags = this.RenderFlags;
			this.MetaStructure.propFlags = this.PropFlags;
			this.MetaStructure.flags = this.Flags;
			this.MetaStructure.anchorId = this.AnchorId;
			this.MetaStructure.propId = this.PropId;
			this.MetaStructure.Unk_2894625425 = this.Unk_2894625425;

 			MUnk_94549140.AddEnumAndStructureInfo(mb);          

			if(isRoot)
			{
				mb.AddItem(this.MetaName, this.MetaStructure);

				this.Meta = mb.GetMeta();
			}
		}
	}
}
