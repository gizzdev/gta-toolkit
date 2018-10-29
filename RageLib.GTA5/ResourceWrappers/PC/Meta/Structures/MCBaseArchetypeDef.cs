using System.Collections.Generic;
using System.Linq;
using SharpDX;
using RageLib.Resources.GTA5.PC.Meta;

namespace RageLib.GTA5.ResourceWrappers.PC.Meta.Structures
{
	public class MCBaseArchetypeDef : MetaStructureWrapper<CBaseArchetypeDef>
	{
        public static MetaName _MetaName = MetaName.CBaseArchetypeDef;
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

        public List<MCExtensionDefLightEffect> ExtensionDefLightEffects = new List<MCExtensionDefLightEffect>();
        public List<MCExtensionDefSpawnPointOverride> ExtensionDefSpawnPointOverrides = new List<MCExtensionDefSpawnPointOverride>();
        public List<MCExtensionDefDoor> ExtensionDefDoors = new List<MCExtensionDefDoor>();
        public List<Mrage__phVerletClothCustomBounds> rage__PhVerletClothCustomBounds = new List<Mrage__phVerletClothCustomBounds>();

		public MCBaseArchetypeDef()
		{
			this.MetaName = MetaName.CBaseArchetypeDef;
			this.MetaStructure = new CBaseArchetypeDef();
		}

        public static void AddEnumAndStructureInfo(MetaBuilder mb)
        {
            var enumInfos = MetaInfo.GetStructureEnumInfo(MCBaseArchetypeDef._MetaName);

            for (int i = 0; i < enumInfos.Length; i++)
                mb.AddEnumInfo((MetaName)enumInfos[i].EnumNameHash);

            mb.AddStructureInfo(MCBaseArchetypeDef._MetaName);
        }


        public override void Parse(MetaFile meta, CBaseArchetypeDef CBaseArchetypeDef)
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
            var extptrs = MetaUtils.GetPointerArray(meta, CBaseArchetypeDef.extensions);

            if (extptrs != null)
            {
                for (int i = 0; i < extptrs.Length; i++)
                {
                    var extptr = extptrs[i];
                    var block = meta.GetBlock(extptr.BlockID);

                    switch (block.StructureNameHash)
                    {
                        case MetaName.CExtensionDefLightEffect:
                            {
                                var data = MetaUtils.GetData<CExtensionDefLightEffect>(meta, extptr);
                                var obj = new MCExtensionDefLightEffect();
                                obj.Parse(meta, data);
                                ExtensionDefLightEffects.Add(obj);
                                break;
                            }

                        case MetaName.CExtensionDefSpawnPointOverride:
                            {
                                var data = MetaUtils.GetData<CExtensionDefSpawnPointOverride>(meta, extptr);
                                var obj = new MCExtensionDefSpawnPointOverride();
                                obj.Parse(meta, data);
                                ExtensionDefSpawnPointOverrides.Add(obj);
                                break;
                            }

                        case MetaName.CExtensionDefDoor:
                            {
                                var data = MetaUtils.GetData<CExtensionDefDoor>(meta, extptr);
                                var obj = new MCExtensionDefDoor();
                                obj.Parse(meta, data);
                                ExtensionDefDoors.Add(obj);
                                break;
                            }

                        case MetaName.rage__phVerletClothCustomBounds:
                            {
                                var data = MetaUtils.GetData<rage__phVerletClothCustomBounds>(meta, extptr);
                                var obj = new Mrage__phVerletClothCustomBounds();
                                obj.Parse(meta, data);
                                rage__PhVerletClothCustomBounds.Add(obj);
                                break;
                            }

                        default: break;
                    }
                }
            }
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
            /*
            if (this.ExtensionDefLightEffects != null)
                this.AddMetaPointers(mb, extPtrs, MetaName.CExtensionDefLightEffect, this.ExtensionDefLightEffects.Select(e => { e.Build(mb); return e.MetaStructure; }));

            if (this.ExtensionDefSpawnPointOverrides != null)
                this.AddMetaPointers(mb, extPtrs, MetaName.CExtensionDefSpawnPointOverride, this.ExtensionDefSpawnPointOverrides.Select(e => { e.Build(mb); return e.MetaStructure; }));

            if (this.ExtensionDefDoors != null)
                this.AddMetaPointers(mb, extPtrs, MetaName.CExtensionDefDoor, this.ExtensionDefDoors.Select(e => { e.Build(mb); return e.MetaStructure; }));

            if (this.rage__PhVerletClothCustomBounds != null)
                this.AddMetaPointers(mb, extPtrs, MetaName.rage__phVerletClothCustomBounds, this.rage__PhVerletClothCustomBounds.Select(e => { e.Build(mb); return e.MetaStructure; }));
            */
            this.MetaStructure.extensions = mb.AddPointerArray(extPtrs.ToArray());

            MCBaseArchetypeDef.AddEnumAndStructureInfo(mb);                    

            if (isRoot)
			{
				mb.AddItem(this.MetaName, this.MetaStructure);

				this.Meta = mb.GetMeta();
			}
        }

    }
}
