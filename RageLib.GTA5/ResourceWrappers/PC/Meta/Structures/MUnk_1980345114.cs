using System.Collections.Generic;
using System.Linq;
using SharpDX;
using RageLib.Resources.GTA5.PC.Meta;

namespace RageLib.GTA5.ResourceWrappers.PC.Meta.Structures
{
	public class MUnk_1980345114 : MetaStructureWrapper<Unk_1980345114>
	{
		public static MetaName _MetaName = (MetaName) (1980345114);
		public MetaFile Meta;
		public ArrayOfChars64 AnimDict;
		public ArrayOfChars64 AnimName;
		public ArrayOfChars64 AnimatedModel;
		public float PunchInPhase;
		public float PunchOutPhase;
		public List<MUnk_3430328684> EffectsData;

		public MUnk_1980345114()
		{
			this.MetaName = (MetaName) (1980345114);
			this.MetaStructure = new Unk_1980345114();
		}

		public static void AddEnumAndStructureInfo(MetaBuilder mb)
		{
			var enumInfos = MetaInfo.GetStructureEnumInfo(MUnk_1980345114._MetaName);

			for (int i = 0; i < enumInfos.Length; i++)
				mb.AddEnumInfo((MetaName) enumInfos[i].EnumNameHash);

			mb.AddStructureInfo(MUnk_1980345114._MetaName);
			mb.AddStructureInfo((MetaName) (-864638612));
		}


		public override void Parse(MetaFile meta, Unk_1980345114 Unk_1980345114)
		{
			this.Meta = meta;
			this.MetaStructure = Unk_1980345114;

			this.AnimDict = Unk_1980345114.AnimDict;
			this.AnimName = Unk_1980345114.AnimName;
			this.AnimatedModel = Unk_1980345114.AnimatedModel;
			this.PunchInPhase = Unk_1980345114.punchInPhase;
			this.PunchOutPhase = Unk_1980345114.punchOutPhase;
			var effectsData = MetaUtils.ConvertDataArray<Unk_3430328684>(meta, Unk_1980345114.effectsData);
			this.EffectsData = effectsData?.Select(e => { var msw = new MUnk_3430328684(); msw.Parse(meta, e); return msw; }).ToList();

		}

		public override void Build(MetaBuilder mb, bool isRoot = false)
		{
			this.MetaStructure.AnimDict = this.AnimDict;
			this.MetaStructure.AnimName = this.AnimName;
			this.MetaStructure.AnimatedModel = this.AnimatedModel;
			this.MetaStructure.punchInPhase = this.PunchInPhase;
			this.MetaStructure.punchOutPhase = this.PunchOutPhase;
			if(this.EffectsData != null)
				this.MetaStructure.effectsData = mb.AddItemArrayPtr((MetaName) (-864638612), this.EffectsData.Select(e => { e.Build(mb); return e.MetaStructure; }).ToArray());
            MUnk_3430328684.AddEnumAndStructureInfo(mb);


            MUnk_1980345114.AddEnumAndStructureInfo(mb);          

			if(isRoot)
			{
				mb.AddItem(this.MetaName, this.MetaStructure);

				this.Meta = mb.GetMeta();
			}
		}
	}
}
