using System.Collections.Generic;
using System.Linq;
using SharpDX;
using RageLib.Resources.GTA5.PC.Meta;

namespace RageLib.GTA5.ResourceWrappers.PC.Meta.Structures
{
	public class MCMloPortalDef : MetaStructureWrapper<CMloPortalDef>
	{
		public static MetaName _MetaName = MetaName.CMloPortalDef;
		public MetaFile Meta;
		public uint RoomFrom;
		public uint RoomTo;
		public uint Flags;
		public uint MirrorPriority;
		public uint Opacity;
		public uint AudioOcclusion;
		public List<Vector3> Corners = new List<Vector3>();
        public List<uint> AttachedObjects = new List<uint>();

		public MCMloPortalDef()
		{
			this.MetaName = MetaName.CMloPortalDef;
			this.MetaStructure = new CMloPortalDef();
		}

		public static void AddEnumAndStructureInfo(MetaBuilder mb)
		{
			var enumInfos = MetaInfo.GetStructureEnumInfo(MCMloPortalDef._MetaName);

			for (int i = 0; i < enumInfos.Length; i++)
				mb.AddEnumInfo((MetaName) enumInfos[i].EnumNameHash);

			mb.AddStructureInfo(MCMloPortalDef._MetaName);
		}


		public override void Parse(MetaFile meta, CMloPortalDef CMloPortalDef)
		{
			this.Meta = meta;
			this.MetaStructure = CMloPortalDef;

			this.RoomFrom = CMloPortalDef.roomFrom;
			this.RoomTo = CMloPortalDef.roomTo;
			this.Flags = CMloPortalDef.flags;
			this.MirrorPriority = CMloPortalDef.mirrorPriority;
			this.Opacity = CMloPortalDef.opacity;
			this.AudioOcclusion = CMloPortalDef.audioOcclusion;
            this.Corners = MetaUtils.ConvertDataArray<Vector4>(meta, CMloPortalDef.corners.Pointer, CMloPortalDef.corners.Count1)?.Select(e => (Vector3)e).ToList();
            this.AttachedObjects = MetaUtils.ConvertDataArray<uint>(meta, CMloPortalDef.attachedObjects.Pointer, CMloPortalDef.attachedObjects.Count1)?.ToList();
        }

        public override void Build(MetaBuilder mb, bool isRoot = false)
        {
            this.MetaStructure.roomFrom = this.RoomFrom;
            this.MetaStructure.roomTo = this.RoomTo;
            this.MetaStructure.flags = this.Flags;
            this.MetaStructure.mirrorPriority = this.MirrorPriority;
            this.MetaStructure.opacity = this.Opacity;
            this.MetaStructure.audioOcclusion = this.AudioOcclusion;
            this.MetaStructure.corners = mb.AddPaddedVector3ArrayPtr(this.Corners.Select(e => (Vector4)e).ToArray());
            this.MetaStructure.attachedObjects = mb.AddUintArrayPtr(this.AttachedObjects.ToArray());

            MCMloPortalDef.AddEnumAndStructureInfo(mb);          

			if(isRoot)
			{
				mb.AddItem(this.MetaName, this.MetaStructure);

				this.Meta = mb.GetMeta();
			}
		}
	}
}
