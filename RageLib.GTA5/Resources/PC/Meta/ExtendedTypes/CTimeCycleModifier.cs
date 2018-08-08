using System.Collections.Generic;
using System.Linq;

using SharpDX;

namespace RageLib.Resources.GTA5.PC.Meta.ExtendedTypes
{
	public class CTimeCycleModifier : MetaStructureWrapper<PC.Meta.CTimeCycleModifier>
	{
		public MetaFile Meta;
		public uint Name;
		public Vector3 MinExtents;
		public Vector3 MaxExtents;
		public float Percentage;
		public float Range;
		public uint StartHour;
		public uint EndHour;

		public CTimeCycleModifier(MetaName metaName) : base(metaName)
		{
			this.MetaStructure = new PC.Meta.CTimeCycleModifier();
		}

		public void Parse(MetaFile meta, PC.Meta.CTimeCycleModifier CTimeCycleModifier)
		{
			this.Meta = meta;
			this.MetaStructure = CTimeCycleModifier;

			this.Name = CTimeCycleModifier.name;
			this.MinExtents = CTimeCycleModifier.minExtents;
			this.MaxExtents = CTimeCycleModifier.maxExtents;
			this.Percentage = CTimeCycleModifier.percentage;
			this.Range = CTimeCycleModifier.range;
			this.StartHour = CTimeCycleModifier.startHour;
			this.EndHour = CTimeCycleModifier.endHour;
		}

		public void Build(MetaBuilder mb, bool isRoot = false)
		{
			this.MetaStructure.name = this.Name;
			this.MetaStructure.minExtents = this.MinExtents;
			this.MetaStructure.maxExtents = this.MaxExtents;
			this.MetaStructure.percentage = this.Percentage;
			this.MetaStructure.range = this.Range;
			this.MetaStructure.startHour = this.StartHour;
			this.MetaStructure.endHour = this.EndHour;

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
