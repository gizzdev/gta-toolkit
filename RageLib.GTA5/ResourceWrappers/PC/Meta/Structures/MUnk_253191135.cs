using System.Collections.Generic;
using System.Linq;
using SharpDX;
using RageLib.Resources.GTA5.PC.Meta;

namespace RageLib.GTA5.ResourceWrappers.PC.Meta.Structures
{
	public class MUnk_253191135 : MetaStructureWrapper<Unk_253191135>
	{
		public static MetaName _MetaName = (MetaName) (253191135);
		public MetaFile Meta;
		public uint Name;
		public ArrayOfBytes12 Unk_173599222 = new ArrayOfBytes12();
		public ArrayOfBytes12 Unk_2991454271 = new ArrayOfBytes12();
		public ArrayOfBytes6 Unk_3598106198 = new ArrayOfBytes6();
		public ArrayOfBytes6 Unk_2095974912 = new ArrayOfBytes6();
		public ArrayOfBytes6 Unk_672172037 = new ArrayOfBytes6();

		public MUnk_253191135()
		{
			this.MetaName = (MetaName) (253191135);
			this.MetaStructure = new Unk_253191135();
		}

		public static void AddEnumAndStructureInfo(MetaBuilder mb)
		{
			var enumInfos = MetaInfo.GetStructureEnumInfo(MUnk_253191135._MetaName);

			for (int i = 0; i < enumInfos.Length; i++)
				mb.AddEnumInfo((MetaName) enumInfos[i].EnumNameHash);

			mb.AddStructureInfo(MUnk_253191135._MetaName);
		}


		public override void Parse(MetaFile meta, Unk_253191135 Unk_253191135)
		{
			this.Meta = meta;
			this.MetaStructure = Unk_253191135;

			this.Name = Unk_253191135.name;
			this.Unk_173599222 = Unk_253191135.Unk_173599222;
			this.Unk_2991454271 = Unk_253191135.Unk_2991454271;
			this.Unk_3598106198 = Unk_253191135.Unk_3598106198;
			this.Unk_2095974912 = Unk_253191135.Unk_2095974912;
			this.Unk_672172037 = Unk_253191135.Unk_672172037;
		}

		public override void Build(MetaBuilder mb, bool isRoot = false)
		{
			this.MetaStructure.name = this.Name;
			this.MetaStructure.Unk_173599222 = this.Unk_173599222;
			this.MetaStructure.Unk_2991454271 = this.Unk_2991454271;
			this.MetaStructure.Unk_3598106198 = this.Unk_3598106198;
			this.MetaStructure.Unk_2095974912 = this.Unk_2095974912;
			this.MetaStructure.Unk_672172037 = this.Unk_672172037;

            MUnk_253191135.AddEnumAndStructureInfo(mb);          

			if(isRoot)
			{
				mb.AddItem(this.MetaName, this.MetaStructure);

				this.Meta = mb.GetMeta();
			}
		}
	}
}
