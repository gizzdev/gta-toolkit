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

namespace RageLib.GTA5.ResourceWrappers.PC.Meta.Structures
{
	public class CMloArchetypeDef : MetaStructureWrapper<RageLib.Resources.GTA5.PC.Meta.CMloArchetypeDef>
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

        //Extensions
        public List<CExtensionDefAudioCollisionSettings> ExtensionDefAudioCollisionSettings = new List<CExtensionDefAudioCollisionSettings>();
        public List<CExtensionDefAudioEmitter> ExtensionDefAudioEmitters = new List<CExtensionDefAudioEmitter>();
        public List<CExtensionDefBuoyancy> ExtensionDefBuoyancies = new List<CExtensionDefBuoyancy>();
        public List<CExtensionDefExplosionEffect> ExtensionDefExplosionEffects = new List<CExtensionDefExplosionEffect>();
        public List<CExtensionDefExpression> ExtensionDefExpressions = new List<CExtensionDefExpression>();
        public List<CExtensionDefLadder> ExtensionDefLadders = new List<CExtensionDefLadder>();
        public List<CExtensionDefLightShaft> ExtensionDefLightShafts = new List<CExtensionDefLightShaft>();
        public List<CExtensionDefParticleEffect> ExtensionDefParticleEffects = new List<CExtensionDefParticleEffect>();
        public List<CExtensionDefProcObject> ExtensionDefProcObjects = new List<CExtensionDefProcObject>();
        public List<CExtensionDefSpawnPoint> ExtensionDefSpawnPoints = new List<CExtensionDefSpawnPoint>();
        public List<CExtensionDefWindDisturbance> ExtensionDefWindDisturbances = new List<CExtensionDefWindDisturbance>();

        public uint MloFlags;
		public List<CEntityDef> Entities;
		public List<CMloRoomDef> Rooms;
		public List<CMloPortalDef> Portals;
		public List<CMloEntitySet> EntitySets;
		public List<CMloTimeCycleModifier> TimeCycleModifiers;

		public CMloArchetypeDef()
		{
			this.MetaName = MetaName.CMloArchetypeDef;
			this.MetaStructure = new RageLib.Resources.GTA5.PC.Meta.CMloArchetypeDef();
		}

		public override void Parse(MetaFile meta, RageLib.Resources.GTA5.PC.Meta.CMloArchetypeDef CMloArchetypeDef)
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
            this.ExtensionDefAudioCollisionSettings = MetaUtils.GetTypedDataArray<RageLib.Resources.GTA5.PC.Meta.CExtensionDefAudioCollisionSettings>(this.Meta, MetaName.CExtensionDefAudioCollisionSettings)?.Select(e => { var obj = new CExtensionDefAudioCollisionSettings(); obj.Parse(meta, e); return obj; }).ToList();
            this.ExtensionDefAudioEmitters = MetaUtils.GetTypedDataArray<RageLib.Resources.GTA5.PC.Meta.CExtensionDefAudioEmitter>(this.Meta, MetaName.CExtensionDefAudioEmitter)?.Select(e => { var obj = new CExtensionDefAudioEmitter(); obj.Parse(meta, e); return obj; }).ToList();
            this.ExtensionDefBuoyancies = MetaUtils.GetTypedDataArray<RageLib.Resources.GTA5.PC.Meta.CExtensionDefBuoyancy>(this.Meta, MetaName.CExtensionDefBuoyancy)?.Select(e => { var obj = new CExtensionDefBuoyancy(); obj.Parse(meta, e); return obj; }).ToList();
            this.ExtensionDefExplosionEffects = MetaUtils.GetTypedDataArray<RageLib.Resources.GTA5.PC.Meta.CExtensionDefExplosionEffect>(this.Meta, MetaName.CExtensionDefExplosionEffect)?.Select(e => { var obj = new CExtensionDefExplosionEffect(); obj.Parse(meta, e); return obj; }).ToList();
            this.ExtensionDefExpressions = MetaUtils.GetTypedDataArray<RageLib.Resources.GTA5.PC.Meta.CExtensionDefExpression>(this.Meta, MetaName.CExtensionDefExpression)?.Select(e => { var obj = new CExtensionDefExpression(); obj.Parse(meta, e); return obj; }).ToList();
            this.ExtensionDefLadders = MetaUtils.GetTypedDataArray<RageLib.Resources.GTA5.PC.Meta.CExtensionDefLadder>(this.Meta, MetaName.CExtensionDefLadder)?.Select(e => { var obj = new CExtensionDefLadder(); obj.Parse(meta, e); return obj; }).ToList();
            this.ExtensionDefLightShafts = MetaUtils.GetTypedDataArray<RageLib.Resources.GTA5.PC.Meta.CExtensionDefLightShaft>(this.Meta, MetaName.CExtensionDefLightShaft)?.Select(e => { var obj = new CExtensionDefLightShaft(); obj.Parse(meta, e); return obj; }).ToList();
            this.ExtensionDefParticleEffects = MetaUtils.GetTypedDataArray<RageLib.Resources.GTA5.PC.Meta.CExtensionDefParticleEffect>(this.Meta, MetaName.CExtensionDefParticleEffect)?.Select(e => { var obj = new CExtensionDefParticleEffect(); obj.Parse(meta, e); return obj; }).ToList();
            this.ExtensionDefProcObjects = MetaUtils.GetTypedDataArray<RageLib.Resources.GTA5.PC.Meta.CExtensionDefProcObject>(this.Meta, MetaName.CExtensionDefProcObject)?.Select(e => { var obj = new CExtensionDefProcObject(); obj.Parse(meta, e); return obj; }).ToList();
            this.ExtensionDefSpawnPoints = MetaUtils.GetTypedDataArray<RageLib.Resources.GTA5.PC.Meta.CExtensionDefSpawnPoint>(this.Meta, MetaName.CExtensionDefSpawnPoint)?.Select(e => { var obj = new CExtensionDefSpawnPoint(); obj.Parse(meta, e); return obj; }).ToList();
            this.ExtensionDefWindDisturbances = MetaUtils.GetTypedDataArray<RageLib.Resources.GTA5.PC.Meta.CExtensionDefWindDisturbance>(this.Meta, MetaName.CExtensionDefWindDisturbance)?.Select(e => { var obj = new CExtensionDefWindDisturbance(); obj.Parse(meta, e); return obj; }).ToList();

            this.MloFlags = CMloArchetypeDef.mloFlags;

            this.Entities = MetaUtils.GetTypedDataArray<RageLib.Resources.GTA5.PC.Meta.CEntityDef>(this.Meta, MetaName.CEntityDef).Select(e => { var obj = new CEntityDef(); obj.Parse(meta, e); return obj; }).ToList();

            var rooms = MetaUtils.ConvertDataArray<RageLib.Resources.GTA5.PC.Meta.CMloRoomDef>(meta, CMloArchetypeDef.rooms);
			this.Rooms = rooms?.Select(e => { var msw = new CMloRoomDef(); msw.Parse(meta, e); return msw; }).ToList();

			var portals = MetaUtils.ConvertDataArray<RageLib.Resources.GTA5.PC.Meta.CMloPortalDef>(meta, CMloArchetypeDef.portals);
			this.Portals = portals?.Select(e => { var msw = new CMloPortalDef(); msw.Parse(meta, e); return msw; }).ToList();

			var entitySets = MetaUtils.ConvertDataArray<RageLib.Resources.GTA5.PC.Meta.CMloEntitySet>(meta, CMloArchetypeDef.entitySets);
			this.EntitySets = entitySets?.Select(e => { var msw = new CMloEntitySet(); msw.Parse(meta, e); return msw; }).ToList();

			var timeCycleModifiers = MetaUtils.ConvertDataArray<RageLib.Resources.GTA5.PC.Meta.CMloTimeCycleModifier>(meta, CMloArchetypeDef.timeCycleModifiers);
			this.TimeCycleModifiers = timeCycleModifiers?.Select(e => { var msw = new CMloTimeCycleModifier(); msw.Parse(meta, e); return msw; }).ToList();

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
			if(this.Portals != null)
				this.MetaStructure.portals = mb.AddItemArrayPtr(MetaName.CMloPortalDef, this.Portals.Select(e => { e.Build(mb); return e.MetaStructure; }).ToArray());
			if(this.EntitySets != null)
				this.MetaStructure.entitySets = mb.AddItemArrayPtr(MetaName.CMloEntitySet, this.EntitySets.Select(e => { e.Build(mb); return e.MetaStructure; }).ToArray());
			if(this.TimeCycleModifiers != null)
				this.MetaStructure.timeCycleModifiers = mb.AddItemArrayPtr(MetaName.CMloTimeCycleModifier, this.TimeCycleModifiers.Select(e => { e.Build(mb); return e.MetaStructure; }).ToArray());

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
