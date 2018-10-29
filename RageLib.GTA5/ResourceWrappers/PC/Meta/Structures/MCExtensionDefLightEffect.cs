using System.Collections.Generic;
using System.Linq;
using SharpDX;
using RageLib.Resources.GTA5.PC.Meta;

namespace RageLib.GTA5.ResourceWrappers.PC.Meta.Structures
{
	public class MCExtensionDefLightEffect : MetaStructureWrapper<CExtensionDefLightEffect>
	{
		public static MetaName _MetaName = MetaName.CExtensionDefLightEffect;
		public MetaFile Meta;
		public uint Name;
		public Vector3 OffsetPosition;
		public List<MCLightAttrDef> Instances = new List<MCLightAttrDef>();

		public MCExtensionDefLightEffect()
		{
			this.MetaName = MetaName.CExtensionDefLightEffect;
			this.MetaStructure = new CExtensionDefLightEffect();
		}

		public static void AddEnumAndStructureInfo(MetaBuilder mb)
		{
			var enumInfos = MetaInfo.GetStructureEnumInfo(MCExtensionDefLightEffect._MetaName);

			for (int i = 0; i < enumInfos.Length; i++)
				mb.AddEnumInfo((MetaName) enumInfos[i].EnumNameHash);

			mb.AddStructureInfo(MCExtensionDefLightEffect._MetaName);
			mb.AddStructureInfo(MetaName.CLightAttrDef);
		}


		public override void Parse(MetaFile meta, CExtensionDefLightEffect CExtensionDefLightEffect)
		{
			this.Meta = meta;
			this.MetaStructure = CExtensionDefLightEffect;

			this.Name = CExtensionDefLightEffect.name;
			this.OffsetPosition = CExtensionDefLightEffect.offsetPosition;
			var instances = MetaUtils.ConvertDataArray<CLightAttrDef>(meta, CExtensionDefLightEffect.instances);

            this.Instances = instances?.Select(e => {var msw = new MCLightAttrDef(); msw.Parse(meta, e); return msw;}).ToList();

		}

		public override void Build(MetaBuilder mb, bool isRoot = false)
		{
			this.MetaStructure.name = this.Name;
			this.MetaStructure.offsetPosition = this.OffsetPosition;

			if(this.Instances != null)
				this.MetaStructure.instances = mb.AddItemArrayPtr(MetaName.CLightAttrDef, this.Instances.Select(e => { e.Build(mb); return e.MetaStructure; }).ToArray());
 			MCLightAttrDef.AddEnumAndStructureInfo(mb);          


 			MCExtensionDefLightEffect.AddEnumAndStructureInfo(mb);          

			if(isRoot)
			{
				mb.AddItem(this.MetaName, this.MetaStructure);

				this.Meta = mb.GetMeta();
			}
		}
	}
}
