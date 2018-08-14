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

namespace RageLib.Resources
{
    public class RAGE_Matrix4 : ResourceSystemBlock
    {
        public override long Length
        {
            get { return 64; }
        }

        // structure data
        public float m11;
        public float m12;
        public float m13;
        public float m14;
        public float m21;
        public float m22;
        public float m23;
        public float m24;
        public float m31;
        public float m32;
        public float m33;
        public float m34;
        public float m41;
        public float m42;
        public float m43;
        public float m44;

        public RAGE_Matrix4()
        {

        }


        public RAGE_Matrix4(float m11, float m12, float m13, float m14, float m21, float m22, float m23, float m24, float m31, float m32, float m33, float m34, float m41, float m42, float m43, float m44)
        {
            this.m11 = m11;
            this.m12 = m12;
            this.m13 = m13;
            this.m14 = m14;
            this.m21 = m21;
            this.m22 = m22;
            this.m23 = m23;
            this.m24 = m24;
            this.m31 = m31;
            this.m32 = m32;
            this.m33 = m33;
            this.m34 = m34;
            this.m41 = m41;
            this.m42 = m42;
            this.m43 = m43;
            this.m44 = m44;
        }

        /// <summary>
        /// Reads the data-block from a stream.
        /// </summary>
        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            // read structure data
            this.m11 = reader.ReadSingle();
            this.m12 = reader.ReadSingle();
            this.m13 = reader.ReadSingle();
            this.m14 = reader.ReadSingle();
            this.m21 = reader.ReadSingle();
            this.m22 = reader.ReadSingle();
            this.m23 = reader.ReadSingle();
            this.m24 = reader.ReadSingle();
            this.m31 = reader.ReadSingle();
            this.m32 = reader.ReadSingle();
            this.m33 = reader.ReadSingle();
            this.m34 = reader.ReadSingle();
            this.m41 = reader.ReadSingle();
            this.m42 = reader.ReadSingle();
            this.m43 = reader.ReadSingle();
            this.m44 = reader.ReadSingle();
        }

        /// <summary>
        /// Writes the data-block to a stream.
        /// </summary>
        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            // write structure data
            writer.Write(this.m11);
            writer.Write(this.m12);
            writer.Write(this.m13);
            writer.Write(this.m14);
            writer.Write(this.m21);
            writer.Write(this.m22);
            writer.Write(this.m23);
            writer.Write(this.m24);
            writer.Write(this.m31);
            writer.Write(this.m32);
            writer.Write(this.m33);
            writer.Write(this.m34);
            writer.Write(this.m41);
            writer.Write(this.m42);
            writer.Write(this.m43);
            writer.Write(this.m44);
        }
    }
}