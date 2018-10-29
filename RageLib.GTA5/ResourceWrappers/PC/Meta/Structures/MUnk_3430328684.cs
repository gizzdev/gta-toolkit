using System.Collections.Generic;
using System.Linq;
using SharpDX;
using RageLib.Resources.GTA5.PC.Meta;

namespace RageLib.GTA5.ResourceWrappers.PC.Meta.Structures
{
	public class MUnk_3430328684 : MetaStructureWrapper<Unk_3430328684>
	{
		public static MetaName _MetaName = (MetaName) (-864638612);
		public MetaFile Meta;
		public uint FxType;
		public Vector3 FxOffsetPos;
		public Vector4 FxOffsetRot;
		public uint BoneTag;
		public float StartPhase;
		public float EndPhase;
		public byte PtFxIsTriggered;
		public ArrayOfChars64 PtFxTag;
		public float PtFxScale;
		public float PtFxProbability;
		public byte PtFxHasTint;
		public byte PtFxTintR;
		public byte PtFxTintG;
		public byte PtFxTintB;
		public Vector3 PtFxSize;

		public MUnk_3430328684()
		{
			this.MetaName = (MetaName) (-864638612);
			this.MetaStructure = new Unk_3430328684();
		}

		public static void AddEnumAndStructureInfo(MetaBuilder mb)
		{
			var enumInfos = MetaInfo.GetStructureEnumInfo(MUnk_3430328684._MetaName);

			for (int i = 0; i < enumInfos.Length; i++)
				mb.AddEnumInfo((MetaName) enumInfos[i].EnumNameHash);

			mb.AddStructureInfo(MUnk_3430328684._MetaName);
		}


		public override void Parse(MetaFile meta, Unk_3430328684 Unk_3430328684)
		{
			this.Meta = meta;
			this.MetaStructure = Unk_3430328684;

			this.FxType = Unk_3430328684.fxType;
			this.FxOffsetPos = Unk_3430328684.fxOffsetPos;
			this.FxOffsetRot = Unk_3430328684.fxOffsetRot;
			this.BoneTag = Unk_3430328684.boneTag;
			this.StartPhase = Unk_3430328684.startPhase;
			this.EndPhase = Unk_3430328684.endPhase;
			this.PtFxIsTriggered = Unk_3430328684.ptFxIsTriggered;
			this.PtFxTag = Unk_3430328684.ptFxTag;
			this.PtFxScale = Unk_3430328684.ptFxScale;
			this.PtFxProbability = Unk_3430328684.ptFxProbability;
			this.PtFxHasTint = Unk_3430328684.ptFxHasTint;
			this.PtFxTintR = Unk_3430328684.ptFxTintR;
			this.PtFxTintG = Unk_3430328684.ptFxTintG;
			this.PtFxTintB = Unk_3430328684.ptFxTintB;
			this.PtFxSize = Unk_3430328684.ptFxSize;
		}

		public override void Build(MetaBuilder mb, bool isRoot = false)
		{
			this.MetaStructure.fxType = this.FxType;
			this.MetaStructure.fxOffsetPos = this.FxOffsetPos;
			this.MetaStructure.fxOffsetRot = this.FxOffsetRot;
			this.MetaStructure.boneTag = this.BoneTag;
			this.MetaStructure.startPhase = this.StartPhase;
			this.MetaStructure.endPhase = this.EndPhase;
			this.MetaStructure.ptFxIsTriggered = this.PtFxIsTriggered;
			this.MetaStructure.ptFxTag = this.PtFxTag;
			this.MetaStructure.ptFxScale = this.PtFxScale;
			this.MetaStructure.ptFxProbability = this.PtFxProbability;
			this.MetaStructure.ptFxHasTint = this.PtFxHasTint;
			this.MetaStructure.ptFxTintR = this.PtFxTintR;
			this.MetaStructure.ptFxTintG = this.PtFxTintG;
			this.MetaStructure.ptFxTintB = this.PtFxTintB;
			this.MetaStructure.ptFxSize = this.PtFxSize;

            MUnk_3430328684.AddEnumAndStructureInfo(mb);          

			if(isRoot)
			{
				mb.AddItem(this.MetaName, this.MetaStructure);

				this.Meta = mb.GetMeta();
			}
		}
	}
}
