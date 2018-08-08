using System.Collections.Generic;
using System.Linq;

using SharpDX;

namespace RageLib.Resources.GTA5.PC.Meta.ExtendedTypes
{
	public class CMloPortalDef : MetaStructureWrapper<PC.Meta.CMloPortalDef>
	{
		public MetaFile Meta;
		public uint RoomFrom;
		public uint RoomTo;
		public uint Flags;
		public uint MirrorPriority;
		public uint Opacity;
		public uint AudioOcclusion;
		public Array_Vector3 Corners;
		public Array_uint Unk_2382704940;

		public CMloPortalDef(MetaName metaName) : base(metaName)
		{
			this.MetaStructure = new PC.Meta.CMloPortalDef();
		}

		public void Parse(MetaFile meta, PC.Meta.CMloPortalDef CMloPortalDef)
		{
			this.Meta = meta;
			this.MetaStructure = CMloPortalDef;

			this.RoomFrom = CMloPortalDef.roomFrom;
			this.RoomTo = CMloPortalDef.roomTo;
			this.Flags = CMloPortalDef.flags;
			this.MirrorPriority = CMloPortalDef.mirrorPriority;
			this.Opacity = CMloPortalDef.opacity;
			this.AudioOcclusion = CMloPortalDef.audioOcclusion;
			// this.Corners = CMloPortalDef.corners;
			// this.Unk_2382704940 = CMloPortalDef.Unk_2382704940;
		}

		public void Build(MetaBuilder mb, bool isRoot = false)
		{
			this.MetaStructure.roomFrom = this.RoomFrom;
			this.MetaStructure.roomTo = this.RoomTo;
			this.MetaStructure.flags = this.Flags;
			this.MetaStructure.mirrorPriority = this.MirrorPriority;
			this.MetaStructure.opacity = this.Opacity;
			this.MetaStructure.audioOcclusion = this.AudioOcclusion;
			// this.MetaStructure.corners = this.Corners;
			// this.MetaStructure.Unk_2382704940 = this.Unk_2382704940;

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
