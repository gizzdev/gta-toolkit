using System.Collections.Generic;
using System.Linq;
using SharpDX;
using RageLib.Resources.GTA5.PC.Meta;

namespace RageLib.GTA5.ResourceWrappers.PC.Meta.Structures
{
	public class MVECTOR3 : MetaStructureWrapper<VECTOR3>
	{
		public static MetaName _MetaName = MetaName.VECTOR3;
		public MetaFile Meta;
		public float X;
		public float Y;
		public float Z;

		public MVECTOR3()
		{
			this.MetaName = MetaName.VECTOR3;
			this.MetaStructure = new VECTOR3();
		}

		public static void AddEnumAndStructureInfo(MetaBuilder mb)
		{
			var enumInfos = MetaInfo.GetStructureEnumInfo(MVECTOR3._MetaName);

			for (int i = 0; i < enumInfos.Length; i++)
				mb.AddEnumInfo((MetaName) enumInfos[i].EnumNameHash);

			mb.AddStructureInfo(MVECTOR3._MetaName);
		}


		public override void Parse(MetaFile meta, VECTOR3 VECTOR3)
		{
			this.Meta = meta;
			this.MetaStructure = VECTOR3;

			this.X = VECTOR3.x;
			this.Y = VECTOR3.y;
			this.Z = VECTOR3.z;
		}

		public override void Build(MetaBuilder mb, bool isRoot = false)
		{
			this.MetaStructure.x = this.X;
			this.MetaStructure.y = this.Y;
			this.MetaStructure.z = this.Z;

 			MVECTOR3.AddEnumAndStructureInfo(mb);                    

			if(isRoot)
			{
				mb.AddItem(this.MetaName, this.MetaStructure);

				this.Meta = mb.GetMeta();
			}
		}
	}
}
