using System.Collections.Generic;
using System.Linq;

using SharpDX;

namespace RageLib.Resources.GTA5.PC.Meta.ExtendedTypes
{
	public class CBaseArchetypeDef : MetaStructureWrapper<PC.Meta.CBaseArchetypeDef>
	{
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
		public Array_StructurePointer Extensions;

		public CBaseArchetypeDef(MetaName metaName) : base(metaName)
		{
			this.MetaStructure = new PC.Meta.CBaseArchetypeDef();
		}

		public void Parse(MetaFile meta, PC.Meta.CBaseArchetypeDef CBaseArchetypeDef)
		{
			this.Meta = meta;
			this.MetaStructure = CBaseArchetypeDef;

			this.LodDist = CBaseArchetypeDef.lodDist;
			this.Flags = CBaseArchetypeDef.flags;
			this.SpecialAttribute = CBaseArchetypeDef.specialAttribute;
			this.BbMin = CBaseArchetypeDef.bbMin;
			this.BbMax = CBaseArchetypeDef.bbMax;
			this.BsCentre = CBaseArchetypeDef.bsCentre;
			this.BsRadius = CBaseArchetypeDef.bsRadius;
			this.HdTextureDist = CBaseArchetypeDef.hdTextureDist;
			this.Name = CBaseArchetypeDef.name;
			this.TextureDictionary = CBaseArchetypeDef.textureDictionary;
			this.ClipDictionary = CBaseArchetypeDef.clipDictionary;
			this.DrawableDictionary = CBaseArchetypeDef.drawableDictionary;
			this.PhysicsDictionary = CBaseArchetypeDef.physicsDictionary;
			// this.Extensions = CBaseArchetypeDef.extensions;
		}

		public void Build(MetaBuilder mb, bool isRoot = false)
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
			// this.MetaStructure.extensions = this.Extensions;

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
