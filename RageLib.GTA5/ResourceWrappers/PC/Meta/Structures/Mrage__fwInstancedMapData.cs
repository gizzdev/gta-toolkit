using System.Collections.Generic;
using System.Linq;
using SharpDX;
using RageLib.Resources.GTA5.PC.Meta;

namespace RageLib.GTA5.ResourceWrappers.PC.Meta.Structures
{
	public class Mrage__fwInstancedMapData : MetaStructureWrapper<rage__fwInstancedMapData>
	{
		public static MetaName _MetaName = MetaName.rage__fwInstancedMapData;
		public MetaFile Meta;
		public uint ImapLink;
		public Array_Structure PropInstanceList;
		public List<Mrage__fwGrassInstanceListDef> GrassInstanceList = new List<Mrage__fwGrassInstanceListDef>();

		public Mrage__fwInstancedMapData()
		{
			this.MetaName = MetaName.rage__fwInstancedMapData;
			this.MetaStructure = new rage__fwInstancedMapData();
		}

		public static void AddEnumAndStructureInfo(MetaBuilder mb)
		{
			var enumInfos = MetaInfo.GetStructureEnumInfo(Mrage__fwInstancedMapData._MetaName);

			for (int i = 0; i < enumInfos.Length; i++)
				mb.AddEnumInfo((MetaName) enumInfos[i].EnumNameHash);

			mb.AddStructureInfo(Mrage__fwInstancedMapData._MetaName);
			mb.AddStructureInfo(MetaName.rage__fwGrassInstanceListDef);
		}


		public override void Parse(MetaFile meta, rage__fwInstancedMapData rage__fwInstancedMapData)
		{
			this.Meta = meta;
			this.MetaStructure = rage__fwInstancedMapData;

			this.ImapLink = rage__fwInstancedMapData.ImapLink;
			// this.PropInstanceList = rage__fwInstancedMapData.PropInstanceList;
			var GrassInstanceList = MetaUtils.ConvertDataArray<rage__fwGrassInstanceListDef>(meta, rage__fwInstancedMapData.GrassInstanceList);
			this.GrassInstanceList = GrassInstanceList?.Select(e => { var msw = new Mrage__fwGrassInstanceListDef(); msw.Parse(meta, e); return msw; }).ToList();

		}

		public override void Build(MetaBuilder mb, bool isRoot = false)
		{
			this.MetaStructure.ImapLink = this.ImapLink;
			this.MetaStructure.PropInstanceList = new Array_Structure();
			if(this.GrassInstanceList != null)
				this.MetaStructure.GrassInstanceList = mb.AddItemArrayPtr(MetaName.rage__fwGrassInstanceListDef, this.GrassInstanceList.Select(e => { e.Build(mb); return e.MetaStructure; }).ToArray());
 			Mrage__fwGrassInstanceListDef.AddEnumAndStructureInfo(mb);


            Mrage__fwInstancedMapData.AddEnumAndStructureInfo(mb);          

			if(isRoot)
			{
				mb.AddItem(this.MetaName, this.MetaStructure);

				this.Meta = mb.GetMeta();
			}
		}
	}
}
