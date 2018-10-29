using System.Collections.Generic;
using System.Linq;
using SharpDX;
using RageLib.Resources.GTA5.PC.Meta;

namespace RageLib.GTA5.ResourceWrappers.PC.Meta.Structures
{
	public class MCExtensionDefParticleEffect : MetaStructureWrapper<CExtensionDefParticleEffect>
	{
		public static MetaName _MetaName = MetaName.CExtensionDefParticleEffect;
		public MetaFile Meta;
		public uint Name;
		public Vector3 OffsetPosition;
		public Vector4 OffsetRotation;
		public string FxName = "";
		public int FxType;
		public int BoneTag;
		public float Scale;
		public int Probability;
		public int Flags;
		public uint Color;

		public MCExtensionDefParticleEffect()
		{
			this.MetaName = MetaName.CExtensionDefParticleEffect;
			this.MetaStructure = new CExtensionDefParticleEffect();
		}

		public static void AddEnumAndStructureInfo(MetaBuilder mb)
		{
			var enumInfos = MetaInfo.GetStructureEnumInfo(MCExtensionDefParticleEffect._MetaName);

			for (int i = 0; i < enumInfos.Length; i++)
				mb.AddEnumInfo((MetaName) enumInfos[i].EnumNameHash);

			mb.AddStructureInfo(MCExtensionDefParticleEffect._MetaName);
		}


		public override void Parse(MetaFile meta, CExtensionDefParticleEffect CExtensionDefParticleEffect)
		{
			this.Meta = meta;
			this.MetaStructure = CExtensionDefParticleEffect;

			this.Name = CExtensionDefParticleEffect.name;
			this.OffsetPosition = CExtensionDefParticleEffect.offsetPosition;
			this.OffsetRotation = CExtensionDefParticleEffect.offsetRotation;
			this.FxName = MetaUtils.GetString(Meta, CExtensionDefParticleEffect.fxName);
			this.FxType = CExtensionDefParticleEffect.fxType;
			this.BoneTag = CExtensionDefParticleEffect.boneTag;
			this.Scale = CExtensionDefParticleEffect.scale;
			this.Probability = CExtensionDefParticleEffect.probability;
			this.Flags = CExtensionDefParticleEffect.flags;
			this.Color = CExtensionDefParticleEffect.color;
		}

		public override void Build(MetaBuilder mb, bool isRoot = false)
		{
			this.MetaStructure.name = this.Name;
			this.MetaStructure.offsetPosition = this.OffsetPosition;
			this.MetaStructure.offsetRotation = this.OffsetRotation;
			this.MetaStructure.fxName = mb.AddStringPtr(this.FxName);
			this.MetaStructure.fxType = this.FxType;
			this.MetaStructure.boneTag = this.BoneTag;
			this.MetaStructure.scale = this.Scale;
			this.MetaStructure.probability = this.Probability;
			this.MetaStructure.flags = this.Flags;
			this.MetaStructure.color = this.Color;

 			MCExtensionDefParticleEffect.AddEnumAndStructureInfo(mb);          

			if(isRoot)
			{
				mb.AddItem(this.MetaName, this.MetaStructure);

				this.Meta = mb.GetMeta();
			}
		}
	}
}
