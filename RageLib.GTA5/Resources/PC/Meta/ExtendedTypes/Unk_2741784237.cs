using System.Collections.Generic;
using System.Linq;

using SharpDX;

namespace RageLib.Resources.GTA5.PC.Meta.ExtendedTypes
{
	public class Unk_2741784237 : MetaStructureWrapper<PC.Meta.Unk_2741784237>
	{
		public MetaFile Meta;
		public Vector3 Bmin;
		public Vector3 Bmax;
		public uint DataSize;
		public DataBlockPointer Verts;
		public ushort Unk_853977995;
		public ushort Unk_2337695078;
		public uint Flags;

		public Unk_2741784237(MetaName metaName) : base(metaName)
		{
			this.MetaStructure = new PC.Meta.Unk_2741784237();
		}

		public void Parse(MetaFile meta, PC.Meta.Unk_2741784237 Unk_2741784237)
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

		public void Build(MetaBuilder mb, bool isRoot = false)
		{
			this.MetaStructure.bmin = this.Bmin;
			this.MetaStructure.bmax = this.Bmax;
			this.MetaStructure.dataSize = this.DataSize;
			this.MetaStructure.verts = this.Verts;
			this.MetaStructure.Unk_853977995 = this.Unk_853977995;
			this.MetaStructure.Unk_2337695078 = this.Unk_2337695078;
			this.MetaStructure.flags = this.Flags;

			var enumInfos = MetaInfo.GetStructureEnumInfo(this.MetaName);
			var structureInfo = MetaInfo.GetStructureInfo(this.MetaName);
			var childStructureInfos = MetaInfo.GetStructureChildInfo(this.MetaName);

			for (int i = 0; i < enumInfos.Length; i++)
				mb.AddEnumInfo((MetaName) enumInfos[i].EnumNameHash);

			mb.AddStructureInfo((MetaName) structureInfo.StructureNameHash);


			for (int i = 0; i < childStructureInfos.Length; i++)
				mb.AddStructureInfo((MetaName) childStructureInfos[i].StructureNameHash);

			if(isRoot)
			{
				mb.AddItem(this.MetaName, this.MetaStructure);

				this.Meta = mb.GetMeta();
			}
		}
	}
}
