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
using RageLib.GTA5.Resources.PC;
using RageLib.GTA5.ResourceWrappers.PC.Meta.Structures;

using RageLib.Resources.GTA5.PC.Meta;
using SharpDX;

namespace RageLib.Resources.GTA5.PC.GameFiles
{
    public class YmtPedDefinitionFile : GameFileBase_Resource<MetaFile>
    {
        public MUnk_376833625 Unk_376833625;

        public YmtPedDefinitionFile()
        {
            this.Unk_376833625 = new RageLib.GTA5.ResourceWrappers.PC.Meta.Structures.MUnk_376833625();
        }

        public override void Parse()
        {
            var Unk_376833625Blocks = this.ResourceFile.ResourceData.FindBlocks((MetaName)376833625);

            if (Unk_376833625Blocks.Length == 0)
                throw new Exception("Unk_376833625 block not found !");

            var Unk_376833625 = MetaUtils.ConvertData<Unk_376833625>(Unk_376833625Blocks[0]);
            this.Unk_376833625 = new RageLib.GTA5.ResourceWrappers.PC.Meta.Structures.MUnk_376833625();

            this.Unk_376833625.Parse(this.ResourceFile.ResourceData, Unk_376833625);
        }

        public override void Build()
        {
            var mb = new MetaBuilder();

            mb.EnsureBlock((MetaName)376833625);

            this.Unk_376833625.Build(mb, true);

            ResourceFile.Version = ResourceFileTypes_GTA5_pc.Meta.Version;
            ResourceFile.ResourceData = this.Unk_376833625.Meta;
        }

    }

}