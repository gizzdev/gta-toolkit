using System.Collections.Generic;
using System.Linq;
using SharpDX;
using RageLib.Resources.GTA5.PC.Meta;

namespace RageLib.GTA5.ResourceWrappers.PC.Meta.Structures
{
	public class CMloEntitySet : MetaStructureWrapper<RageLib.Resources.GTA5.PC.Meta.CMloEntitySet>
	{
		public MetaFile Meta;
		public uint Name;
		public List<uint> Locations = new List<uint>();
		public List<CEntityDef> Entities = new List<CEntityDef>();

		public CMloEntitySet()
		{
			this.MetaName = MetaName.CMloEntitySet;
			this.MetaStructure = new RageLib.Resources.GTA5.PC.Meta.CMloEntitySet();
		}

		public override void Parse(MetaFile meta, RageLib.Resources.GTA5.PC.Meta.CMloEntitySet CMloEntitySet)
		{
			this.Meta = meta;
			this.MetaStructure = CMloEntitySet;

			this.Name = CMloEntitySet.name;
			this.Locations = MetaUtils.ConvertDataArray<uint>(meta, CMloEntitySet.locations.Pointer, CMloEntitySet.locations.Count1).ToList();
            this.Entities = MetaUtils.GetTypedDataArray<RageLib.Resources.GTA5.PC.Meta.CEntityDef>(this.Meta, MetaName.CEntityDef).Select(e => { var obj = new CEntityDef(); obj.Parse(meta, e); return obj; }).ToList();
        }

		public override void Build(MetaBuilder mb, bool isRoot = false)
		{
			this.MetaStructure.name = this.Name;
            this.MetaStructure.locations = mb.AddUintArrayPtr(this.Locations.ToArray());

            var entPtrs = new List<MetaPOINTER>();
            this.AddMetaPointers(mb, entPtrs, MetaName.CEntityDef, this.Entities.Select(e => { e.Build(mb); return e.MetaStructure; }));
            this.MetaStructure.entities = mb.AddPointerArray(entPtrs.ToArray());

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
