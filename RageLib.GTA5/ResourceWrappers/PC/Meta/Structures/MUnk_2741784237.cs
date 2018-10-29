using System.Collections.Generic;
using System.Linq;
using SharpDX;
using RageLib.Resources.GTA5.PC.Meta;

namespace RageLib.GTA5.ResourceWrappers.PC.Meta.Structures
{
	public class MUnk_2741784237 : MetaStructureWrapper<Unk_2741784237>
	{
		public static MetaName _MetaName = (MetaName) (-1553183059);
		public MetaFile Meta;
		public Vector3 Bmin;
		public Vector3 Bmax;
		public uint DataSize;
		public DataBlockPointer Verts;
		public ushort Unk_853977995;
		public ushort Unk_2337695078;
		public uint Flags;

		public MUnk_2741784237()
		{
			this.MetaName = (MetaName) (-1553183059);
			this.MetaStructure = new Unk_2741784237();
		}

		public static void AddEnumAndStructureInfo(MetaBuilder mb)
		{
			var enumInfos = MetaInfo.GetStructureEnumInfo(MUnk_2741784237._MetaName);

			for (int i = 0; i < enumInfos.Length; i++)
				mb.AddEnumInfo((MetaName) enumInfos[i].EnumNameHash);

			mb.AddStructureInfo(MUnk_2741784237._MetaName);
		}


		public override void Parse(MetaFile meta, Unk_2741784237 Unk_2741784237)
		{
			this.Meta = meta;
			this.MetaStructure = Unk_2741784237;

			this.Bmin = Unk_2741784237.bmin;
			this.Bmax = Unk_2741784237.bmax;
			this.DataSize = Unk_2741784237.dataSize;
			this.Verts = Unk_2741784237.verts;
			this.Unk_853977995 = Unk_2741784237.Unk_853977995;
			this.Unk_2337695078 = Unk_2741784237.Unk_2337695078;
			this.Flags = Unk_2741784237.flags;
		}

		public override void Build(MetaBuilder mb, bool isRoot = false)
		{
			this.MetaStructure.bmin = this.Bmin;
			this.MetaStructure.bmax = this.Bmax;
			this.MetaStructure.dataSize = this.DataSize;
			this.MetaStructure.verts = this.Verts;
			this.MetaStructure.Unk_853977995 = this.Unk_853977995;
			this.MetaStructure.Unk_2337695078 = this.Unk_2337695078;
			this.MetaStructure.flags = this.Flags;

            MUnk_2741784237.AddEnumAndStructureInfo(mb);          

			if(isRoot)
			{
				mb.AddItem(this.MetaName, this.MetaStructure);

				this.Meta = mb.GetMeta();
			}
		}
	}
}
