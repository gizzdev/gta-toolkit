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
using SharpDX;
using RageLib.Resources.GTA5.PC.Meta;
using RageLib.GTA5.PSO;
using System.Text;
using RageLib.GTA5.Utilities;

namespace RageLib.GTA5.ResourceWrappers.PC.PSO
{
    public class PsoXml : MetaXmlBase
    {

        public static string GetXml(PsoFile pso)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(XmlHeader);

            if ((pso != null) && (pso.DataSection != null) && (pso.DataMappingSection != null))
            {
                var cont = new PsoCont(pso);

                WriteNode(sb, 0, cont, pso.DataMappingSection.RootIndex, 0, XmlTagMode.Structure);
            }

            return sb.ToString();
        }


        private static void WriteNode(StringBuilder sb, int indent, PsoCont cont, int blockId, int offset, XmlTagMode tagMode = XmlTagMode.None, MetaName structName = 0)
        {

            var block = cont.Pso.GetBlock(blockId);
            if (block == null)
            {
                ErrorXml(sb, indent, "Couldn't find block " + blockId + "!");
                return;
            }


            var boffset = offset + block.Offset;

            if (structName == 0)
            {
                structName = (MetaName)block.NameHash;
            }

            var name = HashString(structName);
            var data = cont.Pso.DataSection.Data;

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
                    OpenTag(sb, indent, name);
                    break;
                case XmlTagMode.Item:
                    OpenTag(sb, indent, "Item");
                    break;
                case XmlTagMode.ItemAndType:
                    OpenTag(sb, indent, "Item type=\"" + name + "\"");
                    break;
            }


            var cind = indent + 1;
            for (int i = 0; i < structInfo.Entries.Count; i++)
            {
                var entry = structInfo.Entries[i];
                if ((MetaName)entry.EntryNameHash == MetaName.ARRAYINFO)
                {
                    continue;
                }
                var ename = HashString((MetaName)entry.EntryNameHash);
                var eoffset = boffset + entry.DataOffset;
                switch (entry.Type)
                {
                    default:
                        ErrorXml(sb, cind, ename + ": Unexpected entry DataType: " + entry.Type.ToString());
                        break;
                    case RageLib.GTA5.PSO.DataType.Array:

                        WriteArrayNode(sb, cind, cont, blockId, offset, entry, structInfo, ename);

                        break;
                    case RageLib.GTA5.PSO.DataType.Bool:
                        var boolVal = BitConverter.ToBoolean(data, eoffset);
                        ValueTag(sb, cind, ename, boolVal ? "true" : "false");
                        break;
                    case RageLib.GTA5.PSO.DataType.SByte: //was LONG_01h //signed byte?
                        //var long1Val = MetaUtils.SwapBytes(BitConverter.ToUInt64(data, eoffset));
                        //ValueTag(sb, cind, ename, long1Val.ToString());
                        var byte1Val = (sbyte)data[eoffset];
                        ValueTag(sb, cind, ename, byte1Val.ToString());
                        break;
                    case RageLib.GTA5.PSO.DataType.UByte:
                        var byte2Val = data[eoffset];
                        ValueTag(sb, cind, ename, byte2Val.ToString());
                        break;
                    case RageLib.GTA5.PSO.DataType.Enum:
                        var enumInfo = cont.GetEnumInfo((MetaName)entry.ReferenceKey);
                        switch (entry.Unk_5h)
                        {
                            default:
                                ErrorXml(sb, cind, ename + ": Unexpected Enum subtype: " + entry.Unk_5h.ToString());
                                break;
                            case 0: //int enum
                                var intEVal = MetaUtils.SwapBytes(BitConverter.ToInt32(data, eoffset));
                                var intE = enumInfo.FindEntry(intEVal);
                                StringTag(sb, cind, ename, HashString((MetaName)(intE?.EntryNameHash ?? 0)));
                                break;
                            case 2: //byte enum
                                var byteEVal = data[eoffset];
                                var byteE = enumInfo.FindEntry(byteEVal);
                                StringTag(sb, cind, ename, HashString((MetaName)(byteE?.EntryNameHash ?? 0)));
                                break;
                        }
                        break;
                    case RageLib.GTA5.PSO.DataType.Flags:
                        uint fCount = ((uint)entry.ReferenceKey >> 16) & 0x0000FFFF;
                        uint fEntry = ((uint)entry.ReferenceKey & 0xFFFF);
                        var fEnt = structInfo.GetEntry((int)fEntry);
                        PsoEnumInfo flagsInfo = null;
                        if ((fEnt != null) && ((MetaName)fEnt.EntryNameHash == MetaName.ARRAYINFO))
                        {
                            flagsInfo = cont.GetEnumInfo((MetaName)fEnt.ReferenceKey);
                        }
                        if (flagsInfo == null)
                        {
                            flagsInfo = cont.GetEnumInfo((MetaName)entry.EntryNameHash);
                        }
                        uint? flagsVal = null;
                        switch (entry.Unk_5h)
                        {
                            default:
                                ErrorXml(sb, cind, ename + ": Unexpected Flags subtype: " + entry.Unk_5h.ToString());
                                break;
                            case 0: //int flags
                                flagsVal = MetaUtils.SwapBytes(BitConverter.ToUInt32(data, eoffset));
                                break;
                            case 1: //short flags
                                flagsVal = MetaUtils.SwapBytes(BitConverter.ToUInt16(data, eoffset));
                                break;
                            case 2: //byte flags
                                flagsVal = data[eoffset];
                                break;
                        }
                        if (flagsVal.HasValue)
                        {
                            uint fv = flagsVal.Value;
                            if (flagsInfo != null)
                            {
                                string fstr = "";
                                for (int n = 0; n < flagsInfo.EntriesCount; n++)
                                {
                                    var fentry = flagsInfo.Entries[n];
                                    var fmask = (1 << fentry.EntryKey);
                                    if ((fv & fmask) > 0)
                                    {
                                        if (fstr != "") fstr += " ";
                                        fstr += HashString((MetaName)fentry.EntryNameHash);
                                    }
                                }
                                StringTag(sb, cind, ename, fstr);
                            }
                            else
                            {
                                if (fv != 0) ValueTag(sb, cind, ename, fv.ToString());
                                else SelfClosingTag(sb, cind, ename);
                            }
                        }
                        break;
                    case RageLib.GTA5.PSO.DataType.Float:
                        var floatVal = MetaUtils.SwapBytes(BitConverter.ToSingle(data, eoffset));
                        ValueTag(sb, cind, ename, FloatUtil.ToString(floatVal));
                        break;
                    case RageLib.GTA5.PSO.DataType.Float2:
                        var v2 = MetaUtils.SwapBytes(MetaUtils.ConvertData<Vector2>(data, eoffset));
                        SelfClosingTag(sb, cind, ename + " x=\"" + FloatUtil.ToString(v2.X) + "\" y=\"" + FloatUtil.ToString(v2.Y) + "\"");
                        break;
                    case RageLib.GTA5.PSO.DataType.Float3:
                        var v3 = MetaUtils.SwapBytes(MetaUtils.ConvertData<Vector3>(data, eoffset));
                        SelfClosingTag(sb, cind, ename + " x=\"" + FloatUtil.ToString(v3.X) + "\" y=\"" + FloatUtil.ToString(v3.Y) + "\" z=\"" + FloatUtil.ToString(v3.Z) + "\"");
                        break;
                    case RageLib.GTA5.PSO.DataType.Float3a: //TODO: check this!
                        var v3a = MetaUtils.SwapBytes(MetaUtils.ConvertData<Vector3>(data, eoffset));
                        SelfClosingTag(sb, cind, ename + " x=\"" + FloatUtil.ToString(v3a.X) + "\" y=\"" + FloatUtil.ToString(v3a.Y) + "\" z=\"" + FloatUtil.ToString(v3a.Z) + "\"");
                        break;
                    case RageLib.GTA5.PSO.DataType.Float4a: //TODO: check this! //...why are there 3 different types of float3?
                        var v3b = MetaUtils.SwapBytes(MetaUtils.ConvertData<Vector3>(data, eoffset));
                        SelfClosingTag(sb, cind, ename + " x=\"" + FloatUtil.ToString(v3b.X) + "\" y=\"" + FloatUtil.ToString(v3b.Y) + "\" z=\"" + FloatUtil.ToString(v3b.Z) + "\"");
                        break;
                    case RageLib.GTA5.PSO.DataType.Float4:
                        var v4 = MetaUtils.SwapBytes(MetaUtils.ConvertData<Vector4>(data, eoffset));
                        SelfClosingTag(sb, cind, ename + " x=\"" + FloatUtil.ToString(v4.X) + "\" y=\"" + FloatUtil.ToString(v4.Y) + "\" z=\"" + FloatUtil.ToString(v4.Z) + "\" w=\"" + FloatUtil.ToString(v4.W) + "\"");
                        break;
                    case RageLib.GTA5.PSO.DataType.SInt: //TODO: convert hashes?
                        var int5Val = MetaUtils.SwapBytes(BitConverter.ToInt32(data, eoffset));
                        ValueTag(sb, cind, ename, int5Val.ToString());
                        break;
                    case RageLib.GTA5.PSO.DataType.UInt:
                        switch (entry.Unk_5h)
                        {
                            default:
                                ErrorXml(sb, cind, ename + ": Unexpected Integer subtype: " + entry.Unk_5h.ToString());
                                break;
                            case 0: //signed int
                                var int6aVal = MetaUtils.SwapBytes(BitConverter.ToInt32(data, eoffset));
                                ValueTag(sb, cind, ename, int6aVal.ToString());
                                break;
                            case 1: //unsigned int
                                var int6bVal = MetaUtils.SwapBytes(BitConverter.ToUInt32(data, eoffset));
                                ValueTag(sb, cind, ename, "0x" + int6bVal.ToString("X").PadLeft(8, '0'));
                                break;
                        }
                        break;
                    case RageLib.GTA5.PSO.DataType.Long:
                        var long2Val = MetaUtils.SwapBytes(BitConverter.ToUInt64(data, eoffset));
                        ValueTag(sb, cind, ename, long2Val.ToString());
                        break;
                    case RageLib.GTA5.PSO.DataType.Map:

                        WriteMapNode(sb, indent, cont, eoffset, entry, structInfo, ename);

                        break;
                    case RageLib.GTA5.PSO.DataType.SShort:
                        var short3Val = (short)MetaUtils.SwapBytes(BitConverter.ToUInt16(data, eoffset));
                        ValueTag(sb, cind, ename, short3Val.ToString());
                        break;
                    case RageLib.GTA5.PSO.DataType.UShort:
                        var short4Val = MetaUtils.SwapBytes(BitConverter.ToUInt16(data, eoffset));
                        ValueTag(sb, cind, ename, short4Val.ToString());
                        break;
                    case RageLib.GTA5.PSO.DataType.HFloat://half float?
                        var short1EVal = MetaUtils.SwapBytes(BitConverter.ToUInt16(data, eoffset));
                        ValueTag(sb, cind, ename, short1EVal.ToString());
                        break;
                    case RageLib.GTA5.PSO.DataType.String:
                        var str0 = GetStringValue(cont.Pso, entry, data, eoffset);
                        if (str0 == null)
                        {
                            ErrorXml(sb, cind, ename + ": Unexpected String subtype: " + entry.Unk_5h.ToString());
                        }
                        else
                        {
                            StringTag(sb, cind, ename, str0);
                        }
                        break;
                    case RageLib.GTA5.PSO.DataType.Structure:
                        switch (entry.Unk_5h)
                        {
                            default:
                                ErrorXml(sb, cind, ename + ": Unexpected Structure subtype: " + entry.Unk_5h.ToString());
                                break;
                            case 0: //default structure
                                OpenTag(sb, cind, ename);
                                WriteNode(sb, cind, cont, blockId, offset + entry.DataOffset, XmlTagMode.None, (MetaName)entry.ReferenceKey);
                                CloseTag(sb, cind, ename);
                                break;
                            case 3: //structure pointer...
                            case 4: //also pointer? what's the difference?
                                var ptrVal = MetaUtils.ConvertData<PsoPOINTER>(data, eoffset);
                                ptrVal.SwapEnd();
                                var pbid = ptrVal.BlockID;
                                bool pbok = true;
                                if (pbid <= 0)
                                {
                                    pbok = false; //no block specified?
                                }
                                if (pbid > cont.Pso.DataMappingSection.EntriesCount)
                                {
                                    pbok = false; //bad pointer? different type..? should output an error message here?
                                }
                                if (pbok)
                                {
                                    WriteNode(sb, cind, cont, ptrVal.BlockID, (int)ptrVal.ItemOffset, XmlTagMode.None, (MetaName)entry.ReferenceKey);
                                }
                                else
                                {
                                    SelfClosingTag(sb, cind, ename);
                                }
                                break;
                        }
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

        private static void WriteArrayNode(StringBuilder sb, int indent, PsoCont cont, int blockId, int offset, PsoStructureEntryInfo entry, PsoStructureInfo estruct, string ename)
        {


            var block = cont.Pso.GetBlock(blockId);
            var boffset = offset + block.Offset;
            var eoffset = boffset + entry.DataOffset;
            var aOffset = offset + entry.DataOffset;
            var aBlockId = blockId;
            uint aCount = ((uint)entry.ReferenceKey >> 16) & 0x0000FFFF;
            var aind = indent + 1;
            string arrTag = ename;
            PsoStructureEntryInfo arrEntry = estruct.GetEntry((int)(entry.ReferenceKey & 0xFFFF));
            if (arrEntry == null)
            {
                ErrorXml(sb, indent, "ARRAYINFO not found for " + ename + "!");
                return;
            }

            var data = cont.Pso.DataSection.Data;

            switch (entry.Unk_5h)
            {
                default:
                    ErrorXml(sb, indent, ename + ": WIP! Unsupported Array subtype: " + entry.Unk_5h.ToString());
                    break;
                case 0: //Array_Structure
                    var arrStruc = MetaUtils.ConvertData<Array_Structure>(data, eoffset);
                    arrStruc.SwapEnd();
                    aBlockId = (int)arrStruc.PointerDataId;
                    aOffset = (int)arrStruc.PointerDataOffset;
                    aCount = arrStruc.Count1;
                    break;
                case 1: //Raw in-line array
                    break;
                case 2: //also raw in-line array, but how different from above?
                    break;
                case 4: //pointer array? default array?
                    if (arrEntry.Unk_5h == 3) //pointers...
                    {
                        var arrStruc4 = MetaUtils.ConvertData<Array_Structure>(data, eoffset);
                        arrStruc4.SwapEnd();
                        aBlockId = (int)arrStruc4.PointerDataId;
                        aOffset = (int)arrStruc4.PointerDataOffset;
                        aCount = arrStruc4.Count1;
                    }
                    break;
                case 129: //also raw inline array? in junctions.pso
                    break;
            }

            switch (arrEntry.Type)
            {
                default:
                    ErrorXml(sb, indent, ename + ": WIP! Unsupported array entry DataType: " + arrEntry.Type.ToString());
                    break;
                case RageLib.GTA5.PSO.DataType.Array:
                    var rk0 = (entry.ReferenceKey >> 16) & 0x0000FFFF;
                    if (rk0 > 0)
                    {
                        //var arrStruc5 = MetaUtils.ConvertDataArray<Array_StructurePointer>(data, eoffset, (int)rk0);
                        //for (int n = 0; n < rk0; n++) arrStruc5[n].SwapEnd();
                        aOffset = offset + entry.DataOffset;

                        OpenTag(sb, indent, arrTag);
                        for (int n = 0; n < rk0; n++) //ARRAY ARRAY!
                        {
                            WriteArrayNode(sb, aind, cont, blockId, aOffset, arrEntry, estruct, "Item");

                            aOffset += 16;//ptr size... todo: what if not pointer array?
                        }
                        CloseTag(sb, indent, ename);
                    }
                    else
                    {
                        SelfClosingTag(sb, indent, arrTag);
                    }
                    break;
                case RageLib.GTA5.PSO.DataType.Structure:
                    switch (arrEntry.Unk_5h)
                    {
                        case 0:
                            break;
                        case 3://structure pointer array
                            var arrStrucPtr = MetaUtils.ConvertData<Array_StructurePointer>(data, eoffset);
                            arrStrucPtr.SwapEnd();
                            aBlockId = (int)arrStrucPtr.PointerDataId;
                            aOffset = (int)arrStrucPtr.PointerDataOffset;
                            aCount = arrStrucPtr.Count1;
                            if (aCount > 0)
                            {
                                var ptrArr = PsoUtils.GetPointerArray(cont.Pso, arrStrucPtr);
                                OpenTag(sb, indent, arrTag);
                                for (int n = 0; n < aCount; n++)
                                {
                                    var ptrVal = ptrArr[n];
                                    WriteNode(sb, aind, cont, ptrVal.BlockID, (int)ptrVal.ItemOffset, XmlTagMode.ItemAndType);
                                }
                                CloseTag(sb, indent, ename);
                            }
                            break;
                        default:
                            break;
                    }
                    arrTag += " itemType=\"" + HashString((MetaName)arrEntry.ReferenceKey) + "\"";
                    if (aCount > 0)
                    {
                        var aBlock = cont.Pso.GetBlock(aBlockId);
                        var atyp = cont.GetStructureInfo((MetaName)arrEntry.ReferenceKey);
                        if (aBlock == null)
                        {
                            ErrorXml(sb, indent, ename + ": Array block not found: " + aBlockId.ToString());
                        }
                        else if ((MetaName)aBlock.NameHash != MetaName.PsoPOINTER)
                        {
                            OpenTag(sb, indent, arrTag);
                            if (atyp == null)
                            {
                                ErrorXml(sb, indent, ename + ": Array type not found: " + HashString((MetaName)arrEntry.ReferenceKey));
                            }
                            else
                            {
                                for (int n = 0; n < aCount; n++)
                                {
                                    WriteNode(sb, aind, cont, aBlockId, aOffset, XmlTagMode.Item, (MetaName)arrEntry.ReferenceKey);
                                    aOffset += atyp.StructureLength;
                                    if ((n < (aCount - 1)) && (aBlock != null) && (aOffset >= aBlock.Length))
                                    {
                                        break;
                                    }
                                }
                            }
                            CloseTag(sb, indent, ename);
                        }
                        else
                        { } //pointer array should get here, but it's already handled above. should improve this.
                    }
                    else
                    {
                        SelfClosingTag(sb, indent, arrTag);
                    }
                    break;
                case RageLib.GTA5.PSO.DataType.String:
                    switch (entry.Unk_5h)
                    {
                        default:
                            ErrorXml(sb, indent, ename + ": Unexpected String array subtype: " + entry.Unk_5h.ToString());
                            break;
                        case 0: //hash array...
                            var arrHash = MetaUtils.ConvertData<Array_uint>(data, eoffset);
                            arrHash.SwapEnd();
                            var hashArr = PsoUtils.GetHashArray(cont.Pso, arrHash);
                            WriteItemArray(sb, hashArr, indent, ename, "Hash", HashString);
                            break;
                    }
                    break;
                case RageLib.GTA5.PSO.DataType.Float2:
                    aCount = ((uint)entry.ReferenceKey >> 16) & 0x0000FFFF;
                    arrTag += " itemType=\"Vector2\"";
                    var v2Arr = MetaUtils.ConvertDataArray<Vector2>(data, eoffset, (int)aCount);
                    WriteRawArray(sb, v2Arr, indent, ename, "Vector2", FormatVector2Swap, 1);
                    break;
                case RageLib.GTA5.PSO.DataType.Float3:
                    aCount = ((uint)entry.ReferenceKey >> 16) & 0x0000FFFF;
                    arrTag += " itemType=\"Vector3\""; //this is actually aligned as vector4, the W values are crazy in places
                    var v4Arr = MetaUtils.ConvertDataArray<Vector4>(data, eoffset, (int)aCount);
                    WriteRawArray(sb, v4Arr, indent, ename, "Vector3", FormatVector4SwapXYZOnly, 1);
                    break;
                case RageLib.GTA5.PSO.DataType.UByte:
                    var barr = new byte[aCount];
                    if (aCount > 0)
                    {
                        var bblock = cont.Pso.GetBlock(aBlockId);
                        var boffs = bblock.Offset + aOffset;
                        Buffer.BlockCopy(data, boffs, barr, 0, (int)aCount);
                    }
                    WriteRawArray(sb, barr, indent, ename, "byte");
                    break;
                case RageLib.GTA5.PSO.DataType.Bool:
                    var barr2 = new byte[aCount];
                    if (aCount > 0)
                    {
                        var bblock = cont.Pso.GetBlock(aBlockId);
                        var boffs = bblock.Offset + aOffset;
                        Buffer.BlockCopy(data, boffs, barr2, 0, (int)aCount);
                    }
                    WriteRawArray(sb, barr2, indent, ename, "boolean"); //todo: true/false output
                    break;
                case RageLib.GTA5.PSO.DataType.Float:
                    var arrFloat = MetaUtils.ConvertData<Array_float>(data, eoffset);
                    arrFloat.SwapEnd();
                    var floatArr = PsoUtils.GetFloatArray(cont.Pso, arrFloat);
                    WriteRawArray(sb, floatArr, indent, ename, "float");
                    break;
                case RageLib.GTA5.PSO.DataType.UShort:
                    var arrShort = MetaUtils.ConvertData<Array_Structure>(data, eoffset);
                    arrShort.SwapEnd();
                    var shortArr = PsoUtils.GetUShortArray(cont.Pso, arrShort);
                    WriteRawArray(sb, shortArr, indent, ename, "ushort");
                    break;
                case RageLib.GTA5.PSO.DataType.UInt:
                    var intArr = MetaUtils.ConvertDataArray<int>(data, eoffset, (int)aCount);
                    WriteRawArray(sb, intArr, indent, ename, "int");
                    break;
                case RageLib.GTA5.PSO.DataType.SInt:
                    var arrUint2 = MetaUtils.ConvertData<Array_uint>(data, eoffset);
                    arrUint2.SwapEnd();
                    var intArr2 = PsoUtils.GetUintArray(cont.Pso, arrUint2);
                    WriteRawArray(sb, intArr2, indent, ename, "int");
                    break;
                case RageLib.GTA5.PSO.DataType.Enum:
                    var arrEnum = MetaUtils.ConvertData<Array_uint>(data, eoffset);
                    arrEnum.SwapEnd();
                    var enumArr = PsoUtils.GetUintArray(cont.Pso, arrEnum);
                    var enumDef = cont.GetEnumInfo((MetaName)arrEntry.ReferenceKey);
                    WriteItemArray(sb, enumArr, indent, ename, "enum", (ie) => {
                        var eval = enumDef?.FindEntry((int)ie);
                        return HashString((MetaName)(eval?.EntryNameHash ?? 0));
                    });
                    break;
            }

        }

        private static void WriteMapNode(StringBuilder sb, int indent, PsoCont cont, int eoffset, PsoStructureEntryInfo entry, PsoStructureInfo structInfo, string ename)
        {
            var cind = indent + 1;
            var data = cont.Pso.DataSection.Data;
            switch (entry.Unk_5h)
            {
                default:
                    ErrorXml(sb, cind, ename + ": Unexpected Map subtype: " + entry.Unk_5h.ToString());
                    break;
                case 1:
                    var mapidx1 = entry.ReferenceKey & 0x0000FFFF;
                    var mapidx2 = (entry.ReferenceKey >> 16) & 0x0000FFFF;
                    var mapreftype1 = structInfo.Entries[mapidx2];
                    var mapreftype2 = structInfo.Entries[mapidx1];
                    var x1 = MetaUtils.SwapBytes(BitConverter.ToInt32(data, eoffset));//same as ref key?
                    var x2 = MetaUtils.SwapBytes(BitConverter.ToInt32(data, eoffset + 4));//0?
                    var x3 = MetaUtils.SwapBytes(BitConverter.ToInt32(data, eoffset + 8));//pointer?
                    var x4 = MetaUtils.SwapBytes(BitConverter.ToInt32(data, eoffset + 12));//
                    var x5 = MetaUtils.SwapBytes(BitConverter.ToInt32(data, eoffset + 16));//count/capacity?
                    var x6 = MetaUtils.SwapBytes(BitConverter.ToInt32(data, eoffset + 20));//

                    //File.WriteAllText("C:\\CodeWalker.Projects\\testxml.xml", sb.ToString());

                    if (x1 != 0x1000000)
                    { }
                    if (x2 != 0)
                    { }
                    if (x4 != 0)
                    { }
                    if (x6 != 0)
                    { }


                    var xBlockId = x3 & 0xFFF;
                    var xOffset = (x3 >> 12) & 0xFFFFF;
                    var xCount1 = x5 & 0xFFFF;
                    var xCount2 = (x5 >> 16) & 0xFFFF;

                    //var x1a = x1 & 0xFFF; //block id? for another pointer?
                    //var x1b = (x1 >> 12) & 0xFFFFF; //offset?
                    //var x4u = (uint)x4;
                    //var x4a = x4 & 0xFFF; //block id?
                    //var x4b = (x4 >> 12) & 0xFFFFF; //offset?
                    //var x2h = (MetaHash)(uint)x2;
                    //var x6h = (MetaHash)(uint)x6;
                    //if (x1a > 0)
                    //{ }



                    var xBlock = cont.Pso.GetBlock(xBlockId);
                    if ((xBlock == null) && (xCount1 > 0))
                    {
                        ErrorXml(sb, cind, ename + ": Couldn't find Map xBlock: " + xBlockId.ToString());
                    }
                    else
                    {
                        if (xCount1 != xCount2)
                        {
                        }
                        if (xCount1 > 0)
                        {
                            var xStruct = cont.GetStructureInfo((MetaName)xBlock.NameHash);
                            var xOffset1 = xOffset;
                            var xind = indent + 1;
                            var aind = indent + 2;
                            var kEntry = xStruct?.FindEntry(MetaName.Key);
                            var iEntry = xStruct?.FindEntry(MetaName.Item);

                            if ((xStruct == null) && (xBlock.NameHash == 0))
                            {
                                SelfClosingTag(sb, cind, ename);
                            }
                            else if (xStruct == null)
                            {
                                ErrorXml(sb, aind, ename + ": Map struct type not found: " + HashString((MetaName)xBlock.NameHash));
                            }
                            else if ((xStruct.IndexInfo == null))// || (xStruct.IndexInfo.NameHash != MetaName.ARRAYINFO))
                            {
                                ErrorXml(sb, aind, ename + ": Map struct was missing IndexInfo! " + (xStruct == null ? "" : xStruct.ToString()));
                            }
                            else if ((kEntry == null) || (iEntry == null))
                            {
                                ErrorXml(sb, aind, ename + ": Map Key/Item entries not found!");
                            }
                            else if (kEntry.Type != RageLib.GTA5.PSO.DataType.String)
                            {
                                ErrorXml(sb, aind, ename + ": Map Key was not a string!");
                            }
                            else if (iEntry.Type != RageLib.GTA5.PSO.DataType.Structure)
                            {
                                ErrorXml(sb, aind, ename + ": Map Item was not a structure!");
                            }
                            else if (iEntry.Unk_5h != 3)
                            {
                                ErrorXml(sb, aind, ename + ": Map Item was not a structure pointer - TODO!");
                            }
                            else
                            {
                                OpenTag(sb, xind, ename);
                                int xOffset2 = (int)xOffset1;
                                int xCount = xCount1;

                                for (int n = 0; n < xCount; n++)
                                {
                                    //WriteNode(sb, aind, cont, xBlockId, xOffset, XmlTagMode.Item, xStruct.IndexInfo.NameHash);

                                    int sOffset = xOffset2 + xBlock.Offset;
                                    var kOffset = sOffset + kEntry.DataOffset;
                                    var iOffset = sOffset + iEntry.DataOffset;
                                    var kStr = GetStringValue(cont.Pso, kEntry, data, kOffset);
                                    var iPtr = MetaUtils.ConvertData<PsoPOINTER>(data, iOffset);
                                    iPtr.SwapEnd();
                                    var iBlock = cont.Pso.GetBlock(iPtr.BlockID);
                                    if (iBlock == null)
                                    {
                                        OpenTag(sb, aind, "Item type=\"" + HashString((MetaName)entry.ReferenceKey) + "\" key=\"" + kStr + "\"");
                                        WriteNode(sb, aind, cont, iPtr.BlockID, (int)iPtr.ItemOffset, XmlTagMode.None, (MetaName)entry.ReferenceKey);
                                        CloseTag(sb, aind, "Item");
                                    }
                                    else
                                    {
                                        var iStr = "Item type=\"" + HashString((MetaName)iBlock.NameHash) + "\" key=\"" + kStr + "\"";
                                        var iStruc = cont.GetStructureInfo((MetaName)iBlock.NameHash);
                                        if (iStruc?.EntriesCount == 0)
                                        {
                                            //SelfClosingTag(sb, aind, iStr);
                                            OpenTag(sb, aind, iStr);
                                            CloseTag(sb, aind, "Item");
                                        }
                                        else
                                        {
                                            OpenTag(sb, aind, iStr);
                                            WriteNode(sb, aind, cont, iPtr.BlockID, (int)iPtr.ItemOffset, XmlTagMode.None);//, (MetaName)entry.ReferenceKey);
                                            CloseTag(sb, aind, "Item");
                                        }
                                    }
                                    xOffset2 += xStruct.StructureLength;
                                    if ((n < (xCount - 1)) && (xBlock != null) && (xOffset >= xBlock.Length))
                                    {
                                        ErrorXml(sb, aind, "Offset out of range! Count is " + xCount.ToString());
                                        break; //out of range...
                                    }
                                }
                                CloseTag(sb, xind, ename);
                            }
                        }
                        else
                        {
                            SelfClosingTag(sb, cind, ename);
                        }
                    }
                    break;
            }
        }




        private static string GetStringValue(PsoFile pso, PsoStructureEntryInfo entry, byte[] data, int eoffset)
        {
            switch (entry.Unk_5h)
            {
                default:
                    return null;
                case 0:
                    var str0len = (int)((entry.ReferenceKey >> 16) & 0xFFFF);
                    return Encoding.ASCII.GetString(data, eoffset, str0len).Replace("\0", "");
                case 1:
                case 2:
                    var dataPtr2 = MetaUtils.ConvertData<DataBlockPointer>(data, eoffset);
                    dataPtr2.SwapEnd();
                    return PsoUtils.GetString(pso, dataPtr2);
                case 3:
                    var charPtr3 = MetaUtils.ConvertData<CharPointer>(data, eoffset);
                    charPtr3.SwapEnd();
                    var strval = PsoUtils.GetString(pso, charPtr3);
                    return strval ?? "";
                case 7:
                case 8:
                    MetaName hashVal = (MetaName)MetaUtils.SwapBytes(MetaUtils.ConvertData<uint>(data, eoffset));
                    return HashString(hashVal);
            }

        }




        public class PsoCont
        {
            public PsoFile Pso { get; set; }

            public Dictionary<MetaName, PsoEnumInfo> EnumDict = new Dictionary<MetaName, PsoEnumInfo>();
            public Dictionary<MetaName, PsoStructureInfo> StructDict = new Dictionary<MetaName, PsoStructureInfo>();


            public PsoCont(PsoFile pso)
            {
                Pso = pso;

                if ((pso.DefinitionSection == null) || (pso.DefinitionSection.Entries == null) || (pso.DefinitionSection.EntriesIdx == null))
                {
                    return;
                }


                for (int i = 0; i < pso.DefinitionSection.Entries.Count; i++)
                {
                    var entry = pso.DefinitionSection.Entries[i];
                    var enuminfo = entry as PsoEnumInfo;
                    var structinfo = entry as PsoStructureInfo;

                    if (enuminfo != null)
                    {
                        if (!EnumDict.ContainsKey((MetaName)enuminfo.IndexInfo.NameHash))
                        {
                            EnumDict.Add((MetaName)enuminfo.IndexInfo.NameHash, enuminfo);
                        }
                        else
                        {
                            //PsoEnumInfo oldei = EnumDict[enuminfo.IndexInfo.NameHash];
                            //if (!ComparePsoEnumInfos(oldei, enuminfo))
                            //{
                            //}
                        }
                    }
                    else if (structinfo != null)
                    {
                        if (!StructDict.ContainsKey((MetaName)structinfo.IndexInfo.NameHash))
                        {
                            StructDict.Add((MetaName)structinfo.IndexInfo.NameHash, structinfo);
                        }
                        else
                        {
                            //PsoStructureInfo oldsi = StructDict[structinfo.IndexInfo.NameHash];
                            //if (!ComparePsoStructureInfos(oldsi, structinfo))
                            //{
                            //}
                        }
                    }

                }
            }


            public PsoStructureInfo GetStructureInfo(MetaName name)
            {
                PsoStructureInfo i = null;
                StructDict.TryGetValue(name, out i);
                return i;
            }
            public PsoEnumInfo GetEnumInfo(MetaName name)
            {
                PsoEnumInfo i = null;
                EnumDict.TryGetValue(name, out i);
                return i;
            }

        }

    }
}
