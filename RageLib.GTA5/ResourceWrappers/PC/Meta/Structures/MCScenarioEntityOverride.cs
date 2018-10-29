using System.Collections.Generic;
using System.Linq;
using SharpDX;
using RageLib.Resources.GTA5.PC.Meta;

namespace RageLib.GTA5.ResourceWrappers.PC.Meta.Structures
{
	public class MCScenarioEntityOverride : MetaStructureWrapper<CScenarioEntityOverride>
	{
		public static MetaName _MetaName = MetaName.CScenarioEntityOverride;
		public MetaFile Meta;
		public Vector3 EntityPosition;
		public uint EntityType;
		public List<MCExtensionDefSpawnPoint> ScenarioPoints;
		public byte Unk_538733109;
		public byte Unk_1035513142;

		public MCScenarioEntityOverride()
		{
			this.MetaName = MetaName.CScenarioEntityOverride;
			this.MetaStructure = new CScenarioEntityOverride();
		}

		public static void AddEnumAndStructureInfo(MetaBuilder mb)
		{
			var enumInfos = MetaInfo.GetStructureEnumInfo(MCScenarioEntityOverride._MetaName);

			for (int i = 0; i < enumInfos.Length; i++)
				mb.AddEnumInfo((MetaName) enumInfos[i].EnumNameHash);

			mb.AddStructureInfo(MCScenarioEntityOverride._MetaName);
			mb.AddStructureInfo(MetaName.CExtensionDefSpawnPoint);
		}


		public override void Parse(MetaFile meta, CScenarioEntityOverride CScenarioEntityOverride)
		{
			this.Meta = meta;
			this.MetaStructure = CScenarioEntityOverride;

			this.EntityPosition = CScenarioEntityOverride.EntityPosition;
			this.EntityType = CScenarioEntityOverride.EntityType;
			var ScenarioPoints = MetaUtils.ConvertDataArray<CExtensionDefSpawnPoint>(meta, CScenarioEntityOverride.ScenarioPoints);
			this.ScenarioPoints = ScenarioPoints?.Select(e => { var msw = new MCExtensionDefSpawnPoint(); msw.Parse(meta, e); return msw; }).ToList();

			this.Unk_538733109 = CScenarioEntityOverride.Unk_538733109;
			this.Unk_1035513142 = CScenarioEntityOverride.Unk_1035513142;
		}

		public override void Build(MetaBuilder mb, bool isRoot = false)
		{
			this.MetaStructure.EntityPosition = this.EntityPosition;
			this.MetaStructure.EntityType = this.EntityType;
			if(this.ScenarioPoints != null)
				this.MetaStructure.ScenarioPoints = mb.AddItemArrayPtr(MetaName.CExtensionDefSpawnPoint, this.ScenarioPoints.Select(e => { e.Build(mb); return e.MetaStructure; }).ToArray());
 			MCExtensionDefSpawnPoint.AddEnumAndStructureInfo(mb);                    

			this.MetaStructure.Unk_538733109 = this.Unk_538733109;
			this.MetaStructure.Unk_1035513142 = this.Unk_1035513142;

 			MCScenarioEntityOverride.AddEnumAndStructureInfo(mb);          

			if(isRoot)
			{
				mb.AddItem(this.MetaName, this.MetaStructure);

				this.Meta = mb.GetMeta();
			}
		}
	}
}
