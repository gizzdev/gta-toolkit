using System.Collections.Generic;
using System.Linq;
using SharpDX;
using RageLib.Resources.GTA5.PC.Meta;

namespace RageLib.GTA5.ResourceWrappers.PC.Meta.Structures
{
	public class Mrage__spdAABB : MetaStructureWrapper<rage__spdAABB>
	{
		public static MetaName _MetaName = MetaName.rage__spdAABB;
		public MetaFile Meta;
		public Vector4 Min;
		public Vector4 Max;

		public Mrage__spdAABB()
		{
			this.MetaName = MetaName.rage__spdAABB;
			this.MetaStructure = new rage__spdAABB();
		}

		public static void AddEnumAndStructureInfo(MetaBuilder mb)
		{
			var enumInfos = MetaInfo.GetStructureEnumInfo(Mrage__spdAABB._MetaName);

			for (int i = 0; i < enumInfos.Length; i++)
				mb.AddEnumInfo((MetaName) enumInfos[i].EnumNameHash);

			mb.AddStructureInfo(Mrage__spdAABB._MetaName);
		}


		public override void Parse(MetaFile meta, rage__spdAABB rage__spdAABB)
		{
			this.Meta = meta;
			this.MetaStructure = rage__spdAABB;

			this.Min = rage__spdAABB.min;
			this.Max = rage__spdAABB.max;
		}

		public override void Build(MetaBuilder mb, bool isRoot = false)
		{
			this.MetaStructure.min = this.Min;
			this.MetaStructure.max = this.Max;

 			Mrage__spdAABB.AddEnumAndStructureInfo(mb);          

			if(isRoot)
			{
				mb.AddItem(this.MetaName, this.MetaStructure);

				this.Meta = mb.GetMeta();
			}
		}
	}
}
