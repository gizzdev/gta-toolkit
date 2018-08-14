using System.Collections.Generic;
using System.Linq;
using SharpDX;
using RageLib.Resources.GTA5.PC.Meta;

namespace RageLib.GTA5.ResourceWrappers.PC.Meta.Structures
{
	public class CBaseArchetypeDef : MetaStructureWrapper<RageLib.Resources.GTA5.PC.Meta.CBaseArchetypeDef>
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
		public MetaName Name;
		public MetaName TextureDictionary;
		public MetaName ClipDictionary;
		public MetaName DrawableDictionary;
		public MetaName PhysicsDictionary;
		public Unk_1991964615 AssetType;
		public MetaName AssetName;

        public List<CExtensionDefLightEffect> ExtensionDefLightEffects = new List<CExtensionDefLightEffect>();
        public List<CExtensionDefSpawnPointOverride> ExtensionDefSpawnPointOverrides = new List<CExtensionDefSpawnPointOverride>();
        public List<CExtensionDefDoor> ExtensionDefDoors = new List<CExtensionDefDoor>();
        public List<rage__phVerletClothCustomBounds> rage__PhVerletClothCustomBounds = new List<rage__phVerletClothCustomBounds>();

		public CBaseArchetypeDef()
		{
			this.MetaName = MetaName.CBaseArchetypeDef;
			this.MetaStructure = new RageLib.Resources.GTA5.PC.Meta.CBaseArchetypeDef();
		}

		public override void Parse(MetaFile meta, RageLib.Resources.GTA5.PC.Meta.CBaseArchetypeDef CBaseArchetypeDef)
		{
			this.Meta = meta;
			this.MetaStructure = CBaseArchetypeDef;

			this.LodDist = CBaseArchetypeDef.lodDist;
			this.Flags = CBaseArchetypeDef.flags;
			this.SpecialAttribute = CBaseArchetypeDef.specialAttribute;
			this.BbMin = CBaseArchetypeDef.bbMin;
			this.BbMax = CBaseArchetypeDef.bbMax;
			this.BsCentre = CBaseArchetypeDef.bsCentre;
			this.BsRadius = CBaseArchetypeDef.bsRadius;
			this.HdTextureDist = CBaseArchetypeDef.hdTextureDist;
			this.Name = (MetaName) CBaseArchetypeDef.name;
			this.TextureDictionary = (MetaName)CBaseArchetypeDef.textureDictionary;
			this.ClipDictionary = (MetaName)CBaseArchetypeDef.clipDictionary;
			this.DrawableDictionary = (MetaName)CBaseArchetypeDef.drawableDictionary;
			this.PhysicsDictionary = (MetaName)CBaseArchetypeDef.physicsDictionary;
			this.AssetType = CBaseArchetypeDef.assetType;
			this.AssetName = (MetaName) CBaseArchetypeDef.assetName;

            // Extensions
            this.ExtensionDefLightEffects = MetaUtils.GetTypedDataArray<RageLib.Resources.GTA5.PC.Meta.CExtensionDefLightEffect>(this.Meta, MetaName.CExtensionDefLightEffect)?.Select(e => { var obj = new CExtensionDefLightEffect(); obj.Parse(meta, e); return obj; }).ToList();
            this.ExtensionDefSpawnPointOverrides = MetaUtils.GetTypedDataArray<RageLib.Resources.GTA5.PC.Meta.CExtensionDefSpawnPointOverride>(this.Meta, MetaName.CExtensionDefSpawnPointOverride)?.Select(e => { var obj = new CExtensionDefSpawnPointOverride(); obj.Parse(meta, e); return obj; }).ToList();
            this.ExtensionDefDoors = MetaUtils.GetTypedDataArray<RageLib.Resources.GTA5.PC.Meta.CExtensionDefDoor>(this.Meta, MetaName.CExtensionDefDoor)?.Select(e => { var obj = new CExtensionDefDoor(); obj.Parse(meta, e); return obj; }).ToList();
            this.rage__PhVerletClothCustomBounds = MetaUtils.GetTypedDataArray<RageLib.Resources.GTA5.PC.Meta.rage__phVerletClothCustomBounds>(this.Meta, MetaName.rage__phVerletClothCustomBounds)?.Select(e => { var obj = new rage__phVerletClothCustomBounds(); obj.Parse(meta, e); return obj; }).ToList();

        }

        public override void Build(MetaBuilder mb, bool isRoot = false)
		{
			this.MetaStructure.lodDist = this.LodDist;
			this.MetaStructure.flags = this.Flags;
			this.MetaStructure.specialAttribute = this.SpecialAttribute;
			this.MetaStructure.bbMin = this.BbMin;
			this.MetaStructure.bbMax = this.BbMax;
			this.MetaStructure.bsCentre = this.BsCentre;
			this.MetaStructure.bsRadius = this.BsRadius;
			this.MetaStructure.hdTextureDist = this.HdTextureDist;
			this.MetaStructure.name = (uint) this.Name;
			this.MetaStructure.textureDictionary = (uint)this.TextureDictionary;
			this.MetaStructure.clipDictionary = (uint)this.ClipDictionary;
			this.MetaStructure.drawableDictionary = (uint)this.DrawableDictionary;
			this.MetaStructure.physicsDictionary = (uint)this.PhysicsDictionary;
			this.MetaStructure.assetType = this.AssetType;
			this.MetaStructure.assetName = (uint) this.AssetName;

            // Extensions
            var extPtrs = new List<MetaPOINTER>();

            if (this.ExtensionDefLightEffects != null)
                this.AddMetaPointers(mb, extPtrs, MetaName.CExtensionDefLightEffect, this.ExtensionDefLightEffects.Select(e => { e.Build(mb); return e.MetaStructure; }));

            if (this.ExtensionDefSpawnPointOverrides != null)
                this.AddMetaPointers(mb, extPtrs, MetaName.CExtensionDefSpawnPointOverride, this.ExtensionDefSpawnPointOverrides.Select(e => { e.Build(mb); return e.MetaStructure; }));

            if (this.ExtensionDefDoors != null)
                this.AddMetaPointers(mb, extPtrs, MetaName.CExtensionDefDoor, this.ExtensionDefDoors.Select(e => { e.Build(mb); return e.MetaStructure; }));

            if (this.rage__PhVerletClothCustomBounds != null)
                this.AddMetaPointers(mb, extPtrs, MetaName.rage__phVerletClothCustomBounds, this.rage__PhVerletClothCustomBounds.Select(e => { e.Build(mb); return e.MetaStructure; }));

            this.MetaStructure.extensions = mb.AddPointerArray(extPtrs.ToArray());

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
