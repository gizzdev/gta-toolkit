using System.Collections.Generic;
using System.Linq;

using SharpDX;

namespace RageLib.Resources.GTA5.PC.Meta.ExtendedTypes
{
	public class CExtensionDefExplosionEffect : MetaStructureWrapper<PC.Meta.CExtensionDefExplosionEffect>
	{
		public MetaFile Meta;
		public uint Name;
		public Vector3 OffsetPosition;
		public Vector4 OffsetRotation;
		public string ExplosionName = "";
		public int BoneTag;
		public int ExplosionTag;
		public int ExplosionType;
		public uint Flags;

		public CExtensionDefExplosionEffect(MetaName metaName) : base(metaName)
		{
			this.MetaStructure = new PC.Meta.CExtensionDefExplosionEffect();
		}

		public void Parse(MetaFile meta, PC.Meta.CExtensionDefExplosionEffect CExtensionDefExplosionEffect)
		{
			this.Meta = meta;
			this.MetaStructure = CExtensionDefExplosionEffect;

			this.Name = CExtensionDefExplosionEffect.name;
			this.OffsetPosition = CExtensionDefExplosionEffect.offsetPosition;
			this.OffsetRotation = CExtensionDefExplosionEffect.offsetRotation;
			this.ExplosionName = MetaUtils.GetString(Meta, CExtensionDefExplosionEffect.explosionName);
			this.BoneTag = CExtensionDefExplosionEffect.boneTag;
			this.ExplosionTag = CExtensionDefExplosionEffect.explosionTag;
			this.ExplosionType = CExtensionDefExplosionEffect.explosionType;
			this.Flags = CExtensionDefExplosionEffect.flags;
		}

		public void Build(MetaBuilder mb, bool isRoot = false)
		{
			this.MetaStructure.name = this.Name;
			this.MetaStructure.offsetPosition = this.OffsetPosition;
			this.MetaStructure.offsetRotation = this.OffsetRotation;
			this.MetaStructure.explosionName = mb.AddStringPtr(this.ExplosionName);
			this.MetaStructure.boneTag = this.BoneTag;
			this.MetaStructure.explosionTag = this.ExplosionTag;
			this.MetaStructure.explosionType = this.ExplosionType;
			this.MetaStructure.flags = this.Flags;

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
