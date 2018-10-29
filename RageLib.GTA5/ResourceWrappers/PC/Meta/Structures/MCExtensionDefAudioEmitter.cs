using System.Collections.Generic;
using System.Linq;
using SharpDX;
using RageLib.Resources.GTA5.PC.Meta;

namespace RageLib.GTA5.ResourceWrappers.PC.Meta.Structures
{
	public class MCExtensionDefAudioEmitter : MetaStructureWrapper<CExtensionDefAudioEmitter>
	{
		public static MetaName _MetaName = MetaName.CExtensionDefAudioEmitter;
		public MetaFile Meta;
		public uint Name;
		public Vector3 OffsetPosition;
		public Vector4 OffsetRotation;
		public uint EffectHash;

		public MCExtensionDefAudioEmitter()
		{
			this.MetaName = MetaName.CExtensionDefAudioEmitter;
			this.MetaStructure = new CExtensionDefAudioEmitter();
		}

		public static void AddEnumAndStructureInfo(MetaBuilder mb)
		{
			var enumInfos = MetaInfo.GetStructureEnumInfo(MCExtensionDefAudioEmitter._MetaName);

			for (int i = 0; i < enumInfos.Length; i++)
				mb.AddEnumInfo((MetaName) enumInfos[i].EnumNameHash);

			mb.AddStructureInfo(MCExtensionDefAudioEmitter._MetaName);
		}


		public override void Parse(MetaFile meta, CExtensionDefAudioEmitter CExtensionDefAudioEmitter)
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

 			MCExtensionDefAudioEmitter.AddEnumAndStructureInfo(mb);          

			if(isRoot)
			{
				mb.AddItem(this.MetaName, this.MetaStructure);

				this.Meta = mb.GetMeta();
			}
		}
	}
}
