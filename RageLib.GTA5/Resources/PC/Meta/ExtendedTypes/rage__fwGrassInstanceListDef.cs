using System.Collections.Generic;
using System.Linq;

using SharpDX;

namespace RageLib.Resources.GTA5.PC.Meta.ExtendedTypes
{
	public class rage__fwGrassInstanceListDef : MetaStructureWrapper<PC.Meta.rage__fwGrassInstanceListDef>
	{
		public MetaFile Meta;
		public rage__spdAABB BatchAABB;
		public Vector3 ScaleRange;
		public uint ArchetypeName;
		public uint LodDist;
		public float LodFadeStartDist;
		public float LodInstFadeRange;
		public float OrientToTerrain;
		public List<rage__fwGrassInstanceListDef__InstanceData> InstanceList;

		public rage__fwGrassInstanceListDef(MetaName metaName) : base(metaName)
		{
			this.MetaStructure = new PC.Meta.rage__fwGrassInstanceListDef();
		}

		public void Parse(MetaFile meta, PC.Meta.rage__fwGrassInstanceListDef rage__fwGrassInstanceListDef)
		{
			this.Meta = meta;
			this.MetaStructure = rage__fwGrassInstanceListDef;

			var BatchAABBBlocks = MetaUtils.FindBlocks(meta, PC.Meta.MetaName.rage__spdAABB);

			if(BatchAABBBlocks.Length > 0)
			{
				var BatchAABB = MetaUtils.GetTypedData<PC.Meta.rage__spdAABB>(meta, MetaName.rage__spdAABB);
				this.BatchAABB = new rage__spdAABB(MetaName.rage__spdAABB);
				this.BatchAABB.Parse(meta, BatchAABB);
			}
			else
			{
			    this.BatchAABB = null;
			}

			this.ScaleRange = rage__fwGrassInstanceListDef.ScaleRange;
			this.ArchetypeName = rage__fwGrassInstanceListDef.archetypeName;
			this.LodDist = rage__fwGrassInstanceListDef.lodDist;
			this.LodFadeStartDist = rage__fwGrassInstanceListDef.LodFadeStartDist;
			this.LodInstFadeRange = rage__fwGrassInstanceListDef.LodInstFadeRange;
			this.OrientToTerrain = rage__fwGrassInstanceListDef.OrientToTerrain;
			var InstanceList = MetaUtils.ConvertArray_Structure<PC.Meta.rage__fwGrassInstanceListDef__InstanceData>(meta, rage__fwGrassInstanceListDef.InstanceList);
			if(InstanceList != null)
				this.InstanceList = (List<rage__fwGrassInstanceListDef__InstanceData>) (InstanceList.ToList().Select(e => { var msw = new rage__fwGrassInstanceListDef__InstanceData(MetaName.rage__fwGrassInstanceListDef__InstanceData); msw.Parse(meta, e); return msw; }));

		}

		public void Build(MetaBuilder mb, bool isRoot = false)
		{
			if(this.BatchAABB != null)
			{
				this.BatchAABB.Build(mb);
				this.MetaStructure.BatchAABB = this.BatchAABB.MetaStructure;
			}

			this.MetaStructure.ScaleRange = this.ScaleRange;
			this.MetaStructure.archetypeName = this.ArchetypeName;
			this.MetaStructure.lodDist = this.LodDist;
			this.MetaStructure.LodFadeStartDist = this.LodFadeStartDist;
			this.MetaStructure.LodInstFadeRange = this.LodInstFadeRange;
			this.MetaStructure.OrientToTerrain = this.OrientToTerrain;
			if(this.InstanceList != null)
				this.MetaStructure.InstanceList = mb.AddItemArrayPtr(MetaName.rage__fwGrassInstanceListDef__InstanceData, this.InstanceList.Select(e => e.MetaStructure).ToArray());

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
