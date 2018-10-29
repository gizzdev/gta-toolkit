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

namespace RageLib.Resources.GTA5.PC.Meta
{
    public struct ArrayOfBytes12 //array of 12 bytes
    {
        public byte b00, b01, b02, b03, b04, b05, b06, b07, b08, b09, b10, b11;

        public void SetByte(int num, byte val)
        {
            switch(num)
            {
                case 0: b00 = val; break;
                case 1: b01 = val; break;
                case 2: b02 = val; break;
                case 3: b03 = val; break;
                case 4: b04 = val; break;
                case 5: b05 = val; break;
                case 6: b06 = val; break;
                case 7: b07 = val; break;
                case 8: b08 = val; break;
                case 9: b09 = val; break;
                case 10: b10 = val; break;
                case 11: b11 = val; break;
                default: break;
            }
        }

        public void SetBytes(byte[] vals)
        {
            for (int i = 0; i < vals.Length; i++)
                this.SetByte(i, vals[i]);
        }
    }
}
