using System.Collections.Generic;
using System.Linq;
using SharpDX;
using RageLib.Resources.GTA5.PC.Meta;

namespace RageLib.GTA5.ResourceWrappers.PC.Meta.Structures
{
	public class MCExtensionDefWindDisturbance : MetaStructureWrapper<CExtensionDefWindDisturbance>
	{
		public static MetaName _MetaName = MetaName.CExtensionDefWindDisturbance;
		public MetaFile Meta;
		public uint Name;
		public Vector3 OffsetPosition;
		public Vector4 OffsetRotation;
		public int DisturbanceType;
		public int BoneTag;
		public Vector4 Size;
		public float Strength;
		public int Flags;

		public MCExtensionDefWindDisturbance()
		{
			this.MetaName = MetaName.CExtensionDefWindDisturbance;
			this.MetaStructure = new CExtensionDefWindDisturbance();
		}

		public static void AddEnumAndStructureInfo(MetaBuilder mb)
		{
			var enumInfos = MetaInfo.GetStructureEnumInfo(MCExtensionDefWindDisturbance._MetaName);

			for (int i = 0; i < enumInfos.Length; i++)
				mb.AddEnumInfo((MetaName) enumInfos[i].EnumNameHash);

			mb.AddStructureInfo(MCExtensionDefWindDisturbance._MetaName);
		}


		public override void Parse(MetaFile meta, CExtensionDefWindDisturbance CExtensionDefWindDisturbance)
		{
			this.Meta = meta;
			this.MetaStructure = CExtensionDefWindDisturbance;

			this.Name = CExtensionDefWindDisturbance.name;
			this.OffsetPosition = CExtensionDefWindDisturbance.offsetPosition;
			this.OffsetRotation = CExtensionDefWindDisturbance.offsetRotation;
			this.DisturbanceType = CExtensionDefWindDisturbance.disturbanceType;
			this.BoneTag = CExtensionDefWindDisturbance.boneTag;
			this.Size = CExtensionDefWindDisturbance.size;
			this.Strength = CExtensionDefWindDisturbance.strength;
			this.Flags = CExtensionDefWindDisturbance.flags;
		}

		public override void Build(MetaBuilder mb, bool isRoot = false)
		{
			this.MetaStructure.name = this.Name;
			this.MetaStructure.offsetPosition = this.OffsetPosition;
			this.MetaStructure.offsetRotation = this.OffsetRotation;
			this.MetaStructure.disturbanceType = this.DisturbanceType;
			this.MetaStructure.boneTag = this.BoneTag;
			this.MetaStructure.size = this.Size;
			this.MetaStructure.strength = this.Strength;
			this.MetaStructure.flags = this.Flags;

 			MCExtensionDefWindDisturbance.AddEnumAndStructureInfo(mb);          

			if(isRoot)
			{
				mb.AddItem(this.MetaName, this.MetaStructure);

				this.Meta = mb.GetMeta();
			}
		}
	}
}
