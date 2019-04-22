/*
    Copyright(c) 2016 Neodymium

    Permission is hereby granted, free of charge, to any person obtaining a copy
    of this software and associated documentation files (the "Software"), to deal
    in the Software without restriction, including without limitation the rights
    to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
    copies of the Software, and to permit persons to whom the Software is
    furnished to do so, subject to the following conditions:

    The above copyright notice and this permission notice shall be included in
    all copies or substantial portions of the Software.

    THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
    IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
    FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
    AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
    LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
    OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
    THE SOFTWARE.
*/

using System.Collections.Generic;
using System.Linq;
using SharpDX;
using RageLib.Resources.GTA5.PC.Meta;
using RageLib.Resources.GTA5.PC.Bounds;
using System;
using RageLib.GTA5.Utilities;

namespace RageLib.GTA5.ResourceWrappers.PC.Meta.Structures
{
    public class MCMloArchetypeDef : MetaStructureWrapper<CMloArchetypeDef>
    {
        public static MetaName _MetaName = MetaName.CMloArchetypeDef;
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

        //Extensions
        public List<MCExtensionDefAudioCollisionSettings> ExtensionDefAudioCollisionSettings = new List<MCExtensionDefAudioCollisionSettings>();
        public List<MCExtensionDefAudioEmitter> ExtensionDefAudioEmitters = new List<MCExtensionDefAudioEmitter>();
        public List<MCExtensionDefBuoyancy> ExtensionDefBuoyancies = new List<MCExtensionDefBuoyancy>();
        public List<MCExtensionDefExplosionEffect> ExtensionDefExplosionEffects = new List<MCExtensionDefExplosionEffect>();
        public List<MCExtensionDefExpression> ExtensionDefExpressions = new List<MCExtensionDefExpression>();
        public List<MCExtensionDefLadder> ExtensionDefLadders = new List<MCExtensionDefLadder>();
        public List<MCExtensionDefLightShaft> ExtensionDefLightShafts = new List<MCExtensionDefLightShaft>();
        public List<MCExtensionDefParticleEffect> ExtensionDefParticleEffects = new List<MCExtensionDefParticleEffect>();
        public List<MCExtensionDefProcObject> ExtensionDefProcObjects = new List<MCExtensionDefProcObject>();
        public List<MCExtensionDefSpawnPoint> ExtensionDefSpawnPoints = new List<MCExtensionDefSpawnPoint>();
        public List<MCExtensionDefWindDisturbance> ExtensionDefWindDisturbances = new List<MCExtensionDefWindDisturbance>();

        public uint MloFlags;
        public List<MCEntityDef> Entities = new List<MCEntityDef>();
        public List<MCMloRoomDef> Rooms = new List<MCMloRoomDef>();
        public List<MCMloPortalDef> Portals = new List<MCMloPortalDef>();
        public List<MCMloEntitySet> EntitySets = new List<MCMloEntitySet>();
        public List<MCMloTimeCycleModifier> TimeCycleModifiers = new List<MCMloTimeCycleModifier>();

        public MCMloArchetypeDef()
        {
            this.MetaName = MetaName.CMloArchetypeDef;
            this.MetaStructure = new CMloArchetypeDef();
        }

        public static void AddEnumAndStructureInfo(MetaBuilder mb)
        {
            var enumInfos = MetaInfo.GetStructureEnumInfo(MCMloArchetypeDef._MetaName);

            for (int i = 0; i < enumInfos.Length; i++)
                mb.AddEnumInfo((MetaName)enumInfos[i].EnumNameHash);

            mb.AddStructureInfo(MCMloArchetypeDef._MetaName);
            mb.AddStructureInfo(MetaName.CMloRoomDef);
            mb.AddStructureInfo(MetaName.CMloPortalDef);
            mb.AddStructureInfo(MetaName.CMloEntitySet);
            mb.AddStructureInfo(MetaName.CMloTimeCycleModifier);
        }

        public override void Parse(MetaFile meta, CMloArchetypeDef CMloArchetypeDef)
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

            // Extensions
            var extptrs = MetaUtils.GetPointerArray(meta, CMloArchetypeDef.extensions);

            if (extptrs != null)
            {
                for (int i = 0; i < extptrs.Length; i++)
                {
                    var extptr = extptrs[i];
                    var block = meta.GetBlock(extptr.BlockID);

                    switch (block.StructureNameHash)
                    {
                        case MetaName.CExtensionDefAudioCollisionSettings:
                            {
                                var data = MetaUtils.GetData<CExtensionDefAudioCollisionSettings>(meta, extptr);
                                var obj = new MCExtensionDefAudioCollisionSettings();
                                obj.Parse(meta, data);
                                ExtensionDefAudioCollisionSettings.Add(obj);
                                break;
                            }

                        case MetaName.CExtensionDefAudioEmitter:
                            {
                                var data = MetaUtils.GetData<CExtensionDefAudioEmitter>(meta, extptr);
                                var obj = new MCExtensionDefAudioEmitter();
                                obj.Parse(meta, data);
                                ExtensionDefAudioEmitters.Add(obj);
                                break;
                            }

                        case MetaName.CExtensionDefBuoyancy:
                            {
                                var data = MetaUtils.GetData<CExtensionDefBuoyancy>(meta, extptr);
                                var obj = new MCExtensionDefBuoyancy();
                                obj.Parse(meta, data);
                                ExtensionDefBuoyancies.Add(obj);
                                break;
                            }

                        case MetaName.CExtensionDefExplosionEffect:
                            {
                                var data = MetaUtils.GetData<CExtensionDefExplosionEffect>(meta, extptr);
                                var obj = new MCExtensionDefExplosionEffect();
                                obj.Parse(meta, data);
                                ExtensionDefExplosionEffects.Add(obj);
                                break;
                            }

                        case MetaName.CExtensionDefExpression:
                            {
                                var data = MetaUtils.GetData<CExtensionDefExpression>(meta, extptr);
                                var obj = new MCExtensionDefExpression();
                                obj.Parse(meta, data);
                                ExtensionDefExpressions.Add(obj);
                                break;
                            }

                        case MetaName.CExtensionDefLadder:
                            {
                                var data = MetaUtils.GetData<CExtensionDefLadder>(meta, extptr);
                                var obj = new MCExtensionDefLadder();
                                obj.Parse(meta, data);
                                ExtensionDefLadders.Add(obj);
                                break;
                            }

                        case MetaName.CExtensionDefLightShaft:
                            {
                                var data = MetaUtils.GetData<CExtensionDefLightShaft>(meta, extptr);
                                var obj = new MCExtensionDefLightShaft();
                                obj.Parse(meta, data);
                                ExtensionDefLightShafts.Add(obj);
                                break;
                            }

                        case MetaName.CExtensionDefParticleEffect:
                            {
                                var data = MetaUtils.GetData<CExtensionDefParticleEffect>(meta, extptr);
                                var obj = new MCExtensionDefParticleEffect();
                                obj.Parse(meta, data);
                                ExtensionDefParticleEffects.Add(obj);
                                break;
                            }

                        case MetaName.CExtensionDefProcObject:
                            {
                                var data = MetaUtils.GetData<CExtensionDefProcObject>(meta, extptr);
                                var obj = new MCExtensionDefProcObject();
                                obj.Parse(meta, data);
                                ExtensionDefProcObjects.Add(obj);
                                break;
                            }

                        case MetaName.CExtensionDefSpawnPoint:
                            {
                                var data = MetaUtils.GetData<CExtensionDefSpawnPoint>(meta, extptr);
                                var obj = new MCExtensionDefSpawnPoint();
                                obj.Parse(meta, data);
                                ExtensionDefSpawnPoints.Add(obj);
                                break;
                            }

                        case MetaName.CExtensionDefWindDisturbance:
                            {
                                var data = MetaUtils.GetData<CExtensionDefWindDisturbance>(meta, extptr);
                                var obj = new MCExtensionDefWindDisturbance();
                                obj.Parse(meta, data);
                                ExtensionDefWindDisturbances.Add(obj);
                                break;
                            }

                        default: break;
                    }
                }
            }

            this.MloFlags = CMloArchetypeDef.mloFlags;

            this.Entities = MetaUtils.ConvertDataArray<CEntityDef>(this.Meta, CMloArchetypeDef.entities)?.Select(e => { var obj = new MCEntityDef(); obj.Parse(meta, e); return obj; }).ToList() ?? new List<MCEntityDef>();

            var rooms = MetaUtils.ConvertDataArray<CMloRoomDef>(meta, CMloArchetypeDef.rooms);
            this.Rooms = rooms?.Select(e => { var msw = new MCMloRoomDef(this); msw.Parse(meta, e); return msw; }).ToList();

            var portals = MetaUtils.ConvertDataArray<CMloPortalDef>(meta, CMloArchetypeDef.portals);
            this.Portals = portals?.Select(e => { var msw = new MCMloPortalDef(); msw.Parse(meta, e); return msw; }).ToList();

            var entitySets = MetaUtils.ConvertDataArray<CMloEntitySet>(meta, CMloArchetypeDef.entitySets);
            this.EntitySets = entitySets?.Select(e => { var msw = new MCMloEntitySet(); msw.Parse(meta, e); return msw; }).ToList();

            var timeCycleModifiers = MetaUtils.ConvertDataArray<CMloTimeCycleModifier>(meta, CMloArchetypeDef.timeCycleModifiers);
            this.TimeCycleModifiers = timeCycleModifiers?.Select(e => { var msw = new MCMloTimeCycleModifier(); msw.Parse(meta, e); return msw; }).ToList();

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
            this.MetaStructure.name = this.Name;
            this.MetaStructure.textureDictionary = this.TextureDictionary;
            this.MetaStructure.clipDictionary = this.ClipDictionary;
            this.MetaStructure.drawableDictionary = this.DrawableDictionary;
            this.MetaStructure.physicsDictionary = this.PhysicsDictionary;
            this.MetaStructure.assetType = this.AssetType;
            this.MetaStructure.assetName = this.AssetName;

            // CEntityDef + ?
            var entPtrs = new List<MetaPOINTER>();
            this.AddMetaPointers(mb, entPtrs, MetaName.CEntityDef, this.Entities.Select(e => { e.Build(mb); return e.MetaStructure; }));
            this.MetaStructure.entities = mb.AddPointerArray(entPtrs.ToArray());

            if (this.Rooms != null)
                this.MetaStructure.rooms = mb.AddItemArrayPtr(MetaName.CMloRoomDef, this.Rooms.Select(e => { e.Build(mb); return e.MetaStructure; }).ToArray());
            MCMloRoomDef.AddEnumAndStructureInfo(mb);

            if (this.Portals != null)
                this.MetaStructure.portals = mb.AddItemArrayPtr(MetaName.CMloPortalDef, this.Portals.Select(e => { e.Build(mb); return e.MetaStructure; }).ToArray());
            MCMloPortalDef.AddEnumAndStructureInfo(mb);

            if (this.EntitySets != null)
                this.MetaStructure.entitySets = mb.AddItemArrayPtr(MetaName.CMloEntitySet, this.EntitySets.Select(e => { e.Build(mb); return e.MetaStructure; }).ToArray());
            MCMloEntitySet.AddEnumAndStructureInfo(mb);

            if (this.TimeCycleModifiers != null)
                this.MetaStructure.timeCycleModifiers = mb.AddItemArrayPtr(MetaName.CMloTimeCycleModifier, this.TimeCycleModifiers.Select(e => { e.Build(mb); return e.MetaStructure; }).ToArray());
            MCMloTimeCycleModifier.AddEnumAndStructureInfo(mb);

            this.MetaStructure.mloFlags = this.MloFlags;

            // Extensions
            var extPtrs = new List<MetaPOINTER>();

            if (this.ExtensionDefAudioCollisionSettings != null)
                this.AddMetaPointers(mb, extPtrs, MetaName.CExtensionDefAudioCollisionSettings, this.ExtensionDefAudioCollisionSettings.Select(e => { e.Build(mb); return e.MetaStructure; }));

            if (this.ExtensionDefAudioEmitters != null)
                this.AddMetaPointers(mb, extPtrs, MetaName.CExtensionDefAudioEmitter, this.ExtensionDefAudioEmitters.Select(e => { e.Build(mb); return e.MetaStructure; }));

            if (this.ExtensionDefBuoyancies != null)
                this.AddMetaPointers(mb, extPtrs, MetaName.CExtensionDefBuoyancy, this.ExtensionDefBuoyancies.Select(e => { e.Build(mb); return e.MetaStructure; }));

            if (this.ExtensionDefExplosionEffects != null)
                this.AddMetaPointers(mb, extPtrs, MetaName.CExtensionDefExplosionEffect, this.ExtensionDefExplosionEffects.Select(e => { e.Build(mb); return e.MetaStructure; }));

            if (this.ExtensionDefExpressions != null)
                this.AddMetaPointers(mb, extPtrs, MetaName.CExtensionDefExpression, this.ExtensionDefExpressions.Select(e => { e.Build(mb); return e.MetaStructure; }));

            if (this.ExtensionDefLadders != null)
                this.AddMetaPointers(mb, extPtrs, MetaName.CExtensionDefLadder, this.ExtensionDefLadders.Select(e => { e.Build(mb); return e.MetaStructure; }));

            if (this.ExtensionDefLightShafts != null)
                this.AddMetaPointers(mb, extPtrs, MetaName.CExtensionDefLightShaft, this.ExtensionDefLightShafts.Select(e => { e.Build(mb); return e.MetaStructure; }));

            if (this.ExtensionDefParticleEffects != null)
                this.AddMetaPointers(mb, extPtrs, MetaName.CExtensionDefParticleEffect, this.ExtensionDefParticleEffects.Select(e => { e.Build(mb); return e.MetaStructure; }));

            if (this.ExtensionDefProcObjects != null)
                this.AddMetaPointers(mb, extPtrs, MetaName.CExtensionDefProcObject, this.ExtensionDefProcObjects.Select(e => { e.Build(mb); return e.MetaStructure; }));

            if (this.ExtensionDefSpawnPoints != null)
                this.AddMetaPointers(mb, extPtrs, MetaName.CExtensionDefSpawnPoint, this.ExtensionDefSpawnPoints.Select(e => { e.Build(mb); return e.MetaStructure; }));

            if (this.ExtensionDefWindDisturbances != null)
                this.AddMetaPointers(mb, extPtrs, MetaName.CExtensionDefWindDisturbance, this.ExtensionDefWindDisturbances.Select(e => { e.Build(mb); return e.MetaStructure; }));

            this.MetaStructure.extensions = mb.AddPointerArray(extPtrs.ToArray());

            MCMloArchetypeDef.AddEnumAndStructureInfo(mb);

            if (isRoot)
            {
                mb.AddItem(this.MetaName, this.MetaStructure);

                this.Meta = mb.GetMeta();
            }

        }

        public int AddEntity(MCEntityDef entity, int idx = -1)
        {
            if (idx == -1)
            {
                this.Entities.Add(entity);
                idx = this.Entities.IndexOf(entity);
            }
            else
            {
                var roomEntities = new Dictionary<MCMloRoomDef, List<MCEntityDef>>();

                for (int i = 0; i < this.Rooms.Count; i++)
                {
                    var room = this.Rooms[i];
                    roomEntities[room] = room.AttachedObjects.Select(e => this.Entities[(int)e]).ToList();
                }

                this.Entities.Insert(idx, entity);

                for (int i = 0; i < this.Rooms.Count; i++)
                {
                    var room = this.Rooms[i];
                    room.AttachedObjects = roomEntities[room].Select(e => (uint)this.Entities.IndexOf(e)).ToList();
                }
            }

            return idx;
        }

        public void RemoveEntity(MCEntityDef entity)
        {
            int idx = this.Entities.IndexOf(entity);

            if (idx == -1)
                return;

            var roomEntities = new Dictionary<MCMloRoomDef, List<MCEntityDef>>();

            for (int i = 0; i < this.Rooms.Count; i++)
            {
                var room = this.Rooms[i];
                roomEntities[room] = room.AttachedObjects.Select(e => this.Entities[(int)e]).ToList();
            }

            this.Entities.RemoveAt(idx);

            for (int i = 0; i < this.Rooms.Count; i++)
            {
                var room = this.Rooms[i];
                room.AttachedObjects = roomEntities[room].Select(e => (uint)this.Entities.IndexOf(e)).ToList();
            }

        }

	}
}
