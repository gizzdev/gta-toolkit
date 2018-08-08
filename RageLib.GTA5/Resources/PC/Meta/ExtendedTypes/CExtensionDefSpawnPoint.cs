using System.Collections.Generic;
using System.Linq;

using SharpDX;

namespace RageLib.Resources.GTA5.PC.Meta.ExtendedTypes
{
	public class CExtensionDefSpawnPoint : MetaStructureWrapper<PC.Meta.CExtensionDefSpawnPoint>
	{
		public MetaFile Meta;
		public uint Name;
		public Vector3 OffsetPosition;
		public Vector4 OffsetRotation;
		public uint SpawnType;
		public uint PedType;
		public uint Group;
		public uint Interior;
		public uint RequiredImap;
		public Unk_3573596290 AvailableInMpSp;
		public float Probability;
		public float TimeTillPedLeaves;
		public float Radius;
		public byte Start;
		public byte End;
		public Unk_700327466 Flags;
		public bool HighPri;
		public bool ExtendedRange;
		public bool ShortRange;

		public CExtensionDefSpawnPoint(MetaName metaName) : base(metaName)
		{
			this.MetaStructure = new PC.Meta.CExtensionDefSpawnPoint();
		}

		public void Parse(MetaFile meta, PC.Meta.CExtensionDefSpawnPoint CExtensionDefSpawnPoint)
		{
			this.Meta = meta;
			this.MetaStructure = CExtensionDefSpawnPoint;

			this.Name = CExtensionDefSpawnPoint.name;
			this.OffsetPosition = CExtensionDefSpawnPoint.offsetPosition;
			this.OffsetRotation = CExtensionDefSpawnPoint.offsetRotation;
			this.SpawnType = CExtensionDefSpawnPoint.spawnType;
			this.PedType = CExtensionDefSpawnPoint.pedType;
			this.Group = CExtensionDefSpawnPoint.group;
			this.Interior = CExtensionDefSpawnPoint.interior;
			this.RequiredImap = CExtensionDefSpawnPoint.requiredImap;
			this.AvailableInMpSp = CExtensionDefSpawnPoint.availableInMpSp;
			this.Probability = CExtensionDefSpawnPoint.probability;
			this.TimeTillPedLeaves = CExtensionDefSpawnPoint.timeTillPedLeaves;
			this.Radius = CExtensionDefSpawnPoint.radius;
			this.Start = CExtensionDefSpawnPoint.start;
			this.End = CExtensionDefSpawnPoint.end;
			this.Flags = CExtensionDefSpawnPoint.flags;
			this.HighPri = CExtensionDefSpawnPoint.highPri;
			this.ExtendedRange = CExtensionDefSpawnPoint.extendedRange;
			this.ShortRange = CExtensionDefSpawnPoint.shortRange;
		}

		public void Build(MetaBuilder mb, bool isRoot = false)
		{
			this.MetaStructure.name = this.Name;
			this.MetaStructure.offsetPosition = this.OffsetPosition;
			this.MetaStructure.offsetRotation = this.OffsetRotation;
			this.MetaStructure.spawnType = this.SpawnType;
			this.MetaStructure.pedType = this.PedType;
			this.MetaStructure.group = this.Group;
			this.MetaStructure.interior = this.Interior;
			this.MetaStructure.requiredImap = this.RequiredImap;
			this.MetaStructure.availableInMpSp = this.AvailableInMpSp;
			this.MetaStructure.probability = this.Probability;
			this.MetaStructure.timeTillPedLeaves = this.TimeTillPedLeaves;
			this.MetaStructure.radius = this.Radius;
			this.MetaStructure.start = this.Start;
			this.MetaStructure.end = this.End;
			this.MetaStructure.flags = this.Flags;
			this.MetaStructure.highPri = this.HighPri;
			this.MetaStructure.extendedRange = this.ExtendedRange;
			this.MetaStructure.shortRange = this.ShortRange;

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
