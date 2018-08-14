using System.Collections.Generic;
using System.Linq;
using SharpDX;
using RageLib.Resources.GTA5.PC.Meta;

namespace RageLib.GTA5.ResourceWrappers.PC.Meta.Structures
{
	public class rage__fwGrassInstanceListDef__InstanceData : MetaStructureWrapper<RageLib.Resources.GTA5.PC.Meta.rage__fwGrassInstanceListDef__InstanceData>
	{
		public MetaFile Meta;
		public ArrayOfBytes3 Position;
		public byte NormalX;
		public byte NormalY;
		public ArrayOfBytes3 Color;
		public byte Scale;
		public byte Ao;
		public ArrayOfBytes3 Pad;

		public rage__fwGrassInstanceListDef__InstanceData()
		{
			this.MetaName = MetaName.rage__fwGrassInstanceListDef__InstanceData;
			this.MetaStructure = new RageLib.Resources.GTA5.PC.Meta.rage__fwGrassInstanceListDef__InstanceData();
		}

		public override void Parse(MetaFile meta, RageLib.Resources.GTA5.PC.Meta.rage__fwGrassInstanceListDef__InstanceData rage__fwGrassInstanceListDef__InstanceData)
		{
			this.Meta = meta;
			this.MetaStructure = rage__fwGrassInstanceListDef__InstanceData;

			this.Position = rage__fwGrassInstanceListDef__InstanceData.Position;
			this.NormalX = rage__fwGrassInstanceListDef__InstanceData.NormalX;
			this.NormalY = rage__fwGrassInstanceListDef__InstanceData.NormalY;
			this.Color = rage__fwGrassInstanceListDef__InstanceData.Color;
			this.Scale = rage__fwGrassInstanceListDef__InstanceData.Scale;
			this.Ao = rage__fwGrassInstanceListDef__InstanceData.Ao;
			this.Pad = rage__fwGrassInstanceListDef__InstanceData.Pad;
		}

		public override void Build(MetaBuilder mb, bool isRoot = false)
		{
			this.MetaStructure.Position = this.Position;
			this.MetaStructure.NormalX = this.NormalX;
			this.MetaStructure.NormalY = this.NormalY;
			this.MetaStructure.Color = this.Color;
			this.MetaStructure.Scale = this.Scale;
			this.MetaStructure.Ao = this.Ao;
			this.MetaStructure.Pad = this.Pad;

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
