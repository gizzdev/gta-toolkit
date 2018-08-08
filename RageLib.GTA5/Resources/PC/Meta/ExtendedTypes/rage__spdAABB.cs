using System.Collections.Generic;
using System.Linq;

using SharpDX;

namespace RageLib.Resources.GTA5.PC.Meta.ExtendedTypes
{
	public class rage__spdAABB : MetaStructureWrapper<PC.Meta.rage__spdAABB>
	{
		public MetaFile Meta;
		public Vector4 Min;
		public Vector4 Max;

		public rage__spdAABB(MetaName metaName) : base(metaName)
		{
			this.MetaStructure = new PC.Meta.rage__spdAABB();
		}

		public void Parse(MetaFile meta, PC.Meta.rage__spdAABB rage__spdAABB)
		{
			this.Meta = meta;
			this.MetaStructure = rage__spdAABB;

			this.Min = rage__spdAABB.min;
			this.Max = rage__spdAABB.max;
		}

		public void Build(MetaBuilder mb, bool isRoot = false)
		{
			this.MetaStructure.min = this.Min;
			this.MetaStructure.max = this.Max;

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
