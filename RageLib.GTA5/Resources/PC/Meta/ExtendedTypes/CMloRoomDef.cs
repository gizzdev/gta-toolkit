using System.Collections.Generic;
using System.Linq;

using SharpDX;

namespace RageLib.Resources.GTA5.PC.Meta.ExtendedTypes
{
	public class CMloRoomDef : MetaStructureWrapper<PC.Meta.CMloRoomDef>
	{
		public MetaFile Meta;
		public string Name = "";
		public Vector3 BbMin;
		public Vector3 BbMax;
		public float Blend;
		public uint TimecycleName;
		public uint SecondaryTimecycleName;
		public uint Flags;
		public uint PortalCount;
		public int FloorId;
		public int ExteriorVisibiltyDepth;
		public Array_uint Unk_2382704940;

		public CMloRoomDef(MetaName metaName) : base(metaName)
		{
			this.MetaStructure = new PC.Meta.CMloRoomDef();
		}

		public void Parse(MetaFile meta, PC.Meta.CMloRoomDef CMloRoomDef)
		{
			this.Meta = meta;
			this.MetaStructure = CMloRoomDef;

			this.Name = MetaUtils.GetString(Meta, CMloRoomDef.name);
			this.BbMin = CMloRoomDef.bbMin;
			this.BbMax = CMloRoomDef.bbMax;
			this.Blend = CMloRoomDef.blend;
			this.TimecycleName = CMloRoomDef.timecycleName;
			this.SecondaryTimecycleName = CMloRoomDef.secondaryTimecycleName;
			this.Flags = CMloRoomDef.flags;
			this.PortalCount = CMloRoomDef.portalCount;
			this.FloorId = CMloRoomDef.floorId;
			this.ExteriorVisibiltyDepth = CMloRoomDef.exteriorVisibiltyDepth;
			// this.Unk_2382704940 = CMloRoomDef.Unk_2382704940;
		}

		public void Build(MetaBuilder mb, bool isRoot = false)
		{
			this.MetaStructure.name = mb.AddStringPtr(this.Name);
			this.MetaStructure.bbMin = this.BbMin;
			this.MetaStructure.bbMax = this.BbMax;
			this.MetaStructure.blend = this.Blend;
			this.MetaStructure.timecycleName = this.TimecycleName;
			this.MetaStructure.secondaryTimecycleName = this.SecondaryTimecycleName;
			this.MetaStructure.flags = this.Flags;
			this.MetaStructure.portalCount = this.PortalCount;
			this.MetaStructure.floorId = this.FloorId;
			this.MetaStructure.exteriorVisibiltyDepth = this.ExteriorVisibiltyDepth;
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
