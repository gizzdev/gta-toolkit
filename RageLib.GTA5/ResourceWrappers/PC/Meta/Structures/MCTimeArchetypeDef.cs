using System.Collections.Generic;
using System.Linq;
using SharpDX;
using RageLib.Resources.GTA5.PC.Meta;

namespace RageLib.GTA5.ResourceWrappers.PC.Meta.Structures
{
	public class MCTimeArchetypeDef : MetaStructureWrapper<CTimeArchetypeDef>
	{
		public static MetaName _MetaName = MetaName.CTimeArchetypeDef;
		public MetaFile Meta;
		public float LodDist;
		public uint Flags;
		public uint SpecialAttribute;
		public Vector3 BbMin;
		public Vector3 BbMax;
		public Vector3 BsCentre;
		public float BsRadius;
		public float HdTextureDist;
		public uint Name;
		public uint TextureDictionary;
		public uint ClipDictionary;
		public uint DrawableDictionary;
		public uint PhysicsDictionary;
		public Unk_1991964615 AssetType;
		public uint AssetName;
		public Array_StructurePointer Extensions;
		public uint TimeFlags;

		public MCTimeArchetypeDef()
		{
			this.MetaName = MetaName.CTimeArchetypeDef;
			this.MetaStructure = new CTimeArchetypeDef();
		}

		public static void AddEnumAndStructureInfo(MetaBuilder mb)
		{
			var enumInfos = MetaInfo.GetStructureEnumInfo(MCTimeArchetypeDef._MetaName);

			for (int i = 0; i < enumInfos.Length; i++)
				mb.AddEnumInfo((MetaName) enumInfos[i].EnumNameHash);

			mb.AddStructureInfo(MCTimeArchetypeDef._MetaName);
		}


		public override void Parse(MetaFile meta, CTimeArchetypeDef CTimeArchetypeDef)
		{
			this.Meta = meta;
			this.MetaStructure = CTimeArchetypeDef;

			this.LodDist = CTimeArchetypeDef.lodDist;
			this.Flags = CTimeArchetypeDef.flags;
			this.SpecialAttribute = CTimeArchetypeDef.specialAttribute;
			this.BbMin = CTimeArchetypeDef.bbMin;
			this.BbMax = CTimeArchetypeDef.bbMax;
			this.BsCentre = CTimeArchetypeDef.bsCentre;
			this.BsRadius = CTimeArchetypeDef.bsRadius;
			this.HdTextureDist = CTimeArchetypeDef.hdTextureDist;
			this.Name = CTimeArchetypeDef.name;
			this.TextureDictionary = CTimeArchetypeDef.textureDictionary;
			this.ClipDictionary = CTimeArchetypeDef.clipDictionary;
			this.DrawableDictionary = CTimeArchetypeDef.drawableDictionary;
			this.PhysicsDictionary = CTimeArchetypeDef.physicsDictionary;
			this.AssetType = CTimeArchetypeDef.assetType;
			this.AssetName = CTimeArchetypeDef.assetName;
			// this.Extensions = CTimeArchetypeDef.extensions;
			this.TimeFlags = CTimeArchetypeDef.timeFlags;
		}

		public override void Build(MetaBuilder mb, bool isRoot = false)
		{
			this.MetaStructure.lodDist = this.LodDist;
			this.MetaStructure.flags = this.Flags;
			this.MetaStructure.specialAttribute = this.SpecialAttribute;
			this.MetaStructure.bbMin = this.BbMin;
			this.MetaStructure.bbMax = this.BbMax;
			this.MetaStructure.bsCentre = this.BsCentre;
			this.MetaStructure.bsRadius = this.BsRadius;
			this.MetaStructure.hdTextureDist = this.HdTextureDist;
			this.MetaStructure.name = this.Name;
			this.MetaStructure.textureDictionary = this.TextureDictionary;
			this.MetaStructure.clipDictionary = this.ClipDictionary;
			this.MetaStructure.drawableDictionary = this.DrawableDictionary;
			this.MetaStructure.physicsDictionary = this.PhysicsDictionary;
			this.MetaStructure.assetType = this.AssetType;
			this.MetaStructure.assetName = this.AssetName;
			this.MetaStructure.extensions = new Array_StructurePointer();
			this.MetaStructure.timeFlags = this.TimeFlags;

 			MCTimeArchetypeDef.AddEnumAndStructureInfo(mb);                    

			if(isRoot)
			{
				mb.AddItem(this.MetaName, this.MetaStructure);

				this.Meta = mb.GetMeta();
			}
		}
	}
}
