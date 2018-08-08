using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using RageLib.Resources.Common;
using SharpDX;

namespace RageLib.Resources.GTA5.PC.Meta
{
    class MetaUtils
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

        public static DataBlock[] FindBlocks(MetaFile meta, MetaName name)
        {
            var blocks = new List<DataBlock>();

            for (int i = 0; i < meta.DataBlocks.Count; i++)
                if ((MetaName)meta.DataBlocks[i].StructureNameHash == name)
                    blocks.Add(meta.DataBlocks[i]);

            return blocks.ToArray();
        }

        public static DataBlock GetRootBlock(MetaFile meta, MetaName name)
        {
            DataBlock block = null;

            int rootIndex = meta.RootBlockIndex - 1;

            if ((rootIndex >= 0) && (rootIndex < meta.DataBlocks.Count) && (meta.DataBlocks.Data != null))
                block = meta.DataBlocks[rootIndex];

            return block;
        }

        public static DataBlock GetBlock(MetaFile meta, int id)
        {
            DataBlock block = null;

            var index = id - 1;

            if ((index >= 0) && (index < meta.DataBlocks.Count) && (meta.DataBlocks.Data != null))
                block = meta.DataBlocks[index];

            return block;
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

        public static T[] ConvertArray_Structure<T>(MetaFile meta, Array_Structure array) where T : struct
        {
            return ConvertArray_Structure<T>(meta, array.Pointer, array.Count1);
        }

        public static T[] ConvertArray_Structure<T>(MetaFile meta, uint pointer, uint count) where T : struct
        {
            T[] items = new T[count];
            int itemSize = Marshal.SizeOf(typeof(T));
            int itemsLeft = (int)count;

            uint ptrIndex = (pointer & 0xFFF) - 1;
            uint ptrOffset = (pointer >> 12) & 0xFFFFF;
            var ptrblock = (ptrIndex < meta.DataBlocks.Count) ? meta.DataBlocks[(int)ptrIndex] : null;

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

            }

            return null;

        }


        public static T[] ConvertArray_StructurePointer<T>(MetaFile meta, Array_StructurePointer array) where T : struct
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
                var block = GetBlock(meta, ptr.BlockID);

                if (block == null)
                    continue;

                if ((offset < 0) || (block.Data == null) || (offset >= block.Data.Count))
                    continue;

                items[i] = ConvertData<T>(block.Data, offset);
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
            var ptrblock = GetBlock(meta, (int)array.PointerDataId);

            if ((ptrblock == null) || (ptrblock.Data == null) || ((MetaName)ptrblock.StructureNameHash != MetaName.POINTER))
                return null;

            for (int i = 0; i < count; i++)
            {
                int offset = ptroffset + (i * ptrsize);
                if (offset >= ptrblock.Data.Count)
                { break; }
                ptrs[i] = ConvertData<MetaPOINTER>(ptrblock.Data, offset);
            }

            return ptrs;
        }


    }
}
