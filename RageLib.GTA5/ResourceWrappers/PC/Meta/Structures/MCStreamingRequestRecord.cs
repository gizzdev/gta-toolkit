using System.Collections.Generic;
using System.Linq;
using SharpDX;
using RageLib.Resources.GTA5.PC.Meta;

namespace RageLib.GTA5.ResourceWrappers.PC.Meta.Structures
{
	public class MCStreamingRequestRecord : MetaStructureWrapper<CStreamingRequestRecord>
	{
		public static MetaName _MetaName = MetaName.CStreamingRequestRecord;
		public MetaFile Meta;
		public List<MCStreamingRequestFrame> Frames;
		public List<MUnk_1358189812> CommonSets;
		public byte NewStyle;

		public MCStreamingRequestRecord()
		{
			this.MetaName = MetaName.CStreamingRequestRecord;
			this.MetaStructure = new CStreamingRequestRecord();
		}

		public static void AddEnumAndStructureInfo(MetaBuilder mb)
		{
			var enumInfos = MetaInfo.GetStructureEnumInfo(MCStreamingRequestRecord._MetaName);

			for (int i = 0; i < enumInfos.Length; i++)
				mb.AddEnumInfo((MetaName) enumInfos[i].EnumNameHash);

			mb.AddStructureInfo(MCStreamingRequestRecord._MetaName);
			mb.AddStructureInfo(MetaName.CStreamingRequestFrame);
			mb.AddStructureInfo((MetaName) (1358189812));
		}


		public override void Parse(MetaFile meta, CStreamingRequestRecord CStreamingRequestRecord)
		{
			this.Meta = meta;
			this.MetaStructure = CStreamingRequestRecord;

			var Frames = MetaUtils.ConvertDataArray<CStreamingRequestFrame>(meta, CStreamingRequestRecord.Frames);
			this.Frames = Frames?.Select(e => { var msw = new MCStreamingRequestFrame(); msw.Parse(meta, e); return msw; }).ToList();

			var CommonSets = MetaUtils.ConvertDataArray<Unk_1358189812>(meta, CStreamingRequestRecord.CommonSets);
			this.CommonSets = CommonSets?.Select(e => { var msw = new MUnk_1358189812(); msw.Parse(meta, e); return msw; }).ToList();

			this.NewStyle = CStreamingRequestRecord.NewStyle;
		}

		public override void Build(MetaBuilder mb, bool isRoot = false)
		{
			if(this.Frames != null)
				this.MetaStructure.Frames = mb.AddItemArrayPtr(MetaName.CStreamingRequestFrame, this.Frames.Select(e => { e.Build(mb); return e.MetaStructure; }).ToArray());
 			MCStreamingRequestFrame.AddEnumAndStructureInfo(mb);                    

			if(this.CommonSets != null)
				this.MetaStructure.CommonSets = mb.AddItemArrayPtr((MetaName) (1358189812), this.CommonSets.Select(e => { e.Build(mb); return e.MetaStructure; }).ToArray());
 			MUnk_1358189812.AddEnumAndStructureInfo(mb);          

			this.MetaStructure.NewStyle = this.NewStyle;

 			MCStreamingRequestRecord.AddEnumAndStructureInfo(mb);                    

			if(isRoot)
			{
				mb.AddItem(this.MetaName, this.MetaStructure);

				this.Meta = mb.GetMeta();
			}
		}
	}
}
