using System.Collections.Generic;
using System.Linq;
using SharpDX;
using RageLib.Resources.GTA5.PC.Meta;

namespace RageLib.GTA5.ResourceWrappers.PC.Meta.Structures
{
	public class MCExtensionDefDoor : MetaStructureWrapper<CExtensionDefDoor>
	{
		public static MetaName _MetaName = MetaName.CExtensionDefDoor;
		public MetaFile Meta;
		public uint Name;
		public Vector3 OffsetPosition;
		public byte EnableLimitAngle;
		public byte StartsLocked;
		public byte CanBreak;
		public float LimitAngle;
		public float DoorTargetRatio;
		public uint AudioHash;

		public MCExtensionDefDoor()
		{
			this.MetaName = MetaName.CExtensionDefDoor;
			this.MetaStructure = new CExtensionDefDoor();
		}

		public static void AddEnumAndStructureInfo(MetaBuilder mb)
		{
			var enumInfos = MetaInfo.GetStructureEnumInfo(MCExtensionDefDoor._MetaName);

			for (int i = 0; i < enumInfos.Length; i++)
				mb.AddEnumInfo((MetaName) enumInfos[i].EnumNameHash);

			mb.AddStructureInfo(MCExtensionDefDoor._MetaName);
		}


		public override void Parse(MetaFile meta, CExtensionDefDoor CExtensionDefDoor)
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

 			MCExtensionDefDoor.AddEnumAndStructureInfo(mb);          

			if(isRoot)
			{
				mb.AddItem(this.MetaName, this.MetaStructure);

				this.Meta = mb.GetMeta();
			}
		}
	}
}
