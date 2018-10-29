using System.Collections.Generic;
using System.Linq;
using SharpDX;
using RageLib.Resources.GTA5.PC.Meta;

namespace RageLib.GTA5.ResourceWrappers.PC.Meta.Structures
{
	public class MCScenarioPointLookUps : MetaStructureWrapper<CScenarioPointLookUps>
	{
		public static MetaName _MetaName = MetaName.CScenarioPointLookUps;
		public MetaFile Meta;
		public Array_uint TypeNames;
		public Array_uint PedModelSetNames;
		public Array_uint VehicleModelSetNames;
		public Array_uint GroupNames;
		public Array_uint InteriorNames;
		public Array_uint RequiredIMapNames;

		public MCScenarioPointLookUps()
		{
			this.MetaName = MetaName.CScenarioPointLookUps;
			this.MetaStructure = new CScenarioPointLookUps();
		}

		public static void AddEnumAndStructureInfo(MetaBuilder mb)
		{
			var enumInfos = MetaInfo.GetStructureEnumInfo(MCScenarioPointLookUps._MetaName);

			for (int i = 0; i < enumInfos.Length; i++)
				mb.AddEnumInfo((MetaName) enumInfos[i].EnumNameHash);

			mb.AddStructureInfo(MCScenarioPointLookUps._MetaName);
		}


		public override void Parse(MetaFile meta, CScenarioPointLookUps CScenarioPointLookUps)
		{
			this.Meta = meta;
			this.MetaStructure = CScenarioPointLookUps;

			// this.TypeNames = CScenarioPointLookUps.TypeNames;
			// this.PedModelSetNames = CScenarioPointLookUps.PedModelSetNames;
			// this.VehicleModelSetNames = CScenarioPointLookUps.VehicleModelSetNames;
			// this.GroupNames = CScenarioPointLookUps.GroupNames;
			// this.InteriorNames = CScenarioPointLookUps.InteriorNames;
			// this.RequiredIMapNames = CScenarioPointLookUps.RequiredIMapNames;
		}

		public override void Build(MetaBuilder mb, bool isRoot = false)
		{
			// this.MetaStructure.TypeNames = this.TypeNames;
			// this.MetaStructure.PedModelSetNames = this.PedModelSetNames;
			// this.MetaStructure.VehicleModelSetNames = this.VehicleModelSetNames;
			// this.MetaStructure.GroupNames = this.GroupNames;
			// this.MetaStructure.InteriorNames = this.InteriorNames;
			// this.MetaStructure.RequiredIMapNames = this.RequiredIMapNames;

 			MCScenarioPointLookUps.AddEnumAndStructureInfo(mb);          

			if(isRoot)
			{
				mb.AddItem(this.MetaName, this.MetaStructure);

				this.Meta = mb.GetMeta();
			}
		}
	}
}
