using System.Collections.Generic;
using System.Linq;
using SharpDX;
using RageLib.Resources.GTA5.PC.Meta;

namespace RageLib.GTA5.ResourceWrappers.PC.Meta.Structures
{
	public class CExtensionDefLightEffect : MetaStructureWrapper<RageLib.Resources.GTA5.PC.Meta.CExtensionDefLightEffect>
	{
		public MetaFile Meta;
		public uint Name;
		public Vector3 OffsetPosition;
		public List<CLightAttrDef> Instances;

		public CExtensionDefLightEffect()
		{
			this.MetaName = MetaName.CExtensionDefLightEffect;
			this.MetaStructure = new RageLib.Resources.GTA5.PC.Meta.CExtensionDefLightEffect();
		}

		public override void Parse(MetaFile meta, RageLib.Resources.GTA5.PC.Meta.CExtensionDefLightEffect CExtensionDefLightEffect)
		{
			this.Meta = meta;
			this.MetaStructure = CExtensionDefLightEffect;

			this.Name = CExtensionDefLightEffect.name;
			this.OffsetPosition = CExtensionDefLightEffect.offsetPosition;
			var instances = MetaUtils.ConvertDataArray<RageLib.Resources.GTA5.PC.Meta.CLightAttrDef>(meta, CExtensionDefLightEffect.instances);
			this.Instances = instances?.Select(e => { var msw = new CLightAttrDef(); msw.Parse(meta, e); return msw; }).ToList();

		}

		public override void Build(MetaBuilder mb, bool isRoot = false)
		{
			this.MetaStructure.name = this.Name;
			this.MetaStructure.offsetPosition = this.OffsetPosition;
			if(this.Instances != null)
				this.MetaStructure.instances = mb.AddItemArrayPtr(MetaName.CLightAttrDef, this.Instances.Select(e => e.MetaStructure).ToArray());

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
