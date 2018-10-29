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

namespace RageLib.Resources.GTA5.PC.Bounds
{
    // phBoundBase
    // phBound
    public class Bound : FileBase64_GTA5_pc, IResourceXXSystemBlock
    {
        public override long Length => 0x70;

        // structure data
        public byte Type;
        public byte Unknown_11h;
        public ushort Unknown_12h;
        public float BoundingSphereRadius;
        public uint Unknown_18h;
        public uint Unknown_1Ch;
        public RAGE_Vector3 BoundingBoxMax;
        public float Margin;
        public RAGE_Vector3 BoundingBoxMin;
        public uint Unknown_3Ch;
        public RAGE_Vector3 BoundingBoxCenter;
        public byte MaterialIndex;
        public byte ProceduralId;
        public byte RoomId_And_PedDensity; //5bits for RoomID and then 3bits for PedDensity
        public byte Unknown_4Fh;
        public RAGE_Vector3 CenterGravity;
        public byte PolyFlags;
        public byte MaterialColorIndex;
        public ushort Unknown_5Eh;
        public float Unknown_60h;
        public float Unknown_64h;
        public float Unknown_68h;
        public float BoundingBoxVolume;

        public int RoomId
        {
            get { return 0b00011111 & this.RoomId_And_PedDensity; }
            set { this.RoomId_And_PedDensity = (byte)((this.RoomId_And_PedDensity & ~0b00011111) | value); }
        }

        public int PedDensity
        {
            get { return (0b11100000 & this.RoomId_And_PedDensity) >> 5; }
            set { this.RoomId_And_PedDensity = (byte)((this.RoomId_And_PedDensity & ~0b11100000) | value << 5); }
        }

        /// <summary>
        /// Reads the data-block from a stream.
        /// </summary>
        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            base.Read(reader, parameters);

            // read structure data
            this.Type = reader.ReadByte();
            this.Unknown_11h = reader.ReadByte();
            this.Unknown_12h = reader.ReadUInt16();
            this.BoundingSphereRadius = reader.ReadSingle();
            this.Unknown_18h = reader.ReadUInt32();
            this.Unknown_1Ch = reader.ReadUInt32();
            this.BoundingBoxMax = reader.ReadBlock<RAGE_Vector3>();
            this.Margin = reader.ReadSingle();
            this.BoundingBoxMin = reader.ReadBlock<RAGE_Vector3>();
            this.Unknown_3Ch = reader.ReadUInt32();
            this.BoundingBoxCenter = reader.ReadBlock<RAGE_Vector3>();
            this.MaterialIndex = reader.ReadByte();
            this.ProceduralId = reader.ReadByte();
            this.RoomId_And_PedDensity = reader.ReadByte();
            this.Unknown_4Fh = reader.ReadByte();
            this.CenterGravity = reader.ReadBlock<RAGE_Vector3>();
            this.PolyFlags = reader.ReadByte();
            this.MaterialColorIndex = reader.ReadByte();
            this.Unknown_5Eh = reader.ReadUInt16();
            this.Unknown_60h = reader.ReadSingle();
            this.Unknown_64h = reader.ReadSingle();
            this.Unknown_68h = reader.ReadSingle();
            this.BoundingBoxVolume = reader.ReadSingle();
        }

        /// <summary>
        /// Writes the data-block to a stream.
        /// </summary>
        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            base.Write(writer, parameters);

            // write structure data
            writer.Write(this.Type);
            writer.Write(this.Unknown_11h);
            writer.Write(this.Unknown_12h);
            writer.Write(this.BoundingSphereRadius);
            writer.Write(this.Unknown_18h);
            writer.Write(this.Unknown_1Ch);
            writer.WriteBlock(this.BoundingBoxMax);
            writer.Write(this.Margin);
            writer.WriteBlock(this.BoundingBoxMin);
            writer.Write(this.Unknown_3Ch);
            writer.WriteBlock(this.BoundingBoxCenter);
            writer.Write(this.MaterialIndex);
            writer.Write(this.ProceduralId);
            writer.Write(this.RoomId_And_PedDensity);
            writer.Write(this.Unknown_4Fh);
            writer.WriteBlock(this.CenterGravity);
            writer.Write(this.PolyFlags);
            writer.Write(this.MaterialColorIndex);
            writer.Write(this.Unknown_5Eh);
            writer.Write(this.Unknown_60h);
            writer.Write(this.Unknown_64h);
            writer.Write(this.Unknown_68h);
            writer.Write(this.BoundingBoxVolume);
        }

        public IResourceSystemBlock GetType(ResourceDataReader reader, params object[] parameters)
        {
            reader.Position += 16;
            var type = reader.ReadByte();
            reader.Position -= 17;

            switch (type)
            {
                case 0: return new BoundSphere();
                case 1: return new BoundCapsule();
                case 3: return new BoundBox();
                case 4: return new BoundGeometry();
                case 8: return new BoundBVH();
                case 10: return new BoundComposite();
                case 12: return new BoundDisc();
                case 13: return new BoundCylinder();
                case 15: return new BoundPlane();
                default: throw new Exception("Unknown bound type");
            }
        }
    }
}
