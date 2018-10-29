using System.Collections.Generic;
using System.Linq;
using SharpDX;
using RageLib.Resources.GTA5.PC.Meta;

namespace RageLib.GTA5.ResourceWrappers.PC.Meta.Structures
{
	public class MCExtensionDefExplosionEffect : MetaStructureWrapper<CExtensionDefExplosionEffect>
	{
		public static MetaName _MetaName = MetaName.CExtensionDefExplosionEffect;
		public MetaFile Meta;
		public uint Name;
		public Vector3 OffsetPosition;
		public Vector4 OffsetRotation;
		public string ExplosionName = "";
		public int BoneTag;
		public int ExplosionTag;
		public int ExplosionType;
		public uint Flags;

		public MCExtensionDefExplosionEffect()
		{
			this.MetaName = MetaName.CExtensionDefExplosionEffect;
			this.MetaStructure = new CExtensionDefExplosionEffect();
		}

		public static void AddEnumAndStructureInfo(MetaBuilder mb)
		{
			var enumInfos = MetaInfo.GetStructureEnumInfo(MCExtensionDefExplosionEffect._MetaName);

			for (int i = 0; i < enumInfos.Length; i++)
				mb.AddEnumInfo((MetaName) enumInfos[i].EnumNameHash);

			mb.AddStructureInfo(MCExtensionDefExplosionEffect._MetaName);
		}


		public override void Parse(MetaFile meta, CExtensionDefExplosionEffect CExtensionDefExplosionEffect)
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

		public override void Build(MetaBuilder mb, bool isRoot = false)
		{
			this.MetaStructure.name = this.Name;
			this.MetaStructure.offsetPosition = this.OffsetPosition;
			this.MetaStructure.offsetRotation = this.OffsetRotation;
			this.MetaStructure.explosionName = mb.AddStringPtr(this.ExplosionName);
			this.MetaStructure.boneTag = this.BoneTag;
			this.MetaStructure.explosionTag = this.ExplosionTag;
			this.MetaStructure.explosionType = this.ExplosionType;
			this.MetaStructure.flags = this.Flags;

 			MCExtensionDefExplosionEffect.AddEnumAndStructureInfo(mb);          

			if(isRoot)
			{
				mb.AddItem(this.MetaName, this.MetaStructure);

				this.Meta = mb.GetMeta();
			}
		}
	}
}
