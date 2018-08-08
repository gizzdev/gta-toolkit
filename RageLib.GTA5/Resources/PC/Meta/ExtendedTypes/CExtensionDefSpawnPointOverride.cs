using System.Collections.Generic;
using System.Linq;

using SharpDX;

namespace RageLib.Resources.GTA5.PC.Meta.ExtendedTypes
{
	public class CExtensionDefSpawnPointOverride : MetaStructureWrapper<PC.Meta.CExtensionDefSpawnPointOverride>
	{
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

		public CExtensionDefSpawnPointOverride(MetaName metaName) : base(metaName)
		{
			this.MetaStructure = new PC.Meta.CExtensionDefSpawnPointOverride();
		}

		public void Parse(MetaFile meta, PC.Meta.CExtensionDefSpawnPointOverride CExtensionDefSpawnPointOverride)
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

		public void Build(MetaBuilder mb, bool isRoot = false)
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
