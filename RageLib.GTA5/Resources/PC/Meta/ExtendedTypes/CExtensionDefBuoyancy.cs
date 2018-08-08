using System.Collections.Generic;
using System.Linq;

using SharpDX;

namespace RageLib.Resources.GTA5.PC.Meta.ExtendedTypes
{
	public class CExtensionDefBuoyancy : MetaStructureWrapper<PC.Meta.CExtensionDefBuoyancy>
	{
		public MetaFile Meta;
		public uint Name;
		public Vector3 OffsetPosition;

		public CExtensionDefBuoyancy(MetaName metaName) : base(metaName)
		{
			this.MetaStructure = new PC.Meta.CExtensionDefBuoyancy();
		}

		public void Parse(MetaFile meta, PC.Meta.CExtensionDefBuoyancy CExtensionDefBuoyancy)
		{
			this.Meta = meta;
			this.MetaStructure = CExtensionDefBuoyancy;

			this.Name = CExtensionDefBuoyancy.name;
			this.OffsetPosition = CExtensionDefBuoyancy.offsetPosition;
		}

		public void Build(MetaBuilder mb, bool isRoot = false)
		{
			this.MetaStructure.name = this.Name;
			this.MetaStructure.offsetPosition = this.OffsetPosition;

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
