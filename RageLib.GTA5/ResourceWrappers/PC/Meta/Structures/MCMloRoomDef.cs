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
using System;

namespace RageLib.GTA5.ResourceWrappers.PC.Meta.Structures
{
	public class MCMloRoomDef : MetaStructureWrapper<CMloRoomDef>
	{
        public static MetaName _MetaName = MetaName.CMloRoomDef;
        public MetaFile Meta;
		public string Name = "";
		public Vector3 BbMin;
		public Vector3 BbMax;
		public float Blend;
		public uint TimecycleName;
		public uint SecondaryTimecycleName;
		public uint Flags;
		public uint PortalCount;
		public int FloorId;
		public int ExteriorVisibiltyDepth;
		public List<uint> AttachedObjects = new List<uint>();
        public MCMloArchetypeDef Parent;

		public MCMloRoomDef(MCMloArchetypeDef parent)
		{
			this.MetaName = MetaName.CMloRoomDef;
			this.MetaStructure = new CMloRoomDef();
            Parent = parent;
        }

        public static void AddEnumAndStructureInfo(MetaBuilder mb)
        {
            var enumInfos = MetaInfo.GetStructureEnumInfo(MCMloRoomDef._MetaName);

            for (int i = 0; i < enumInfos.Length; i++)
                mb.AddEnumInfo((MetaName)enumInfos[i].EnumNameHash);

            mb.AddStructureInfo(MCMloRoomDef._MetaName);
        }

        public override void Parse(MetaFile meta, CMloRoomDef CMloRoomDef)
		{
			this.Meta = meta;
			this.MetaStructure = CMloRoomDef;

			this.Name = MetaUtils.GetString(Meta, CMloRoomDef.name);
			this.BbMin = CMloRoomDef.bbMin;
			this.BbMax = CMloRoomDef.bbMax;
			this.Blend = CMloRoomDef.blend;
			this.TimecycleName = CMloRoomDef.timecycleName;
			this.SecondaryTimecycleName = CMloRoomDef.secondaryTimecycleName;
			this.Flags = CMloRoomDef.flags;
			this.PortalCount = CMloRoomDef.portalCount;
			this.FloorId = CMloRoomDef.floorId;
			this.ExteriorVisibiltyDepth = CMloRoomDef.exteriorVisibiltyDepth;
			this.AttachedObjects = MetaUtils.ConvertDataArray<uint>(meta, CMloRoomDef.attachedObjects.Pointer, CMloRoomDef.attachedObjects.Count1)?.ToList();
        }

        public override void Build(MetaBuilder mb, bool isRoot = false)
		{
			this.MetaStructure.name = mb.AddStringPtr(this.Name);

            this.MetaStructure.bbMin = this.BbMin;
			this.MetaStructure.bbMax = this.BbMax;

			this.MetaStructure.blend = this.Blend;
			this.MetaStructure.timecycleName = this.TimecycleName;
			this.MetaStructure.secondaryTimecycleName = this.SecondaryTimecycleName;
			this.MetaStructure.flags = this.Flags;
			this.MetaStructure.portalCount = this.PortalCount;
			this.MetaStructure.floorId = this.FloorId;
			this.MetaStructure.exteriorVisibiltyDepth = this.ExteriorVisibiltyDepth;
            this.MetaStructure.attachedObjects = mb.AddUintArrayPtr(this.AttachedObjects.ToArray());

              MCMloRoomDef.AddEnumAndStructureInfo(mb);          

            if (isRoot)
			{
				mb.AddItem(this.MetaName, this.MetaStructure);

				this.Meta = mb.GetMeta();
			}
		}

        public void AddEntity(MCEntityDef entity)
        {
            var idx = this.AttachedObjects.IndexOf((uint) this.Parent.Entities.IndexOf(entity));

            if (idx != -1)
                return;

            this.AttachedObjects.Add((uint) idx);

        }

        public void RemoveEntity(MCEntityDef entity)
        {
            var idx = this.AttachedObjects.IndexOf((uint)this.Parent.Entities.IndexOf(entity));

            if (idx == -1)
                return;

            this.AttachedObjects.RemoveAt(idx);

        }
    }
}
