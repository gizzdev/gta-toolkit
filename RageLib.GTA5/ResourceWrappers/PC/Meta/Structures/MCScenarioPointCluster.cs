using System.Collections.Generic;
using System.Linq;
using SharpDX;
using RageLib.Resources.GTA5.PC.Meta;

namespace RageLib.GTA5.ResourceWrappers.PC.Meta.Structures
{
	public class MCScenarioPointCluster : MetaStructureWrapper<CScenarioPointCluster>
	{
		public static MetaName _MetaName = MetaName.CScenarioPointCluster;
		public MetaFile Meta;
		public MCScenarioPointContainer Points;
		public Mrage__spdSphere ClusterSphere;
		public float Unk_1095875445;
		public byte Unk_3129415068;

		public MCScenarioPointCluster()
		{
			this.MetaName = MetaName.CScenarioPointCluster;
			this.MetaStructure = new CScenarioPointCluster();
		}

		public static void AddEnumAndStructureInfo(MetaBuilder mb)
		{
			var enumInfos = MetaInfo.GetStructureEnumInfo(MCScenarioPointCluster._MetaName);

			for (int i = 0; i < enumInfos.Length; i++)
				mb.AddEnumInfo((MetaName) enumInfos[i].EnumNameHash);

			mb.AddStructureInfo(MCScenarioPointCluster._MetaName);
			mb.AddStructureInfo(MetaName.CScenarioPointContainer);
			mb.AddStructureInfo(MetaName.rage__spdSphere);
		}


		public override void Parse(MetaFile meta, CScenarioPointCluster CScenarioPointCluster)
		{
			this.Meta = meta;
			this.MetaStructure = CScenarioPointCluster;

			this.Points = new MCScenarioPointContainer();
			this.Points.Parse(meta, CScenarioPointCluster.Points);
			this.ClusterSphere = new Mrage__spdSphere();
			this.ClusterSphere.Parse(meta, CScenarioPointCluster.ClusterSphere);
			this.Unk_1095875445 = CScenarioPointCluster.Unk_1095875445;
			this.Unk_3129415068 = CScenarioPointCluster.Unk_3129415068;
		}

		public override void Build(MetaBuilder mb, bool isRoot = false)
		{
			this.Points.Build(mb);
			this.MetaStructure.Points = this.Points.MetaStructure;
 			MCScenarioPointContainer.AddEnumAndStructureInfo(mb);          

			this.ClusterSphere.Build(mb);
			this.MetaStructure.ClusterSphere = this.ClusterSphere.MetaStructure;
 			Mrage__spdSphere.AddEnumAndStructureInfo(mb);          

			this.MetaStructure.Unk_1095875445 = this.Unk_1095875445;
			this.MetaStructure.Unk_3129415068 = this.Unk_3129415068;

 			MCScenarioPointCluster.AddEnumAndStructureInfo(mb);          

			if(isRoot)
			{
				mb.AddItem(this.MetaName, this.MetaStructure);

				this.Meta = mb.GetMeta();
			}
		}
	}
}
