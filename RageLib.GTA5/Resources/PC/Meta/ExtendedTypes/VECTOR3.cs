using System.Collections.Generic;
using System.Linq;

using SharpDX;

namespace RageLib.Resources.GTA5.PC.Meta.ExtendedTypes
{
	public class VECTOR3 : MetaStructureWrapper<PC.Meta.VECTOR3>
	{
		public MetaFile Meta;
		public float X;
		public float Y;
		public float Z;

		public VECTOR3(MetaName metaName) : base(metaName)
		{
			this.MetaStructure = new PC.Meta.VECTOR3();
		}

		public void Parse(MetaFile meta, PC.Meta.VECTOR3 VECTOR3)
		{
			this.Meta = meta;
			this.MetaStructure = VECTOR3;

			this.X = VECTOR3.x;
			this.Y = VECTOR3.y;
			this.Z = VECTOR3.z;
		}

		public void Build(MetaBuilder mb, bool isRoot = false)
		{
			this.MetaStructure.x = this.X;
			this.MetaStructure.y = this.Y;
			this.MetaStructure.z = this.Z;

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
