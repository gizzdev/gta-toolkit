using System.Collections.Generic;
using System.Linq;
using SharpDX;
using RageLib.Resources.GTA5.PC.Meta;

namespace RageLib.GTA5.ResourceWrappers.PC.Meta.Structures
{
	public class MUnk_254518642 : MetaStructureWrapper<Unk_254518642>
	{
		public static MetaName _MetaName = (MetaName) (254518642);
		public MetaFile Meta;
		public int Inclusions = 0;
		public int Exclusions = 0;
		public byte TexId = 0;
		public byte InclusionId = 0;
		public byte ExclusionId = 0;
		public byte Distribution = 255;

		public MUnk_254518642()
		{
			this.MetaName = (MetaName) (254518642);
			this.MetaStructure = new Unk_254518642();
		}

		public static void AddEnumAndStructureInfo(MetaBuilder mb)
		{
			var enumInfos = MetaInfo.GetStructureEnumInfo(MUnk_254518642._MetaName);

			for (int i = 0; i < enumInfos.Length; i++)
				mb.AddEnumInfo((MetaName) enumInfos[i].EnumNameHash);

			mb.AddStructureInfo(MUnk_254518642._MetaName);
		}


		public override void Parse(MetaFile meta, Unk_254518642 Unk_254518642)
		{
			this.Meta = meta;
			this.MetaStructure = Unk_254518642;

			this.Inclusions = Unk_254518642.inclusions;
			this.Exclusions = Unk_254518642.exclusions;
			this.TexId = Unk_254518642.texId;
			this.InclusionId = Unk_254518642.inclusionId;
			this.ExclusionId = Unk_254518642.exclusionId;
			this.Distribution = Unk_254518642.distribution;
		}

		public override void Build(MetaBuilder mb, bool isRoot = false)
		{
			this.MetaStructure.inclusions = this.Inclusions;
			this.MetaStructure.exclusions = this.Exclusions;
			this.MetaStructure.texId = this.TexId;
			this.MetaStructure.inclusionId = this.InclusionId;
			this.MetaStructure.exclusionId = this.ExclusionId;
			this.MetaStructure.distribution = this.Distribution;

            MUnk_254518642.AddEnumAndStructureInfo(mb);          

			if(isRoot)
			{
				mb.AddItem(this.MetaName, this.MetaStructure);

				this.Meta = mb.GetMeta();
			}
		}
	}
}
