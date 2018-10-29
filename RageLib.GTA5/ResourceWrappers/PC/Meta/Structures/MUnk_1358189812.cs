using System.Collections.Generic;
using System.Linq;
using SharpDX;
using RageLib.Resources.GTA5.PC.Meta;

namespace RageLib.GTA5.ResourceWrappers.PC.Meta.Structures
{
	public class MUnk_1358189812 : MetaStructureWrapper<Unk_1358189812>
	{
		public static MetaName _MetaName = (MetaName) (1358189812);
		public MetaFile Meta;
		public Array_uint Requests;

		public MUnk_1358189812()
		{
			this.MetaName = (MetaName) (1358189812);
			this.MetaStructure = new Unk_1358189812();
		}

		public static void AddEnumAndStructureInfo(MetaBuilder mb)
		{
			var enumInfos = MetaInfo.GetStructureEnumInfo(MUnk_1358189812._MetaName);

			for (int i = 0; i < enumInfos.Length; i++)
				mb.AddEnumInfo((MetaName) enumInfos[i].EnumNameHash);

			mb.AddStructureInfo(MUnk_1358189812._MetaName);
		}


		public override void Parse(MetaFile meta, Unk_1358189812 Unk_1358189812)
		{
			this.Meta = meta;
			this.MetaStructure = Unk_1358189812;

			// this.Requests = Unk_1358189812.Requests;
		}

		public override void Build(MetaBuilder mb, bool isRoot = false)
		{
            // this.MetaStructure.Requests = this.Requests;

            MUnk_1358189812.AddEnumAndStructureInfo(mb);          

			if(isRoot)
			{
				mb.AddItem(this.MetaName, this.MetaStructure);

				this.Meta = mb.GetMeta();
			}
		}
	}
}
