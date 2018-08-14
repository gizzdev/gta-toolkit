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
	public class CBlockDesc : MetaStructureWrapper<RageLib.Resources.GTA5.PC.Meta.CBlockDesc>
	{
		public MetaFile Meta;
		public uint Version;
		public uint Flags;
		public string Name = "";
		public string ExportedBy = "";
		public string Owner = "";
		public string Time = "";

		public CBlockDesc()
		{
			this.MetaName = MetaName.CBlockDesc;
			this.MetaStructure = new RageLib.Resources.GTA5.PC.Meta.CBlockDesc();
		}

		public override void Parse(MetaFile meta, RageLib.Resources.GTA5.PC.Meta.CBlockDesc CBlockDesc)
		{
			this.Meta = meta;
			this.MetaStructure = CBlockDesc;

			this.Version = CBlockDesc.version;
			this.Flags = CBlockDesc.flags;
			this.Name = MetaUtils.GetString(Meta, CBlockDesc.name);
			this.ExportedBy = MetaUtils.GetString(Meta, CBlockDesc.exportedBy);
			this.Owner = MetaUtils.GetString(Meta, CBlockDesc.owner);
			this.Time = MetaUtils.GetString(Meta, CBlockDesc.time);
		}

		public override void Build(MetaBuilder mb, bool isRoot = false)
		{
			this.MetaStructure.version = this.Version;
			this.MetaStructure.flags = this.Flags;
			this.MetaStructure.name = mb.AddStringPtr(this.Name);
			this.MetaStructure.exportedBy = mb.AddStringPtr(this.ExportedBy);
			this.MetaStructure.owner = mb.AddStringPtr(this.Owner);
			this.MetaStructure.time = mb.AddStringPtr(this.Time);

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
