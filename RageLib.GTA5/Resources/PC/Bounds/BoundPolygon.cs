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
    public class BoundPolygon : ResourceSystemBlock
    {
        public override long Length => 0x10;

        // structure data
        public byte[] data = new byte[0x10];

        private BoundPolygonBase _Polygon;
        public BoundPolygonBase Polygon { get { return this.GetPolygon(); } }

        /// <summary>
        /// Reads the data-block from a stream.
        /// </summary>
        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            // read structure data
            this.data = reader.ReadBytes(0x10);
        }

        /// <summary>
        /// Writes the data-block to a stream.
        /// </summary>
        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            // write structure data
            writer.Write(this.data);
        }

        public BoundPolygonBase GetPolygon()
        {
            byte b0 = this.data[0];

            BoundPolygonType type = (BoundPolygonType)(b0 & 7);

            BoundPolygonBase p = this._Polygon;

            if(p == null)
            {
                switch (type)
                {
                    case BoundPolygonType.Triangle: p = new BoundPolygonTriangle(); break;
                    case BoundPolygonType.Sphere: p = new BoundPolygonSphere(); break;
                    case BoundPolygonType.Capsule: p = new BoundPolygonCapsule(); break;
                    case BoundPolygonType.Box: p = new BoundPolygonBox(); break;
                    case BoundPolygonType.Cylinder: p = new BoundPolygonCylinder(); break;
                    default: break;
                }
            }

            if (p != null)
            {
                p.Read(this.data, 0);
            }

            return p;
        }
    }

    public enum BoundPolygonType
    {
        Triangle = 0,
        Sphere = 1,
        Capsule = 2,
        Box = 3,
        Cylinder = 4,
    }

    public abstract class BoundPolygonBase
    {
        public BoundPolygonType Type { get; set; }
        public abstract void Read(byte[] bytes, int offset);

        public override string ToString()
        {
            return Type.ToString();
        }
    }

    public class BoundPolygonTriangle : BoundPolygonBase
    {
        public float TriArea;
        public ushort TriIndex1;
        public ushort TriIndex2;
        public ushort TriIndex3;
        public short EdgeIndex1;
        public short EdgeIndex2;
        public short EdgeIndex3;

        public int VertIndex1 { get { return TriIndex1 & 0x7FFF; } }
        public int VertIndex2 { get { return TriIndex2 & 0x7FFF; } }
        public int VertIndex3 { get { return TriIndex3 & 0x7FFF; } }
        public bool VertFlag1 { get { return (TriIndex1 & 0x8000) > 0; } }
        public bool VertFlag2 { get { return (TriIndex2 & 0x8000) > 0; } }
        public bool VertFlag3 { get { return (TriIndex3 & 0x8000) > 0; } }


        public BoundPolygonTriangle()
        {
            Type = BoundPolygonType.Triangle;
        }
        public override void Read(byte[] bytes, int offset)
        {
            TriArea = BitConverter.ToSingle(bytes, offset + 0);
            TriIndex1 = BitConverter.ToUInt16(bytes, offset + 4);
            TriIndex2 = BitConverter.ToUInt16(bytes, offset + 6);
            TriIndex3 = BitConverter.ToUInt16(bytes, offset + 8);
            EdgeIndex1 = BitConverter.ToInt16(bytes, offset + 10);
            EdgeIndex2 = BitConverter.ToInt16(bytes, offset + 12);
            EdgeIndex3 = BitConverter.ToInt16(bytes, offset + 14);
        }

        public override string ToString()
        {
            return base.ToString() + ": " + VertIndex1.ToString() + ", " + VertIndex2.ToString() + ", " + VertIndex3.ToString();
        }
    }

    public class BoundPolygonSphere : BoundPolygonBase
    {
        public ushort SphereType;
        public ushort SphereIndex;
        public float SphereRadius;
        public uint Unused0;
        public uint Unused1;

        public BoundPolygonSphere()
        {
            Type = BoundPolygonType.Sphere;
        }
        public override void Read(byte[] bytes, int offset)
        {
            SphereType = BitConverter.ToUInt16(bytes, offset + 0);
            SphereIndex = BitConverter.ToUInt16(bytes, offset + 2);
            SphereRadius = BitConverter.ToSingle(bytes, offset + 4);
            Unused0 = BitConverter.ToUInt32(bytes, offset + 8);
            Unused1 = BitConverter.ToUInt32(bytes, offset + 12);
        }

        public override string ToString()
        {
            return base.ToString() + ": " + SphereIndex.ToString() + ", " + SphereRadius.ToString();
        }
    }

    public class BoundPolygonCapsule : BoundPolygonBase
    {
        public ushort CapsuleType;
        public ushort CapsuleIndex1;
        public float CapsuleRadius;
        public ushort CapsuleIndex2;
        public ushort Unused0;
        public uint Unused1;

        public BoundPolygonCapsule()
        {
            Type = BoundPolygonType.Capsule;
        }
        public override void Read(byte[] bytes, int offset)
        {
            CapsuleType = BitConverter.ToUInt16(bytes, offset + 0);
            CapsuleIndex1 = BitConverter.ToUInt16(bytes, offset + 2);
            CapsuleRadius = BitConverter.ToSingle(bytes, offset + 4);
            CapsuleIndex2 = BitConverter.ToUInt16(bytes, offset + 8);
            Unused0 = BitConverter.ToUInt16(bytes, offset + 10);
            Unused1 = BitConverter.ToUInt32(bytes, offset + 12);
        }

        public override string ToString()
        {
            return base.ToString() + ": " + CapsuleIndex1.ToString() + ", " + CapsuleIndex2.ToString() + ", " + CapsuleRadius.ToString();
        }
    }

    public class BoundPolygonBox : BoundPolygonBase
    {
        public uint BoxType;
        public short BoxIndex1;
        public short BoxIndex2;
        public short BoxIndex3;
        public short BoxIndex4;
        public uint Unused0;

        public BoundPolygonBox()
        {
            Type = BoundPolygonType.Box;
        }
        public override void Read(byte[] bytes, int offset)
        {
            BoxType = BitConverter.ToUInt32(bytes, offset + 0);
            BoxIndex1 = BitConverter.ToInt16(bytes, offset + 4);
            BoxIndex2 = BitConverter.ToInt16(bytes, offset + 6);
            BoxIndex3 = BitConverter.ToInt16(bytes, offset + 8);
            BoxIndex4 = BitConverter.ToInt16(bytes, offset + 10);
            Unused0 = BitConverter.ToUInt32(bytes, offset + 12);
        }

        public override string ToString()
        {
            return base.ToString() + ": " + BoxIndex1.ToString() + ", " + BoxIndex2.ToString() + ", " + BoxIndex3.ToString() + ", " + BoxIndex4.ToString();
        }
    }

    public class BoundPolygonCylinder : BoundPolygonBase
    {
        public ushort CylinderType;
        public ushort CylinderIndex1;
        public float CylinderRadius;
        public ushort CylinderIndex2;
        public ushort Unused0;
        public uint Unused1;

        public BoundPolygonCylinder()
        {
            Type = BoundPolygonType.Cylinder;
        }
        public override void Read(byte[] bytes, int offset)
        {
            CylinderType = BitConverter.ToUInt16(bytes, offset + 0);
            CylinderIndex1 = BitConverter.ToUInt16(bytes, offset + 2);
            CylinderRadius = BitConverter.ToSingle(bytes, offset + 4);
            CylinderIndex2 = BitConverter.ToUInt16(bytes, offset + 8);
            Unused0 = BitConverter.ToUInt16(bytes, offset + 10);
            Unused1 = BitConverter.ToUInt32(bytes, offset + 12);
        }

        public override string ToString()
        {
            return base.ToString() + ": " + CylinderIndex1.ToString() + ", " + CylinderIndex2.ToString() + ", " + CylinderRadius.ToString();
        }
    }
}
