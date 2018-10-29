using System.Collections.Generic;
using System.Linq;
using SharpDX;
using RageLib.Resources.GTA5.PC.Meta;

namespace RageLib.GTA5.ResourceWrappers.PC.Meta.Structures
{
	public class Mrage__phVerletClothCustomBounds : MetaStructureWrapper<rage__phVerletClothCustomBounds>
	{
		public static MetaName _MetaName = MetaName.rage__phVerletClothCustomBounds;
		public MetaFile Meta;
		public uint Name;
		public List<MUnk_1701774085> CollisionData;

		public Mrage__phVerletClothCustomBounds()
		{
			this.MetaName = MetaName.rage__phVerletClothCustomBounds;
			this.MetaStructure = new rage__phVerletClothCustomBounds();
		}

		public static void AddEnumAndStructureInfo(MetaBuilder mb)
		{
			var enumInfos = MetaInfo.GetStructureEnumInfo(Mrage__phVerletClothCustomBounds._MetaName);

			for (int i = 0; i < enumInfos.Length; i++)
				mb.AddEnumInfo((MetaName) enumInfos[i].EnumNameHash);

			mb.AddStructureInfo(Mrage__phVerletClothCustomBounds._MetaName);
			mb.AddStructureInfo((MetaName) (1701774085));
		}


		public override void Parse(MetaFile meta, rage__phVerletClothCustomBounds rage__phVerletClothCustomBounds)
		{
			this.Meta = meta;
			this.MetaStructure = rage__phVerletClothCustomBounds;

			this.Name = rage__phVerletClothCustomBounds.name;
			var CollisionData = MetaUtils.ConvertDataArray<Unk_1701774085>(meta, rage__phVerletClothCustomBounds.CollisionData);
			this.CollisionData = CollisionData?.Select(e => { var msw = new MUnk_1701774085(); msw.Parse(meta, e); return msw; }).ToList();

		}

		public override void Build(MetaBuilder mb, bool isRoot = false)
		{
			this.MetaStructure.name = this.Name;
			if(this.CollisionData != null)
				this.MetaStructure.CollisionData = mb.AddItemArrayPtr((MetaName) (1701774085), this.CollisionData.Select(e => { e.Build(mb); return e.MetaStructure; }).ToArray());
 			MUnk_1701774085.AddEnumAndStructureInfo(mb);          


 			Mrage__phVerletClothCustomBounds.AddEnumAndStructureInfo(mb);          

			if(isRoot)
			{
				mb.AddItem(this.MetaName, this.MetaStructure);

				this.Meta = mb.GetMeta();
			}
		}
	}
}
