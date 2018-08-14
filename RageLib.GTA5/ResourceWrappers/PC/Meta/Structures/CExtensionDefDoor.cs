using System.Collections.Generic;
using System.Linq;
using SharpDX;
using RageLib.Resources.GTA5.PC.Meta;

namespace RageLib.GTA5.ResourceWrappers.PC.Meta.Structures
{
	public class CExtensionDefDoor : MetaStructureWrapper<RageLib.Resources.GTA5.PC.Meta.CExtensionDefDoor>
	{
		public MetaFile Meta;
		public uint Name;
		public Vector3 OffsetPosition;
		public bool EnableLimitAngle;
		public bool StartsLocked;
		public bool CanBreak;
		public float LimitAngle;
		public float DoorTargetRatio;
		public uint AudioHash;

		public CExtensionDefDoor()
		{
			this.MetaName = MetaName.CExtensionDefDoor;
			this.MetaStructure = new RageLib.Resources.GTA5.PC.Meta.CExtensionDefDoor();
		}

		public override void Parse(MetaFile meta, RageLib.Resources.GTA5.PC.Meta.CExtensionDefDoor CExtensionDefDoor)
		{
			this.Meta = meta;
			this.MetaStructure = CExtensionDefDoor;

			this.Name = CExtensionDefDoor.name;
			this.OffsetPosition = CExtensionDefDoor.offsetPosition;
			this.EnableLimitAngle = CExtensionDefDoor.enableLimitAngle;
			this.StartsLocked = CExtensionDefDoor.startsLocked;
			this.CanBreak = CExtensionDefDoor.canBreak;
			this.LimitAngle = CExtensionDefDoor.limitAngle;
			this.DoorTargetRatio = CExtensionDefDoor.doorTargetRatio;
			this.AudioHash = CExtensionDefDoor.audioHash;
		}

		public override void Build(MetaBuilder mb, bool isRoot = false)
		{
			this.MetaStructure.name = this.Name;
			this.MetaStructure.offsetPosition = this.OffsetPosition;
			this.MetaStructure.enableLimitAngle = this.EnableLimitAngle;
			this.MetaStructure.startsLocked = this.StartsLocked;
			this.MetaStructure.canBreak = this.CanBreak;
			this.MetaStructure.limitAngle = this.LimitAngle;
			this.MetaStructure.doorTargetRatio = this.DoorTargetRatio;
			this.MetaStructure.audioHash = this.AudioHash;

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
