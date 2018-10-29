using System.Collections.Generic;
using System.Linq;
using SharpDX;
using RageLib.Resources.GTA5.PC.Meta;

namespace RageLib.GTA5.ResourceWrappers.PC.Meta.Structures
{
	public class MCExtensionDefBuoyancy : MetaStructureWrapper<CExtensionDefBuoyancy>
	{
		public static MetaName _MetaName = MetaName.CExtensionDefBuoyancy;
		public MetaFile Meta;
		public uint Name;
		public Vector3 OffsetPosition;

		public MCExtensionDefBuoyancy()
		{
			this.MetaName = MetaName.CExtensionDefBuoyancy;
			this.MetaStructure = new CExtensionDefBuoyancy();
		}

		public static void AddEnumAndStructureInfo(MetaBuilder mb)
		{
			var enumInfos = MetaInfo.GetStructureEnumInfo(MCExtensionDefBuoyancy._MetaName);

			for (int i = 0; i < enumInfos.Length; i++)
				mb.AddEnumInfo((MetaName) enumInfos[i].EnumNameHash);

			mb.AddStructureInfo(MCExtensionDefBuoyancy._MetaName);
		}


		public override void Parse(MetaFile meta, CExtensionDefBuoyancy CExtensionDefBuoyancy)
		{
			this.Meta = meta;
			this.MetaStructure = CExtensionDefBuoyancy;

			this.Name = CExtensionDefBuoyancy.name;
			this.OffsetPosition = CExtensionDefBuoyancy.offsetPosition;
		}

		public override void Build(MetaBuilder mb, bool isRoot = false)
		{
			this.MetaStructure.name = this.Name;
			this.MetaStructure.offsetPosition = this.OffsetPosition;

 			MCExtensionDefBuoyancy.AddEnumAndStructureInfo(mb);                    

			if(isRoot)
			{
				mb.AddItem(this.MetaName, this.MetaStructure);

				this.Meta = mb.GetMeta();
			}
		}
	}
}
