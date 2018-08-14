using System.Collections.Generic;
using System.Linq;
using SharpDX;
using RageLib.Resources.GTA5.PC.Meta;

namespace RageLib.GTA5.ResourceWrappers.PC.Meta.Structures
{
	public class CExtensionDefAudioCollisionSettings : MetaStructureWrapper<RageLib.Resources.GTA5.PC.Meta.CExtensionDefAudioCollisionSettings>
	{
		public MetaFile Meta;
		public uint Name;
		public Vector3 OffsetPosition;
		public uint Settings;

		public CExtensionDefAudioCollisionSettings()
		{
			this.MetaName = MetaName.CExtensionDefAudioCollisionSettings;
			this.MetaStructure = new RageLib.Resources.GTA5.PC.Meta.CExtensionDefAudioCollisionSettings();
		}

		public override void Parse(MetaFile meta, RageLib.Resources.GTA5.PC.Meta.CExtensionDefAudioCollisionSettings CExtensionDefAudioCollisionSettings)
		{
			this.Meta = meta;
			this.MetaStructure = CExtensionDefAudioCollisionSettings;

			this.Name = CExtensionDefAudioCollisionSettings.name;
			this.OffsetPosition = CExtensionDefAudioCollisionSettings.offsetPosition;
			this.Settings = CExtensionDefAudioCollisionSettings.settings;
		}

		public override void Build(MetaBuilder mb, bool isRoot = false)
		{
			this.MetaStructure.name = this.Name;
			this.MetaStructure.offsetPosition = this.OffsetPosition;
			this.MetaStructure.settings = this.Settings;

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
