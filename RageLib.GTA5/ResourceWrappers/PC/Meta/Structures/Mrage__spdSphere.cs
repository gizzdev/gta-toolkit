using System.Collections.Generic;
using System.Linq;
using SharpDX;
using RageLib.Resources.GTA5.PC.Meta;

namespace RageLib.GTA5.ResourceWrappers.PC.Meta.Structures
{
	public class Mrage__spdSphere : MetaStructureWrapper<rage__spdSphere>
	{
		public static MetaName _MetaName = MetaName.rage__spdSphere;
		public MetaFile Meta;
		public Vector4 CenterAndRadius;

		public Mrage__spdSphere()
		{
			this.MetaName = MetaName.rage__spdSphere;
			this.MetaStructure = new rage__spdSphere();
		}

		public static void AddEnumAndStructureInfo(MetaBuilder mb)
		{
			var enumInfos = MetaInfo.GetStructureEnumInfo(Mrage__spdSphere._MetaName);

			for (int i = 0; i < enumInfos.Length; i++)
				mb.AddEnumInfo((MetaName) enumInfos[i].EnumNameHash);

			mb.AddStructureInfo(Mrage__spdSphere._MetaName);
		}


		public override void Parse(MetaFile meta, rage__spdSphere rage__spdSphere)
		{
			this.Meta = meta;
			this.MetaStructure = rage__spdSphere;

			this.CenterAndRadius = rage__spdSphere.centerAndRadius;
		}

		public override void Build(MetaBuilder mb, bool isRoot = false)
		{
			this.MetaStructure.centerAndRadius = this.CenterAndRadius;

            Mrage__spdSphere.AddEnumAndStructureInfo(mb);          

			if(isRoot)
			{
				mb.AddItem(this.MetaName, this.MetaStructure);

				this.Meta = mb.GetMeta();
			}
		}
	}
}
