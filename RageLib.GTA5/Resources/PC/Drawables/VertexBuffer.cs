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

namespace RageLib.Resources.GTA5.PC.Drawables
{
    public enum VertexType : uint
    {
        Default = 89, //PNCT
        DefaultEx = 16473, //PNCTX
        PNCCT = 121,
        PNCCTTTT = 1017,
        PBBNCCTTX = 16639,
        PBBNCCT = 127,
        PNCTTTX = 16857,
        PNCTTX = 16601,
        PNCTTTX_2 = 19545,
        PNCTTTX_3 = 17113,
        PNCCTTX = 16633,
        PNCCTTX_2 = 17017,
        PNCCTTTX = 17145,
        PBBNCCTX = 16511,
        PBBNCTX = 16479,
        PBBNCT = 95,
        PNCCTT = 249,
        PNCCTX = 16505,
        PCT = 81,
        PT = 65,
        PTT = 193,
        PNC = 25,
        PC = 17,
        PCC = 7,
        PCCH2H4 = 2147500121, //0x80004059  (16473 + 0x80000000) DefaultEx Cloth?
        PNCH2 = 2147483737, //0x80000059  (89 + 0x80000000) Default Cloth?
        PNCTTTTX = 19673,  //normal_spec_detail_dpm_vertdecal_tnt
        PNCTTTT = 985,
        PBBNCCTT = 255,
        PCTT = 209,
        PBBCCT = 119,
        PBBNC = 31,
        PBBNCTT = 223,
        PBBNCTTX = 16607,
        PBBNCTTT = 479,
        PNCTT = 217,
        PNCTTT = 473,
        PBBNCTTTX = 16863,
    }

    // datBase
    // grcVertexBuffer
    // grcVertexBufferD3D11
    public class VertexBuffer : ResourceSystemBlock
    {
        public override long Length => 0x80;

        // structure data
        public uint VFT;
        public uint Unknown_4h; // 0x00000001
        public ushort VertexStride;
        public ushort Unknown_Ah;
        public uint Unknown_Ch; // 0x00000000
        public ulong DataPointer1;
        public uint VertexCount;
        public uint Unknown_1Ch; // 0x00000000
        public ulong DataPointer2;
        public uint Unknown_28h; // 0x00000000
        public uint Unknown_2Ch; // 0x00000000
        public ulong InfoPointer;
        public uint Unknown_38h; // 0x00000000
        public uint Unknown_3Ch; // 0x00000000
        public uint Unknown_40h; // 0x00000000
        public uint Unknown_44h; // 0x00000000
        public uint Unknown_48h; // 0x00000000
        public uint Unknown_4Ch; // 0x00000000
        public uint Unknown_50h; // 0x00000000
        public uint Unknown_54h; // 0x00000000
        public uint Unknown_58h; // 0x00000000
        public uint Unknown_5Ch; // 0x00000000
        public uint Unknown_60h; // 0x00000000
        public uint Unknown_64h; // 0x00000000
        public uint Unknown_68h; // 0x00000000
        public uint Unknown_6Ch; // 0x00000000
        public uint Unknown_70h; // 0x00000000
        public uint Unknown_74h; // 0x00000000
        public uint Unknown_78h; // 0x00000000
        public uint Unknown_7Ch; // 0x00000000

        // reference data
        public VertexData_GTA5_pc Data1;
        public VertexData_GTA5_pc Data2;
        public VertexDeclaration Info;

        /// <summary>
        /// Reads the data-block from a stream.
        /// </summary>
        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            // read structure data
            this.VFT = reader.ReadUInt32();
            this.Unknown_4h = reader.ReadUInt32();
            this.VertexStride = reader.ReadUInt16();
            this.Unknown_Ah = reader.ReadUInt16();
            this.Unknown_Ch = reader.ReadUInt32();
            this.DataPointer1 = reader.ReadUInt64();
            this.VertexCount = reader.ReadUInt32();
            this.Unknown_1Ch = reader.ReadUInt32();
            this.DataPointer2 = reader.ReadUInt64();
            this.Unknown_28h = reader.ReadUInt32();
            this.Unknown_2Ch = reader.ReadUInt32();
            this.InfoPointer = reader.ReadUInt64();
            this.Unknown_38h = reader.ReadUInt32();
            this.Unknown_3Ch = reader.ReadUInt32();
            this.Unknown_40h = reader.ReadUInt32();
            this.Unknown_44h = reader.ReadUInt32();
            this.Unknown_48h = reader.ReadUInt32();
            this.Unknown_4Ch = reader.ReadUInt32();
            this.Unknown_50h = reader.ReadUInt32();
            this.Unknown_54h = reader.ReadUInt32();
            this.Unknown_58h = reader.ReadUInt32();
            this.Unknown_5Ch = reader.ReadUInt32();
            this.Unknown_60h = reader.ReadUInt32();
            this.Unknown_64h = reader.ReadUInt32();
            this.Unknown_68h = reader.ReadUInt32();
            this.Unknown_6Ch = reader.ReadUInt32();
            this.Unknown_70h = reader.ReadUInt32();
            this.Unknown_74h = reader.ReadUInt32();
            this.Unknown_78h = reader.ReadUInt32();
            this.Unknown_7Ch = reader.ReadUInt32();

            // read reference data
            this.Info = reader.ReadBlockAt<VertexDeclaration>(
                this.InfoPointer // offset
            );
            this.Data1 = reader.ReadBlockAt<VertexData_GTA5_pc>(
                this.DataPointer1, // offset
                this.VertexStride,
                this.VertexCount,
                this.Info
            );
            this.Data2 = reader.ReadBlockAt<VertexData_GTA5_pc>(
                this.DataPointer2, // offset
                this.VertexStride,
                this.VertexCount,
                this.Info
            );
        }

        /// <summary>
        /// Writes the data-block to a stream.
        /// </summary>
        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            // update structure data
            this.VertexCount = (uint)(this.Data1 != null ? this.Data1.VertexCount : 0);
            this.DataPointer1 = (ulong)(this.Data1 != null ? this.Data1.Position : 0);
            this.DataPointer2 = (ulong)(this.Data2 != null ? this.Data2.Position : 0);
            this.InfoPointer = (ulong)(this.Info != null ? this.Info.Position : 0);

            // write structure data
            writer.Write(this.VFT);
            writer.Write(this.Unknown_4h);
            writer.Write(this.VertexStride);
            writer.Write(this.Unknown_Ah);
            writer.Write(this.Unknown_Ch);
            writer.Write(this.DataPointer1);
            writer.Write(this.VertexCount);
            writer.Write(this.Unknown_1Ch);
            writer.Write(this.DataPointer2);
            writer.Write(this.Unknown_28h);
            writer.Write(this.Unknown_2Ch);
            writer.Write(this.InfoPointer);
            writer.Write(this.Unknown_38h);
            writer.Write(this.Unknown_3Ch);
            writer.Write(this.Unknown_40h);
            writer.Write(this.Unknown_44h);
            writer.Write(this.Unknown_48h);
            writer.Write(this.Unknown_4Ch);
            writer.Write(this.Unknown_50h);
            writer.Write(this.Unknown_54h);
            writer.Write(this.Unknown_58h);
            writer.Write(this.Unknown_5Ch);
            writer.Write(this.Unknown_60h);
            writer.Write(this.Unknown_64h);
            writer.Write(this.Unknown_68h);
            writer.Write(this.Unknown_6Ch);
            writer.Write(this.Unknown_70h);
            writer.Write(this.Unknown_74h);
            writer.Write(this.Unknown_78h);
            writer.Write(this.Unknown_7Ch);
        }

        /// <summary>
        /// Returns a list of data blocks which are referenced by this block.
        /// </summary>
        public override IResourceBlock[] GetReferences()
        {
            var list = new List<IResourceBlock>();
            if (Data1 != null) list.Add(Data1);
            if (Data2 != null) list.Add(Data2);
            if (Info != null) list.Add(Info);
            return list.ToArray();
        }
    }

    public class VertexData_GTA5_pc : ResourceSystemBlock
    {


        // private int length = 0;
        public override long Length
        {
            get
            {
                return VertexBytes?.Length ?? 0; //this.length;
            }
        }
        public VertexDeclaration info { get; set; }
        public object[] Data { get; set; }
        public uint[] Types { get; set; }
        public VertexType VertexType { get; set; }
        public byte[] VertexBytes { get; set; }
        public int VertexCount { get; set; }
        public int VertexStride { get; set; }




        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            VertexStride = Convert.ToInt32(parameters[0]);
            VertexCount = Convert.ToInt32(parameters[1]);
            info = (VertexDeclaration)parameters[2];

            VertexType = (VertexType)info.Flags;

            VertexBytes = reader.ReadBytes(VertexCount * VertexStride);

            switch (info.Types)
            {
                case 8598872888530528662: //YDR - 0x7755555555996996
                    break;
                case 216172782140628998:  //YFT - 0x030000000199A006
                    switch (info.Flags)
                    {
                        case 16473: VertexType = VertexType.PCCH2H4; break;  //  PCCH2H4 
                        default: break;
                    }
                    break;
                case 216172782140612614:  //YFT - 0x0300000001996006  PNCH2H4
                    switch (info.Flags)
                    {
                        case 89: VertexType = VertexType.PNCH2; break;     //  PNCH2
                        default: break;
                    }
                    break;
                default:
                    break;
            }
        }

        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            if (VertexBytes != null)
            {
                writer.Write(VertexBytes); //not dealing with individual vertex data here any more!
            }
        }

    }
}
