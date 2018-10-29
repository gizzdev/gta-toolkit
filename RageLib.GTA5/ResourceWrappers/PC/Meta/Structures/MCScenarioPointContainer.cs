using System.Collections.Generic;
using System.Linq;
using SharpDX;
using RageLib.Resources.GTA5.PC.Meta;

namespace RageLib.GTA5.ResourceWrappers.PC.Meta.Structures
{
	public class MCScenarioPointContainer : MetaStructureWrapper<CScenarioPointContainer>
	{
		public static MetaName _MetaName = MetaName.CScenarioPointContainer;
		public MetaFile Meta;
		public List<MCExtensionDefSpawnPoint> LoadSavePoints;
		public List<MCScenarioPoint> MyPoints;

		public MCScenarioPointContainer()
		{
			this.MetaName = MetaName.CScenarioPointContainer;
			this.MetaStructure = new CScenarioPointContainer();
		}

		public static void AddEnumAndStructureInfo(MetaBuilder mb)
		{
			var enumInfos = MetaInfo.GetStructureEnumInfo(MCScenarioPointContainer._MetaName);

			for (int i = 0; i < enumInfos.Length; i++)
				mb.AddEnumInfo((MetaName) enumInfos[i].EnumNameHash);

			mb.AddStructureInfo(MCScenarioPointContainer._MetaName);
			mb.AddStructureInfo(MetaName.CExtensionDefSpawnPoint);
			mb.AddStructureInfo(MetaName.CScenarioPoint);
		}


		public override void Parse(MetaFile meta, CScenarioPointContainer CScenarioPointContainer)
		{
			this.Meta = meta;
			this.MetaStructure = CScenarioPointContainer;

			var LoadSavePoints = MetaUtils.ConvertDataArray<CExtensionDefSpawnPoint>(meta, CScenarioPointContainer.LoadSavePoints);
			this.LoadSavePoints = LoadSavePoints?.Select(e => { var msw = new MCExtensionDefSpawnPoint(); msw.Parse(meta, e); return msw; }).ToList();

			var MyPoints = MetaUtils.ConvertDataArray<CScenarioPoint>(meta, CScenarioPointContainer.MyPoints);
			this.MyPoints = MyPoints?.Select(e => { var msw = new MCScenarioPoint(); msw.Parse(meta, e); return msw; }).ToList();

		}

		public override void Build(MetaBuilder mb, bool isRoot = false)
		{
			if(this.LoadSavePoints != null)
				this.MetaStructure.LoadSavePoints = mb.AddItemArrayPtr(MetaName.CExtensionDefSpawnPoint, this.LoadSavePoints.Select(e => { e.Build(mb); return e.MetaStructure; }).ToArray());
 			MCExtensionDefSpawnPoint.AddEnumAndStructureInfo(mb);                    

			if(this.MyPoints != null)
				this.MetaStructure.MyPoints = mb.AddItemArrayPtr(MetaName.CScenarioPoint, this.MyPoints.Select(e => { e.Build(mb); return e.MetaStructure; }).ToArray());
 			MCScenarioPoint.AddEnumAndStructureInfo(mb);                    


 			MCScenarioPointContainer.AddEnumAndStructureInfo(mb);                    

			if(isRoot)
			{
				mb.AddItem(this.MetaName, this.MetaStructure);

				this.Meta = mb.GetMeta();
			}
		}
	}
}
