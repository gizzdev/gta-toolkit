using System.Collections.Generic;
using System.Linq;

using SharpDX;

namespace RageLib.Resources.GTA5.PC.Meta.ExtendedTypes
{
	public class CMloArchetypeDef : MetaStructureWrapper<PC.Meta.CMloArchetypeDef>
	{
		public MetaFile Meta;
		public float LodDist;
		public uint Flags;
		public uint SpecialAttribute;
		public Vector3 BbMin;
		public Vector3 BbMax;
		public Vector3 BsCentre;
		public float BsRadius;
		public float HdTextureDist;
		public uint Name;
		public uint TextureDictionary;
		public uint ClipDictionary;
		public uint DrawableDictionary;
		public uint PhysicsDictionary;
		public Unk_1991964615 AssetType;
		public uint AssetName;
		public Array_StructurePointer Extensions;
		public uint MloFlags;
		public Array_StructurePointer Entities;
		public List<CMloRoomDef> Rooms;
		public List<CMloPortalDef> Portals;
		public List<CMloEntitySet> EntitySets;
		public List<CMloTimeCycleModifier> TimeCycleModifiers;

		public CMloArchetypeDef(MetaName metaName) : base(metaName)
		{
			this.MetaStructure = new PC.Meta.CMloArchetypeDef();
		}

		public void Parse(MetaFile meta, PC.Meta.CMloArchetypeDef CMloArchetypeDef)
		{
			this.Meta = meta;
			this.MetaStructure = CMloArchetypeDef;

			this.LodDist = CMloArchetypeDef.lodDist;
			this.Flags = CMloArchetypeDef.flags;
			this.SpecialAttribute = CMloArchetypeDef.specialAttribute;
			this.BbMin = CMloArchetypeDef.bbMin;
			this.BbMax = CMloArchetypeDef.bbMax;
			this.BsCentre = CMloArchetypeDef.bsCentre;
			this.BsRadius = CMloArchetypeDef.bsRadius;
			this.HdTextureDist = CMloArchetypeDef.hdTextureDist;
			this.Name = CMloArchetypeDef.name;
			this.TextureDictionary = CMloArchetypeDef.textureDictionary;
			this.ClipDictionary = CMloArchetypeDef.clipDictionary;
			this.DrawableDictionary = CMloArchetypeDef.drawableDictionary;
			this.PhysicsDictionary = CMloArchetypeDef.physicsDictionary;
			this.AssetType = CMloArchetypeDef.assetType;
			this.AssetName = CMloArchetypeDef.assetName;
			// this.Extensions = CMloArchetypeDef.extensions;
			this.MloFlags = CMloArchetypeDef.mloFlags;
			// this.Entities = CMloArchetypeDef.entities;
			var rooms = MetaUtils.ConvertArray_Structure<PC.Meta.CMloRoomDef>(meta, CMloArchetypeDef.rooms);
			if(rooms != null)
				this.Rooms = (List<CMloRoomDef>) (rooms.ToList().Select(e => { var msw = new CMloRoomDef(MetaName.CMloRoomDef); msw.Parse(meta, e); return msw; }));

			var portals = MetaUtils.ConvertArray_Structure<PC.Meta.CMloPortalDef>(meta, CMloArchetypeDef.portals);
			if(portals != null)
				this.Portals = (List<CMloPortalDef>) (portals.ToList().Select(e => { var msw = new CMloPortalDef(MetaName.CMloPortalDef); msw.Parse(meta, e); return msw; }));

			var entitySets = MetaUtils.ConvertArray_Structure<PC.Meta.CMloEntitySet>(meta, CMloArchetypeDef.entitySets);
			if(entitySets != null)
				this.EntitySets = (List<CMloEntitySet>) (entitySets.ToList().Select(e => { var msw = new CMloEntitySet(MetaName.CMloEntitySet); msw.Parse(meta, e); return msw; }));

			var timeCycleModifiers = MetaUtils.ConvertArray_Structure<PC.Meta.CMloTimeCycleModifier>(meta, CMloArchetypeDef.timeCycleModifiers);
			if(timeCycleModifiers != null)
				this.TimeCycleModifiers = (List<CMloTimeCycleModifier>) (timeCycleModifiers.ToList().Select(e => { var msw = new CMloTimeCycleModifier(MetaName.CMloTimeCycleModifier); msw.Parse(meta, e); return msw; }));

		}

		public void Build(MetaBuilder mb, bool isRoot = false)
		{
			this.MetaStructure.lodDist = this.LodDist;
			this.MetaStructure.flags = this.Flags;
			this.MetaStructure.specialAttribute = this.SpecialAttribute;
			this.MetaStructure.bbMin = this.BbMin;
			this.MetaStructure.bbMax = this.BbMax;
			this.MetaStructure.bsCentre = this.BsCentre;
			this.MetaStructure.bsRadius = this.BsRadius;
			this.MetaStructure.hdTextureDist = this.HdTextureDist;
			this.MetaStructure.name = this.Name;
			this.MetaStructure.textureDictionary = this.TextureDictionary;
			this.MetaStructure.clipDictionary = this.ClipDictionary;
			this.MetaStructure.drawableDictionary = this.DrawableDictionary;
			this.MetaStructure.physicsDictionary = this.PhysicsDictionary;
			this.MetaStructure.assetType = this.AssetType;
			this.MetaStructure.assetName = this.AssetName;
			// this.MetaStructure.extensions = this.Extensions;
			this.MetaStructure.mloFlags = this.MloFlags;
			// this.MetaStructure.entities = this.Entities;
			if(this.Rooms != null)
				this.MetaStructure.rooms = mb.AddItemArrayPtr(MetaName.CMloRoomDef, this.Rooms.Select(e => e.MetaStructure).ToArray());
			if(this.Portals != null)
				this.MetaStructure.portals = mb.AddItemArrayPtr(MetaName.CMloPortalDef, this.Portals.Select(e => e.MetaStructure).ToArray());
			if(this.EntitySets != null)
				this.MetaStructure.entitySets = mb.AddItemArrayPtr(MetaName.CMloEntitySet, this.EntitySets.Select(e => e.MetaStructure).ToArray());
			if(this.TimeCycleModifiers != null)
				this.MetaStructure.timeCycleModifiers = mb.AddItemArrayPtr(MetaName.CMloTimeCycleModifier, this.TimeCycleModifiers.Select(e => e.MetaStructure).ToArray());

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
