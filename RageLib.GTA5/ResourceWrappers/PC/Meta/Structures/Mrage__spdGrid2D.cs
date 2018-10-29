using System.Collections.Generic;
using System.Linq;
using SharpDX;
using RageLib.Resources.GTA5.PC.Meta;

namespace RageLib.GTA5.ResourceWrappers.PC.Meta.Structures
{
	public class Mrage__spdGrid2D : MetaStructureWrapper<rage__spdGrid2D>
	{
		public static MetaName _MetaName = MetaName.rage__spdGrid2D;
		public MetaFile Meta;
		public int Unk_860552138;
		public int Unk_3824598937;
		public int Unk_496029782;
		public int Unk_3374647798;
		public float Unk_2690909759;
		public float Unk_3691675019;

		public Mrage__spdGrid2D()
		{
			this.MetaName = MetaName.rage__spdGrid2D;
			this.MetaStructure = new rage__spdGrid2D();
		}

		public static void AddEnumAndStructureInfo(MetaBuilder mb)
		{
			var enumInfos = MetaInfo.GetStructureEnumInfo(Mrage__spdGrid2D._MetaName);

			for (int i = 0; i < enumInfos.Length; i++)
				mb.AddEnumInfo((MetaName) enumInfos[i].EnumNameHash);

			mb.AddStructureInfo(Mrage__spdGrid2D._MetaName);
		}


		public override void Parse(MetaFile meta, rage__spdGrid2D rage__spdGrid2D)
		{
			this.Meta = meta;
			this.MetaStructure = rage__spdGrid2D;

			this.Unk_860552138 = rage__spdGrid2D.Unk_860552138;
			this.Unk_3824598937 = rage__spdGrid2D.Unk_3824598937;
			this.Unk_496029782 = rage__spdGrid2D.Unk_496029782;
			this.Unk_3374647798 = rage__spdGrid2D.Unk_3374647798;
			this.Unk_2690909759 = rage__spdGrid2D.Unk_2690909759;
			this.Unk_3691675019 = rage__spdGrid2D.Unk_3691675019;
		}

		public override void Build(MetaBuilder mb, bool isRoot = false)
		{
			this.MetaStructure.Unk_860552138 = this.Unk_860552138;
			this.MetaStructure.Unk_3824598937 = this.Unk_3824598937;
			this.MetaStructure.Unk_496029782 = this.Unk_496029782;
			this.MetaStructure.Unk_3374647798 = this.Unk_3374647798;
			this.MetaStructure.Unk_2690909759 = this.Unk_2690909759;
			this.MetaStructure.Unk_3691675019 = this.Unk_3691675019;

            Mrage__spdGrid2D.AddEnumAndStructureInfo(mb);          

			if(isRoot)
			{
				mb.AddItem(this.MetaName, this.MetaStructure);

				this.Meta = mb.GetMeta();
			}
		}
	}
}
