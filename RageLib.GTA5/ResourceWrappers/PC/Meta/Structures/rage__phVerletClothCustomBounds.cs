using System.Collections.Generic;
using System.Linq;
using SharpDX;
using RageLib.Resources.GTA5.PC.Meta;

namespace RageLib.GTA5.ResourceWrappers.PC.Meta.Structures
{
	public class rage__phVerletClothCustomBounds : MetaStructureWrapper<RageLib.Resources.GTA5.PC.Meta.rage__phVerletClothCustomBounds>
	{
		public MetaFile Meta;
		public uint Name;
		public List<Unk_1701774085> CollisionData;

		public rage__phVerletClothCustomBounds()
		{
			this.MetaName = MetaName.rage__phVerletClothCustomBounds;
			this.MetaStructure = new RageLib.Resources.GTA5.PC.Meta.rage__phVerletClothCustomBounds();
		}

		public override void Parse(MetaFile meta, RageLib.Resources.GTA5.PC.Meta.rage__phVerletClothCustomBounds rage__phVerletClothCustomBounds)
		{
			this.Meta = meta;
			this.MetaStructure = rage__phVerletClothCustomBounds;

			this.Name = rage__phVerletClothCustomBounds.name;
			var CollisionData = MetaUtils.ConvertDataArray<RageLib.Resources.GTA5.PC.Meta.Unk_1701774085>(meta, rage__phVerletClothCustomBounds.CollisionData);
			this.CollisionData = CollisionData?.Select(e => { var msw = new Unk_1701774085(); msw.Parse(meta, e); return msw; }).ToList();

		}

		public override void Build(MetaBuilder mb, bool isRoot = false)
		{
			this.MetaStructure.name = this.Name;
			if(this.CollisionData != null)
				this.MetaStructure.CollisionData = mb.AddItemArrayPtr((MetaName) (1701774085), this.CollisionData.Select(e => e.MetaStructure).ToArray());

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
