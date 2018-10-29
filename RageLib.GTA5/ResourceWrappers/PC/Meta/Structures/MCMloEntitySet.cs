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
		public Array_uint Locations;
		public Array_StructurePointer Entities;

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
			// this.Locations = CMloEntitySet.locations;
			// this.Entities = CMloEntitySet.entities;
		}

		public override void Build(MetaBuilder mb, bool isRoot = false)
		{
			this.MetaStructure.name = this.Name;
			// this.MetaStructure.locations = this.Locations;
			// this.MetaStructure.entities = this.Entities;

 			MCMloEntitySet.AddEnumAndStructureInfo(mb);          

			if(isRoot)
			{
				mb.AddItem(this.MetaName, this.MetaStructure);

				this.Meta = mb.GetMeta();
			}
		}
	}
}
