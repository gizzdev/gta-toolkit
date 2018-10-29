using System.Collections.Generic;
using System.Linq;
using SharpDX;
using RageLib.Resources.GTA5.PC.Meta;

namespace RageLib.GTA5.ResourceWrappers.PC.Meta.Structures
{
	public class MCStreamingRequestFrame : MetaStructureWrapper<CStreamingRequestFrame>
	{
		public static MetaName _MetaName = MetaName.CStreamingRequestFrame;
		public MetaFile Meta;
		public Array_uint AddList;
		public Array_uint RemoveList;
		public Vector3 CamPos;
		public Vector3 CamDir;
		public Array_byte Unk_1762439591;
		public uint Flags;

		public MCStreamingRequestFrame()
		{
			this.MetaName = MetaName.CStreamingRequestFrame;
			this.MetaStructure = new CStreamingRequestFrame();
		}

		public static void AddEnumAndStructureInfo(MetaBuilder mb)
		{
			var enumInfos = MetaInfo.GetStructureEnumInfo(MCStreamingRequestFrame._MetaName);

			for (int i = 0; i < enumInfos.Length; i++)
				mb.AddEnumInfo((MetaName) enumInfos[i].EnumNameHash);

			mb.AddStructureInfo(MCStreamingRequestFrame._MetaName);
		}


		public override void Parse(MetaFile meta, CStreamingRequestFrame CStreamingRequestFrame)
		{
			this.Meta = meta;
			this.MetaStructure = CStreamingRequestFrame;

			// this.AddList = CStreamingRequestFrame.AddList;
			// this.RemoveList = CStreamingRequestFrame.RemoveList;
			this.CamPos = CStreamingRequestFrame.CamPos;
			this.CamDir = CStreamingRequestFrame.CamDir;
			// this.Unk_1762439591 = CStreamingRequestFrame.Unk_1762439591;
			this.Flags = CStreamingRequestFrame.Flags;
		}

		public override void Build(MetaBuilder mb, bool isRoot = false)
		{
			// this.MetaStructure.AddList = this.AddList;
			// this.MetaStructure.RemoveList = this.RemoveList;
			this.MetaStructure.CamPos = this.CamPos;
			this.MetaStructure.CamDir = this.CamDir;
			// this.MetaStructure.Unk_1762439591 = this.Unk_1762439591;
			this.MetaStructure.Flags = this.Flags;

 			MCStreamingRequestFrame.AddEnumAndStructureInfo(mb);          

			if(isRoot)
			{
				mb.AddItem(this.MetaName, this.MetaStructure);

				this.Meta = mb.GetMeta();
			}
		}
	}
}
