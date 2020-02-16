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
using RageLib.GTA5.Resources.PC;
using RageLib.GTA5.ResourceWrappers.PC.Meta.Structures;
using RageLib.Resources.GTA5.PC.Meta;

namespace RageLib.Resources.GTA5.PC.GameFiles
{
    public class YmapFile : GameFileBase_Resource<MetaFile>
    {
        public MCMapData CMapData;

        public YmapFile()
        {
            this.CMapData = new MCMapData();
        }

        public override void Parse(object[] parameters = null)
        {
            bool parseFast = false;

            if (parameters != null && parameters.Length > 0)
                parseFast = (bool) parameters[0];

            var CMapDataBlocks = this.ResourceFile.ResourceData.FindBlocks(MetaName.CMapData);

            if (CMapDataBlocks.Length == 0)
                throw new Exception("CMapData block not found !");

            var CMapData = MetaUtils.ConvertData<CMapData>(CMapDataBlocks[0]);
            this.CMapData = new MCMapData();

            if(parseFast)
                this.CMapData.ParseFast(this.ResourceFile.ResourceData, CMapData);
            else
                this.CMapData.Parse(this.ResourceFile.ResourceData, CMapData);

            for (int i = 0; i < this.CMapData.Entities.Count; i++)
                this.CMapData.Entities[i].ParentIndex = this.CMapData.Entities[i].ParentIndex;
        }

        public override void Build(object[] parameters = null)
        {
            for (int i = 0; i < this.CMapData.Entities.Count; i++)
                this.CMapData.Entities[i].ParentEntity = this.CMapData.Entities[i].ParentEntity;

            var mb = new MetaBuilder();

            mb.EnsureBlock(MetaName.CMapData);

            this.CMapData.Build(mb, true);

            ResourceFile.Version = ResourceFileTypes_GTA5_pc.Maps.Version;
            ResourceFile.ResourceData = this.CMapData.Meta;
        }

    }

}