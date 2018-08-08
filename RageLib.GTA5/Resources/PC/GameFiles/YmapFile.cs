using System;
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

        public void AddStructureAndEnumInfo(MetaBuilder mb, MetaName name)
        {
            var ei = MetaInfo.GetStructureEnumInfo(name);
            var sci = MetaInfo.GetStructureChildInfo(name);
            var sei = MetaInfo.GetStructureEnumInfo(name);

            for (int i = 0; i < sei.Length; i++)
                mb.AddEnumInfo((MetaName) sei[i].EnumNameHash);

            mb.AddStructureInfo(name);

            for(int i=0; i<sci.Length; i++)
                AddStructureAndEnumInfo(mb, (MetaName) sci[i].StructureNameHash);
        }

    }

}