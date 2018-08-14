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
using RageLib.GTA5.PSO;
using System;

namespace RageLib.GTA5.ResourceWrappers.PC.Meta.Structures
{
	public class CMapTypes : MetaStructureWrapper<RageLib.Resources.GTA5.PC.Meta.CMapTypes>
	{
		public MetaFile Meta;
		public Array_StructurePointer Extensions = new Array_StructurePointer();
		public List<CBaseArchetypeDef> Archetypes = new List<CBaseArchetypeDef>();
        public List<CMloArchetypeDef> MloArchetypes = new List<CMloArchetypeDef>();
        public List<CTimeArchetypeDef> TimeArchetypes = new List<CTimeArchetypeDef>();
        public MetaName Name;
		public List<uint> Dependencies = new List<uint>();
		public List<CCompositeEntityType> CompositeEntityTypes = new List<CCompositeEntityType>();

		public CMapTypes()
		{
			this.MetaName = MetaName.CMapTypes;
			this.MetaStructure = new RageLib.Resources.GTA5.PC.Meta.CMapTypes();
		}

		public override void Parse(MetaFile meta, RageLib.Resources.GTA5.PC.Meta.CMapTypes CMapTypes)
		{
			this.Meta = meta;
			this.MetaStructure = CMapTypes;

            // this.Extensions = CMapTypes.extensions;

            // CBaseArchetypeDef + CMloArchetypeDef + CTimeArchetypeDef
            var archPtrs = MetaUtils.GetPointerArray(this.Meta, this.MetaStructure.archetypes);

            if (archPtrs != null)
            {
                for (int i = 0; i < archPtrs.Length; i++)
                {
                    var ptr = archPtrs[i];
                    var block = Meta.GetBlock(ptr.BlockID);

                    if (block == null)
                        continue;

                    if ((ptr.Offset < 0) || (block.Data == null) || (ptr.Offset >= block.Data.Length))
                        continue;

                    byte[] data = Array.ConvertAll(block.Data.ToArray(), e => (byte)e);

                    switch ((MetaName) block.StructureNameHash)
                    {
                        case MetaName.CBaseArchetypeDef:
                            {
                                var struc = PsoUtils.ConvertDataRaw<RageLib.Resources.GTA5.PC.Meta.CBaseArchetypeDef>(data, ptr.Offset);
                                var arch = new CBaseArchetypeDef();
                                arch.Parse(meta, struc);
                                this.Archetypes.Add(arch);
                                break;
                            }

                        case MetaName.CMloArchetypeDef:
                            {
                                var struc = PsoUtils.ConvertDataRaw<RageLib.Resources.GTA5.PC.Meta.CMloArchetypeDef>(data, ptr.Offset);
                                var arch = new CMloArchetypeDef();
                                arch.Parse(meta, struc);
                                this.MloArchetypes.Add(arch);
                                break;
                            }

                        case MetaName.CTimeArchetypeDef:
                            {
                                var struc = PsoUtils.ConvertDataRaw<RageLib.Resources.GTA5.PC.Meta.CTimeArchetypeDef>(data, ptr.Offset);
                                var arch = new CTimeArchetypeDef();
                                arch.Parse(meta, struc);
                                this.TimeArchetypes.Add(arch);
                                break;
                            }

                        default: continue;
                    }
                }
            }

            this.Name = (MetaName) CMapTypes.name;
			// this.Dependencies = CMapTypes.dependencies;
			var compositeEntityTypes = MetaUtils.ConvertDataArray<RageLib.Resources.GTA5.PC.Meta.CCompositeEntityType>(meta, CMapTypes.compositeEntityTypes);
			this.CompositeEntityTypes = compositeEntityTypes?.Select(e => { var msw = new CCompositeEntityType(); msw.Parse(meta, e); return msw; }).ToList();

		}

		public override void Build(MetaBuilder mb, bool isRoot = false)
		{
            // this.MetaStructure.extensions = this.Extensions;

            // CBaseArchetypeDef + CMloArchetypeDef + CTimeArchetypeDef
            var archetypePtrs = new List<MetaPOINTER>();
            this.AddMetaPointers(mb, archetypePtrs, MetaName.CBaseArchetypeDef, this.Archetypes.Select(e => { e.Build(mb); return e.MetaStructure; }));
            this.AddMetaPointers(mb, archetypePtrs, MetaName.CMloArchetypeDef, this.MloArchetypes.Select(e => { e.Build(mb); return e.MetaStructure; }));
            this.AddMetaPointers(mb, archetypePtrs, MetaName.CTimeArchetypeDef, this.TimeArchetypes.Select(e => { e.Build(mb); return e.MetaStructure; }));
            this.MetaStructure.archetypes = mb.AddPointerArray(archetypePtrs.ToArray());

            this.MetaStructure.name = (uint) this.Name;
			this.MetaStructure.dependencies = mb.AddUintArrayPtr(this.Dependencies.ToArray());
			if(this.CompositeEntityTypes != null)
				this.MetaStructure.compositeEntityTypes = mb.AddItemArrayPtr(MetaName.CCompositeEntityType, this.CompositeEntityTypes.Select(e => e.MetaStructure).ToArray());

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
