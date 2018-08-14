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

using System;
using System.Collections.Generic;
using RageLib.Resources.GTA5.PC.Meta;
using SharpDX;

namespace RageLib.Resources.GTA5.PC.GameFiles
{
    public class YmapFile : GameFileBase_Resource<MetaFile>
    {
        public RageLib.GTA5.ResourceWrappers.PC.Meta.Structures.CMapData CMapData;

        public YmapFile()
        {
            this.CMapData = new RageLib.GTA5.ResourceWrappers.PC.Meta.Structures.CMapData();
        }

        public override void Parse()
        {
            var CMapDataBlocks = this.ResourceFile.ResourceData.FindBlocks(MetaName.CMapData);

            if (CMapDataBlocks.Length == 0)
                throw new Exception("CMapData block not found !");

            var CMapData = MetaUtils.ConvertData<CMapData>(CMapDataBlocks[0]);
            this.CMapData = new RageLib.GTA5.ResourceWrappers.PC.Meta.Structures.CMapData();

            this.CMapData.Parse(this.ResourceFile.ResourceData, CMapData);
        }

        public override void Build()
        {
            var mb = new MetaBuilder();

            mb.EnsureBlock(MetaName.CMapData);

            this.CMapData.Build(mb, true);

            ResourceFile.Version = 2;
            ResourceFile.ResourceData = this.CMapData.Meta;
        }

    }

}