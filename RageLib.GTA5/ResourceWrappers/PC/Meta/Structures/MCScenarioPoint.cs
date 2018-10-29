using System.Collections.Generic;
using System.Linq;
using SharpDX;
using RageLib.Resources.GTA5.PC.Meta;

namespace RageLib.GTA5.ResourceWrappers.PC.Meta.Structures
{
	public class MCScenarioPoint : MetaStructureWrapper<CScenarioPoint>
	{
		public static MetaName _MetaName = MetaName.CScenarioPoint;
		public MetaFile Meta;
		public byte IType;
		public byte ModelSetId;
		public byte IInterior;
		public byte IRequiredIMapId;
		public byte IProbability;
		public byte UAvailableInMpSp;
		public byte ITimeStartOverride;
		public byte ITimeEndOverride;
		public byte IRadius;
		public byte ITimeTillPedLeaves;
		public ushort IScenarioGroup;
		public Unk_700327466 Flags;
		public Vector4 VPositionAndDirection;

		public MCScenarioPoint()
		{
			this.MetaName = MetaName.CScenarioPoint;
			this.MetaStructure = new CScenarioPoint();
		}

		public static void AddEnumAndStructureInfo(MetaBuilder mb)
		{
			var enumInfos = MetaInfo.GetStructureEnumInfo(MCScenarioPoint._MetaName);

			for (int i = 0; i < enumInfos.Length; i++)
				mb.AddEnumInfo((MetaName) enumInfos[i].EnumNameHash);

			mb.AddStructureInfo(MCScenarioPoint._MetaName);
		}


		public override void Parse(MetaFile meta, CScenarioPoint CScenarioPoint)
		{
			this.Meta = meta;
			this.MetaStructure = CScenarioPoint;

			this.IType = CScenarioPoint.iType;
			this.ModelSetId = CScenarioPoint.ModelSetId;
			this.IInterior = CScenarioPoint.iInterior;
			this.IRequiredIMapId = CScenarioPoint.iRequiredIMapId;
			this.IProbability = CScenarioPoint.iProbability;
			this.UAvailableInMpSp = CScenarioPoint.uAvailableInMpSp;
			this.ITimeStartOverride = CScenarioPoint.iTimeStartOverride;
			this.ITimeEndOverride = CScenarioPoint.iTimeEndOverride;
			this.IRadius = CScenarioPoint.iRadius;
			this.ITimeTillPedLeaves = CScenarioPoint.iTimeTillPedLeaves;
			this.IScenarioGroup = CScenarioPoint.iScenarioGroup;
			this.Flags = CScenarioPoint.Flags;
			this.VPositionAndDirection = CScenarioPoint.vPositionAndDirection;
		}

		public override void Build(MetaBuilder mb, bool isRoot = false)
		{
			this.MetaStructure.iType = this.IType;
			this.MetaStructure.ModelSetId = this.ModelSetId;
			this.MetaStructure.iInterior = this.IInterior;
			this.MetaStructure.iRequiredIMapId = this.IRequiredIMapId;
			this.MetaStructure.iProbability = this.IProbability;
			this.MetaStructure.uAvailableInMpSp = this.UAvailableInMpSp;
			this.MetaStructure.iTimeStartOverride = this.ITimeStartOverride;
			this.MetaStructure.iTimeEndOverride = this.ITimeEndOverride;
			this.MetaStructure.iRadius = this.IRadius;
			this.MetaStructure.iTimeTillPedLeaves = this.ITimeTillPedLeaves;
			this.MetaStructure.iScenarioGroup = this.IScenarioGroup;
			this.MetaStructure.Flags = this.Flags;
			this.MetaStructure.vPositionAndDirection = this.VPositionAndDirection;

 			MCScenarioPoint.AddEnumAndStructureInfo(mb);                    

			if(isRoot)
			{
				mb.AddItem(this.MetaName, this.MetaStructure);

				this.Meta = mb.GetMeta();
			}
		}
	}
}
