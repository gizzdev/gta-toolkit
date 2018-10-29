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
using System.IO;
using System.Text;
using RageLib.Resources.GTA5.PC.Meta;
using SharpDX;

namespace RageLib.Resources.GTA5.PC.GameFiles
{
    public class Gxt2File : GameFileBase
    {
        public uint EntryCount = 0;
        public List<Gxt2Entry> TextEntries = new List<Gxt2Entry>();

        public override void Parse()
        {
            using (BinaryReader br = new BinaryReader(this.Stream))
            {
                uint gxt2 = br.ReadUInt32(); //"GXT2" - 1196971058

                if (gxt2 != 1196971058)
                    return;

                EntryCount = br.ReadUInt32();
                TextEntries = new List<Gxt2Entry>();

                for (uint i = 0; i < EntryCount; i++)
                {
                    var e = new Gxt2Entry();
                    e.Hash = br.ReadUInt32();
                    e.Offset = br.ReadUInt32();
                    TextEntries.Add(e);
                }

                gxt2 = br.ReadUInt32(); //another "GXT2"
                if (gxt2 != 1196971058)
                { return; }

                uint endpos = br.ReadUInt32();

                List<byte> buf = new List<byte>();

                for (uint i = 0; i < EntryCount; i++)
                {
                    var e = TextEntries[(int)i];
                    br.BaseStream.Position = e.Offset;

                    buf.Clear();
                    byte b = br.ReadByte();
                    while ((b != 0) && (br.BaseStream.Position < endpos))
                    {
                        buf.Add(b);
                        b = br.ReadByte();
                    }

                    e.Text = Encoding.UTF8.GetString(buf.ToArray());
                }

            }
        }

        public override void Build()
        {

        }
    }

    public class Gxt2Entry
    {
        public uint Hash;
        public uint Offset;
        public string Text;
    }
}