using System.Collections.Generic;
using System.Linq;
using SharpDX;
using RageLib.Resources.GTA5.PC.Meta;

namespace RageLib.GTA5.ResourceWrappers.PC.Meta.Structures
{
	public class Mrage__fwGrassInstanceListDef : MetaStructureWrapper<rage__fwGrassInstanceListDef>
	{
		public static MetaName _MetaName = MetaName.rage__fwGrassInstanceListDef;
		public MetaFile Meta;
		public Mrage__spdAABB BatchAABB;
		public Vector3 ScaleRange;
		public uint ArchetypeName;
		public uint LodDist;
		public float LodFadeStartDist;
		public float LodInstFadeRange;
		public float OrientToTerrain;
		public List<Mrage__fwGrassInstanceListDef__InstanceData> InstanceList = new List<Mrage__fwGrassInstanceListDef__InstanceData>();

		public Mrage__fwGrassInstanceListDef()
		{
			this.MetaName = MetaName.rage__fwGrassInstanceListDef;
			this.MetaStructure = new rage__fwGrassInstanceListDef();
		}

		public static void AddEnumAndStructureInfo(MetaBuilder mb)
		{
			var enumInfos = MetaInfo.GetStructureEnumInfo(Mrage__fwGrassInstanceListDef._MetaName);

			for (int i = 0; i < enumInfos.Length; i++)
				mb.AddEnumInfo((MetaName) enumInfos[i].EnumNameHash);

			mb.AddStructureInfo(Mrage__fwGrassInstanceListDef._MetaName);
			mb.AddStructureInfo(MetaName.rage__spdAABB);
			mb.AddStructureInfo(MetaName.rage__fwGrassInstanceListDef__InstanceData);
		}


		public override void Parse(MetaFile meta, rage__fwGrassInstanceListDef rage__fwGrassInstanceListDef)
		{
			this.Meta = meta;
			this.MetaStructure = rage__fwGrassInstanceListDef;

			this.BatchAABB = new Mrage__spdAABB();
			this.BatchAABB.Parse(meta, rage__fwGrassInstanceListDef.BatchAABB);
			this.ScaleRange = rage__fwGrassInstanceListDef.ScaleRange;
			this.ArchetypeName = rage__fwGrassInstanceListDef.archetypeName;
			this.LodDist = rage__fwGrassInstanceListDef.lodDist;
			this.LodFadeStartDist = rage__fwGrassInstanceListDef.LodFadeStartDist;
			this.LodInstFadeRange = rage__fwGrassInstanceListDef.LodInstFadeRange;
			this.OrientToTerrain = rage__fwGrassInstanceListDef.OrientToTerrain;
			var InstanceList = MetaUtils.ConvertDataArray<rage__fwGrassInstanceListDef__InstanceData>(meta, rage__fwGrassInstanceListDef.InstanceList);
			this.InstanceList = InstanceList?.Select(e => { var msw = new Mrage__fwGrassInstanceListDef__InstanceData(); msw.Parse(meta, e); return msw; }).ToList();

		}

		public override void Build(MetaBuilder mb, bool isRoot = false)
		{
			this.BatchAABB.Build(mb);
			this.MetaStructure.BatchAABB = this.BatchAABB.MetaStructure;
            Mrage__spdAABB.AddEnumAndStructureInfo(mb);          

			this.MetaStructure.ScaleRange = this.ScaleRange;
			this.MetaStructure.archetypeName = this.ArchetypeName;
			this.MetaStructure.lodDist = this.LodDist;
			this.MetaStructure.LodFadeStartDist = this.LodFadeStartDist;
			this.MetaStructure.LodInstFadeRange = this.LodInstFadeRange;
			this.MetaStructure.OrientToTerrain = this.OrientToTerrain;
			if(this.InstanceList != null)
				this.MetaStructure.InstanceList = mb.AddItemArrayPtr(MetaName.rage__fwGrassInstanceListDef__InstanceData, this.InstanceList.Select(e => { e.Build(mb); return e.MetaStructure; }).ToArray());
            Mrage__fwGrassInstanceListDef__InstanceData.AddEnumAndStructureInfo(mb);


            Mrage__fwGrassInstanceListDef.AddEnumAndStructureInfo(mb);          

			if(isRoot)
			{
				mb.AddItem(this.MetaName, this.MetaStructure);

				this.Meta = mb.GetMeta();
			}
		}
	}
}
