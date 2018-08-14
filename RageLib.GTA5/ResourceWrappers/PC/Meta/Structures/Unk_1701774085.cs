using System.Collections.Generic;
using System.Linq;
using SharpDX;
using RageLib.Resources.GTA5.PC.Meta;

namespace RageLib.GTA5.ResourceWrappers.PC.Meta.Structures
{
	public class Unk_1701774085 : MetaStructureWrapper<RageLib.Resources.GTA5.PC.Meta.Unk_1701774085>
	{
		public MetaFile Meta;
		public string OwnerName = "";
		public Vector4 Rotation;
		public Vector3 Position;
		public Vector3 Normal;
		public float CapsuleRadius;
		public float CapsuleLen;
		public float CapsuleHalfHeight;
		public float CapsuleHalfWidth;
		public Unk_3044470860 Flags;

		public Unk_1701774085()
		{
			this.MetaName = (MetaName) (1701774085);
			this.MetaStructure = new RageLib.Resources.GTA5.PC.Meta.Unk_1701774085();
		}

		public override void Parse(MetaFile meta, RageLib.Resources.GTA5.PC.Meta.Unk_1701774085 Unk_1701774085)
		{
			this.Meta = meta;
			this.MetaStructure = Unk_1701774085;

			this.OwnerName = MetaUtils.GetString(Meta, Unk_1701774085.OwnerName);
			this.Rotation = Unk_1701774085.Rotation;
			this.Position = Unk_1701774085.Position;
			this.Normal = Unk_1701774085.Normal;
			this.CapsuleRadius = Unk_1701774085.CapsuleRadius;
			this.CapsuleLen = Unk_1701774085.CapsuleLen;
			this.CapsuleHalfHeight = Unk_1701774085.CapsuleHalfHeight;
			this.CapsuleHalfWidth = Unk_1701774085.CapsuleHalfWidth;
			this.Flags = Unk_1701774085.Flags;
		}

		public override void Build(MetaBuilder mb, bool isRoot = false)
		{
			this.MetaStructure.OwnerName = mb.AddStringPtr(this.OwnerName);
			this.MetaStructure.Rotation = this.Rotation;
			this.MetaStructure.Position = this.Position;
			this.MetaStructure.Normal = this.Normal;
			this.MetaStructure.CapsuleRadius = this.CapsuleRadius;
			this.MetaStructure.CapsuleLen = this.CapsuleLen;
			this.MetaStructure.CapsuleHalfHeight = this.CapsuleHalfHeight;
			this.MetaStructure.CapsuleHalfWidth = this.CapsuleHalfWidth;
			this.MetaStructure.Flags = this.Flags;

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
