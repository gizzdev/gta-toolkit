using System.Collections.Generic;
using System.Linq;
using SharpDX;
using RageLib.Resources.GTA5.PC.Meta;

namespace RageLib.GTA5.ResourceWrappers.PC.Meta.Structures
{
	public class CMloTimeCycleModifier : MetaStructureWrapper<RageLib.Resources.GTA5.PC.Meta.CMloTimeCycleModifier>
	{
		public MetaFile Meta;
		public uint Name;
		public Vector4 Sphere;
		public float Percentage;
		public float Range;
		public uint StartHour;
		public uint EndHour;

		public CMloTimeCycleModifier()
		{
			this.MetaName = MetaName.CMloTimeCycleModifier;
			this.MetaStructure = new RageLib.Resources.GTA5.PC.Meta.CMloTimeCycleModifier();
		}

		public override void Parse(MetaFile meta, RageLib.Resources.GTA5.PC.Meta.CMloTimeCycleModifier CMloTimeCycleModifier)
		{
			this.Meta = meta;
			this.MetaStructure = CMloTimeCycleModifier;

			this.Name = CMloTimeCycleModifier.name;
			this.Sphere = CMloTimeCycleModifier.sphere;
			this.Percentage = CMloTimeCycleModifier.percentage;
			this.Range = CMloTimeCycleModifier.range;
			this.StartHour = CMloTimeCycleModifier.startHour;
			this.EndHour = CMloTimeCycleModifier.endHour;
		}

		public override void Build(MetaBuilder mb, bool isRoot = false)
		{
			this.MetaStructure.name = this.Name;
			this.MetaStructure.sphere = this.Sphere;
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
