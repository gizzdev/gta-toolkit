/*
    Copyright(c) 2017 Neodymium

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

namespace RageLib.Resources.GTA5.PC.Bounds
{
    public class BoundMaterial : ResourceSystemBlock
    {
        public override long Length => 8;

        // structure data
        public byte MaterialIndex;
        public byte ProcIc;
        public byte RoomId_And_PedDensity;  // First 5 bits for RoomId, Last 3 bits for PedDensity
        public byte Unknown_3h;
        public byte Unknown_4h;
        public byte MaterialColorIndex;
        public short Unknown_6h;

        public int RoomId
        {
            get
            {
                return 0b00011111 & this.RoomId_And_PedDensity;
            }
            
            set
            {
                this.RoomId_And_PedDensity = (byte) ((this.RoomId_And_PedDensity & ~0b00011111) | value);
            }
        }

        public int PedDensity
        {
            get
            {
                return (0b11100000 & this.RoomId_And_PedDensity) >> 5;
            }

            set
            {
                this.RoomId_And_PedDensity = (byte)((this.RoomId_And_PedDensity & ~0b11100000) | value << 5);
            }
        }

        /// <summary>
        /// Reads the data-block from a stream.
        /// </summary>
        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            // read structure data
            this.MaterialIndex = reader.ReadByte();
            this.ProcIc = reader.ReadByte();
            this.RoomId_And_PedDensity = reader.ReadByte();
            this.Unknown_3h = reader.ReadByte();
            this.Unknown_4h = reader.ReadByte();
            this.MaterialColorIndex = reader.ReadByte();
            this.Unknown_6h = reader.ReadInt16(); ;
        }

        /// <summary>
        /// Writes the data-block to a stream.
        /// </summary>
        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            // write structure data
            writer.Write(this.MaterialIndex);
            writer.Write(this.ProcIc);
            writer.Write(this.RoomId_And_PedDensity);
            writer.Write(this.Unknown_3h);
            writer.Write(this.Unknown_4h);
            writer.Write(this.MaterialColorIndex);
            writer.Write(this.Unknown_6h);
        }
    }
}
