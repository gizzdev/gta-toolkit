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

namespace RageLib.GTA5.Cryptography.Helpers
{
    public class GTA5NGLUT
    {
        public GTA5NGLUT()
        {
            LUT0 = new byte[256][];
            for (int i = 0; i < 256; i++)
            {
                LUT0[i] = new byte[256];
            }
            LUT1 = new byte[256][];
            for (int j = 0; j < 256; j++)
            {
                LUT1[j] = new byte[256];
            }
            Indices = new byte[65536];
        }

        public byte LookUp(uint value)
        {
            uint num = (value & 0xFFFF0000) >> 16;
            uint num2 = (value & 0xFF00) >> 8;
            uint num3 = value & 0xFF;
            return this.LUT0[(int)this.LUT1[(int)this.Indices[(int)num]][(int)num2]][(int)num3];
        }

        public byte[][] LUT0;

        public byte[][] LUT1;

        public byte[] Indices;
    }
}