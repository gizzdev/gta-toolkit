using System;
using System.Collections.Generic;
using RageLib.Resources.GTA5.PC.Meta;
using SharpDX;

namespace RageLib.Resources.GTA5.PC.GameFiles
{
    public class YmapFile : GameFileBase<MetaFile>
    {
        public Meta.ExtendedTypes.CMapData CMapData;

        public uint Flags { get => this.CMapData.Flags; set => this.CMapData.Flags = value; }
        public uint ContentFlags { get => this.CMapData.ContentFlags; set => this.CMapData.ContentFlags = value; }

        public Vector3 StreamingExtentsMin { get => this.CMapData.StreamingExtentsMin; set => this.CMapData.StreamingExtentsMin = value; }
        public Vector3 StreamingExtentsMax { get => this.CMapData.StreamingExtentsMax; set => this.CMapData.StreamingExtentsMax = value; }
        public Vector3 EntitiesExtentsMin { get => this.CMapData.EntitiesExtentsMin; set => this.CMapData.EntitiesExtentsMin = value; }
        public Vector3 EntitiesExtentsMax { get => this.CMapData.EntitiesExtentsMax; set => this.CMapData.EntitiesExtentsMax = value; }

        public List<Meta.ExtendedTypes.CEntityDef> Entities { get => this.CMapData.Entities; set => this.CMapData.Entities = value; }

        public Array_Structure ContainerLods { get => CMapData.ContainerLods; set => CMapData.ContainerLods = value; }
        public List<Meta.ExtendedTypes.Unk_975711773> BoxOccluders { get => CMapData.BoxOccluders; set => CMapData.BoxOccluders = value; }
        public List<Meta.ExtendedTypes.Unk_2741784237> OccludeModels { get => CMapData.OccludeModels; set => CMapData.OccludeModels = value; }
        public Array_uint PhysicsDictionaries { get => CMapData.PhysicsDictionaries; set => CMapData.PhysicsDictionaries = value; }
        public Meta.ExtendedTypes.rage__fwInstancedMapData InstancedData { get => CMapData.InstancedData; set => CMapData.InstancedData = value; }
        public List<Meta.ExtendedTypes.CTimeCycleModifier> TimeCycleModifiers { get => CMapData.TimeCycleModifiers; set => CMapData.TimeCycleModifiers = value; }
        public List<Meta.ExtendedTypes.CCarGen> CarGenerators { get => CMapData.CarGenerators; set => CMapData.CarGenerators = value; }
        public Meta.ExtendedTypes.CLODLight LODLightsSOA { get => CMapData.LODLightsSOA; set => CMapData.LODLightsSOA = value; }
        public Meta.ExtendedTypes.CDistantLODLight DistantLODLightsSOA { get => CMapData.DistantLODLightsSOA; set => CMapData.DistantLODLightsSOA = value; }
        public Meta.ExtendedTypes.CBlockDesc Block { get => CMapData.Block; set => CMapData.Block = value; }

        public override void Parse()
        {
            var CMapDataBlocks = MetaUtils.FindBlocks(this.ResourceFile.ResourceData, MetaName.CMapData);

            if (CMapDataBlocks.Length == 0)
                throw new Exception("CMapData block not found !");

            var CMapData = MetaUtils.ConvertData<CMapData>(CMapDataBlocks[0]);
            this.CMapData = new Meta.ExtendedTypes.CMapData(MetaName.CMapData);

            this.CMapData.Parse(this.ResourceFile.ResourceData, CMapData);
        }

        public override void Build()
        {
            var mb = new MetaBuilder();

            mb.EnsureBlock(MetaName.CMapData);

            this.CMapData.Build(mb, true);

            ResourceFile.ResourceData = this.CMapData.Meta;
        }

    }

}