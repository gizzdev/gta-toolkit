using System.Collections.Generic;
using System.Linq;

using SharpDX;

namespace RageLib.Resources.GTA5.PC.Meta.ExtendedTypes
{
	public class CExtensionDefLightShaft : MetaStructureWrapper<PC.Meta.CExtensionDefLightShaft>
	{
		public MetaFile Meta;
		public uint Name;
		public Vector3 OffsetPosition;
		public Vector3 CornerA;
		public Vector3 CornerB;
		public Vector3 CornerC;
		public Vector3 CornerD;
		public Vector3 Direction;
		public float DirectionAmount;
		public float Length;
		public float Unk_1616789093;
		public float Unk_120454521;
		public float Unk_1297365553;
		public float Unk_75548206;
		public float Unk_40301253;
		public float Unk_475013030;
		public uint Color;
		public float Intensity;
		public byte Flashiness;
		public uint Flags;
		public Unk_1931949281 DensityType;
		public Unk_2266515059 VolumeType;
		public float Softness;
		public bool Unk_59101696;

		public CExtensionDefLightShaft(MetaName metaName) : base(metaName)
		{
			this.MetaStructure = new PC.Meta.CExtensionDefLightShaft();
		}

		public void Parse(MetaFile meta, PC.Meta.CExtensionDefLightShaft CExtensionDefLightShaft)
		{
			this.Meta = meta;
			this.MetaStructure = CExtensionDefLightShaft;

			this.Name = CExtensionDefLightShaft.name;
			this.OffsetPosition = CExtensionDefLightShaft.offsetPosition;
			this.CornerA = CExtensionDefLightShaft.cornerA;
			this.CornerB = CExtensionDefLightShaft.cornerB;
			this.CornerC = CExtensionDefLightShaft.cornerC;
			this.CornerD = CExtensionDefLightShaft.cornerD;
			this.Direction = CExtensionDefLightShaft.direction;
			this.DirectionAmount = CExtensionDefLightShaft.directionAmount;
			this.Length = CExtensionDefLightShaft.length;
			this.Unk_1616789093 = CExtensionDefLightShaft.Unk_1616789093;
			this.Unk_120454521 = CExtensionDefLightShaft.Unk_120454521;
			this.Unk_1297365553 = CExtensionDefLightShaft.Unk_1297365553;
			this.Unk_75548206 = CExtensionDefLightShaft.Unk_75548206;
			this.Unk_40301253 = CExtensionDefLightShaft.Unk_40301253;
			this.Unk_475013030 = CExtensionDefLightShaft.Unk_475013030;
			this.Color = CExtensionDefLightShaft.color;
			this.Intensity = CExtensionDefLightShaft.intensity;
			this.Flashiness = CExtensionDefLightShaft.flashiness;
			this.Flags = CExtensionDefLightShaft.flags;
			this.DensityType = CExtensionDefLightShaft.densityType;
			this.VolumeType = CExtensionDefLightShaft.volumeType;
			this.Softness = CExtensionDefLightShaft.softness;
			this.Unk_59101696 = CExtensionDefLightShaft.Unk_59101696;
		}

		public void Build(MetaBuilder mb, bool isRoot = false)
		{
			this.MetaStructure.name = this.Name;
			this.MetaStructure.offsetPosition = this.OffsetPosition;
			this.MetaStructure.cornerA = this.CornerA;
			this.MetaStructure.cornerB = this.CornerB;
			this.MetaStructure.cornerC = this.CornerC;
			this.MetaStructure.cornerD = this.CornerD;
			this.MetaStructure.direction = this.Direction;
			this.MetaStructure.directionAmount = this.DirectionAmount;
			this.MetaStructure.length = this.Length;
			this.MetaStructure.Unk_1616789093 = this.Unk_1616789093;
			this.MetaStructure.Unk_120454521 = this.Unk_120454521;
			this.MetaStructure.Unk_1297365553 = this.Unk_1297365553;
			this.MetaStructure.Unk_75548206 = this.Unk_75548206;
			this.MetaStructure.Unk_40301253 = this.Unk_40301253;
			this.MetaStructure.Unk_475013030 = this.Unk_475013030;
			this.MetaStructure.color = this.Color;
			this.MetaStructure.intensity = this.Intensity;
			this.MetaStructure.flashiness = this.Flashiness;
			this.MetaStructure.flags = this.Flags;
			this.MetaStructure.densityType = this.DensityType;
			this.MetaStructure.volumeType = this.VolumeType;
			this.MetaStructure.softness = this.Softness;
			this.MetaStructure.Unk_59101696 = this.Unk_59101696;

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
