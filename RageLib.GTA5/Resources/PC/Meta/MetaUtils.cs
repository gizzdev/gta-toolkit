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
using System.Runtime.InteropServices;
using System.Text;
using RageLib.Resources.Common;
using SharpDX;

namespace RageLib.Resources.GTA5.PC.Meta
{
    public class MetaUtils
    {
        public static ushort SwapBytes(ushort x)
        {
            return (ushort)(((x & 0xFF00) >> 8) | ((x & 0x00FF) << 8));
        }
        public static uint SwapBytes(uint x)
        {
            // swap adjacent 16-bit blocks
            x = (x >> 16) | (x << 16);
            // swap adjacent 8-bit blocks
            return ((x & 0xFF00FF00) >> 8) | ((x & 0x00FF00FF) << 8);
        }
        public static int SwapBytes(int x)
        {
            return (int)SwapBytes((uint)x);
        }
        public static ulong SwapBytes(ulong x)
        {
            // swap adjacent 32-bit blocks
            x = (x >> 32) | (x << 32);
            // swap adjacent 16-bit blocks
            x = ((x & 0xFFFF0000FFFF0000) >> 16) | ((x & 0x0000FFFF0000FFFF) << 16);
            // swap adjacent 8-bit blocks
            return ((x & 0xFF00FF00FF00FF00) >> 8) | ((x & 0x00FF00FF00FF00FF) << 8);
        }
        public static float SwapBytes(float f)
        {
            var a = BitConverter.GetBytes(f);
            Array.Reverse(a);
            return BitConverter.ToSingle(a, 0);
        }

        public static Vector2 SwapBytes(Vector2 v)
        {
            var x = SwapBytes(v.X);
            var y = SwapBytes(v.Y);
            return new Vector2(x, y);
        }

        public static Vector3 SwapBytes(Vector3 v)
        {
            var x = SwapBytes(v.X);
            var y = SwapBytes(v.Y);
            var z = SwapBytes(v.Z);
            return new Vector3(x, y, z);
        }
        public static Vector4 SwapBytes(Vector4 v)
        {
            var x = SwapBytes(v.X);
            var y = SwapBytes(v.Y);
            var z = SwapBytes(v.Z);
            var w = SwapBytes(v.W);
            return new Vector4(x, y, z, w);
        }

        public static byte[] ConvertToBytes<T>(T item) where T : struct
        {
            int size = Marshal.SizeOf(typeof(T));
            int offset = 0;
            byte[] arr = new byte[size];
            IntPtr ptr = Marshal.AllocHGlobal(size);
            Marshal.StructureToPtr(item, ptr, true);
            Marshal.Copy(ptr, arr, 0, size);
            Marshal.FreeHGlobal(ptr);
            offset += size;
            return arr;
        }

        public static byte[] ConvertArrayToBytes<T>(params T[] items) where T : struct
        {
            if (items == null) return null;
            int size = Marshal.SizeOf(typeof(T));
            int sizetot = size * items.Length;
            byte[] arrout = new byte[sizetot];
            int offset = 0;
            for (int i = 0; i < items.Length; i++)
            {
                byte[] arr = new byte[size];
                IntPtr ptr = Marshal.AllocHGlobal(size);
                Marshal.StructureToPtr(items[i], ptr, true);
                Marshal.Copy(ptr, arr, 0, size);
                Marshal.FreeHGlobal(ptr);
                Buffer.BlockCopy(arr, 0, arrout, offset, size);
                offset += size;
            }
            return arrout;
        }

        public static T ConvertData<T>(byte[] data, int offset = 0) where T : struct
        {
            GCHandle handle = GCHandle.Alloc(data, GCHandleType.Pinned);
            var h = handle.AddrOfPinnedObject();
            var r = Marshal.PtrToStructure<T>(h + offset);
            handle.Free();
            return r;
        }

        public static T ConvertData<T>(ResourceSimpleArray<byte_r> byteRArray, int offset = 0) where T : struct
        {
            byte[] data = Array.ConvertAll(byteRArray.Data.ToArray(), e => (byte)e);
            return ConvertData<T>(data, offset);
        }

        public static T ConvertData<T>(DataBlock block, int offset = 0) where T : struct
        {
            return ConvertData<T>(block.Data, offset);
        }

        public static T[] ConvertDataArray<T>(MetaFile meta, Array_Structure array) where T : struct
        {
            return ConvertDataArray<T>(meta, array.Pointer, array.Count1);
        }

        public static T[] ConvertDataArray<T>(MetaFile meta, uint pointer, uint count) where T : struct
        {
            T[] items = new T[count];
            int itemSize = Marshal.SizeOf(typeof(T));
            int itemsLeft = (int)count;

            uint ptrIndex = (pointer & 0xFFF) - 1;
            uint ptrOffset = (pointer >> 12) & 0xFFFFF;
            var ptrblock = (ptrIndex < meta.DataBlocks.Count) ? meta.DataBlocks[(int)ptrIndex] : null;

            if (ptrblock == null)
                return items;

            int byteoffset = (int)ptrOffset;
            int itemoffset = byteoffset / itemSize;

            int c = 0;

            while (itemsLeft > 0)
            {
                int blockcount = ptrblock.DataLength / itemSize;

                int itemcount = blockcount - itemoffset;

                if (itemcount > itemsLeft)
                    itemcount = itemsLeft;

                for (int i = 0; i < itemcount; i++)
                {
                    int offset = (itemoffset + i) * itemSize;
                    int index = c + i;
                    items[index] = ConvertData<T>(ptrblock, offset);
                }
                itemoffset = 0;
                c += itemcount;
                itemsLeft -= itemcount;

                if (itemsLeft <= 0)
                    return items;

                ptrIndex++;
                ptrblock = (ptrIndex < meta.DataBlocks.Count) ? meta.DataBlocks[(int)ptrIndex] : null;

                if (ptrblock == null)
                    return items;

            }

            return items;

        }

        public static T[] ConvertDataArray<T>(MetaFile meta, Array_StructurePointer array) where T : struct
        {
            uint count = array.Count1;

            if (count == 0)
                return null;

            MetaPOINTER[] ptrs = GetPointerArray(meta, array);

            if (ptrs == null)
                return null;

            T[] items = new T[count];
            int itemsize = Marshal.SizeOf(typeof(T));
            int itemsleft = (int)count; //large arrays get split into chunks...

            //MetaName blocktype = 0;
            for (int i = 0; i < count; i++)
            {
                var ptr = ptrs[i];
                var offset = ptr.Offset;
                var block = meta.GetBlock(ptr.BlockID);

                if (block == null)
                    continue;

                if ((offset < 0) || (block.Data == null) || (offset >= block.Data.Count))
                    continue;

                items[i] = ConvertData<T>(block.Data, offset);
            }

            return items;
        }

        public static T[] ConvertDataArray<T>(byte[] data, int offset, int count) where T : struct
        {
            T[] items = new T[count];
            int itemsize = Marshal.SizeOf(typeof(T));
            for (int i = 0; i < count; i++)
            {
                int off = offset + i * itemsize;
                items[i] = ConvertData<T>(data, off);
            }
            return items;
        }

        public static T GetTypedData<T>(MetaFile meta, MetaName name) where T : struct
        {
            foreach (var block in meta.DataBlocks)
            {
                if ((MetaName) block.StructureNameHash == name)
                {
                    return ConvertData<T>(block.Data);
                }
            }
            throw new Exception("Couldn't find " + name.ToString() + " block.");
        }

        public static T[] GetTypedDataArray<T>(MetaFile meta, MetaName name) where T : struct
        {
            if ((meta == null) || (meta.DataBlocks == null)) return null;

            var datablocks = meta.DataBlocks.Data;

            DataBlock startblock = null;
            int startblockind = -1;
            for (int i = 0; i < datablocks.Count; i++)
            {
                var block = datablocks[i];
                if ((MetaName) block.StructureNameHash == name)
                {
                    startblock = block;
                    startblockind = i;
                    break;
                }
            }
            if (startblock == null)
            {
                return null; //couldn't find the data.
            }

            int count = 0; //try figure out how many items there are, from the block size(s).
            int itemsize = Marshal.SizeOf(typeof(T));
            var currentblock = startblock;
            int currentblockind = startblockind;
            while (currentblock != null)
            {
                int blockitems = currentblock.DataLength / itemsize;
                count += blockitems;
                currentblockind++;
                if (currentblockind >= datablocks.Count) break; //last block, can't go any further
                currentblock = datablocks[currentblockind];
                if ((MetaName) currentblock.StructureNameHash != name) break; //not the right block type, can't go further
            }

            if (count <= 0)
            {
                return null; //didn't find anything...
            }

            return ConvertDataArray<T>(meta, (uint)startblockind + 1, (uint)count);
        }

        public static string GetString(MetaFile meta, CharPointer ptr)
        {
            var blocki = (int)ptr.PointerDataIndex;// (ptr.Pointer & 0xFFF) - 1;
            var offset = (int)ptr.PointerDataOffset;// (ptr.Pointer >> 12) & 0xFFFFF;

            if ((blocki < 0) || (blocki >= meta.DataBlocks.Length))
                return null;

            var block = meta.DataBlocks[blocki];

            if ((MetaName)block.StructureNameHash != MetaName.STRING)
                return null;

            var length = ptr.Count1;
            var lastbyte = offset + length;

            if (lastbyte >= block.DataLength)
                return null;

            byte[] data = Array.ConvertAll(block.Data.Data.ToArray(), e => (byte)e);
            string s = Encoding.ASCII.GetString(data, offset, length);

            return s;
        }

        public static MetaPOINTER[] GetPointerArray(MetaFile meta, Array_StructurePointer array)
        {
            uint count = array.Count1;

            if (count == 0)
                return null;

            MetaPOINTER[] ptrs = new MetaPOINTER[count];
            int ptrsize = Marshal.SizeOf(typeof(MetaPOINTER));
            int ptroffset = (int)array.PointerDataOffset;
            var ptrblock = meta.GetBlock((int)array.PointerDataId);

            if ((ptrblock == null) || (ptrblock.Data == null) || ((MetaName)ptrblock.StructureNameHash != MetaName.POINTER))
                return null;

            for (int i = 0; i < count; i++)
            {
                int offset = ptroffset + (i * ptrsize);

                if (offset >= ptrblock.Data.Count)
                    break;

                ptrs[i] = ConvertData<MetaPOINTER>(ptrblock.Data, offset);
            }

            return ptrs;
        }

        public static int GetDataOffset(DataBlock block, MetaPOINTER ptr)
        {
            if (block == null) return -1;
            var offset = ptr.Offset;
            if (ptr.ExtraOffset != 0)
            { }
            //offset += (int)ptr.ExtraOffset;
            if ((offset < 0) || (block.Data == null) || (offset >= block.Data.Length))
            { return -1; }
            return offset;
        }

        public static T GetData<T>(MetaFile meta, MetaPOINTER ptr) where T : struct
        {
            var block = meta.GetBlock(ptr.BlockID);
            var offset = GetDataOffset(block, ptr);
            if (offset < 0) return new T();
            return ConvertData<T>(block.Data, offset);
        }

        public static string GetCSharpTypeName(StructureEntryDataType t)
        {
            switch (t)
            {
                case StructureEntryDataType.Boolean: return "bool";
                case StructureEntryDataType.SignedByte: return "sbyte";
                case StructureEntryDataType.UnsignedByte: return "byte";
                case StructureEntryDataType.SignedShort: return "short";
                case StructureEntryDataType.UnsignedShort: return "ushort";
                case StructureEntryDataType.SignedInt: return "int";
                case StructureEntryDataType.UnsignedInt: return "uint";
                case StructureEntryDataType.Float: return "float";
                case StructureEntryDataType.Float_XYZ: return "Vector3";
                case StructureEntryDataType.Float_XYZW: return "Vector4";

                case StructureEntryDataType.Hash: return "uint"; //uint hashes...
                case StructureEntryDataType.ByteEnum: return "byte"; //convert to enum later..
                case StructureEntryDataType.IntEnum: return "int";
                case StructureEntryDataType.ShortFlags: return "short";
                case StructureEntryDataType.IntFlags1: return "int";
                case StructureEntryDataType.IntFlags2: return "int";

                case StructureEntryDataType.ArrayOfChars: return "ArrayOfChars64";

                case StructureEntryDataType.Array:
                case StructureEntryDataType.ArrayOfBytes:
                case StructureEntryDataType.DataBlockPointer:
                case StructureEntryDataType.CharPointer:
                case StructureEntryDataType.StructurePointer:
                case StructureEntryDataType.Structure:
                default:
                    return t.ToString();
            }
        }

        public static long GetCSharpTypeSize(StructureEntryDataType t, long size)
        {
            switch (t)
            {
                case StructureEntryDataType.Boolean: return sizeof(bool);
                case StructureEntryDataType.SignedByte: return sizeof(sbyte);
                case StructureEntryDataType.UnsignedByte: return sizeof(byte);
                case StructureEntryDataType.SignedShort: return sizeof(short);
                case StructureEntryDataType.UnsignedShort: return sizeof(ushort);
                case StructureEntryDataType.SignedInt: return sizeof(int);
                case StructureEntryDataType.UnsignedInt: return sizeof(uint);
                case StructureEntryDataType.Float: return sizeof(float);
                case StructureEntryDataType.Float_XYZ: return sizeof(float) * 3;
                case StructureEntryDataType.Float_XYZW: return sizeof(float) * 4;

                case StructureEntryDataType.Hash: return sizeof(uint); //uint hashes...
                case StructureEntryDataType.ByteEnum: return sizeof(byte); //convert to enum later..
                case StructureEntryDataType.IntEnum: return sizeof(int);
                case StructureEntryDataType.ShortFlags: return sizeof(short);
                case StructureEntryDataType.IntFlags1: return sizeof(int);
                case StructureEntryDataType.IntFlags2: return sizeof(int);

                case StructureEntryDataType.ArrayOfChars: return 64;

                case StructureEntryDataType.Array:
                case StructureEntryDataType.ArrayOfBytes:
                case StructureEntryDataType.DataBlockPointer:
                case StructureEntryDataType.CharPointer:
                case StructureEntryDataType.StructurePointer:
                case StructureEntryDataType.Structure:
                default:
                    return size;
            }
        }
    }
}
