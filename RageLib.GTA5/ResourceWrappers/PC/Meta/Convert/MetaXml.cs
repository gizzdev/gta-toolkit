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
using RageLib.GTA5.Utilities;
using RageLib.Resources.GTA5.PC.Meta;
using SharpDX;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace RageLib.GTA5.ResourceWrappers.PC.Meta
{
    public class MetaXml : MetaXmlBase
    {
        public static string GetXml(MetaFile meta)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(XmlHeader);

            if (meta != null)
            {
                var cont = new MetaCont(meta);

                WriteNode(sb, 0, cont, meta.RootBlockIndex, 0, XmlTagMode.Structure, 0, (string)meta.Name);
            }

            return sb.ToString();
        }

        private static void WriteNode(StringBuilder sb, int indent, MetaCont cont, int blockId, int offset, XmlTagMode tagMode = XmlTagMode.None, MetaName structName = 0, string metaName = "")
        {

            var block = cont.Meta.GetBlock(blockId);
            if (block == null)
            {
                ErrorXml(sb, indent, "Couldn't find block " + blockId + "!");
                return;
            }

            if (structName == 0)
            {
                structName = (MetaName)block.StructureNameHash;
            }

            var name = HashString(structName);
            var data = Array.ConvertAll(block.Data.Data.ToArray(), e => (byte) e);

            var structInfo = cont.GetStructureInfo(structName);
            if (structInfo == null)
            {
                ErrorXml(sb, indent, "Couldn't find structure info " + name + "!");
                return;
            }
            if (structInfo.Entries == null)
            {
                ErrorXml(sb, indent, "Couldn't find structure info entries for " + name + "!");
                return;
            }


            switch (tagMode)
            {
                case XmlTagMode.Structure:
                    OpenTag(sb, indent, name, true, metaName);
                    break;
                case XmlTagMode.Item:
                    OpenTag(sb, indent, "Item", true, metaName);
                    break;
                case XmlTagMode.ItemAndType:
                    OpenTag(sb, indent, "Item type=\"" + name + "\"", true, metaName);
                    break;
            }

            var cind = indent + 1;
            StructureEntryInfo arrEntry = new StructureEntryInfo();
            for (int i = 0; i < structInfo.Entries.Count; i++)
            {
                var entry = structInfo.Entries[i];
                if ((MetaName) entry.EntryNameHash == MetaName.ARRAYINFO)
                {
                    arrEntry = entry;
                    continue;
                }
                var ename = HashString((MetaName)entry.EntryNameHash);
                var eoffset = offset + entry.DataOffset;
                switch (entry.DataType)
                {
                    default:
                        ErrorXml(sb, cind, ename + ": Unexpected entry DataType: " + entry.DataType.ToString());
                        break;
                    case StructureEntryDataType.Array:

                        WriteArrayNode(sb, cind, cont, data, arrEntry, ename, eoffset);

                        break;
                    case StructureEntryDataType.ArrayOfBytes:

                        WriteParsedArrayOfBytesNode(sb, cind, data, ename, eoffset, entry, arrEntry);

                        break;
                    case StructureEntryDataType.ArrayOfChars:
                        OpenTag(sb, cind, ename, false);
                        uint charArrLen = (uint)entry.ReferenceKey;
                        for (int n = 0; n < charArrLen; n++)
                        {
                            var bidx = eoffset + n;
                            if ((bidx >= 0) && (bidx < data.Length))
                            {
                                byte b = data[bidx];
                                if (b == 0) break;
                                sb.Append((char)b);
                            }
                        }
                        CloseTag(sb, 0, ename);
                        break;
                    case StructureEntryDataType.Boolean:
                        var boolVal = BitConverter.ToBoolean(data, eoffset);
                        ValueTag(sb, cind, ename, boolVal?"true":"false");
                        break;
                    case StructureEntryDataType.ByteEnum:
                        var byteEnumVal = data[eoffset];
                        ValueTag(sb, cind, ename, byteEnumVal.ToString());
                        break;
                    case StructureEntryDataType.CharPointer:
                        var charPtr = MetaUtils.ConvertData<CharPointer>(data, eoffset);
                        string charStr = MetaUtils.GetString(cont.Meta, charPtr);
                        OneLineTag(sb, cind, ename, charStr);
                        break;
                    case StructureEntryDataType.DataBlockPointer:
                        OpenTag(sb, cind, ename);
                        var dataPtr = MetaUtils.ConvertData<DataBlockPointer>(data, eoffset);
                        ErrorXml(sb, cind + 1, "DataBlockPointer not currently supported here!"); //TODO! ymap occludeModels vertices data is this type!
                        CloseTag(sb, cind, ename);
                        break;
                    case StructureEntryDataType.Float:
                        var floatVal = BitConverter.ToSingle(data, eoffset);
                        ValueTag(sb, cind, ename, floatVal.ToString(CultureInfo.InvariantCulture));
                        break;
                    case StructureEntryDataType.Float_XYZ:
                        var v3 = MetaUtils.ConvertData<Vector3>(data, eoffset);
                        SelfClosingTag(sb, cind, ename + " x=\"" + v3.X.ToString(CultureInfo.InvariantCulture) + "\" y=\"" + v3.Y.ToString(CultureInfo.InvariantCulture) + "\" z=\"" + v3.Z.ToString(CultureInfo.InvariantCulture) + "\"");
                        break;
                    case StructureEntryDataType.Float_XYZW:
                        var v4 = MetaUtils.ConvertData<Vector4>(data, eoffset);
                        SelfClosingTag(sb, cind, ename + " x=\"" + v4.X.ToString(CultureInfo.InvariantCulture) + "\" y=\"" + v4.Y.ToString(CultureInfo.InvariantCulture) + "\" z=\"" + v4.Z.ToString(CultureInfo.InvariantCulture) + "\" w=\"" + v4.W.ToString(CultureInfo.InvariantCulture) + "\"");
                        break;
                    case StructureEntryDataType.Hash:
                        var hashVal = (MetaName) MetaUtils.ConvertData<uint>(data, eoffset);
                        var hashStr = HashString(hashVal);
                        StringTag(sb, cind, ename, hashStr);
                        break;
                    case StructureEntryDataType.IntEnum:
                        var intEnumVal = BitConverter.ToInt32(data, eoffset);
                        var intEnumStr = GetEnumString(cont, entry, intEnumVal);
                        StringTag(sb, cind, ename, intEnumStr);
                        break;
                    case StructureEntryDataType.IntFlags1:
                        var intFlags1Val = BitConverter.ToInt32(data, eoffset);
                        var intFlags1Str = GetEnumString(cont, entry, intFlags1Val);
                        StringTag(sb, cind, ename, intFlags1Str);
                        break;
                    case StructureEntryDataType.IntFlags2:
                        var intFlags2Val = BitConverter.ToInt32(data, eoffset);
                        var intFlags2Str = GetEnumString(cont, entry, intFlags2Val);
                        StringTag(sb, cind, ename, intFlags2Str);
                        break;
                    case StructureEntryDataType.ShortFlags:
                        var shortFlagsVal = BitConverter.ToInt16(data, eoffset);
                        var shortFlagsStr = shortFlagsVal.ToString(); // GetEnumString(cont, entry, shortFlagsVal);
                        StringTag(sb, cind, ename, shortFlagsStr);
                        break;
                    case StructureEntryDataType.SignedByte:
                        sbyte sbyteVal = (sbyte)data[eoffset];
                        ValueTag(sb, cind, ename, sbyteVal.ToString());
                        break;
                    case StructureEntryDataType.SignedInt:
                        var intVal = BitConverter.ToInt32(data, eoffset);
                        ValueTag(sb, cind, ename, intVal.ToString());
                        break;
                    case StructureEntryDataType.SignedShort:
                        var shortVal = BitConverter.ToInt16(data, eoffset);
                        ValueTag(sb, cind, ename, shortVal.ToString());
                        break;
                    case StructureEntryDataType.Structure:
                        OpenTag(sb, cind, ename);
                        WriteNode(sb, cind, cont, blockId, eoffset, XmlTagMode.None, (MetaName) entry.ReferenceKey);
                        CloseTag(sb, cind, ename);
                        break;
                    case StructureEntryDataType.StructurePointer:
                        OpenTag(sb, cind, ename);
                        ErrorXml(sb, cind + 1, "StructurePointer not supported here! Tell dexy!");
                        CloseTag(sb, cind, ename);
                        break;
                    case StructureEntryDataType.UnsignedByte:
                        var byteVal = data[eoffset];
                        ValueTag(sb, cind, ename, byteVal.ToString());
                        //ValueTag(sb, cind, ename, "0x" + byteVal.ToString("X").PadLeft(2, '0'));
                        break;
                    case StructureEntryDataType.UnsignedInt:
                        var uintVal = BitConverter.ToUInt32(data, eoffset);
                        switch ((MetaName)entry.EntryNameHash)
                        {
                            default:
                                ValueTag(sb, cind, ename, uintVal.ToString());
                                break;
                            case MetaName.color:
                                ValueTag(sb, cind, ename, "0x" + uintVal.ToString("X").PadLeft(8, '0'));
                                break;
                        }

                        break;
                    case StructureEntryDataType.UnsignedShort:
                        var ushortVal = BitConverter.ToUInt16(data, eoffset);
                        ValueTag(sb, cind, ename, ushortVal.ToString());// "0x" + ushortVal.ToString("X").PadLeft(4, '0'));
                        break;
                }
            }

            switch (tagMode)
            {
                case XmlTagMode.Structure:
                    CloseTag(sb, indent, name);
                    break;
                case XmlTagMode.Item:
                case XmlTagMode.ItemAndType:
                    CloseTag(sb, indent, "Item");
                    break;
            }

        }

        private static void WriteArrayNode(StringBuilder sb, int indent, MetaCont cont, byte[] data, StructureEntryInfo arrEntry, string ename, int eoffset)
        {
            int aCount = 0;
            var aind = indent + 1;
            string arrTag = ename;
            switch (arrEntry.DataType)
            {
                default:
                    ErrorXml(sb, indent, ename + ": Unexpected array entry DataType: " + arrEntry.DataType.ToString());
                    break;
                case StructureEntryDataType.Structure:
                    var arrStruc = MetaUtils.ConvertData<Array_Structure>(data, eoffset);
                    var aBlockId = (int)arrStruc.PointerDataId;
                    var aOffset = (int)arrStruc.PointerDataOffset;
                    aCount = arrStruc.Count1;
                    arrTag += " itemType=\"" + HashString((MetaName)arrEntry.ReferenceKey) + "\"";
                    if (aCount > 0)
                    {
                        OpenTag(sb, indent, arrTag);
                        var atyp = cont.GetStructureInfo((MetaName)arrEntry.ReferenceKey);
                        var aBlock = cont.Meta.GetBlock(aBlockId);
                        for (int n = 0; n < aCount; n++)
                        {
                            WriteNode(sb, aind, cont, aBlockId, aOffset, XmlTagMode.Item, (MetaName)arrEntry.ReferenceKey);
                            aOffset += atyp.StructureLength;

                            if ((n < (aCount - 1)) && (aBlock != null) && (aOffset >= aBlock.DataLength))
                            {
                                aOffset = 0;
                                aBlockId++;
                                aBlock = cont.Meta.GetBlock(aBlockId);
                            }
                        }
                        CloseTag(sb, indent, ename);
                    }
                    else
                    {
                        SelfClosingTag(sb, indent, arrTag);
                    }
                    break;
                case StructureEntryDataType.StructurePointer:
                    var arrStrucP = MetaUtils.ConvertData<Array_StructurePointer>(data, eoffset);
                    var ptrArr = MetaUtils.GetPointerArray(cont.Meta, arrStrucP);
                    aCount = ptrArr?.Length ?? 0;
                    if (aCount > 0)
                    {
                        OpenTag(sb, indent, arrTag);
                        for (int n = 0; n < aCount; n++)
                        {
                            var ptr = ptrArr[n];
                            var offset = ptr.Offset;
                            WriteNode(sb, aind, cont, ptr.BlockID, offset, XmlTagMode.ItemAndType);
                        }
                        CloseTag(sb, indent, ename);
                    }
                    else
                    {
                        SelfClosingTag(sb, indent, arrTag);
                    }
                    break;
                case StructureEntryDataType.UnsignedInt:
                    var arrUint = MetaUtils.ConvertData<Array_uint>(data, eoffset);
                    var uintArr = MetaUtils.ConvertDataArray<uint>(cont.Meta, arrUint.Pointer, arrUint.Count1); ;
                    WriteRawArray(sb, uintArr, indent, ename, "uint");
                    break;
                case StructureEntryDataType.UnsignedShort:
                    var arrUshort = MetaUtils.ConvertData<Array_ushort>(data, eoffset);
                    var ushortArr = MetaUtils.ConvertDataArray<ushort>(cont.Meta, arrUshort.Pointer, arrUshort.Count1); ;
                    WriteRawArray(sb, ushortArr, indent, ename, "ushort");
                    break;
                case StructureEntryDataType.UnsignedByte:
                    var arrUbyte = MetaUtils.ConvertData<Array_byte>(data, eoffset);
                    var byteArr = MetaUtils.ConvertDataArray<byte>(cont.Meta, arrUbyte.Pointer, arrUbyte.Count1); ;
                    WriteRawArray(sb, byteArr, indent, ename, "byte");
                    break;
                case StructureEntryDataType.Float:
                    var arrFloat = MetaUtils.ConvertData<Array_float>(data, eoffset);
                    var floatArr = MetaUtils.ConvertDataArray<float>(cont.Meta, arrFloat.Pointer, arrFloat.Count1); ;
                    WriteRawArray(sb, floatArr, indent, ename, "float");
                    break;
                case StructureEntryDataType.Float_XYZ:
                    var arrV3 = MetaUtils.ConvertData<Array_Vector3>(data, eoffset);
                    var v4Arr = MetaUtils.ConvertDataArray<Vector4>(cont.Meta, arrV3.Pointer, arrV3.Count1);
                    WriteItemArray(sb, v4Arr, indent, ename, "Vector3/4", FormatVector4);
                    break;
                case StructureEntryDataType.CharPointer:
                    ErrorXml(sb, indent, "CharPointer ARRAY not supported here!");
                    break;
                case StructureEntryDataType.DataBlockPointer:
                    ErrorXml(sb, indent, "DataBlockPointer ARRAY not supported here!");
                    break;
                case StructureEntryDataType.Hash:
                    var arrHash = MetaUtils.ConvertData<Array_uint>(data, eoffset);
                    var uintArr2 = MetaUtils.ConvertDataArray<uint>(cont.Meta, arrHash.Pointer, arrHash.Count1);
                    var hashArr = Array.ConvertAll(uintArr2, e => (MetaName) e);
                    WriteItemArray(sb, hashArr, indent, ename, "Hash", FormatHash);
                    break;
            }
        }

        private static void WriteParsedArrayOfBytesNode(StringBuilder sb, int indent, byte[] data, string ename, int eoffset, StructureEntryInfo entry, StructureEntryInfo arrEntry)
        {
            OpenTag(sb, indent, ename, false);

            var byteArrLen = ((int)entry.ReferenceKey);

            switch (arrEntry.DataType)
            {
                default:
                    for (int n = 0; n < byteArrLen; n++)
                    {
                        var bidx = eoffset + n;
                        byte b = ((bidx >= 0) && (bidx < data.Length)) ? data[bidx] : (byte)0;
                        sb.Append(b.ToString("X").PadLeft(2, '0'));
                    }
                    break;
                case StructureEntryDataType.SignedByte:
                    for (int n = 0; n < byteArrLen; n++)
                    {
                        var bidx = eoffset + n;
                        sbyte b = ((bidx >= 0) && (bidx < data.Length)) ? (sbyte)data[bidx] : (sbyte)0;
                        sb.Append(b.ToString()); //sb.Append(b.ToString("X").PadLeft(2, '0')); to show HEX values
                        if (n < byteArrLen - 1) sb.Append(" ");
                    }
                    break;

                case StructureEntryDataType.UnsignedByte:
                    for (int n = 0; n < byteArrLen; n++)
                    {
                        var bidx = eoffset + n;
                        byte b = ((bidx >= 0) && (bidx < data.Length)) ? data[bidx] : (byte)0;
                        sb.Append(b.ToString());
                        if (n < byteArrLen - 1) sb.Append(" ");
                    }
                    break;
                case StructureEntryDataType.SignedShort:
                    for (int n = 0; n < byteArrLen; n++)
                    {
                        var bidx = eoffset + n * 2;
                        short b = ((bidx >= 0) && (bidx < data.Length)) ? BitConverter.ToInt16(data, bidx) : (short)0;
                        sb.Append(b.ToString());
                        if (n < byteArrLen - 1) sb.Append(" ");
                    }
                    break;
                case StructureEntryDataType.UnsignedShort:
                    for (int n = 0; n < byteArrLen; n++)
                    {
                        var bidx = eoffset + n * 2;
                        ushort b = ((bidx >= 0) && (bidx < data.Length)) ? BitConverter.ToUInt16(data, bidx) : (ushort)0;
                        sb.Append(b.ToString());
                        if (n < byteArrLen - 1) sb.Append(" ");
                    }
                    break;
                case StructureEntryDataType.SignedInt:
                    for (int n = 0; n < byteArrLen; n++)
                    {
                        var bidx = eoffset + n * 4;
                        int b = ((bidx >= 0) && (bidx < data.Length)) ? BitConverter.ToInt32(data, bidx) : (int)0;
                        sb.Append(b.ToString());
                        if (n < byteArrLen - 1) sb.Append(" ");
                    }
                    break;
                case StructureEntryDataType.UnsignedInt:
                    for (int n = 0; n < byteArrLen; n++)
                    {
                        var bidx = eoffset + n * 4;
                        uint b = ((bidx >= 0) && (bidx < data.Length)) ? BitConverter.ToUInt32(data, bidx) : (uint)0;
                        sb.Append(b.ToString());
                        if (n < byteArrLen - 1) sb.Append(" ");
                    }
                    break;
                case StructureEntryDataType.Float:
                    for (int n = 0; n < byteArrLen; n++)
                    {
                        var bidx = eoffset + n * 4;
                        float b = ((bidx >= 0) && (bidx < data.Length)) ? BitConverter.ToSingle(data, bidx) : (float)0;
                        sb.Append(FloatUtil.ToString(b));
                        if (n < byteArrLen - 1) sb.Append(" ");
                    }
                    break;
            }
            CloseTag(sb, 0, ename);
        }

        private static string GetEnumString(MetaCont cont, StructureEntryInfo entry, int value)
        {
            var eName = (MetaName) entry.ReferenceKey;
            var eInfo = cont.GetEnumInfo(eName);
            if ((eInfo == null) || (eInfo.Entries == null))
            {
                return value.ToString();
            }

            bool isFlags = (entry.DataType == StructureEntryDataType.IntFlags1) ||
                           (entry.DataType == StructureEntryDataType.IntFlags2);// ||
                           //(entry.DataType == StructureEntryDataType.ShortFlags);

            if (isFlags)
            {
                StringBuilder sb = new StringBuilder();
                foreach (var ev in eInfo.Entries)
                {
                    var v = ev.EntryValue;
                    var m = 1 << v;
                    if ((value & m) > 0)
                    {
                        if (sb.Length > 0) sb.Append(", ");
                        sb.Append(HashString((MetaName)ev.EntryNameHash));
                    }
                }
                return sb.ToString();
            }
            else
            {
                foreach (var ev in eInfo.Entries)
                {
                    if (ev.EntryValue == value)
                    {
                        return HashString((MetaName)ev.EntryNameHash);
                    }
                }
                return value.ToString(); //if we got here, there was no match...
            }
        }

        private class MetaCont
        {
            public MetaFile Meta { get; set; }

            Dictionary<MetaName, StructureInfo> structInfos = new Dictionary<MetaName, StructureInfo>();
            Dictionary<MetaName, EnumInfo> enumInfos = new Dictionary<MetaName, EnumInfo>();

            public MetaCont(MetaFile meta)
            {
                Meta = meta;

                if (meta.StructureInfos != null)
                {
                    foreach (var si in meta.StructureInfos)
                    {
                        structInfos[(MetaName)si.StructureNameHash] = si;
                    }
                }
                if (meta.EnumInfos != null)
                {
                    foreach (var ei in meta.EnumInfos)
                    {
                        enumInfos[(MetaName)ei.EnumNameHash] = ei;
                    }
                }
            }

            public StructureInfo GetStructureInfo(MetaName name)
            {
                StructureInfo i = null;
                structInfos.TryGetValue(name, out i);
                return i;
            }
            public EnumInfo GetEnumInfo(MetaName name)
            {
                EnumInfo i = null;
                enumInfos.TryGetValue(name, out i);
                return i;
            }

        }

    }
}
