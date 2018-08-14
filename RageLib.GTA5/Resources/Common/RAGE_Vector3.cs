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

using SharpDX;

namespace RageLib.Resources
{
    public class RAGE_Vector3 : ResourceSystemBlock
    {
        public override long Length
        {
            get { return 12; }
        }

        // structure data
        public float x1;
        public float x2;
        public float x3;

        public RAGE_Vector3()
        {

        }

        public RAGE_Vector3(float x1, float x2, float x3)
        {
            this.x1 = x1;
            this.x2 = x2;
            this.x3 = x3;
        }

        /// <summary>
        /// Reads the data-block from a stream.
        /// </summary>
        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            // read structure data
            this.x1 = reader.ReadSingle();
            this.x2 = reader.ReadSingle();
            this.x3 = reader.ReadSingle();
        }

        /// <summary>
        /// Writes the data-block to a stream.
        /// </summary>
        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            // write structure data
            writer.Write(this.x1);
            writer.Write(this.x2);
            writer.Write(this.x3);
        }

        public static explicit operator Vector3(RAGE_Vector3 v)
        {
            return new Vector3(v.x1, v.x2, v.x3);
        }

        public static explicit operator RAGE_Vector3(Vector3 v)
        {
            return new RAGE_Vector3()
            {
                x1 = v.X,
                x2 = v.Y,
                x3 = v.Z,
            };
        }
    }
}