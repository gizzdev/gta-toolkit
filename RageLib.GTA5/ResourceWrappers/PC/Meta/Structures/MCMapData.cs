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
using System;

namespace RageLib.GTA5.ResourceWrappers.PC.Meta.Structures
{
	public class MCMapData : MetaStructureWrapper<CMapData>
	{
        public static MetaName _MetaName = MetaName.CMapData;
		public MetaFile Meta;
		public MetaName Name;
		public MetaName Parent;
		public uint Flags;
		public uint ContentFlags;
		public Vector3 StreamingExtentsMin;
		public Vector3 StreamingExtentsMax;
		public Vector3 EntitiesExtentsMin;
		public Vector3 EntitiesExtentsMax;
		public List<MCEntityDef> Entities = new List<MCEntityDef>();
        public List<MCMloInstanceDef> MloInstances = new List<MCMloInstanceDef>();
		public Array_Structure ContainerLods;
		public List<MUnk_975711773> BoxOccluders;
		public List<MUnk_2741784237> OccludeModels;
		public Array_uint PhysicsDictionaries;
		public Mrage__fwInstancedMapData InstancedData;
		public List<MCTimeCycleModifier> TimeCycleModifiers;
		public List<MCCarGen> CarGenerators;
		public MCLODLight LODLightsSOA;
		public MCDistantLODLight DistantLODLightsSOA;
		public MCBlockDesc Block = new MCBlockDesc();

        public MCMapData _ParentMapData;
        public MCMapData ParentMapData
        {
            get
            {
                return this._ParentMapData;
            }
            set
            {
                this._ParentMapData = value;

                for (int k = 0; k < this.Entities.Count; k++)
                {
                    var entity = this.Entities[k];
                    entity.ParentIndex = entity.ParentIndex;
                }
            }
        }

		public MCMapData(MCMapData parentMapData = null)
		{
			this.MetaName = MetaName.CMapData;
			this.MetaStructure = new CMapData();

            this.ParentMapData = parentMapData;

        }

        public static void AddEnumAndStructureInfo(MetaBuilder mb)
        {
            var enumInfos = MetaInfo.GetStructureEnumInfo(MCMapData._MetaName);

            for (int i = 0; i < enumInfos.Length; i++)
                mb.AddEnumInfo((MetaName)enumInfos[i].EnumNameHash);

            mb.AddStructureInfo(MCMapData._MetaName);
            mb.AddStructureInfo((MetaName)(975711773));
            mb.AddStructureInfo((MetaName)(-1553183059));
            mb.AddStructureInfo(MetaName.rage__fwInstancedMapData);
            mb.AddStructureInfo(MetaName.CTimeCycleModifier);
            mb.AddStructureInfo(MetaName.CCarGen);
            mb.AddStructureInfo(MetaName.CLODLight);
            mb.AddStructureInfo(MetaName.CDistantLODLight);
            mb.AddStructureInfo(MetaName.CBlockDesc);
        }

        public override void Parse(MetaFile meta, CMapData CMapData)
		{
			this.Meta = meta;
			this.MetaStructure = CMapData;

			this.Name = (MetaName) CMapData.name;
			this.Parent = (MetaName) CMapData.parent;
			this.Flags = CMapData.flags;
			this.ContentFlags = CMapData.contentFlags;
			this.StreamingExtentsMin = CMapData.streamingExtentsMin;
			this.StreamingExtentsMax = CMapData.streamingExtentsMax;
			this.EntitiesExtentsMin = CMapData.entitiesExtentsMin;
			this.EntitiesExtentsMax = CMapData.entitiesExtentsMax;

            this.Entities = MetaUtils.GetTypedDataArray<CEntityDef>(this.Meta, MetaName.CEntityDef)?.Select(e => { var obj = new MCEntityDef(this); obj.Parse(meta, e); return obj; }).ToList() ?? new List<MCEntityDef>();

            this.MloInstances = MetaUtils.GetTypedDataArray<CMloInstanceDef>(this.Meta, MetaName.CMloInstanceDef)?.Select(e => { var obj = new MCMloInstanceDef(); obj.Parse(meta, e); return obj; }).ToList() ?? new List<MCMloInstanceDef>();

            this.ContainerLods = new Array_Structure();
            var boxOccluders = MetaUtils.ConvertDataArray<Unk_975711773>(meta, CMapData.boxOccluders);
            this.BoxOccluders = boxOccluders?.Select(e => { var msw = new MUnk_975711773(); msw.Parse(meta, e); return msw; }).ToList();

            var occludeModels = MetaUtils.ConvertDataArray<Unk_2741784237>(meta, CMapData.occludeModels);
            this.OccludeModels = occludeModels?.Select(e => { var msw = new MUnk_2741784237(); msw.Parse(meta, e); return msw; }).ToList();

            this.PhysicsDictionaries = CMapData.physicsDictionaries;
            this.InstancedData = new Mrage__fwInstancedMapData();
            this.InstancedData.Parse(meta, CMapData.instancedData);


            var timeCycleModifiers = MetaUtils.ConvertDataArray<CTimeCycleModifier>(meta, CMapData.timeCycleModifiers);
            this.TimeCycleModifiers = timeCycleModifiers?.Select(e => { var msw = new MCTimeCycleModifier(); msw.Parse(meta, e); return msw; }).ToList();


            var carGenerators = MetaUtils.ConvertDataArray<CCarGen>(meta, CMapData.carGenerators);
            this.CarGenerators = carGenerators?.Select(e => { var msw = new MCCarGen(); msw.Parse(meta, e); return msw; }).ToList();

            this.LODLightsSOA = new MCLODLight();
            this.LODLightsSOA.Parse(meta, CMapData.LODLightsSOA);
            this.DistantLODLightsSOA = new MCDistantLODLight();
            this.DistantLODLightsSOA.Parse(meta, CMapData.DistantLODLightsSOA);
            this.Block = new MCBlockDesc();
            this.Block.Parse(meta, CMapData.block);

        }

        public void ParseFast(MetaFile meta, CMapData CMapData)
        {
            this.Meta = meta;
            this.MetaStructure = CMapData;

            this.Name = (MetaName)CMapData.name;
            this.Parent = (MetaName)CMapData.parent;
            this.Flags = CMapData.flags;
            this.ContentFlags = CMapData.contentFlags;
            this.StreamingExtentsMin = CMapData.streamingExtentsMin;
            this.StreamingExtentsMax = CMapData.streamingExtentsMax;
            this.EntitiesExtentsMin = CMapData.entitiesExtentsMin;
            this.EntitiesExtentsMax = CMapData.entitiesExtentsMax;

            this.Entities = MetaUtils.GetTypedDataArray<CEntityDef>(this.Meta, MetaName.CEntityDef)?.Select(e => { var obj = new MCEntityDef(this); obj.ParseWithoutExtensions(meta, e); return obj; }).ToList() ?? new List<MCEntityDef>();

            this.MloInstances = MetaUtils.GetTypedDataArray<CMloInstanceDef>(this.Meta, MetaName.CMloInstanceDef)?.Select(e => { var obj = new MCMloInstanceDef(); obj.Parse(meta, e); return obj; }).ToList() ?? new List<MCMloInstanceDef>();

            this.Block = new MCBlockDesc();
            this.Block.Parse(meta, CMapData.block);

        }

        public override void Build(MetaBuilder mb, bool isRoot = false)
		{
			this.MetaStructure.name = (uint) this.Name;
			this.MetaStructure.parent = (uint) this.Parent;
			this.MetaStructure.flags = this.Flags;
			this.MetaStructure.contentFlags = this.ContentFlags;
			this.MetaStructure.streamingExtentsMin = this.StreamingExtentsMin;
			this.MetaStructure.streamingExtentsMax = this.StreamingExtentsMax;
			this.MetaStructure.entitiesExtentsMin = this.EntitiesExtentsMin;
			this.MetaStructure.entitiesExtentsMax = this.EntitiesExtentsMax;

            // CEntityDef + CMloInstanceDef
            var entityPtrs = new List<MetaPOINTER>();
            this.AddMetaPointers(mb, entityPtrs, MetaName.CEntityDef, Entities.Select(e => {e.Build(mb); return e.MetaStructure; }));
            this.AddMetaPointers(mb, entityPtrs, MetaName.CMloInstanceDef, MloInstances.Select(e => { e.Build(mb); return e.MetaStructure; }));
            this.MetaStructure.entities = mb.AddPointerArray(entityPtrs.ToArray());

            // this.MetaStructure.containerLods = this.ContainerLods;
            if (this.BoxOccluders != null)
                this.MetaStructure.boxOccluders = mb.AddItemArrayPtr((MetaName)(975711773), this.BoxOccluders.Select(e => e.MetaStructure).ToArray());
              MUnk_975711773.AddEnumAndStructureInfo(mb);          

            if (this.OccludeModels != null)
                this.MetaStructure.occludeModels = mb.AddItemArrayPtr((MetaName)(-1553183059), this.OccludeModels.Select(e => e.MetaStructure).ToArray());
              MUnk_2741784237.AddEnumAndStructureInfo(mb);          

            this.MetaStructure.physicsDictionaries = this.PhysicsDictionaries;

            if(this.InstancedData != null)
            {
                this.InstancedData.Build(mb);
                this.MetaStructure.instancedData = this.InstancedData.MetaStructure;
            }
            Mrage__fwInstancedMapData.AddEnumAndStructureInfo(mb);

            if (this.TimeCycleModifiers != null)
                this.MetaStructure.timeCycleModifiers = mb.AddItemArrayPtr(MetaName.CTimeCycleModifier, this.TimeCycleModifiers.Select(e => e.MetaStructure).ToArray());
             MCTimeCycleModifier.AddEnumAndStructureInfo(mb);          

            if (this.CarGenerators != null)
                this.MetaStructure.carGenerators = mb.AddItemArrayPtr(MetaName.CCarGen, this.CarGenerators.Select(e => e.MetaStructure).ToArray());
              MCCarGen.AddEnumAndStructureInfo(mb);


            if (this.LODLightsSOA != null)
            {
                this.LODLightsSOA.Build(mb);
                this.MetaStructure.LODLightsSOA = this.LODLightsSOA.MetaStructure;
            }
            MCLODLight.AddEnumAndStructureInfo(mb);


            if (this.DistantLODLightsSOA != null)
            {
                this.DistantLODLightsSOA.Build(mb);
                this.MetaStructure.DistantLODLightsSOA = this.DistantLODLightsSOA.MetaStructure;
            }
            MCDistantLODLight.AddEnumAndStructureInfo(mb);                    

            this.Block.Build(mb);
            this.MetaStructure.block = this.Block.MetaStructure;
            MCBlockDesc.AddEnumAndStructureInfo(mb);                    

            MCMapData.AddEnumAndStructureInfo(mb);          

            if (isRoot)
			{
				mb.AddItem(this.MetaName, this.MetaStructure);

				this.Meta = mb.GetMeta();
			}
		}

        public int AddEntity(MCEntityDef entity)
        {
            entity.Parent = this;
            this.Entities.Add(entity);
            return this.Entities.Count - 1;
        }

        public int[] AddEntities(IEnumerable<MCEntityDef> entities)
        {
            var list = entities.ToList();
            var indexes = new List<int>();

            for (int i = 0; i < list.Count; i++)
                indexes.Add(this.AddEntity(list[i]));

            return indexes.ToArray();
        }

        public void RemoveEntity(MCEntityDef entity)
        {
            entity.Parent = null;
            this.Entities.Remove(entity);
        }

        public void RemoveEntities(IEnumerable<MCEntityDef> entities)
        {
            var list = entities.ToList();

            for (int i = 0; i < list.Count; i++)
                this.RemoveEntity(list[i]);
        }

        public static List<Unk_1264241711> LodLevels = new List<Unk_1264241711>
        {
            Unk_1264241711.LODTYPES_DEPTH_SLOD4,
            Unk_1264241711.LODTYPES_DEPTH_SLOD3,
            Unk_1264241711.LODTYPES_DEPTH_SLOD2,
            Unk_1264241711.LODTYPES_DEPTH_SLOD1,
            Unk_1264241711.LODTYPES_DEPTH_LOD,
            Unk_1264241711.LODTYPES_DEPTH_HD,
            Unk_1264241711.LODTYPES_DEPTH_ORPHANHD
        };
    }
}
