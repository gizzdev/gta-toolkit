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

using RageLib.Resources.GTA5.PC.Meta;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace RageLib.GTA5.PSO
{
    public static class PsoUtils
    {
        //for parsing schema info in PSO files to generate structs for PSO parsing.
        //equivalent of MetaTypes but for PSO.

        public static Dictionary<MetaName, PsoEnumInfo> EnumDict = new Dictionary<MetaName, PsoEnumInfo>();
        public static Dictionary<MetaName, PsoStructureInfo> StructDict = new Dictionary<MetaName, PsoStructureInfo>();



        public static void Clear()
        {
            StructDict.Clear();
        }

        public static string GetTypesString()
        {
            StringBuilder sbe = new StringBuilder();
            StringBuilder sbs = new StringBuilder();

            sbe.AppendLine("//Enum infos");
            sbs.AppendLine("//Struct infos");


            foreach (var kvp in EnumDict)
            {
                var ei = kvp.Value;
                string name = GetSafeName((MetaName)ei.IndexInfo.NameHash, ei.Type);
                sbe.AppendLine("public enum " + name + " //Type:" + ei.Type.ToString());
                sbe.AppendLine("{");
                foreach (var entry in ei.Entries)
                {
                    string eename = GetSafeName((MetaName)entry.EntryNameHash, (uint)entry.EntryKey);
                    sbe.AppendFormat("   {0} = {1},", eename, entry.EntryKey);
                    sbe.AppendLine();
                }
                sbe.AppendLine("}");
                sbe.AppendLine();
            }

            foreach (var kvp in StructDict)
            {
                var si = kvp.Value;
                string name = GetSafeName((MetaName)si.IndexInfo.NameHash, si.Type);
                sbs.AppendLine("public struct " + name + " //" + si.StructureLength.ToString() + " bytes, Type:" + si.Type.ToString());
                sbs.AppendLine("{");
                for (int i = 0; i < si.Entries.Count; i++)
                {
                    var entry = si.Entries[i];

                    if ((entry.DataOffset == 0) && ((MetaName)entry.EntryNameHash == MetaName.ARRAYINFO)) //referred to by array
                    {
                    }
                    else
                    {
                        string sename = GetSafeName((MetaName)entry.EntryNameHash, (uint)entry.ReferenceKey);
                        string fmt = "   public {0} {1}; //{2}   {3}";

                        if (entry.Type == DataType.Array)
                        {
                            if (entry.ReferenceKey >= si.Entries.Count)
                            {
                                sbs.AppendFormat(fmt, entry.Type.ToString(), sename, entry.DataOffset, entry.ToString() + "  { unexpected key! " + entry.ReferenceKey.ToString() + "}");
                                sbs.AppendLine();
                            }
                            else
                            {
                                var structentry = si.Entries[(int)entry.ReferenceKey];
                                var typename = "Array_" + PsoUtils.GetCSharpTypeName(structentry.Type);
                                sbs.AppendFormat(fmt, typename, sename, entry.DataOffset, entry.ToString() + "  {" + structentry.ToString() + "}");
                                sbs.AppendLine();
                            }
                        }
                        else if (entry.Type == DataType.Structure)
                        {
                            var typename = GetSafeName((MetaName)entry.ReferenceKey, (uint) entry.ReferenceKey);
                            sbs.AppendFormat(fmt, typename, sename, entry.DataOffset, entry.ToString());
                            sbs.AppendLine();
                        }
                        else
                        {
                            var typename = PsoUtils.GetCSharpTypeName(entry.Type);
                            sbs.AppendFormat(fmt, typename, sename, entry.DataOffset, entry);
                            sbs.AppendLine();
                        }
                    }
                }
                sbs.AppendLine("}");
                sbs.AppendLine();
            }


            sbe.AppendLine();
            sbe.AppendLine();
            sbe.AppendLine();
            sbe.AppendLine();
            sbe.AppendLine();
            sbe.Append(sbs.ToString());

            string result = sbe.ToString();

            return result;
        }

        private static string GetSafeName(MetaName namehash, uint key)
        {
            string name = namehash.ToString();
            if (string.IsNullOrEmpty(name))
            {
                name = "Unk_" + key;
            }
            if (!char.IsLetter(name[0]))
            {
                name = "Unk_" + name;
            }
            return name;
        }



        public static T ConvertDataRaw<T>(byte[] data) where T : struct
        {
            GCHandle handle = GCHandle.Alloc(data, GCHandleType.Pinned);
            var h = handle.AddrOfPinnedObject();
            var r = Marshal.PtrToStructure<T>(h);
            handle.Free();
            return r;
        }
        public static T ConvertDataRaw<T>(byte[] data, int offset) where T : struct
        {
            GCHandle handle = GCHandle.Alloc(data, GCHandleType.Pinned);
            var h = handle.AddrOfPinnedObject();
            var r = Marshal.PtrToStructure<T>(h + offset);
            handle.Free();
            return r;
        }
        public static T ConvertData<T>(byte[] data, int offset) where T : struct, IPsoSwapEnd
        {
            GCHandle handle = GCHandle.Alloc(data, GCHandleType.Pinned);
            var h = handle.AddrOfPinnedObject();
            var r = Marshal.PtrToStructure<T>(h + offset);
            handle.Free();
            r.SwapEnd();
            return r;
        }
        public static T[] ConvertDataArrayRaw<T>(byte[] data, int offset, int count) where T : struct
        {
            T[] items = new T[count];
            int itemsize = Marshal.SizeOf(typeof(T));
            for (int i = 0; i < count; i++)
            {
                int off = offset + i * itemsize;
                items[i] = ConvertDataRaw<T>(data, off);
            }
            return items;
        }


        public static T GetItem<T>(PsoFile pso, int offset) where T : struct, IPsoSwapEnd
        {
            return ConvertData<T>(pso.DataSection.Data, offset);
        }
        public static T GetRootItem<T>(PsoFile pso) where T : struct, IPsoSwapEnd
        {
            var i = pso.DataMappingSection.RootIndex - 1;
            var e = pso.DataMappingSection.Entries[i];
            return GetItem<T>(pso, e.Offset);
        }
        public static PsoDataMappingEntry GetRootEntry(PsoFile pso)
        {
            var i = pso.DataMappingSection.RootIndex - 1;
            var e = pso.DataMappingSection.Entries[i];
            return e;
        }

        public static T[] GetItemArrayRaw<T>(PsoFile pso, Array_Structure arr) where T : struct
        {
            if ((arr.Count1 > 0) && (arr.Pointer > 0))
            {
                var entry = pso.DataMappingSection.Entries[(int)arr.PointerDataIndex];
                return ConvertDataArrayRaw<T>(pso.DataSection.Data, entry.Offset, arr.Count1);
            }
            return null;
        }
        public static T[] GetItemArray<T>(PsoFile pso, Array_Structure arr) where T : struct, IPsoSwapEnd
        {
            if ((arr.Count1 > 0) && (arr.Pointer > 0))
            {
                var entry = pso.DataMappingSection.Entries[(int)arr.PointerDataIndex];
                var res = ConvertDataArrayRaw<T>(pso.DataSection.Data, entry.Offset, arr.Count1);
                if (res != null)
                {
                    for (int i = 0; i < res.Length; i++)
                    {
                        res[i].SwapEnd();
                    }
                }
                return res;
            }
            return null;
        }


        public static uint[] GetUintArrayRaw(PsoFile pso, Array_uint arr)
        {
            byte[] data = pso.DataSection.Data;
            var entryid = arr.Pointer & 0xFFF;
            if ((entryid == 0) || (entryid > pso.DataMappingSection.EntriesCount))
            {
                return null;
            }
            var entryoffset = (arr.Pointer & 0xFFFFFF) >> 12;
            var arrentry = pso.DataMappingSection.Entries[(int)entryid - 1];
            int totoffset = arrentry.Offset + (int)entryoffset;
            uint[] readdata = ConvertDataArrayRaw<uint>(data, totoffset, arr.Count1);
            return readdata;
        }
        public static uint[] GetUintArray(PsoFile pso, Array_uint arr)
        {
            uint[] uints = GetUintArrayRaw(pso, arr);
            if (uints == null) return null;
            for (int i = 0; i < uints.Length; i++)
            {
                uints[i] = MetaUtils.SwapBytes(uints[i]);
            }
            return uints;
        }

        public static MetaName[] GetHashArray(PsoFile pso, Array_uint arr)
        {
            uint[] uints = GetUintArrayRaw(pso, arr);
            if (uints == null) return null;
            MetaName[] hashes = new MetaName[uints.Length];
            for (int n = 0; n < uints.Length; n++)
            {
                hashes[n] = (MetaName) MetaUtils.SwapBytes(uints[n]);
            }
            return hashes;
        }




        public static float[] GetFloatArrayRaw(PsoFile pso, Array_float arr)
        {
            byte[] data = pso.DataSection.Data;
            var entryid = arr.Pointer & 0xFFF;
            if ((entryid == 0) || (entryid > pso.DataMappingSection.EntriesCount))
            {
                return null;
            }
            var entryoffset = (arr.Pointer & 0xFFFFFF) >> 12;
            var arrentry = pso.DataMappingSection.Entries[(int)entryid - 1];
            int totoffset = arrentry.Offset + (int)entryoffset;
            float[] readdata = ConvertDataArrayRaw<float>(data, totoffset, arr.Count1);
            return readdata;
        }
        public static float[] GetFloatArray(PsoFile pso, Array_float arr)
        {
            float[] floats = GetFloatArrayRaw(pso, arr);
            if (floats == null) return null;
            for (int i = 0; i < floats.Length; i++)
            {
                floats[i] = MetaUtils.SwapBytes(floats[i]);
            }
            return floats;
        }





        public static ushort[] GetUShortArrayRaw(PsoFile pso, Array_Structure arr)
        {
            byte[] data = pso.DataSection.Data;
            var entryid = arr.Pointer & 0xFFF;
            if ((entryid == 0) || (entryid > pso.DataMappingSection.EntriesCount))
            {
                return null;
            }
            var entryoffset = (arr.Pointer & 0xFFFFFF) >> 12;
            var arrentry = pso.DataMappingSection.Entries[(int)entryid - 1];
            int totoffset = arrentry.Offset + (int)entryoffset;
            ushort[] readdata = ConvertDataArrayRaw<ushort>(data, totoffset, arr.Count1);
            return readdata;
        }
        public static ushort[] GetUShortArray(PsoFile pso, Array_Structure arr)
        {
            ushort[] ushorts = GetUShortArrayRaw(pso, arr);
            if (ushorts == null) return null;
            for (int i = 0; i < ushorts.Length; i++)
            {
                ushorts[i] = MetaUtils.SwapBytes(ushorts[i]);
            }
            return ushorts;
        }






        public static T[] GetObjectArray<T, U>(PsoFile pso, Array_Structure arr) where U : struct, IPsoSwapEnd where T : PsoClass<U>, new()
        {
            U[] items = GetItemArray<U>(pso, arr);
            if (items == null) return null;
            if (items.Length == 0) return null;
            T[] result = new T[items.Length];
            for (int i = 0; i < items.Length; i++)
            {
                T newitem = new T();
                newitem.Init(pso, ref items[i]);
                result[i] = newitem;
            }
            return result;
        }


        public static byte[] GetByteArray(PsoFile pso, PsoStructureEntryInfo entry, int offset)
        {
            var aCount = (entry.ReferenceKey >> 16) & 0x0000FFFF;
            var aBlockId = (int)entry.ReferenceKey & 0x0000FFFF;
            var block = pso.GetBlock(aBlockId);
            if (block == null) return null;

            //block.Offset

            return null;
        }






        public static PsoPOINTER[] GetPointerArray(PsoFile pso, Array_StructurePointer array)
        {
            uint count = array.Count1;
            if (count == 0) return null;

            int ptrsize = Marshal.SizeOf(typeof(MetaPOINTER));
            int itemsleft = (int)count; //large arrays get split into chunks...
            uint ptr = array.Pointer;
            int ptrindex = (int)(ptr & 0xFFF) - 1;
            int ptroffset = (int)((ptr >> 12) & 0xFFFFF);
            var ptrblock = (ptrindex < pso.DataMappingSection.EntriesCount) ? pso.DataMappingSection.Entries[ptrindex] : null;
            if ((ptrblock == null) || ((MetaName) ptrblock.NameHash != MetaName.PsoPOINTER))
            { return null; }

            var offset = ptrblock.Offset;
            int boffset = offset + ptroffset;

            var ptrs = ConvertDataArrayRaw<PsoPOINTER>(pso.DataSection.Data, boffset, (int)count);
            if (ptrs != null)
            {
                for (int i = 0; i < ptrs.Length; i++)
                {
                    ptrs[i].SwapEnd();
                }
            }

            return ptrs;
        }


        public static T[] ConvertDataArray<T>(PsoFile pso, Array_StructurePointer array) where T : struct, IPsoSwapEnd
        {
            uint count = array.Count1;
            if (count == 0) return null;
            PsoPOINTER[] ptrs = GetPointerArray(pso, array);
            if (ptrs == null) return null;
            if (ptrs.Length < count)
            { return null; }

            T[] items = new T[count];
            int itemsize = Marshal.SizeOf(typeof(T));

            for (int i = 0; i < count; i++)
            {
                var sptr = ptrs[i];
                int blocki = sptr.BlockID - 1;
                int offset = (int)sptr.ItemOffset;// * 16;//block data size...
                if (blocki >= pso.DataMappingSection.EntriesCount)
                { continue; }
                var block = pso.DataMappingSection.Entries[blocki];

                if ((offset < 0) || (offset >= block.Length))
                { continue; }

                int boffset = block.Offset + offset;

                items[i] = ConvertData<T>(pso.DataSection.Data, boffset);
            }

            return items;
        }



        public static string GetString(PsoFile pso, CharPointer ptr)
        {
            if (ptr.Count1 == 0) return null;

            var blocki = (int)ptr.PointerDataId;// (ptr.Pointer & 0xFFF) - 1;
            var offset = (int)ptr.PointerDataOffset;// (ptr.Pointer >> 12) & 0xFFFFF;

            var block = pso.GetBlock(blocki);
            if (block == null)
            { return null; }

            var length = ptr.Count1;
            var lastbyte = offset + length;
            if (lastbyte >= block.Length)
            { return null; }

            var data = pso.DataSection?.Data;
            if (data == null)
            { return null; }

            var doffset = block.Offset + offset;

            string s = Encoding.ASCII.GetString(data, doffset, length);

            //if (meta.Strings == null) return null;
            //if (offset < 0) return null;
            //if (offset >= meta.Strings.Length) return null;
            //string s = meta.Strings[offset];

            return s;
        }
        public static string GetString(PsoFile pso, DataBlockPointer ptr)
        {
            var blocki = (int)ptr.PointerDataId;// (ptr.Pointer & 0xFFF) - 1;
            var offset = (int)ptr.PointerDataOffset;// (ptr.Pointer >> 12) & 0xFFFFF;

            var block = pso.GetBlock(blocki);
            if (block == null)
            { return null; }

            //var length = ptr.Count1;
            //var lastbyte = offset + length;
            //if (lastbyte >= block.Length)
            //{ return null; }

            var data = pso.DataSection?.Data;
            if (data == null)
            { return null; }

            //var doffset = block.Offset + offset;

            //string s = Encoding.ASCII.GetString(data, doffset, length);

            StringBuilder sb = new StringBuilder();
            var o = block.Offset + offset;
            char c = (char)data[o];
            while (c != 0)
            {
                sb.Append(c);
                o++;
                c = (char)data[o];
            }
            var s = sb.ToString();

            return s;
        }

        public static string GetCSharpTypeName(DataType t)
        {
            switch (t)
            {
                case DataType.Bool: return "bool";
                case DataType.SByte: return "sbyte";
                case DataType.UByte: return "byte";
                case DataType.SShort: return "short";
                case DataType.UShort: return "ushort";
                case DataType.SInt: return "int";
                case DataType.UInt: return "int";
                case DataType.Float: return "float";
                case DataType.Float2: return "long";
                case DataType.String: return "uint"; //hash? NEEDS WORK?
                case DataType.Enum: return "byte";
                case DataType.Flags: return "short";
                case DataType.HFloat: return "short";
                case DataType.Long: return "long";
                case DataType.Float3:
                case DataType.Float4:
                case DataType.Map:
                case DataType.Float3a:
                case DataType.Float4a:
                case DataType.Structure:
                case DataType.Array:
                default:
                    return t.ToString();
            }
        }
    }

    public abstract class PsoClass<T> where T : struct, IPsoSwapEnd
    {
        public abstract void Init(PsoFile pso, ref T v);
    }

}
