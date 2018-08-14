using System.Collections.Generic;
using System.Linq;
using SharpDX;
using RageLib.Resources.GTA5.PC.Meta;

namespace RageLib.GTA5.ResourceWrappers.PC.Meta.Structures
{
	public class CExtensionDefParticleEffect : MetaStructureWrapper<RageLib.Resources.GTA5.PC.Meta.CExtensionDefParticleEffect>
	{
		public MetaFile Meta;
		public MetaName Name;
		public Vector3 OffsetPosition;
		public Vector4 OffsetRotation;
		public string FxName = "";
		public int FxType;
		public int BoneTag;
		public float Scale;
		public int Probability;
		public int Flags;
		public uint Color;

		public CExtensionDefParticleEffect()
		{
			this.MetaName = MetaName.CExtensionDefParticleEffect;
			this.MetaStructure = new RageLib.Resources.GTA5.PC.Meta.CExtensionDefParticleEffect();
		}

		public override void Parse(MetaFile meta, RageLib.Resources.GTA5.PC.Meta.CExtensionDefParticleEffect CExtensionDefParticleEffect)
		{
			this.Meta = meta;
			this.MetaStructure = CExtensionDefParticleEffect;

			this.Name = (MetaName) CExtensionDefParticleEffect.name;
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
			this.MetaStructure.name = (uint) this.Name;
			this.MetaStructure.offsetPosition = this.OffsetPosition;
			this.MetaStructure.offsetRotation = this.OffsetRotation;
			this.MetaStructure.fxName = mb.AddStringPtr(this.FxName);
			this.MetaStructure.fxType = this.FxType;
			this.MetaStructure.boneTag = this.BoneTag;
			this.MetaStructure.scale = this.Scale;
			this.MetaStructure.probability = this.Probability;
			this.MetaStructure.flags = this.Flags;
			this.MetaStructure.color = this.Color;

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
