using System.Collections.Generic;
using System.Linq;
using SharpDX;
using RageLib.Resources.GTA5.PC.Meta;

namespace RageLib.GTA5.ResourceWrappers.PC.Meta.Structures
{
	public class CLightAttrDef : MetaStructureWrapper<RageLib.Resources.GTA5.PC.Meta.CLightAttrDef>
	{
		public MetaFile Meta;
		public ArrayOfBytes3 Posn;
		public ArrayOfBytes3 Colour;
		public byte Flashiness;
		public float Intensity;
		public uint Flags;
		public short BoneTag;
		public byte LightType;
		public byte GroupId;
		public uint TimeFlags;
		public float Falloff;
		public float FalloffExponent;
		public ArrayOfBytes4 CullingPlane;
		public byte ShadowBlur;
		public byte Padding1;
		public short Padding2;
		public uint Padding3;
		public float VolIntensity;
		public float VolSizeScale;
		public ArrayOfBytes3 VolOuterColour;
		public byte LightHash;
		public float VolOuterIntensity;
		public float CoronaSize;
		public float VolOuterExponent;
		public byte LightFadeDistance;
		public byte ShadowFadeDistance;
		public byte SpecularFadeDistance;
		public byte VolumetricFadeDistance;
		public float ShadowNearClip;
		public float CoronaIntensity;
		public float CoronaZBias;
		public ArrayOfBytes3 Direction;
		public ArrayOfBytes3 Tangent;
		public float ConeInnerAngle;
		public float ConeOuterAngle;
		public ArrayOfBytes3 Extents;
		public uint ProjectedTextureKey;

		public CLightAttrDef()
		{
			this.MetaName = MetaName.CLightAttrDef;
			this.MetaStructure = new RageLib.Resources.GTA5.PC.Meta.CLightAttrDef();
		}

		public override void Parse(MetaFile meta, RageLib.Resources.GTA5.PC.Meta.CLightAttrDef CLightAttrDef)
		{
			this.Meta = meta;
			this.MetaStructure = CLightAttrDef;

			this.Posn = CLightAttrDef.posn;
			this.Colour = CLightAttrDef.colour;
			this.Flashiness = CLightAttrDef.flashiness;
			this.Intensity = CLightAttrDef.intensity;
			this.Flags = CLightAttrDef.flags;
			this.BoneTag = CLightAttrDef.boneTag;
			this.LightType = CLightAttrDef.lightType;
			this.GroupId = CLightAttrDef.groupId;
			this.TimeFlags = CLightAttrDef.timeFlags;
			this.Falloff = CLightAttrDef.falloff;
			this.FalloffExponent = CLightAttrDef.falloffExponent;
			this.CullingPlane = CLightAttrDef.cullingPlane;
			this.ShadowBlur = CLightAttrDef.shadowBlur;
			this.Padding1 = CLightAttrDef.padding1;
			this.Padding2 = CLightAttrDef.padding2;
			this.Padding3 = CLightAttrDef.padding3;
			this.VolIntensity = CLightAttrDef.volIntensity;
			this.VolSizeScale = CLightAttrDef.volSizeScale;
			this.VolOuterColour = CLightAttrDef.volOuterColour;
			this.LightHash = CLightAttrDef.lightHash;
			this.VolOuterIntensity = CLightAttrDef.volOuterIntensity;
			this.CoronaSize = CLightAttrDef.coronaSize;
			this.VolOuterExponent = CLightAttrDef.volOuterExponent;
			this.LightFadeDistance = CLightAttrDef.lightFadeDistance;
			this.ShadowFadeDistance = CLightAttrDef.shadowFadeDistance;
			this.SpecularFadeDistance = CLightAttrDef.specularFadeDistance;
			this.VolumetricFadeDistance = CLightAttrDef.volumetricFadeDistance;
			this.ShadowNearClip = CLightAttrDef.shadowNearClip;
			this.CoronaIntensity = CLightAttrDef.coronaIntensity;
			this.CoronaZBias = CLightAttrDef.coronaZBias;
			this.Direction = CLightAttrDef.direction;
			this.Tangent = CLightAttrDef.tangent;
			this.ConeInnerAngle = CLightAttrDef.coneInnerAngle;
			this.ConeOuterAngle = CLightAttrDef.coneOuterAngle;
			this.Extents = CLightAttrDef.extents;
			this.ProjectedTextureKey = CLightAttrDef.projectedTextureKey;
		}

		public override void Build(MetaBuilder mb, bool isRoot = false)
		{
			this.MetaStructure.posn = this.Posn;
			this.MetaStructure.colour = this.Colour;
			this.MetaStructure.flashiness = this.Flashiness;
			this.MetaStructure.intensity = this.Intensity;
			this.MetaStructure.flags = this.Flags;
			this.MetaStructure.boneTag = this.BoneTag;
			this.MetaStructure.lightType = this.LightType;
			this.MetaStructure.groupId = this.GroupId;
			this.MetaStructure.timeFlags = this.TimeFlags;
			this.MetaStructure.falloff = this.Falloff;
			this.MetaStructure.falloffExponent = this.FalloffExponent;
			this.MetaStructure.cullingPlane = this.CullingPlane;
			this.MetaStructure.shadowBlur = this.ShadowBlur;
			this.MetaStructure.padding1 = this.Padding1;
			this.MetaStructure.padding2 = this.Padding2;
			this.MetaStructure.padding3 = this.Padding3;
			this.MetaStructure.volIntensity = this.VolIntensity;
			this.MetaStructure.volSizeScale = this.VolSizeScale;
			this.MetaStructure.volOuterColour = this.VolOuterColour;
			this.MetaStructure.lightHash = this.LightHash;
			this.MetaStructure.volOuterIntensity = this.VolOuterIntensity;
			this.MetaStructure.coronaSize = this.CoronaSize;
			this.MetaStructure.volOuterExponent = this.VolOuterExponent;
			this.MetaStructure.lightFadeDistance = this.LightFadeDistance;
			this.MetaStructure.shadowFadeDistance = this.ShadowFadeDistance;
			this.MetaStructure.specularFadeDistance = this.SpecularFadeDistance;
			this.MetaStructure.volumetricFadeDistance = this.VolumetricFadeDistance;
			this.MetaStructure.shadowNearClip = this.ShadowNearClip;
			this.MetaStructure.coronaIntensity = this.CoronaIntensity;
			this.MetaStructure.coronaZBias = this.CoronaZBias;
			this.MetaStructure.direction = this.Direction;
			this.MetaStructure.tangent = this.Tangent;
			this.MetaStructure.coneInnerAngle = this.ConeInnerAngle;
			this.MetaStructure.coneOuterAngle = this.ConeOuterAngle;
			this.MetaStructure.extents = this.Extents;
			this.MetaStructure.projectedTextureKey = this.ProjectedTextureKey;

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
