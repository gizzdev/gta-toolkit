using System.Collections.Generic;
using System.Linq;
using SharpDX;
using RageLib.Resources.GTA5.PC.Meta;

namespace RageLib.GTA5.ResourceWrappers.PC.Meta.Structures
{
	public class MCMloEntitySet : MetaStructureWrapper<CMloEntitySet>
	{
		public static MetaName _MetaName = MetaName.CMloEntitySet;
		public MetaFile Meta;
		public uint Name;
		public List<uint> Locations = new List<uint>();
        public List<MCEntityDef> Entities;

		public MCMloEntitySet()
		{
			this.MetaName = MetaName.CMloEntitySet;
			this.MetaStructure = new CMloEntitySet();
		}

		public static void AddEnumAndStructureInfo(MetaBuilder mb)
		{
			var enumInfos = MetaInfo.GetStructureEnumInfo(MCMloEntitySet._MetaName);

			for (int i = 0; i < enumInfos.Length; i++)
				mb.AddEnumInfo((MetaName) enumInfos[i].EnumNameHash);

			mb.AddStructureInfo(MCMloEntitySet._MetaName);
		}


		public override void Parse(MetaFile meta, CMloEntitySet CMloEntitySet)
		{
			this.Meta = meta;
			this.MetaStructure = CMloEntitySet;

			this.Name = CMloEntitySet.name;
            this.Locations = MetaUtils.ConvertDataArray<uint>(meta, CMloEntitySet.locations.Pointer, CMloEntitySet.locations.Count1)?.ToList();
            this.Entities = MetaUtils.ConvertDataArray<CEntityDef>(this.Meta, CMloEntitySet.entities)?.Select(e => { var obj = new MCEntityDef(); obj.Parse(meta, e); return obj; }).ToList() ?? new List<MCEntityDef>();
        }

		public override void Build(MetaBuilder mb, bool isRoot = false)
		{
			this.MetaStructure.name = this.Name;
			this.MetaStructure.locations = mb.AddUintArrayPtr(this.Locations.ToArray());

            var entPtrs = new List<MetaPOINTER>();
            this.AddMetaPointers(mb, entPtrs, MetaName.CEntityDef, this.Entities.Select(e => { e.Build(mb); return e.MetaStructure; }));
            this.MetaStructure.entities = mb.AddPointerArray(entPtrs.ToArray());

            MCMloEntitySet.AddEnumAndStructureInfo(mb);          

			if(isRoot)
			{
				mb.AddItem(this.MetaName, this.MetaStructure);

				this.Meta = mb.GetMeta();
			}
		}

        public int AddEntity(MCEntityDef entity, int room)
        {
            this.Entities.Add(entity);
            return this.Entities.IndexOf(entity);
        }

        public void RemoveEntity(MCEntityDef entity)
        {
            int idx = this.Entities.IndexOf(entity);

            if (idx == -1)
                return;

            this.Entities.RemoveAt(idx);
            this.Locations.RemoveAt(idx);

        }

    }
}
