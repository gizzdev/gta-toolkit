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

    public struct Array_Structure //16 bytes - pointer for a structure array
    {
        public uint Pointer { get; set; }
        public uint Unk0 { get; set; }
        public ushort Count1 { get; set; }
        public ushort Count2 { get; set; }
        public uint Unk1 { get; set; }

        public uint PointerDataId { get { return (Pointer & 0xFFF); } }
        public uint PointerDataIndex { get { return (Pointer & 0xFFF) - 1; } }
        public uint PointerDataOffset { get { return ((Pointer >> 12) & 0xFFFFF); } }

        public Array_Structure(uint ptr, int cnt)
        {
            Pointer = ptr;
            Unk0 = 0;
            Count1 = (ushort)cnt;
            Count2 = Count1;
            Unk1 = 0;
        }

        public Array_Structure(MetaBuilderPointer ptr)
        {
            Pointer = ptr.Pointer;
            Unk0 = 0;
            Count1 = (ushort)ptr.Length;
            Count2 = Count1;
            Unk1 = 0;
        }

        public void SwapEnd()
        {
            Pointer = MetaUtils.SwapBytes(Pointer);
            Unk0 = MetaUtils.SwapBytes(Unk0);
            Count1 = MetaUtils.SwapBytes(Count1);
            Count2 = MetaUtils.SwapBytes(Count2);
            Unk1 = MetaUtils.SwapBytes(Unk1);
        }
        public override string ToString()
        {
            return "Array_Structure: " + PointerDataIndex.ToString() + " (" + Count1.ToString() + "/" + Count2.ToString() + ")";
        }
    }

    public struct Array_uint //16 bytes - pointer for a uint array
    {
        public uint Pointer { get; set; }
        public uint Unk0 { get; set; }
        public ushort Count1 { get; set; }
        public ushort Count2 { get; set; }
        public uint Unk1 { get; set; }

        public uint PointerDataId { get { return (Pointer & 0xFFF); } }
        public uint PointerDataIndex { get { return (Pointer & 0xFFF) - 1; } }
        public uint PointerDataOffset { get { return ((Pointer >> 12) & 0xFFFFF); } }


        public Array_uint(uint ptr, int cnt)
        {
            Pointer = ptr;
            Unk0 = 0;
            Count1 = (ushort)cnt;
            Count2 = Count1;
            Unk1 = 0;
        }

        public Array_uint(MetaBuilderPointer ptr)
        {
            Pointer = ptr.Pointer;
            Unk0 = 0;
            Count1 = (ushort)ptr.Length;
            Count2 = Count1;
            Unk1 = 0;
        }
 
        public void SwapEnd()
        {
            Pointer = MetaUtils.SwapBytes(Pointer);
            Unk0 = MetaUtils.SwapBytes(Unk0);
            Count1 = MetaUtils.SwapBytes(Count1);
            Count2 = MetaUtils.SwapBytes(Count2);
            Unk1 = MetaUtils.SwapBytes(Unk1);
        }
        public override string ToString()
        {
            return "Array_uint: " + PointerDataIndex.ToString() + " (" + Count1.ToString() + "/" + Count2.ToString() + ")";
        }
    }

    public struct Array_ushort //16 bytes - pointer for a ushort array
    {
        public uint Pointer { get; set; }
        public uint Unk0 { get; set; }
        public ushort Count1 { get; set; }
        public ushort Count2 { get; set; }
        public uint Unk1 { get; set; }

        public uint PointerDataId { get { return (Pointer & 0xFFF); } }
        public uint PointerDataIndex { get { return (Pointer & 0xFFF) - 1; } }
        public uint PointerDataOffset { get { return ((Pointer >> 12) & 0xFFFFF); } }


        public Array_ushort(uint ptr, int cnt)
        {
            Pointer = ptr;
            Unk0 = 0;
            Count1 = (ushort)cnt;
            Count2 = Count1;
            Unk1 = 0;
        }

        public Array_ushort(MetaBuilderPointer ptr)
        {
            Pointer = ptr.Pointer;
            Unk0 = 0;
            Count1 = (ushort)ptr.Length;
            Count2 = Count1;
            Unk1 = 0;
        }

        public void SwapEnd()
        {
            Pointer = MetaUtils.SwapBytes(Pointer);
            Unk0 = MetaUtils.SwapBytes(Unk0);
            Count1 = MetaUtils.SwapBytes(Count1);
            Count2 = MetaUtils.SwapBytes(Count2);
            Unk1 = MetaUtils.SwapBytes(Unk1);
        }
        public override string ToString()
        {
            return "Array_ushort: " + PointerDataIndex.ToString() + " (" + Count1.ToString() + "/" + Count2.ToString() + ")";
        }
    }

    public struct Array_byte //16 bytes - pointer for a byte array
    {
        public uint Pointer { get; set; }
        public uint Unk0 { get; set; }
        public ushort Count1 { get; set; }
        public ushort Count2 { get; set; }
        public uint Unk1 { get; set; }

        public uint PointerDataId { get { return (Pointer & 0xFFF); } }
        public uint PointerDataIndex { get { return (Pointer & 0xFFF) - 1; } }
        public uint PointerDataOffset { get { return ((Pointer >> 12) & 0xFFFFF); } }

        public Array_byte(uint ptr, int cnt)
        {
            Pointer = ptr;
            Unk0 = 0;
            Count1 = (ushort)cnt;
            Count2 = Count1;
            Unk1 = 0;
        }
 
        public Array_byte(MetaBuilderPointer ptr)
        {
            Pointer = ptr.Pointer;
            Unk0 = 0;
            Count1 = (ushort)ptr.Length;
            Count2 = Count1;
            Unk1 = 0;
        }

        public void SwapEnd()
        {
            Pointer = MetaUtils.SwapBytes(Pointer);
            Unk0 = MetaUtils.SwapBytes(Unk0);
            Count1 = MetaUtils.SwapBytes(Count1);
            Count2 = MetaUtils.SwapBytes(Count2);
            Unk1 = MetaUtils.SwapBytes(Unk1);
        }
        public override string ToString()
        {
            return "Array_byte: " + PointerDataIndex.ToString() + " (" + Count1.ToString() + "/" + Count2.ToString() + ")";
        }
    }

    public struct Array_float //16 bytes - pointer for a float array
    {
        public uint Pointer { get; set; }
        public uint Unk0 { get; set; }
        public ushort Count1 { get; set; }
        public ushort Count2 { get; set; }
        public uint Unk1 { get; set; }

        public uint PointerDataId { get { return (Pointer & 0xFFF); } }
        public uint PointerDataIndex { get { return (Pointer & 0xFFF) - 1; } }
        public uint PointerDataOffset { get { return ((Pointer >> 12) & 0xFFFFF); } }

        public Array_float(uint ptr, int cnt)
        {
            Pointer = ptr;
            Unk0 = 0;
            Count1 = (ushort)cnt;
            Count2 = Count1;
            Unk1 = 0;
        }

        public Array_float(MetaBuilderPointer ptr)
        {
            Pointer = ptr.Pointer;
            Unk0 = 0;
            Count1 = (ushort)ptr.Length;
            Count2 = Count1;
            Unk1 = 0;
        }

        public void SwapEnd()
        {
            Pointer = MetaUtils.SwapBytes(Pointer);
            Unk0 = MetaUtils.SwapBytes(Unk0);
            Count1 = MetaUtils.SwapBytes(Count1);
            Count2 = MetaUtils.SwapBytes(Count2);
            Unk1 = MetaUtils.SwapBytes(Unk1);
        }
        public override string ToString()
        {
            return "Array_float: " + PointerDataIndex.ToString() + " (" + Count1.ToString() + "/" + Count2.ToString() + ")";
        }
    }

    public struct Array_Vector3 //16 bytes - pointer for a Vector3 array
    {
        public uint Pointer { get; set; }
        public uint Unk0 { get; set; }
        public ushort Count1 { get; set; }
        public ushort Count2 { get; set; }
        public uint Unk1 { get; set; }

        public uint PointerDataId { get { return (Pointer & 0xFFF); } }
        public uint PointerDataIndex { get { return (Pointer & 0xFFF) - 1; } }
        public uint PointerDataOffset { get { return ((Pointer >> 12) & 0xFFFFF); } }

        public Array_Vector3(uint ptr, int cnt)
        {
            Pointer = ptr;
            Unk0 = 0;
            Count1 = (ushort)cnt;
            Count2 = Count1;
            Unk1 = 0;
        }

        public Array_Vector3(MetaBuilderPointer ptr)
        {
            Pointer = ptr.Pointer;
            Unk0 = 0;
            Count1 = (ushort)ptr.Length;
            Count2 = Count1;
            Unk1 = 0;
        }

        public void SwapEnd()
        {
            Pointer = MetaUtils.SwapBytes(Pointer);
            Unk0 = MetaUtils.SwapBytes(Unk0);
            Count1 = MetaUtils.SwapBytes(Count1);
            Count2 = MetaUtils.SwapBytes(Count2);
            Unk1 = MetaUtils.SwapBytes(Unk1);
        }
        public override string ToString()
        {
            return "Array_Vector3: " + PointerDataIndex.ToString() + " (" + Count1.ToString() + "/" + Count2.ToString() + ")";
        }
    }

    public struct CharPointer //16 bytes - pointer for a char array
    {
        public uint Pointer { get; set; }
        public uint Unk0 { get; set; }
        public ushort Count1 { get; set; }
        public ushort Count2 { get; set; }
        public uint Unk1 { get; set; }

        public uint PointerDataId { get { return (Pointer & 0xFFF); } }
        public uint PointerDataIndex { get { return (Pointer & 0xFFF) - 1; } }
        public uint PointerDataOffset { get { return ((Pointer >> 12) & 0xFFFFF); } }

        public CharPointer(uint ptr, int len)
        {
            Pointer = ptr;
            Unk0 = 0;
            Count1 = (ushort)len;
            Count2 = Count1;
            Unk1 = 0;
        }

        public CharPointer(MetaBuilderPointer ptr)
        {
            Pointer = ptr.Pointer;
            Unk0 = 0;
            Count1 = (ushort)ptr.Length;
            Count2 = Count1;
            Unk1 = 0;
        }

        public void SwapEnd()
        {
            Pointer = MetaUtils.SwapBytes(Pointer);
            Unk0 = MetaUtils.SwapBytes(Unk0);
            Count1 = MetaUtils.SwapBytes(Count1);
            Count2 = MetaUtils.SwapBytes(Count2);
            Unk1 = MetaUtils.SwapBytes(Unk1);
        }
        public override string ToString()
        {
            return "CharPointer: " + Pointer.ToString() + " (" + Count1.ToString() + "/" + Count2.ToString() + ")";
        }
    }
    
    public struct DataBlockPointer //8 bytes - pointer to data block
    {
        public uint Ptr0 { get; set; }
        public uint Ptr1 { get; set; }

        public uint PointerDataId { get { return (Ptr0 & 0xFFF); } }
        public uint PointerDataIndex { get { return (Ptr0 & 0xFFF) - 1; } }
        public uint PointerDataOffset { get { return ((Ptr0 >> 12) & 0xFFFFF); } }

        public override string ToString()
        {
            return "DataBlockPointer: " + Ptr0.ToString() + ", " + Ptr1.ToString();
        }

        public void SwapEnd()
        {
            Ptr0 = MetaUtils.SwapBytes(Ptr0);
            Ptr1 = MetaUtils.SwapBytes(Ptr1);
        }
    }

    public struct ArrayOfUshorts3 //array of 3 ushorts
    {
        public ushort u0, u1, u2;
        public override string ToString()
        {
            return u0.ToString() + ", " + u1.ToString() + ", " + u2.ToString();
        }
    }
    
    public struct ArrayOfBytes3 //array of 3 bytes
    {
        public byte b0, b1, b2;
        public override string ToString()
        {
            return b0.ToString() + ", " + b1.ToString() + ", " + b2.ToString();
        }
    }
    
    public struct ArrayOfBytes4 //array of 4 bytes
    {
        public byte b0, b1, b2, b3;
        public override string ToString()
        {
            return b0.ToString() + ", " + b1.ToString() + ", " + b2.ToString() + ", " + b3.ToString();
        }
    }
    
    public struct ArrayOfBytes5 //array of 5 bytes
    {
        public byte b0, b1, b2, b3, b4;
    }
    
    public struct ArrayOfBytes6 //array of 6 bytes
    {
        public byte b0, b1, b2, b3, b4, b5;
    }
    
    public struct ArrayOfBytes12 //array of 12 bytes
    {
        public byte b00, b01, b02, b03, b04, b05, b06, b07, b08, b09, b10, b11;
    }
    
    public struct ArrayOfChars64 //array of 64 chars (bytes)
    {
        public byte
            b00, b01, b02, b03, b04, b05, b06, b07, b08, b09, b10, b11, b12, b13, b14, b15, b16, b17, b18, b19,
            b20, b21, b22, b23, b24, b25, b26, b27, b28, b29, b30, b31, b32, b33, b34, b35, b36, b37, b38, b39,
            b40, b41, b42, b43, b44, b45, b46, b47, b48, b49, b50, b51, b52, b53, b54, b55, b56, b57, b58, b59,
            b60, b61, b62, b63;
        public override string ToString()
        {
            byte[] bytes =
            {
                b00, b01, b02, b03, b04, b05, b06, b07, b08, b09, b10, b11, b12, b13, b14, b15, b16, b17, b18, b19,
                b20, b21, b22, b23, b24, b25, b26, b27, b28, b29, b30, b31, b32, b33, b34, b35, b36, b37, b38, b39,
                b40, b41, b42, b43, b44, b45, b46, b47, b48, b49, b50, b51, b52, b53, b54, b55, b56, b57, b58, b59,
                b60, b61, b62, b63
            };
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                if (bytes[i] == 0) break;
                sb.Append((char)bytes[i]);
            }
            return sb.ToString();
        }
    }

    public struct MetaPOINTER //8 bytes - pointer to data item //was: SectionUNKNOWN10
    {
        public uint Pointer { get; set; }
        public uint ExtraOffset { get; set; }

        public int BlockIndex { get { return BlockID - 1; } }
        public int BlockID { get { return (int)(Pointer & 0xFFF); } set { Pointer = (Pointer & 0xFFFFF000) + ((uint)value & 0xFFF); } }
        public int Offset { get { return (int)((Pointer >> 12) & 0xFFFFF); } set { Pointer = (Pointer & 0xFFF) + (((uint)value << 12) & 0xFFFFF000); } }

        public MetaPOINTER(int blockID, int itemOffset, uint extra)
        {
            Pointer = (((uint)itemOffset << 12) & 0xFFFFF000) + ((uint)blockID & 0xFFF);
            ExtraOffset = extra;
        }

        public override string ToString()
        {
            return BlockID.ToString() + ", " + Offset.ToString() + ", " + ExtraOffset.ToString();
        }
    }

    public abstract class MetaWrapper
    {
        public virtual string Name { get { return ToString(); } }
        public abstract void Load(MetaFile meta, MetaPOINTER ptr);
        public abstract MetaPOINTER Save(MetaBuilder mb);
    }
}
