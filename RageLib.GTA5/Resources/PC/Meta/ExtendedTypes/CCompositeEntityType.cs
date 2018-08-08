using System.Collections.Generic;
using System.Linq;

using SharpDX;

namespace RageLib.Resources.GTA5.PC.Meta.ExtendedTypes
{
	public class CCompositeEntityType : MetaStructureWrapper<PC.Meta.CCompositeEntityType>
	{
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
		public List<Unk_1980345114> Animations;

		public CCompositeEntityType(MetaName metaName) : base(metaName)
		{
			this.MetaStructure = new PC.Meta.CCompositeEntityType();
		}

		public void Parse(MetaFile meta, PC.Meta.CCompositeEntityType CCompositeEntityType)
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
			var Animations = MetaUtils.ConvertArray_Structure<PC.Meta.Unk_1980345114>(meta, CCompositeEntityType.Animations);
			if(Animations != null)
				this.Animations = (List<Unk_1980345114>) (Animations.ToList().Select(e => { var msw = new Unk_1980345114((MetaName) (1980345114)); msw.Parse(meta, e); return msw; }));

		}

		public void Build(MetaBuilder mb, bool isRoot = false)
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
				this.MetaStructure.Animations = mb.AddItemArrayPtr((MetaName) (1980345114), this.Animations.Select(e => e.MetaStructure).ToArray());

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
