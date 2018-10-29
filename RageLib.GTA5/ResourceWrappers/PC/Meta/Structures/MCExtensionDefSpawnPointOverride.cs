using System.Collections.Generic;
using System.Linq;
using SharpDX;
using RageLib.Resources.GTA5.PC.Meta;

namespace RageLib.GTA5.ResourceWrappers.PC.Meta.Structures
{
	public class MCExtensionDefSpawnPointOverride : MetaStructureWrapper<CExtensionDefSpawnPointOverride>
	{
		public static MetaName _MetaName = MetaName.CExtensionDefSpawnPointOverride;
		public MetaFile Meta;
		public uint Name;
		public Vector3 OffsetPosition;
		public uint ScenarioType;
		public byte ITimeStartOverride;
		public byte ITimeEndOverride;
		public uint Group;
		public uint ModelSet;
		public Unk_3573596290 AvailabilityInMpSp;
		public Unk_700327466 Flags;
		public float Radius;
		public float TimeTillPedLeaves;

		public MCExtensionDefSpawnPointOverride()
		{
			this.MetaName = MetaName.CExtensionDefSpawnPointOverride;
			this.MetaStructure = new CExtensionDefSpawnPointOverride();
		}

		public static void AddEnumAndStructureInfo(MetaBuilder mb)
		{
			var enumInfos = MetaInfo.GetStructureEnumInfo(MCExtensionDefSpawnPointOverride._MetaName);

			for (int i = 0; i < enumInfos.Length; i++)
				mb.AddEnumInfo((MetaName) enumInfos[i].EnumNameHash);

			mb.AddStructureInfo(MCExtensionDefSpawnPointOverride._MetaName);
		}


		public override void Parse(MetaFile meta, CExtensionDefSpawnPointOverride CExtensionDefSpawnPointOverride)
		{
			this.Meta = meta;
			this.MetaStructure = CExtensionDefSpawnPointOverride;

			this.Name = CExtensionDefSpawnPointOverride.name;
			this.OffsetPosition = CExtensionDefSpawnPointOverride.offsetPosition;
			this.ScenarioType = CExtensionDefSpawnPointOverride.ScenarioType;
			this.ITimeStartOverride = CExtensionDefSpawnPointOverride.iTimeStartOverride;
			this.ITimeEndOverride = CExtensionDefSpawnPointOverride.iTimeEndOverride;
			this.Group = CExtensionDefSpawnPointOverride.Group;
			this.ModelSet = CExtensionDefSpawnPointOverride.ModelSet;
			this.AvailabilityInMpSp = CExtensionDefSpawnPointOverride.AvailabilityInMpSp;
			this.Flags = CExtensionDefSpawnPointOverride.Flags;
			this.Radius = CExtensionDefSpawnPointOverride.Radius;
			this.TimeTillPedLeaves = CExtensionDefSpawnPointOverride.TimeTillPedLeaves;
		}

		public override void Build(MetaBuilder mb, bool isRoot = false)
		{
			this.MetaStructure.name = this.Name;
			this.MetaStructure.offsetPosition = this.OffsetPosition;
			this.MetaStructure.ScenarioType = this.ScenarioType;
			this.MetaStructure.iTimeStartOverride = this.ITimeStartOverride;
			this.MetaStructure.iTimeEndOverride = this.ITimeEndOverride;
			this.MetaStructure.Group = this.Group;
			this.MetaStructure.ModelSet = this.ModelSet;
			this.MetaStructure.AvailabilityInMpSp = this.AvailabilityInMpSp;
			this.MetaStructure.Flags = this.Flags;
			this.MetaStructure.Radius = this.Radius;
			this.MetaStructure.TimeTillPedLeaves = this.TimeTillPedLeaves;

 			MCExtensionDefSpawnPointOverride.AddEnumAndStructureInfo(mb);          

			if(isRoot)
			{
				mb.AddItem(this.MetaName, this.MetaStructure);

				this.Meta = mb.GetMeta();
			}
		}
	}
}
