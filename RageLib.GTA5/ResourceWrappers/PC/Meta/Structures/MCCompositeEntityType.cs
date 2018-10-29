using System.Collections.Generic;
using System.Linq;
using SharpDX;
using RageLib.Resources.GTA5.PC.Meta;

namespace RageLib.GTA5.ResourceWrappers.PC.Meta.Structures
{
	public class MCCompositeEntityType : MetaStructureWrapper<CCompositeEntityType>
	{
		public static MetaName _MetaName = MetaName.CCompositeEntityType;
		public MetaFile Meta;
		public ArrayOfChars64 Name;
		public float LodDist;
		public uint Flags;
		public uint SpecialAttribute;
		public Vector3 BbMin;
		public Vector3 BbMax;
		public Vector3 BsCentre;
		public float BsRadius;
		public ArrayOfChars64 StartModel;
		public ArrayOfChars64 EndModel;
		public uint StartImapFile;
		public uint EndImapFile;
		public uint PtFxAssetName;
		public List<MUnk_1980345114> Animations;

		public MCCompositeEntityType()
		{
			this.MetaName = MetaName.CCompositeEntityType;
			this.MetaStructure = new CCompositeEntityType();
		}

		public static void AddEnumAndStructureInfo(MetaBuilder mb)
		{
			var enumInfos = MetaInfo.GetStructureEnumInfo(MCCompositeEntityType._MetaName);

			for (int i = 0; i < enumInfos.Length; i++)
				mb.AddEnumInfo((MetaName) enumInfos[i].EnumNameHash);

			mb.AddStructureInfo(MCCompositeEntityType._MetaName);
			mb.AddStructureInfo((MetaName) (1980345114));
		}


		public override void Parse(MetaFile meta, CCompositeEntityType CCompositeEntityType)
		{
			this.Meta = meta;
			this.MetaStructure = CCompositeEntityType;

			this.Name = CCompositeEntityType.Name;
			this.LodDist = CCompositeEntityType.lodDist;
			this.Flags = CCompositeEntityType.flags;
			this.SpecialAttribute = CCompositeEntityType.specialAttribute;
			this.BbMin = CCompositeEntityType.bbMin;
			this.BbMax = CCompositeEntityType.bbMax;
			this.BsCentre = CCompositeEntityType.bsCentre;
			this.BsRadius = CCompositeEntityType.bsRadius;
			this.StartModel = CCompositeEntityType.StartModel;
			this.EndModel = CCompositeEntityType.EndModel;
			this.StartImapFile = CCompositeEntityType.StartImapFile;
			this.EndImapFile = CCompositeEntityType.EndImapFile;
			this.PtFxAssetName = CCompositeEntityType.PtFxAssetName;
			var Animations = MetaUtils.ConvertDataArray<RageLib.Resources.GTA5.PC.Meta.Unk_1980345114>(meta, CCompositeEntityType.Animations);
			this.Animations = Animations?.Select(e => { var msw = new MUnk_1980345114(); msw.Parse(meta, e); return msw; }).ToList();

		}

		public override void Build(MetaBuilder mb, bool isRoot = false)
		{
			this.MetaStructure.Name = this.Name;
			this.MetaStructure.lodDist = this.LodDist;
			this.MetaStructure.flags = this.Flags;
			this.MetaStructure.specialAttribute = this.SpecialAttribute;
			this.MetaStructure.bbMin = this.BbMin;
			this.MetaStructure.bbMax = this.BbMax;
			this.MetaStructure.bsCentre = this.BsCentre;
			this.MetaStructure.bsRadius = this.BsRadius;
			this.MetaStructure.StartModel = this.StartModel;
			this.MetaStructure.EndModel = this.EndModel;
			this.MetaStructure.StartImapFile = this.StartImapFile;
			this.MetaStructure.EndImapFile = this.EndImapFile;
			this.MetaStructure.PtFxAssetName = this.PtFxAssetName;
			if(this.Animations != null)
				this.MetaStructure.Animations = mb.AddItemArrayPtr((MetaName) (1980345114), this.Animations.Select(e => { e.Build(mb); return e.MetaStructure; }).ToArray());
            MUnk_1980345114.AddEnumAndStructureInfo(mb);          


 			MCCompositeEntityType.AddEnumAndStructureInfo(mb);          

			if(isRoot)
			{
				mb.AddItem(this.MetaName, this.MetaStructure);

				this.Meta = mb.GetMeta();
			}
		}
	}
}
