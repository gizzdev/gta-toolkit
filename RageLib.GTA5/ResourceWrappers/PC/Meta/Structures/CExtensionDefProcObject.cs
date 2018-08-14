using System.Collections.Generic;
using System.Linq;
using SharpDX;
using RageLib.Resources.GTA5.PC.Meta;

namespace RageLib.GTA5.ResourceWrappers.PC.Meta.Structures
{
	public class CExtensionDefProcObject : MetaStructureWrapper<RageLib.Resources.GTA5.PC.Meta.CExtensionDefProcObject>
	{
		public MetaFile Meta;
		public uint Name;
		public Vector3 OffsetPosition;
		public float RadiusInner;
		public float RadiusOuter;
		public float Spacing;
		public float MinScale;
		public float MaxScale;
		public float Unk_3913056845;
		public float Unk_147400493;
		public float Unk_2591582364;
		public float Unk_3889902555;
		public uint ObjectHash;
		public uint Flags;

		public CExtensionDefProcObject()
		{
			this.MetaName = MetaName.CExtensionDefProcObject;
			this.MetaStructure = new RageLib.Resources.GTA5.PC.Meta.CExtensionDefProcObject();
		}

		public override void Parse(MetaFile meta, RageLib.Resources.GTA5.PC.Meta.CExtensionDefProcObject CExtensionDefProcObject)
		{
			this.Meta = meta;
			this.MetaStructure = CExtensionDefProcObject;

			this.Name = CExtensionDefProcObject.name;
			this.OffsetPosition = CExtensionDefProcObject.offsetPosition;
			this.RadiusInner = CExtensionDefProcObject.radiusInner;
			this.RadiusOuter = CExtensionDefProcObject.radiusOuter;
			this.Spacing = CExtensionDefProcObject.spacing;
			this.MinScale = CExtensionDefProcObject.minScale;
			this.MaxScale = CExtensionDefProcObject.maxScale;
			this.Unk_3913056845 = CExtensionDefProcObject.Unk_3913056845;
			this.Unk_147400493 = CExtensionDefProcObject.Unk_147400493;
			this.Unk_2591582364 = CExtensionDefProcObject.Unk_2591582364;
			this.Unk_3889902555 = CExtensionDefProcObject.Unk_3889902555;
			this.ObjectHash = CExtensionDefProcObject.objectHash;
			this.Flags = CExtensionDefProcObject.flags;
		}

		public override void Build(MetaBuilder mb, bool isRoot = false)
		{
			this.MetaStructure.name = this.Name;
			this.MetaStructure.offsetPosition = this.OffsetPosition;
			this.MetaStructure.radiusInner = this.RadiusInner;
			this.MetaStructure.radiusOuter = this.RadiusOuter;
			this.MetaStructure.spacing = this.Spacing;
			this.MetaStructure.minScale = this.MinScale;
			this.MetaStructure.maxScale = this.MaxScale;
			this.MetaStructure.Unk_3913056845 = this.Unk_3913056845;
			this.MetaStructure.Unk_147400493 = this.Unk_147400493;
			this.MetaStructure.Unk_2591582364 = this.Unk_2591582364;
			this.MetaStructure.Unk_3889902555 = this.Unk_3889902555;
			this.MetaStructure.objectHash = this.ObjectHash;
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
