using System.Collections.Generic;
using System.Linq;
using SharpDX;
using RageLib.Resources.GTA5.PC.Meta;

namespace RageLib.GTA5.ResourceWrappers.PC.Meta.Structures
{
	public class MCScenarioPointRegion : MetaStructureWrapper<CScenarioPointRegion>
	{
		public static MetaName _MetaName = MetaName.CScenarioPointRegion;
		public MetaFile Meta;
		public int VersionNumber;
		public MCScenarioPointContainer Points;
		public List<MCScenarioEntityOverride> EntityOverrides;
		public MUnk_4023740759 Unk_3696045377;
		public Mrage__spdGrid2D AccelGrid;
		public Array_ushort Unk_3844724227;
		public List<MCScenarioPointCluster> Clusters;
		public MCScenarioPointLookUps LookUps;

		public MCScenarioPointRegion()
		{
			this.MetaName = MetaName.CScenarioPointRegion;
			this.MetaStructure = new CScenarioPointRegion();
		}

		public static void AddEnumAndStructureInfo(MetaBuilder mb)
		{
			var enumInfos = MetaInfo.GetStructureEnumInfo(MCScenarioPointRegion._MetaName);

			for (int i = 0; i < enumInfos.Length; i++)
				mb.AddEnumInfo((MetaName) enumInfos[i].EnumNameHash);

			mb.AddStructureInfo(MCScenarioPointRegion._MetaName);
			mb.AddStructureInfo(MetaName.CScenarioPointContainer);
			mb.AddStructureInfo(MetaName.CScenarioEntityOverride);
			mb.AddStructureInfo((MetaName) (-271226537));
			mb.AddStructureInfo(MetaName.rage__spdGrid2D);
			mb.AddStructureInfo(MetaName.CScenarioPointCluster);
			mb.AddStructureInfo(MetaName.CScenarioPointLookUps);
		}


		public override void Parse(MetaFile meta, CScenarioPointRegion CScenarioPointRegion)
		{
			this.Meta = meta;
			this.MetaStructure = CScenarioPointRegion;

			this.VersionNumber = CScenarioPointRegion.VersionNumber;
			this.Points = new MCScenarioPointContainer();
			this.Points.Parse(meta, CScenarioPointRegion.Points);
			var EntityOverrides = MetaUtils.ConvertDataArray<CScenarioEntityOverride>(meta, CScenarioPointRegion.EntityOverrides);
			this.EntityOverrides = EntityOverrides?.Select(e => { var msw = new MCScenarioEntityOverride(); msw.Parse(meta, e); return msw; }).ToList();

			this.Unk_3696045377 = new MUnk_4023740759();
			this.Unk_3696045377.Parse(meta, CScenarioPointRegion.Unk_3696045377);
			this.AccelGrid = new Mrage__spdGrid2D();
			this.AccelGrid.Parse(meta, CScenarioPointRegion.AccelGrid);
			// this.Unk_3844724227 = CScenarioPointRegion.Unk_3844724227;
			var Clusters = MetaUtils.ConvertDataArray<CScenarioPointCluster>(meta, CScenarioPointRegion.Clusters);
			this.Clusters = Clusters?.Select(e => { var msw = new MCScenarioPointCluster(); msw.Parse(meta, e); return msw; }).ToList();

			this.LookUps = new MCScenarioPointLookUps();
			this.LookUps.Parse(meta, CScenarioPointRegion.LookUps);
		}

		public override void Build(MetaBuilder mb, bool isRoot = false)
		{
			this.MetaStructure.VersionNumber = this.VersionNumber;
			this.Points.Build(mb);
			this.MetaStructure.Points = this.Points.MetaStructure;
 			MCScenarioPointContainer.AddEnumAndStructureInfo(mb);                    

			if(this.EntityOverrides != null)
				this.MetaStructure.EntityOverrides = mb.AddItemArrayPtr(MetaName.CScenarioEntityOverride, this.EntityOverrides.Select(e => { e.Build(mb); return e.MetaStructure; }).ToArray());
 			MCScenarioEntityOverride.AddEnumAndStructureInfo(mb);                    

			this.Unk_3696045377.Build(mb);
			this.MetaStructure.Unk_3696045377 = this.Unk_3696045377.MetaStructure;
            MUnk_4023740759.AddEnumAndStructureInfo(mb);          

			this.AccelGrid.Build(mb);
			this.MetaStructure.AccelGrid = this.AccelGrid.MetaStructure;
            Mrage__spdGrid2D.AddEnumAndStructureInfo(mb);          

			// this.MetaStructure.Unk_3844724227 = this.Unk_3844724227;
			if(this.Clusters != null)
				this.MetaStructure.Clusters = mb.AddItemArrayPtr(MetaName.CScenarioPointCluster, this.Clusters.Select(e => { e.Build(mb); return e.MetaStructure; }).ToArray());
 			MCScenarioPointCluster.AddEnumAndStructureInfo(mb);                    

			this.LookUps.Build(mb);
			this.MetaStructure.LookUps = this.LookUps.MetaStructure;
 			MCScenarioPointLookUps.AddEnumAndStructureInfo(mb);                    


 			MCScenarioPointRegion.AddEnumAndStructureInfo(mb);          

			if(isRoot)
			{
				mb.AddItem(this.MetaName, this.MetaStructure);

				this.Meta = mb.GetMeta();
			}
		}
	}
}
