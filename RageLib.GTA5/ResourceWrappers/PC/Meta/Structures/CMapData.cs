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
	public class CMapData : MetaStructureWrapper<RageLib.Resources.GTA5.PC.Meta.CMapData>
	{
		public MetaFile Meta;
		public MetaName Name;
		public MetaName Parent;
		public uint Flags;
		public uint ContentFlags;
		public Vector3 StreamingExtentsMin;
		public Vector3 StreamingExtentsMax;
		public Vector3 EntitiesExtentsMin;
		public Vector3 EntitiesExtentsMax;
		public List<CEntityDef> Entities = new List<CEntityDef>();
        public List<CMloInstanceDef> MloInstances = new List<CMloInstanceDef>();
		public Array_Structure ContainerLods;
		public List<Unk_975711773> BoxOccluders;
		public List<Unk_2741784237> OccludeModels;
		public Array_uint PhysicsDictionaries;
		public rage__fwInstancedMapData InstancedData;
		public List<CTimeCycleModifier> TimeCycleModifiers;
		public List<CCarGen> CarGenerators;
		public CLODLight LODLightsSOA;
		public CDistantLODLight DistantLODLightsSOA;
		public CBlockDesc Block;

		public CMapData()
		{
			this.MetaName = MetaName.CMapData;
			this.MetaStructure = new RageLib.Resources.GTA5.PC.Meta.CMapData();
		}

		public override void Parse(MetaFile meta, RageLib.Resources.GTA5.PC.Meta.CMapData CMapData)
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

            this.Entities = MetaUtils.GetTypedDataArray<RageLib.Resources.GTA5.PC.Meta.CEntityDef>(this.Meta, MetaName.CEntityDef)?.Select(e => { var obj = new CEntityDef(); obj.Parse(meta, e); return obj; }).ToList();
            this.MloInstances = MetaUtils.GetTypedDataArray<RageLib.Resources.GTA5.PC.Meta.CMloInstanceDef>(this.Meta, MetaName.CMloInstanceDef)?.Select(e => { var obj = new CMloInstanceDef(); obj.Parse(meta, e); return obj; }).ToList();

            // this.ContainerLods = CMapData.containerLods;
            var boxOccluders = MetaUtils.ConvertDataArray<RageLib.Resources.GTA5.PC.Meta.Unk_975711773>(meta, CMapData.boxOccluders);
			this.BoxOccluders = boxOccluders?.Select(e => { var msw = new Unk_975711773(); msw.Parse(meta, e); return msw; }).ToList();

			var occludeModels = MetaUtils.ConvertDataArray<RageLib.Resources.GTA5.PC.Meta.Unk_2741784237>(meta, CMapData.occludeModels);
			this.OccludeModels =occludeModels?.Select(e => { var msw = new Unk_2741784237(); msw.Parse(meta, e); return msw; }).ToList();

			// this.PhysicsDictionaries = CMapData.physicsDictionaries;
			var instancedDataBlocks = meta.FindBlocks(RageLib.Resources.GTA5.PC.Meta.MetaName.rage__fwInstancedMapData);

			if(instancedDataBlocks.Length > 0)
			{
				var instancedData = MetaUtils.GetTypedData<RageLib.Resources.GTA5.PC.Meta.rage__fwInstancedMapData>(meta, MetaName.rage__fwInstancedMapData);
				this.InstancedData = new rage__fwInstancedMapData();
				this.InstancedData.Parse(meta, instancedData);
			}
			else
			{
			    this.InstancedData = null;
			}

			var timeCycleModifiers = MetaUtils.ConvertDataArray<RageLib.Resources.GTA5.PC.Meta.CTimeCycleModifier>(meta, CMapData.timeCycleModifiers);
			if(timeCycleModifiers != null)
				this.TimeCycleModifiers = timeCycleModifiers?.Select(e => { var msw = new CTimeCycleModifier(); msw.Parse(meta, e); return msw; }).ToList();

			var carGenerators = MetaUtils.ConvertDataArray<RageLib.Resources.GTA5.PC.Meta.CCarGen>(meta, CMapData.carGenerators);
			if(carGenerators != null)
				this.CarGenerators = carGenerators?.Select(e => { var msw = new CCarGen(); msw.Parse(meta, e); return msw; }).ToList();

			var LODLightsSOABlocks = meta.FindBlocks(RageLib.Resources.GTA5.PC.Meta.MetaName.CLODLight);

			if(LODLightsSOABlocks.Length > 0)
			{
				var LODLightsSOA = MetaUtils.GetTypedData<RageLib.Resources.GTA5.PC.Meta.CLODLight>(meta, MetaName.CLODLight);
				this.LODLightsSOA = new CLODLight();
				this.LODLightsSOA.Parse(meta, LODLightsSOA);
			}
			else
			{
			    this.LODLightsSOA = null;
			}

			var DistantLODLightsSOABlocks = meta.FindBlocks(RageLib.Resources.GTA5.PC.Meta.MetaName.CDistantLODLight);

			if(DistantLODLightsSOABlocks.Length > 0)
			{
				var DistantLODLightsSOA = MetaUtils.GetTypedData<RageLib.Resources.GTA5.PC.Meta.CDistantLODLight>(meta, MetaName.CDistantLODLight);
				this.DistantLODLightsSOA = new CDistantLODLight();
				this.DistantLODLightsSOA.Parse(meta, DistantLODLightsSOA);
			}
			else
			{
			    this.DistantLODLightsSOA = null;
			}

			var blockBlocks = meta.FindBlocks(RageLib.Resources.GTA5.PC.Meta.MetaName.CBlockDesc);

			if(blockBlocks.Length > 0)
			{
				var block = MetaUtils.GetTypedData<RageLib.Resources.GTA5.PC.Meta.CBlockDesc>(meta, MetaName.CBlockDesc);
				this.Block = new CBlockDesc();
				this.Block.Parse(meta, block);
			}
			else
			{
			    this.Block = null;
			}

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
				this.MetaStructure.boxOccluders = mb.AddItemArrayPtr((MetaName) (975711773), this.BoxOccluders.Select(e => e.MetaStructure).ToArray());
			if(this.OccludeModels != null)
				this.MetaStructure.occludeModels = mb.AddItemArrayPtr((MetaName) (-1553183059), this.OccludeModels.Select(e => e.MetaStructure).ToArray());
			// this.MetaStructure.physicsDictionaries = this.PhysicsDictionaries;
			if(this.InstancedData != null)
			{
				this.InstancedData.Build(mb);
				this.MetaStructure.instancedData = this.InstancedData.MetaStructure;
			}

			if(this.TimeCycleModifiers != null)
				this.MetaStructure.timeCycleModifiers = mb.AddItemArrayPtr(MetaName.CTimeCycleModifier, this.TimeCycleModifiers.Select(e => e.MetaStructure).ToArray());
			if(this.CarGenerators != null)
				this.MetaStructure.carGenerators = mb.AddItemArrayPtr(MetaName.CCarGen, this.CarGenerators.Select(e => e.MetaStructure).ToArray());
			if(this.LODLightsSOA != null)
			{
				this.LODLightsSOA.Build(mb);
				this.MetaStructure.LODLightsSOA = this.LODLightsSOA.MetaStructure;
			}

			if(this.DistantLODLightsSOA != null)
			{
				this.DistantLODLightsSOA.Build(mb);
				this.MetaStructure.DistantLODLightsSOA = this.DistantLODLightsSOA.MetaStructure;
			}

			if(this.Block != null)
			{
				this.Block.Build(mb);
				this.MetaStructure.block = this.Block.MetaStructure;
			}


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
