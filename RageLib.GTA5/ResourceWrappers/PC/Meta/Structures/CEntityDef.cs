/*
    Copyright(c) 2016 Neodymium

    Permission is hereby granted, free of charge, to any person obtaining a copy
    of this software and associated documentation files (the "Software"), to deal
    in the Software without restriction, including without limitation the rights
    to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
    copies of the Software, and to permit persons to whom the Software is
    furnished to do so, subject to the following conditions:

    The above copyright notice and this permission notice shall be included in
    all copies or substantial portions of the Software.

    THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
    IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
    FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
    AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
    LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
    OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
    THE SOFTWARE.
*/

using System.Collections.Generic;
using System.Linq;
using SharpDX;
using RageLib.Resources.GTA5.PC.Meta;

namespace RageLib.GTA5.ResourceWrappers.PC.Meta.Structures
{
    public class CEntityDef : MetaStructureWrapper<RageLib.Resources.GTA5.PC.Meta.CEntityDef>
	{
		public MetaFile Meta;
		public uint ArchetypeName;
		public uint Flags;
		public uint Guid;
		public Vector3 Position;
		public Vector4 Rotation;
		public float ScaleXY;
		public float ScaleZ;
		public int ParentIndex;
		public float LodDist;
		public float ChildLodDist;
		public Unk_1264241711 LodLevel;
		public uint NumChildren;
		public Unk_648413703 PriorityLevel;
		public Array_StructurePointer Extensions;
		public int AmbientOcclusionMultiplier;
		public int ArtificialAmbientOcclusion;
		public uint TintValue;

		public CEntityDef()
		{
			this.MetaName = MetaName.CEntityDef;
			this.MetaStructure = new RageLib.Resources.GTA5.PC.Meta.CEntityDef();
		}

		public override void Parse(MetaFile meta, RageLib.Resources.GTA5.PC.Meta.CEntityDef CEntityDef)
		{
			this.Meta = meta;
			this.MetaStructure = CEntityDef;

			this.ArchetypeName = CEntityDef.archetypeName;
			this.Flags = CEntityDef.flags;
			this.Guid = CEntityDef.guid;
			this.Position = CEntityDef.position;
			this.Rotation = CEntityDef.rotation;
			this.ScaleXY = CEntityDef.scaleXY;
			this.ScaleZ = CEntityDef.scaleZ;
			this.ParentIndex = CEntityDef.parentIndex;
			this.LodDist = CEntityDef.lodDist;
			this.ChildLodDist = CEntityDef.childLodDist;
			this.LodLevel = CEntityDef.lodLevel;
			this.NumChildren = CEntityDef.numChildren;
			this.PriorityLevel = CEntityDef.priorityLevel;
			// this.Extensions = CEntityDef.extensions;
			this.AmbientOcclusionMultiplier = CEntityDef.ambientOcclusionMultiplier;
			this.ArtificialAmbientOcclusion = CEntityDef.artificialAmbientOcclusion;
			this.TintValue = CEntityDef.tintValue;
		}

		public override void Build(MetaBuilder mb, bool isRoot = false)
		{
			this.MetaStructure.archetypeName = this.ArchetypeName;
			this.MetaStructure.flags = this.Flags;
			this.MetaStructure.guid = this.Guid;
			this.MetaStructure.position = this.Position;
			this.MetaStructure.rotation = this.Rotation;
			this.MetaStructure.scaleXY = this.ScaleXY;
			this.MetaStructure.scaleZ = this.ScaleZ;
			this.MetaStructure.parentIndex = this.ParentIndex;
			this.MetaStructure.lodDist = this.LodDist;
			this.MetaStructure.childLodDist = this.ChildLodDist;
			this.MetaStructure.lodLevel = this.LodLevel;
			this.MetaStructure.numChildren = this.NumChildren;
			this.MetaStructure.priorityLevel = this.PriorityLevel;
			// this.MetaStructure.extensions = this.Extensions;
			this.MetaStructure.ambientOcclusionMultiplier = this.AmbientOcclusionMultiplier;
			this.MetaStructure.artificialAmbientOcclusion = this.ArtificialAmbientOcclusion;
			this.MetaStructure.tintValue = this.TintValue;

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
