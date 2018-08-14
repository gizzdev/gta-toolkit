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

using System.Text;

namespace RageLib.Resources.GTA5.PC.Meta
{
    public struct Array_StructurePointer //16 bytes - pointer for a structure pointer array
    {
        public uint Pointer { get; set; }
        public uint Unk0 { get; set; }
        public ushort Count1 { get; set; }
        public ushort Count2 { get; set; }
        public uint Unk1 { get; set; }

        public uint PointerDataId { get { return (Pointer & 0xFFF); } }
        public uint PointerDataIndex { get { return (Pointer & 0xFFF) - 1; } }
        public uint PointerDataOffset { get { return ((Pointer >> 12) & 0xFFFFF); } }

        public void SwapEnd()
        {
            Pointer = MetaUtils.SwapBytes(Pointer);
            Count1 = MetaUtils.SwapBytes(Count1);
            Count2 = MetaUtils.SwapBytes(Count2);
            Unk1 = MetaUtils.SwapBytes(Unk1);
        }
        public override string ToString()
        {
            return "Array_StructurePointer: " + PointerDataIndex.ToString() + " (" + Count1.ToString() + "/" + Count2.ToString() + ")";
        }
    }
}
