using System.Collections.Generic;
using System.Linq;
using SharpDX;
using RageLib.Resources.GTA5.PC.Meta;

namespace RageLib.GTA5.ResourceWrappers.PC.Meta.Structures
{
	public class CMloPortalDef : MetaStructureWrapper<RageLib.Resources.GTA5.PC.Meta.CMloPortalDef>
	{
		public MetaFile Meta;
		public uint RoomFrom;
		public uint RoomTo;
		public uint Flags;
		public uint MirrorPriority;
		public uint Opacity;
		public uint AudioOcclusion;
		public List<Vector4> Corners = new List<Vector4>();
		public List<uint> attachedObjects = new List<uint>();

		public CMloPortalDef()
		{
			this.MetaName = MetaName.CMloPortalDef;
			this.MetaStructure = new RageLib.Resources.GTA5.PC.Meta.CMloPortalDef();
		}

		public override void Parse(MetaFile meta, RageLib.Resources.GTA5.PC.Meta.CMloPortalDef CMloPortalDef)
		{
			this.Meta = meta;
			this.MetaStructure = CMloPortalDef;

			this.RoomFrom = CMloPortalDef.roomFrom;
			this.RoomTo = CMloPortalDef.roomTo;
			this.Flags = CMloPortalDef.flags;
			this.MirrorPriority = CMloPortalDef.mirrorPriority;
			this.Opacity = CMloPortalDef.opacity;
			this.AudioOcclusion = CMloPortalDef.audioOcclusion;
            this.Corners = MetaUtils.ConvertDataArray<Vector4>(meta, CMloPortalDef.corners.Pointer, CMloPortalDef.corners.Count1).ToList();
            this.attachedObjects = MetaUtils.ConvertDataArray<uint>(meta, CMloPortalDef.attachedObjects.Pointer, CMloPortalDef.attachedObjects.Count1).ToList(); ;
		}

		public override void Build(MetaBuilder mb, bool isRoot = false)
		{
			this.MetaStructure.roomFrom = this.RoomFrom;
			this.MetaStructure.roomTo = this.RoomTo;
			this.MetaStructure.flags = this.Flags;
			this.MetaStructure.mirrorPriority = this.MirrorPriority;
			this.MetaStructure.opacity = this.Opacity;
			this.MetaStructure.audioOcclusion = this.AudioOcclusion;
            this.MetaStructure.corners = mb.AddPaddedVector3ArrayPtr(this.Corners.ToArray());
			this.MetaStructure.attachedObjects = mb.AddUintArrayPtr(this.attachedObjects.ToArray());

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
