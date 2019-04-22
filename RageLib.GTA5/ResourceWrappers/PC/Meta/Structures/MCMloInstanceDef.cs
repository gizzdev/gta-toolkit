using System.Collections.Generic;
using System.Linq;
using SharpDX;
using RageLib.Resources.GTA5.PC.Meta;

namespace RageLib.GTA5.ResourceWrappers.PC.Meta.Structures
{
	public class MCMloInstanceDef : MetaStructureWrapper<CMloInstanceDef>
	{
		public static MetaName _MetaName = MetaName.CMloInstanceDef;
		public MetaFile Meta;
		public uint ArchetypeName;
		public uint Flags;
		public uint Guid;
		public Vector3 Position;
		public Vector4 Rotation;
		public float ScaleXY;
		public float ScaleZ;
		public int ParentIndex;
		public float LodDist;
		public float ChildLodDist;
		public Unk_1264241711 LodLevel;
		public uint NumChildren;
		public Unk_648413703 PriorityLevel;
		public Array_StructurePointer Extensions;
		public int AmbientOcclusionMultiplier;
		public int ArtificialAmbientOcclusion;
		public uint TintValue;
		public uint GroupId;
		public uint FloorId;
		public Array_uint DefaultEntitySets;
		public uint NumExitPortals;
		public uint MLOInstflags;

		public MCMloInstanceDef()
		{
			this.MetaName = MetaName.CMloInstanceDef;
			this.MetaStructure = new CMloInstanceDef();
		}

		public static void AddEnumAndStructureInfo(MetaBuilder mb)
		{
			var enumInfos = MetaInfo.GetStructureEnumInfo(MCMloInstanceDef._MetaName);

			for (int i = 0; i < enumInfos.Length; i++)
				mb.AddEnumInfo((MetaName) enumInfos[i].EnumNameHash);

			mb.AddStructureInfo(MCMloInstanceDef._MetaName);
		}


		public override void Parse(MetaFile meta, CMloInstanceDef CMloInstanceDef)
		{
			this.Meta = meta;
			this.MetaStructure = CMloInstanceDef;

			this.ArchetypeName = CMloInstanceDef.archetypeName;
			this.Flags = CMloInstanceDef.flags;
			this.Guid = CMloInstanceDef.guid;
			this.Position = CMloInstanceDef.position;
			this.Rotation = CMloInstanceDef.rotation;
			this.ScaleXY = CMloInstanceDef.scaleXY;
			this.ScaleZ = CMloInstanceDef.scaleZ;
			this.ParentIndex = CMloInstanceDef.parentIndex;
			this.LodDist = CMloInstanceDef.lodDist;
			this.ChildLodDist = CMloInstanceDef.childLodDist;
			this.LodLevel = CMloInstanceDef.lodLevel;
			this.NumChildren = CMloInstanceDef.numChildren;
			this.PriorityLevel = CMloInstanceDef.priorityLevel;
			this.Extensions = new Array_StructurePointer();
			this.AmbientOcclusionMultiplier = CMloInstanceDef.ambientOcclusionMultiplier;
			this.ArtificialAmbientOcclusion = CMloInstanceDef.artificialAmbientOcclusion;
			this.TintValue = CMloInstanceDef.tintValue;
			this.GroupId = CMloInstanceDef.groupId;
			this.FloorId = CMloInstanceDef.floorId;
			this.DefaultEntitySets = CMloInstanceDef.defaultEntitySets;
			this.NumExitPortals = CMloInstanceDef.numExitPortals;
			this.MLOInstflags = CMloInstanceDef.MLOInstflags;
		}

		public override void Build(MetaBuilder mb, bool isRoot = false)
		{
			this.MetaStructure.archetypeName = this.ArchetypeName;
			this.MetaStructure.flags = this.Flags;
			this.MetaStructure.guid = this.Guid;
			this.MetaStructure.position = this.Position;
			this.MetaStructure.rotation = this.Rotation;
			this.MetaStructure.scaleXY = this.ScaleXY;
			this.MetaStructure.scaleZ = this.ScaleZ;
			this.MetaStructure.parentIndex = this.ParentIndex;
			this.MetaStructure.lodDist = this.LodDist;
			this.MetaStructure.childLodDist = this.ChildLodDist;
			this.MetaStructure.lodLevel = this.LodLevel;
			this.MetaStructure.numChildren = this.NumChildren;
			this.MetaStructure.priorityLevel = this.PriorityLevel;
			this.MetaStructure.extensions = this.Extensions;
			this.MetaStructure.ambientOcclusionMultiplier = this.AmbientOcclusionMultiplier;
			this.MetaStructure.artificialAmbientOcclusion = this.ArtificialAmbientOcclusion;
			this.MetaStructure.tintValue = this.TintValue;
			this.MetaStructure.groupId = this.GroupId;
			this.MetaStructure.floorId = this.FloorId;
			this.MetaStructure.defaultEntitySets = this.DefaultEntitySets;
			this.MetaStructure.numExitPortals = this.NumExitPortals;
			this.MetaStructure.MLOInstflags = this.MLOInstflags;

 			MCMloInstanceDef.AddEnumAndStructureInfo(mb);          

			if(isRoot)
			{
				mb.AddItem(this.MetaName, this.MetaStructure);

				this.Meta = mb.GetMeta();
			}
		}
	}
}
