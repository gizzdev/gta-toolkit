using System.Collections.Generic;
using System.Linq;

using SharpDX;

namespace RageLib.Resources.GTA5.PC.Meta.ExtendedTypes
{
	public class CEntityDef : MetaStructureWrapper<PC.Meta.CEntityDef>
	{
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

		public CEntityDef(MetaName metaName) : base(metaName)
		{
			this.MetaStructure = new PC.Meta.CEntityDef();
		}

		public void Parse(MetaFile meta, PC.Meta.CEntityDef CEntityDef)
		{
			this.Meta = meta;
			this.MetaStructure = CEntityDef;

			this.ArchetypeName = CEntityDef.archetypeName;
			this.Flags = CEntityDef.flags;
			this.Guid = CEntityDef.guid;
			this.Position = CEntityDef.position;
			this.Rotation = CEntityDef.rotation;
			this.ScaleXY = CEntityDef.scaleXY;
			this.ScaleZ = CEntityDef.scaleZ;
			this.ParentIndex = CEntityDef.parentIndex;
			this.LodDist = CEntityDef.lodDist;
			this.ChildLodDist = CEntityDef.childLodDist;
			this.LodLevel = CEntityDef.lodLevel;
			this.NumChildren = CEntityDef.numChildren;
			this.PriorityLevel = CEntityDef.priorityLevel;
			// this.Extensions = CEntityDef.extensions;
			this.AmbientOcclusionMultiplier = CEntityDef.ambientOcclusionMultiplier;
			this.ArtificialAmbientOcclusion = CEntityDef.artificialAmbientOcclusion;
			this.TintValue = CEntityDef.tintValue;
		}

		public void Build(MetaBuilder mb, bool isRoot = false)
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
			// this.MetaStructure.extensions = this.Extensions;
			this.MetaStructure.ambientOcclusionMultiplier = this.AmbientOcclusionMultiplier;
			this.MetaStructure.artificialAmbientOcclusion = this.ArtificialAmbientOcclusion;
			this.MetaStructure.tintValue = this.TintValue;

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
