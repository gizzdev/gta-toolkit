using System.Collections.Generic;
using System.Linq;
using SharpDX;
using RageLib.Resources.GTA5.PC.Meta;

namespace RageLib.GTA5.ResourceWrappers.PC.Meta.Structures
{
	public class CCarGen : MetaStructureWrapper<RageLib.Resources.GTA5.PC.Meta.CCarGen>
	{
		public MetaFile Meta;
		public Vector3 Position;
		public float OrientX;
		public float OrientY;
		public float PerpendicularLength;
		public uint CarModel;
		public uint Flags;
		public int BodyColorRemap1;
		public int BodyColorRemap2;
		public int BodyColorRemap3;
		public int BodyColorRemap4;
		public uint PopGroup;
		public sbyte Livery;

		public CCarGen()
		{
			this.MetaName = MetaName.CCarGen;
			this.MetaStructure = new RageLib.Resources.GTA5.PC.Meta.CCarGen();
		}

		public override void Parse(MetaFile meta, RageLib.Resources.GTA5.PC.Meta.CCarGen CCarGen)
		{
			this.Meta = meta;
			this.MetaStructure = CCarGen;

			this.Position = CCarGen.position;
			this.OrientX = CCarGen.orientX;
			this.OrientY = CCarGen.orientY;
			this.PerpendicularLength = CCarGen.perpendicularLength;
			this.CarModel = CCarGen.carModel;
			this.Flags = CCarGen.flags;
			this.BodyColorRemap1 = CCarGen.bodyColorRemap1;
			this.BodyColorRemap2 = CCarGen.bodyColorRemap2;
			this.BodyColorRemap3 = CCarGen.bodyColorRemap3;
			this.BodyColorRemap4 = CCarGen.bodyColorRemap4;
			this.PopGroup = CCarGen.popGroup;
			this.Livery = CCarGen.livery;
		}

		public override void Build(MetaBuilder mb, bool isRoot = false)
		{
			this.MetaStructure.position = this.Position;
			this.MetaStructure.orientX = this.OrientX;
			this.MetaStructure.orientY = this.OrientY;
			this.MetaStructure.perpendicularLength = this.PerpendicularLength;
			this.MetaStructure.carModel = this.CarModel;
			this.MetaStructure.flags = this.Flags;
			this.MetaStructure.bodyColorRemap1 = this.BodyColorRemap1;
			this.MetaStructure.bodyColorRemap2 = this.BodyColorRemap2;
			this.MetaStructure.bodyColorRemap3 = this.BodyColorRemap3;
			this.MetaStructure.bodyColorRemap4 = this.BodyColorRemap4;
			this.MetaStructure.popGroup = this.PopGroup;
			this.MetaStructure.livery = this.Livery;

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
