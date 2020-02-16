using System.Collections.Generic;
using System.Linq;
using SharpDX;
using RageLib.Resources.GTA5.PC.Meta;

namespace RageLib.GTA5.ResourceWrappers.PC.Meta.Structures
{
	public class MCEntityDef : MetaStructureWrapper<CEntityDef>
	{
		public static MetaName _MetaName = MetaName.CEntityDef;
		public MetaFile Meta;
		public uint ArchetypeName;
		public uint Flags;
		public uint Guid;
		public Vector3 Position;
		public Vector4 Rotation;
		public float ScaleXY;
		public float ScaleZ;

        public int _ParentIndex;

        public int ParentIndex
        {
            get
            {
                return this._ParentIndex;
            }
            set
            {
                this._ParentIndex = value;

                if(this.Parent != null)
                {
                    if (this._ParentEntity != null && this._ParentEntity.Children.IndexOf(this) != -1)
                    {
                        this._ParentEntity.Children.Remove(this);
                        this._ParentEntity.NumChildren = (uint)this._ParentEntity.Children.Count;
                    }

                    if (value == -1)
                    {
                        this._ParentEntity = null;
                    }
                    else
                    {
                        var ymapEntity = this.Parent.Entities.Count > value ? this.Parent.Entities[value] : null;
                        var parentYmapEntity = this.Parent?.ParentMapData?.Entities.Count > value ? this.Parent.ParentMapData.Entities[value] : null;
                        var lodLevel = MCMapData.LodLevels.IndexOf(this.LodLevel);

                        bool found = false;

                        if (ymapEntity != null)
                        {
                            var ymapEntityLodLevel = MCMapData.LodLevels.IndexOf(ymapEntity.LodLevel);

                            if (ymapEntityLodLevel < lodLevel)
                            {
                                found = true;
                                this._ParentEntity = ymapEntity;
                            }
                        }

                        if(!found && parentYmapEntity != null)
                        {
                            var parentYmapEntityLodLevel = MCMapData.LodLevels.IndexOf(parentYmapEntity.LodLevel);

                            if (parentYmapEntityLodLevel < lodLevel)
                            {
                                found = true;
                                this._ParentEntity = parentYmapEntity;
                            }
                        }

                        if(!found)
                        {
                            this._ParentEntity = null;
                        }

                    }

                    if (this._ParentEntity != null && this._ParentEntity.Children.IndexOf(this) == -1)
                    {
                        this._ParentEntity.Children.Add(this);
                        this._ParentEntity.NumChildren = (uint)this._ParentEntity.Children.Count;
                    }
                }
            }
        }

        public float LodDist;
		public float ChildLodDist;
		public Unk_1264241711 LodLevel;
		public uint NumChildren;
		public Unk_648413703 PriorityLevel;
		public Array_StructurePointer Extensions;

        public MCMapData Parent;
        public MCEntityDef _ParentEntity = null;

        public MCEntityDef ParentEntity
        {
            get
            {
                return this._ParentEntity;
            }
            set
            {
                if (this._ParentEntity != null && this.ParentEntity.Children.IndexOf(this) != -1)
                {
                    this._ParentEntity.Children.Remove(this);
                    this._ParentEntity.NumChildren = (uint)this._ParentEntity.Children.Count;
                }

                this._ParentEntity = value;

                if (value == null)
                {
                    this._ParentIndex = -1;
                }
                else
                {
                    this._ParentIndex = value.Parent?.Entities.IndexOf(value) ?? -1;
                }

                if (this._ParentEntity != null && this.ParentEntity.Children.IndexOf(this) == -1)
                {
                    this._ParentEntity.Children.Add(this);
                    this._ParentEntity.NumChildren = (uint) this._ParentEntity.Children.Count;
                }
            }
        }

        public List<MCEntityDef> Children = new List<MCEntityDef>();

        //Extensions
        public List<MCExtensionDefLightEffect> ExtensionDefLightEffect = new List<MCExtensionDefLightEffect>();
        public List<MCExtensionDefSpawnPointOverride> ExtensionDefSpawnPointOverride = new List<MCExtensionDefSpawnPointOverride>();
        public List<MCExtensionDefDoor> ExtensionDefDoor = new List<MCExtensionDefDoor>();
        public List<Mrage__phVerletClothCustomBounds> rage__phVerletClothCustomBounds = new List<Mrage__phVerletClothCustomBounds>();

        public int AmbientOcclusionMultiplier;
		public int ArtificialAmbientOcclusion;
		public uint TintValue;


        public MCEntityDef(MCMapData parent = null)
		{
			this.MetaName = MetaName.CEntityDef;
			this.MetaStructure = new CEntityDef();

            this.Parent = parent;
		}

		public static void AddEnumAndStructureInfo(MetaBuilder mb)
		{
			var enumInfos = MetaInfo.GetStructureEnumInfo(MCEntityDef._MetaName);

			for (int i = 0; i < enumInfos.Length; i++)
				mb.AddEnumInfo((MetaName) enumInfos[i].EnumNameHash);

			mb.AddStructureInfo(MCEntityDef._MetaName);
		}


        public override void Parse(MetaFile meta, CEntityDef CEntityDef)
        {
            this.Meta = meta;
            this.MetaStructure = CEntityDef;

            this.ArchetypeName = CEntityDef.archetypeName;
            this.Flags = CEntityDef.flags;
            this.Guid = CEntityDef.guid;
            this.Position = CEntityDef.position;
            this.Rotation = CEntityDef.rotation;
            this.ScaleXY = CEntityDef.scaleXY;
            this.ScaleZ = CEntityDef.scaleZ;
            this.ParentIndex = CEntityDef.parentIndex;
            this.LodDist = CEntityDef.lodDist;
            this.ChildLodDist = CEntityDef.childLodDist;
            this.LodLevel = CEntityDef.lodLevel;
            this.NumChildren = CEntityDef.numChildren;
            this.PriorityLevel = CEntityDef.priorityLevel;

            // Extensions
            var extptrs = MetaUtils.GetPointerArray(meta, CEntityDef.extensions);

            if(extptrs != null)
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
                                ExtensionDefLightEffect.Add(obj);
                                break;
                            }

                        case MetaName.CExtensionDefSpawnPointOverride:
                            {
                                var data = MetaUtils.GetData<CExtensionDefSpawnPointOverride>(meta, extptr);
                                var obj = new MCExtensionDefSpawnPointOverride();
                                obj.Parse(meta, data);
                                ExtensionDefSpawnPointOverride.Add(obj);
                                break;
                            }

                        case MetaName.CExtensionDefDoor:
                            {
                                var data = MetaUtils.GetData<CExtensionDefDoor>(meta, extptr);
                                var obj = new MCExtensionDefDoor();
                                obj.Parse(meta, data);
                                ExtensionDefDoor.Add(obj);
                                break;
                            }

                        case MetaName.rage__phVerletClothCustomBounds:
                            {
                                var data = MetaUtils.GetData<rage__phVerletClothCustomBounds>(meta, extptr);
                                var obj = new Mrage__phVerletClothCustomBounds();
                                obj.Parse(meta, data);
                                rage__phVerletClothCustomBounds.Add(obj);
                                break;
                            }

                        default: break;
                    }
                }
            }

            this.AmbientOcclusionMultiplier = CEntityDef.ambientOcclusionMultiplier;
			this.ArtificialAmbientOcclusion = CEntityDef.artificialAmbientOcclusion;
			this.TintValue = CEntityDef.tintValue;
		}

        public void ParseWithoutExtensions(MetaFile meta, CEntityDef CEntityDef)
        {
            this.Meta = meta;
            this.MetaStructure = CEntityDef;

            this.ArchetypeName = CEntityDef.archetypeName;
            this.Flags = CEntityDef.flags;
            this.Guid = CEntityDef.guid;
            this.Position = CEntityDef.position;
            this.Rotation = CEntityDef.rotation;
            this.ScaleXY = CEntityDef.scaleXY;
            this.ScaleZ = CEntityDef.scaleZ;
            this.ParentIndex = CEntityDef.parentIndex;
            this.LodDist = CEntityDef.lodDist;
            this.ChildLodDist = CEntityDef.childLodDist;
            this.LodLevel = CEntityDef.lodLevel;
            this.NumChildren = CEntityDef.numChildren;
            this.PriorityLevel = CEntityDef.priorityLevel;

            this.AmbientOcclusionMultiplier = CEntityDef.ambientOcclusionMultiplier;
            this.ArtificialAmbientOcclusion = CEntityDef.artificialAmbientOcclusion;
            this.TintValue = CEntityDef.tintValue;
        }


        public override void Build(MetaBuilder mb, bool isRoot = false)
		{
			this.MetaStructure.archetypeName = this.ArchetypeName;
			this.MetaStructure.flags = this.Flags;
			this.MetaStructure.guid = this.Guid;
			this.MetaStructure.position = this.Position;
			this.MetaStructure.rotation = this.Rotation;
			this.MetaStructure.scaleXY = this.ScaleXY;
			this.MetaStructure.scaleZ = this.ScaleZ;
			this.MetaStructure.parentIndex = this.ParentIndex;
			this.MetaStructure.lodDist = this.LodDist;
			this.MetaStructure.childLodDist = this.ChildLodDist;
			this.MetaStructure.lodLevel = this.LodLevel;
			this.MetaStructure.numChildren = this.NumChildren;
			this.MetaStructure.priorityLevel = this.PriorityLevel;

            // Extensions
            var extPtrs = new List<MetaPOINTER>();

            if (this.ExtensionDefLightEffect != null)
                this.AddMetaPointers(mb, extPtrs, MetaName.CExtensionDefLightEffect, this.ExtensionDefLightEffect.Select(e => { e.Build(mb); return e.MetaStructure; }));

            if (this.ExtensionDefSpawnPointOverride != null)
                this.AddMetaPointers(mb, extPtrs, MetaName.CExtensionDefSpawnPointOverride, this.ExtensionDefSpawnPointOverride.Select(e => { e.Build(mb); return e.MetaStructure; }));

            if (this.ExtensionDefDoor != null)
                this.AddMetaPointers(mb, extPtrs, MetaName.CExtensionDefDoor, this.ExtensionDefDoor.Select(e => { e.Build(mb); return e.MetaStructure; }));

            if (this.rage__phVerletClothCustomBounds != null)
                this.AddMetaPointers(mb, extPtrs, MetaName.rage__phVerletClothCustomBounds, this.rage__phVerletClothCustomBounds.Select(e => { e.Build(mb); return e.MetaStructure; }));

            this.MetaStructure.extensions = mb.AddPointerArray(extPtrs.ToArray());


            this.MetaStructure.ambientOcclusionMultiplier = this.AmbientOcclusionMultiplier;
			this.MetaStructure.artificialAmbientOcclusion = this.ArtificialAmbientOcclusion;
			this.MetaStructure.tintValue = this.TintValue;

 			MCEntityDef.AddEnumAndStructureInfo(mb);          

			if(isRoot)
			{
				mb.AddItem(this.MetaName, this.MetaStructure);

				this.Meta = mb.GetMeta();
			}
		}
	}
}
