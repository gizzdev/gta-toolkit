using System.Collections.Generic;
using System.Linq;

using SharpDX;

namespace RageLib.Resources.GTA5.PC.Meta.ExtendedTypes
{
	public class CMapData : MetaStructureWrapper<PC.Meta.CMapData>
	{
		public MetaFile Meta;
		public uint Name;
		public uint Parent;
		public uint Flags;
		public uint ContentFlags;
		public Vector3 StreamingExtentsMin;
		public Vector3 StreamingExtentsMax;
		public Vector3 EntitiesExtentsMin;
		public Vector3 EntitiesExtentsMax;
		public List<CEntityDef> Entities;
		public Array_Structure ContainerLods;
		public List<Unk_975711773> BoxOccluders;
		public List<Unk_2741784237> OccludeModels;
		public Array_uint PhysicsDictionaries;
		public rage__fwInstancedMapData InstancedData;
		public List<CTimeCycleModifier> TimeCycleModifiers;
		public List<CCarGen> CarGenerators;
		public CLODLight LODLightsSOA;
		public CDistantLODLight DistantLODLightsSOA;
        public CBlockDesc Block = new CBlockDesc(MetaName.CBlockDesc);

        public CMapData(MetaName metaName) : base(metaName)
		{
            this.MetaStructure = new PC.Meta.CMapData();
		}

		public void Parse(MetaFile meta, PC.Meta.CMapData CMapData)
		{
            this.Meta = meta;
			this.MetaStructure = CMapData;

			this.Name = CMapData.name;
			this.Parent = CMapData.parent;
			this.Flags = CMapData.flags;
			this.ContentFlags = CMapData.contentFlags;
			this.StreamingExtentsMin = CMapData.streamingExtentsMin;
			this.StreamingExtentsMax = CMapData.streamingExtentsMax;
			this.EntitiesExtentsMin = CMapData.entitiesExtentsMin;
			this.EntitiesExtentsMax = CMapData.entitiesExtentsMax;

            this.Entities = (MetaUtils.ConvertArray_StructurePointer<PC.Meta.CEntityDef>(meta, CMapData.entities).Select(e => { var obj = new CEntityDef(MetaName.CEntityDef); obj.Parse(meta, e); return obj; }).ToList());
           
            // this.ContainerLods = CMapData.containerLods;
            var boxOccluders = MetaUtils.ConvertArray_Structure<PC.Meta.Unk_975711773>(meta, CMapData.boxOccluders);
			if(boxOccluders != null)
				this.BoxOccluders = (List<Unk_975711773>) (boxOccluders.ToList().Select(e => { var msw = new Unk_975711773((MetaName) (975711773)); msw.Parse(meta, e); return msw; }));

			var occludeModels = MetaUtils.ConvertArray_Structure<PC.Meta.Unk_2741784237>(meta, CMapData.occludeModels);
			if(occludeModels != null)
				this.OccludeModels = (List<Unk_2741784237>) (occludeModels.ToList().Select(e => { var msw = new Unk_2741784237((MetaName) (-1553183059)); msw.Parse(meta, e); return msw; }));

			// this.PhysicsDictionaries = CMapData.physicsDictionaries;
			var instancedDataBlocks = MetaUtils.FindBlocks(meta, PC.Meta.MetaName.rage__fwInstancedMapData);

			if(instancedDataBlocks.Length > 0)
			{
				var instancedData = MetaUtils.GetTypedData<PC.Meta.rage__fwInstancedMapData>(meta, MetaName.rage__fwInstancedMapData);
				this.InstancedData = new rage__fwInstancedMapData(MetaName.rage__fwInstancedMapData);
				this.InstancedData.Parse(meta, instancedData);
			}
			else
			{
			    this.InstancedData = null;
			}

			var timeCycleModifiers = MetaUtils.ConvertArray_Structure<PC.Meta.CTimeCycleModifier>(meta, CMapData.timeCycleModifiers);
			if(timeCycleModifiers != null)
				this.TimeCycleModifiers = (List<CTimeCycleModifier>) (timeCycleModifiers.ToList().Select(e => { var msw = new CTimeCycleModifier(MetaName.CTimeCycleModifier); msw.Parse(meta, e); return msw; }));

			var carGenerators = MetaUtils.ConvertArray_Structure<PC.Meta.CCarGen>(meta, CMapData.carGenerators);
			if(carGenerators != null)
				this.CarGenerators = (List<CCarGen>) (carGenerators.ToList().Select(e => { var msw = new CCarGen(MetaName.CCarGen); msw.Parse(meta, e); return msw; }));

			var LODLightsSOABlocks = MetaUtils.FindBlocks(meta, PC.Meta.MetaName.CLODLight);

			if(LODLightsSOABlocks.Length > 0)
			{
				var LODLightsSOA = MetaUtils.GetTypedData<PC.Meta.CLODLight>(meta, MetaName.CLODLight);
				this.LODLightsSOA = new CLODLight(MetaName.CLODLight);
				this.LODLightsSOA.Parse(meta, LODLightsSOA);
			}
			else
			{
			    this.LODLightsSOA = null;
			}

			var DistantLODLightsSOABlocks = MetaUtils.FindBlocks(meta, PC.Meta.MetaName.CDistantLODLight);

			if(DistantLODLightsSOABlocks.Length > 0)
			{
				var DistantLODLightsSOA = MetaUtils.GetTypedData<PC.Meta.CDistantLODLight>(meta, MetaName.CDistantLODLight);
				this.DistantLODLightsSOA = new CDistantLODLight(MetaName.CDistantLODLight);
				this.DistantLODLightsSOA.Parse(meta, DistantLODLightsSOA);
			}
			else
			{
			    this.DistantLODLightsSOA = null;
			}

			var blockBlocks = MetaUtils.FindBlocks(meta, PC.Meta.MetaName.CBlockDesc);

			if(blockBlocks.Length > 0)
			{
				var block = MetaUtils.GetTypedData<PC.Meta.CBlockDesc>(meta, MetaName.CBlockDesc);
				this.Block = new CBlockDesc(MetaName.CBlockDesc);
				this.Block.Parse(meta, block);
			}

		}

		public void Build(MetaBuilder mb, bool isRoot = false)
		{
			this.MetaStructure.name = this.Name;
			this.MetaStructure.parent = this.Parent;
			this.MetaStructure.flags = this.Flags;
			this.MetaStructure.contentFlags = this.ContentFlags;
			this.MetaStructure.streamingExtentsMin = this.StreamingExtentsMin;
			this.MetaStructure.streamingExtentsMax = this.StreamingExtentsMax;
			this.MetaStructure.entitiesExtentsMin = this.EntitiesExtentsMin;
			this.MetaStructure.entitiesExtentsMax = this.EntitiesExtentsMax;

            this.MetaStructure.entities = mb.AddItemPointerArrayPtr(MetaName.CEntityDef, this.Entities.Select(e => { e.Build(mb);  return e.MetaStructure; }).ToArray());
            
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

            if (isRoot)
            {
                mb.AddItem(this.MetaName, this.MetaStructure);

                this.Meta = mb.GetMeta();
            }
        }
	}
}
