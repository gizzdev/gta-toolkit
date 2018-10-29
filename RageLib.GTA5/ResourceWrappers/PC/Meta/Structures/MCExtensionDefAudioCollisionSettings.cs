using System.Collections.Generic;
using System.Linq;
using SharpDX;
using RageLib.Resources.GTA5.PC.Meta;

namespace RageLib.GTA5.ResourceWrappers.PC.Meta.Structures
{
	public class MCExtensionDefAudioCollisionSettings : MetaStructureWrapper<CExtensionDefAudioCollisionSettings>
	{
		public static MetaName _MetaName = MetaName.CExtensionDefAudioCollisionSettings;
		public MetaFile Meta;
		public uint Name;
		public Vector3 OffsetPosition;
		public uint Settings;

		public MCExtensionDefAudioCollisionSettings()
		{
			this.MetaName = MetaName.CExtensionDefAudioCollisionSettings;
			this.MetaStructure = new CExtensionDefAudioCollisionSettings();
		}

		public static void AddEnumAndStructureInfo(MetaBuilder mb)
		{
			var enumInfos = MetaInfo.GetStructureEnumInfo(MCExtensionDefAudioCollisionSettings._MetaName);

			for (int i = 0; i < enumInfos.Length; i++)
				mb.AddEnumInfo((MetaName) enumInfos[i].EnumNameHash);

			mb.AddStructureInfo(MCExtensionDefAudioCollisionSettings._MetaName);
		}


		public override void Parse(MetaFile meta, CExtensionDefAudioCollisionSettings CExtensionDefAudioCollisionSettings)
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

 			MCExtensionDefAudioCollisionSettings.AddEnumAndStructureInfo(mb);          

			if(isRoot)
			{
				mb.AddItem(this.MetaName, this.MetaStructure);

				this.Meta = mb.GetMeta();
			}
		}
	}
}
