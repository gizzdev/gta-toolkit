using System.Collections.Generic;
using System.Linq;
using SharpDX;
using RageLib.Resources.GTA5.PC.Meta;

namespace RageLib.GTA5.ResourceWrappers.PC.Meta.Structures
{
	public class CExtensionDefAudioEmitter : MetaStructureWrapper<RageLib.Resources.GTA5.PC.Meta.CExtensionDefAudioEmitter>
	{
		public MetaFile Meta;
		public uint Name;
		public Vector3 OffsetPosition;
		public Vector4 OffsetRotation;
		public uint EffectHash;

		public CExtensionDefAudioEmitter()
		{
			this.MetaName = MetaName.CExtensionDefAudioEmitter;
			this.MetaStructure = new RageLib.Resources.GTA5.PC.Meta.CExtensionDefAudioEmitter();
		}

		public override void Parse(MetaFile meta, RageLib.Resources.GTA5.PC.Meta.CExtensionDefAudioEmitter CExtensionDefAudioEmitter)
		{
			this.Meta = meta;
			this.MetaStructure = CExtensionDefAudioEmitter;

			this.Name = CExtensionDefAudioEmitter.name;
			this.OffsetPosition = CExtensionDefAudioEmitter.offsetPosition;
			this.OffsetRotation = CExtensionDefAudioEmitter.offsetRotation;
			this.EffectHash = CExtensionDefAudioEmitter.effectHash;
		}

		public override void Build(MetaBuilder mb, bool isRoot = false)
		{
			this.MetaStructure.name = this.Name;
			this.MetaStructure.offsetPosition = this.OffsetPosition;
			this.MetaStructure.offsetRotation = this.OffsetRotation;
			this.MetaStructure.effectHash = this.EffectHash;

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
