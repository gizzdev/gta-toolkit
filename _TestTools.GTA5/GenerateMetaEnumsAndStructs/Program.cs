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
using System.IO;
using System.Linq;
using System.Text;
using RageLib.GTA5.Cryptography;
using RageLib.GTA5.Utilities;
using RageLib.Hash;
using RageLib.Resources.GTA5;
using RageLib.Resources.GTA5.PC.Meta;

namespace GenerateMetaEnumsAndStructs
{
    class Program
    {
        static Dictionary<int, string> Hashes = new Dictionary<int, string>();
        static Dictionary<int, EnumInfo> Enums = new Dictionary<int, EnumInfo>();
        static Dictionary<int, StructureInfo> Structures = new Dictionary<int, StructureInfo>();
        static Dictionary<int, long> StructuresLengths = new Dictionary<int, long>();
        static List<StructureInfo> StructureInfos = new List<StructureInfo>();
        static List<EnumInfo> EnumInfos = new List<EnumInfo>();
        static Dictionary<StructureInfo, List<EnumInfo>> StructureEnums = new Dictionary<StructureInfo, List<EnumInfo>>();
        static Dictionary<StructureInfo, List<int>> StructureChildren = new Dictionary<StructureInfo, List<int>>();
        static void Main(string[] args)
        {
            GTA5Constants.LoadFromPath(".");

            string GTAVDir = args[0];

            HashEmbeddedStrings();
            SaveMetaNames();

            var sbe  = new StringBuilder();
            var sbs  = new StringBuilder();
            var sbei = new StringBuilder();
            var sbsi = new StringBuilder();
            var sbmt = new StringBuilder();
            var sbsei = new StringBuilder();
            var sbsci = new StringBuilder();

            sbe.AppendLine("\t// Enums\n");
            sbs.AppendLine("\t// Structures\n");

            Console.CancelKeyPress += new ConsoleCancelEventHandler((s, e) =>
            {
                Console.Error.WriteLine("Generating structure and enum infos for gathered data");

                GenerateStructureData(sbs, sbsi);
                GenerateEnumData(sbei);
                GenerateStructureEnumInfos(sbsei);
                GenerateStructureChildInfos(sbsci);

                var data = CompileData(sbmt, sbei, sbsi, sbe, sbs, sbsei, sbsci);

                File.WriteAllText("MetaTypes.cs", data);

                Environment.Exit(0);
            });

            ArchiveUtilities.ForEachFile(GTAVDir, (fullName, file, encryption) =>
            {
                if(file.Name.EndsWith(".ymap") || file.Name.EndsWith(".ytyp"))
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        file.Export(ms);

                        try
                        {
                            var res = new ResourceFile_GTA5_pc<MetaFile>();

                            res.Load(ms);

                            var meta = res.ResourceData;

                            GetEnums(meta, Enums, sbe);
                            GetStructures(meta, Structures);
                        }
                        catch (Exception e)
                        {
                        }

                    }
                }
            });
        }

        static void GetEnums(MetaFile meta, Dictionary<int, EnumInfo> enums, StringBuilder sb)
        {
            if (meta.EnumInfos != null)
            {
                foreach (var ei in meta.EnumInfos)
                {
                    if (!enums.ContainsKey(ei.EnumKey))
                    {
                        var enumName = GetSafeName(ei.EnumNameHash, ei.EnumKey);

                        Console.Error.WriteLine("ENUM   " + enumName);

                        sb.AppendFormat("\tpublic enum {0} // Key : {1}\n", enumName, ei.EnumKey);
                        sb.AppendLine("\t{");

                        foreach (var entry in ei.Entries)
                        {
                            string entryName = GetSafeName(entry.EntryNameHash, entry.EntryValue);
                            sb.AppendFormat("\t\t{0} = {1},\n", entryName, entry.EntryValue);
                        }

                        EnumInfos.Add(ei);

                        sb.AppendLine("\t}\n");

                        enums.Add(ei.EnumKey, ei);
                    }
                }
            }
        }

        static void GetStructures(MetaFile meta, Dictionary<int, StructureInfo> structs)
        {
            if (meta.StructureInfos != null)
            {
                var data = new Dictionary<int, string>();

                foreach (var si in meta.StructureInfos)
                {
                    if (!structs.ContainsKey(si.StructureKey))
                    {
                        Console.Error.WriteLine("STRUCT " + GetSafeName(si.StructureNameHash, si.StructureKey));

                        structs.Add(si.StructureKey, si);

                        if (!StructuresLengths.ContainsKey(si.StructureNameHash))
                            StructuresLengths.Add(si.StructureNameHash, si.StructureLength);

                        if (!StructureEnums.ContainsKey(si))
                            StructureEnums.Add(si, new List<EnumInfo>());

                        if (!StructureChildren.ContainsKey(si))
                            StructureChildren.Add(si, new List<int>());

                        StructureInfos.Add(si);
                    }
                }

            }
        }

        static void SaveMetaNames()
        {
            var sb = new StringBuilder();

            sb.AppendLine("namespace RageLib.Resources.GTA5.PC.Meta");
            sb.AppendLine("{");
            sb.AppendLine("\tpublic enum MetaName : int");
            sb.AppendLine("\t{");

            foreach (var entry in Hashes)
                sb.AppendFormat("\t\t{0} = {1},\n", entry.Value, entry.Key);

            sb.AppendLine("\t}");
            sb.AppendLine("}");

            File.WriteAllText("MetaNames.cs", sb.ToString());
        }

        static void HashEmbeddedStrings()
        {
            var metanames = Resource.metanames.Split(new string[] { "\r\n" }, StringSplitOptions.None);

            Hashes.Add(-489959468, "VECTOR3"); //0xe2cbcfd4, //this hash isn't correct, but is used in CDistantLODLight
            Hashes.Add(0x33, "VECTOR4");
            Hashes.Add(0x4a, "HASH");
            Hashes.Add(0x10, "STRING");
            Hashes.Add(0x7, "POINTER");
            Hashes.Add(0x13, "USHORT");
            Hashes.Add(0x15, "UINT");
            Hashes.Add(0x100, "ARRAYINFO");
            Hashes.Add(17, "BYTE");
            Hashes.Add(33, "FLOAT");
            Hashes.Add(12, "PsoPOINTER");

            for (int i = 0; i < metanames.Length; i++)
            {
                int hash = (int)Jenkins.Hash(metanames[i]);

                if (!String.IsNullOrEmpty(metanames[i]) && !Hashes.ContainsKey(hash))
                {
                    bool found = false;

                    foreach (var kvp in Hashes)
                    {
                        if (kvp.Value == metanames[i])
                        {
                            found = true;
                            break;
                        }
                    }

                    if(!found)
                        Hashes.Add(hash, metanames[i]);
                }
            }
        }

        static string GetSafeName(int hash, int key)
        {
            string name = GetString(hash);

            if (string.IsNullOrEmpty(name))
                name = "Unk_" + (uint) key;

            if (!char.IsLetter(name[0]))
                name = "Unk_" + name;

            return name;
        }

        static string GetString(int hash)
        {
            string str;

            if(!Hashes.TryGetValue(hash, out str))
                str = ((uint) hash).ToString();

            return str;
        }

        static string GetMetaNameString(int n)
        {
            if (Enum.IsDefined(typeof(MetaName), n))
            {
                return "MetaName." + ((MetaName)n).ToString();
            }
            else
            {
                return "(MetaName) (" + Convert.ToString(n) + ")";
            }
        }

        static int GetHash(string name)
        {
            return Hashes.FirstOrDefault(x => x.Value == name).Key;
        }

        static string GetCSharpTypeName(StructureEntryDataType t)
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

        static long GetCSharpTypeSize(StructureEntryDataType t, long size)
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

        static long GetStructureSize(int key)
        {
            lock (StructuresLengths)
            {
                long size;

                if (!StructuresLengths.TryGetValue(key, out size))
                    size = (long)-1;

                return size;
            }
        }

        static string GenerateStructureDefiniton(StructureInfo def)
        {
            var sb = new StringBuilder();

            var structureName = GetSafeName(def.StructureNameHash, def.StructureKey);
            var structureKeyName = GetString(def.StructureKey);
            int unusedCount = 0;
            long offset = 0;
            StructureEntryInfo prevEntry = null;
            var sbc = new StringBuilder();
            var sbcp = new StringBuilder();
            var sbcb = new StringBuilder();

            sbc.AppendLine("using System.Collections.Generic;");
            sbc.AppendLine("using System.Linq;");
            sbc.AppendLine("using SharpDX;");
            sbc.AppendLine("using RageLib.Resources.GTA5.PC.Meta;\n");
            sbc.AppendLine("namespace RageLib.GTA5.ResourceWrappers.PC.Meta.Structures");
            sbc.AppendLine("{");
            sbc.AppendFormat("\tpublic class {0} : MetaStructureWrapper<RageLib.Resources.GTA5.PC.Meta.{0}>\n", structureName);
            sbc.AppendLine("\t{");
            sbc.AppendLine("\t\tpublic MetaFile Meta;");

            sb.AppendFormat("\t[StructLayout(LayoutKind.Sequential)] public struct {0} // {1} bytes, Key:{2}\n", structureName, def.StructureLength, structureKeyName);
            sb.AppendLine("\t{");

            for (int i = 0; i < def.Entries.Count; i++)
            {
                var entry = def.Entries[i];
                var entryName = GetSafeName(entry.EntryNameHash, entry.ReferenceKey);
                var entryNameUpper = char.ToUpper(entryName[0]) + entryName.Substring(1);

                EnumInfo matchingEnum = null;

                for (int j = 0; j < EnumInfos.Count; j++)
                {
                    if (entry.ReferenceKey == EnumInfos[j].EnumNameHash)
                    {
                        matchingEnum = EnumInfos[j];
                        StructureEnums[def].Add(matchingEnum);
                        break;
                    }
                }

                var sc = StructureChildren[def];

                if (prevEntry == null || entry.DataOffset > offset)
                {
                    long remaining = entry.DataOffset - offset;

                    while (remaining > 0)
                    {
                        if (remaining % 8 == 0)
                        {
                            sb.AppendFormat("\t\tpublic {0} {1}; // {2}\n", "long", "Unused" + (unusedCount++), offset, 8);
                            offset += 8;
                            remaining -= 8;
                        }
                        else if (remaining % 4 == 0)
                        {
                            sb.AppendFormat("\t\tpublic {0} {1}; // {2}\n", "uint", "Unused" + (unusedCount++), offset, 4);
                            offset += 4;
                            remaining -= 4;
                        }
                        else if (remaining % 2 == 0)
                        {
                            sb.AppendFormat("\t\tpublic {0} {1}; // {2}\n", "short", "Unused" + (unusedCount++), offset, 2);
                            offset += 2;
                            remaining -= 2;
                        }
                        else
                        {
                            sb.AppendFormat("\t\tpublic {0} {1}; // {2}\n", "byte", "Unused" + (unusedCount++), offset, 1);
                            offset += 1;
                            remaining -= 1;
                        }
                    }
                }

                if (entry.DataType == StructureEntryDataType.Array)
                {
                    var structEntry = def.Entries[entry.ReferenceTypeIndex];
                    var entryTypeString = (matchingEnum == null) ? GetCSharpTypeName(structEntry.DataType) : GetSafeName(matchingEnum.EnumNameHash, matchingEnum.EnumKey);
                    var CSharpTypeName = GetCSharpTypeName(structEntry.DataType);
                    long structureSize = GetStructureSize(structEntry.ReferenceKey);

                    sb.AppendFormat("\t\tpublic Array_{0} {1}; // {2}  Key: {3}\n", GetCSharpTypeName(structEntry.DataType), entryName, entry.DataOffset, GetString(structEntry.ReferenceKey));

                    if (CSharpTypeName == "Structure" && structureSize != -1)
                    {
                        sbc.AppendFormat("\t\tpublic List<{0}> {1};\n", GetSafeName(structEntry.ReferenceKey, structEntry.ReferenceKey), entryNameUpper);

                        sbcp.AppendFormat("\t\t\tvar {0} = MetaUtils.ConvertDataArray<RageLib.Resources.GTA5.PC.Meta.{1}>(meta, {2}.{0});\n", entryName, GetSafeName(structEntry.ReferenceKey, structEntry.ReferenceKey), structureName);
                        sbcp.AppendFormat("\t\t\tthis.{0} = {3}?.Select(e => {{ var msw = new {1}(); msw.Parse(meta, e); return msw; }}).ToList();\n\n", entryNameUpper, GetSafeName(structEntry.ReferenceKey, structEntry.ReferenceKey), structureName, entryName);

                        sbcb.AppendFormat("\t\t\tif(this.{0} != null)\n", entryNameUpper);
                        sbcb.AppendFormat("\t\t\t\tthis.MetaStructure.{0} = mb.AddItemArrayPtr({1}, this.{2}.Select(e => e.MetaStructure).ToArray());\n", entryName, GetMetaNameString(structEntry.ReferenceKey), entryNameUpper);
                    }
                    else
                    {
                        sbc.AppendFormat("\t\tpublic Array_{0} {1};\n", GetCSharpTypeName(structEntry.DataType), entryNameUpper);

                        sbcp.AppendFormat("\t\t\t// this.{0} = {1}.{2};\n", entryNameUpper, structureName, entryName);
                        sbcb.AppendFormat("\t\t\t// this.MetaStructure.{0} = this.{1};\n", entryName, entryNameUpper);
                    }

                    offset += GetCSharpTypeSize(entry.DataType, entry.Length);
                }
                else
                {
                    if (entry.DataType == StructureEntryDataType.Structure)
                    {
                        if(entry.EntryNameHash == 256)
                        {
                        }
                        else
                        {
                            long structureSize = GetStructureSize(entry.ReferenceKey);
                            var entryTypeString = (matchingEnum == null) ? GetCSharpTypeName(entry.DataType) : GetSafeName(matchingEnum.EnumNameHash, matchingEnum.EnumKey);

                            if (structureSize == -1)
                            {
                                sb.AppendFormat("\t\tpublic Array_{0} {1}; // {2}  Key: {3}\n", GetString(entry.ReferenceTypeIndex), entryName, entry.DataOffset, GetString(entry.ReferenceTypeIndex));
                                sbc.AppendFormat("\t\tpublic Array_{0};\n", entryTypeString, entryNameUpper);
                                sbcp.AppendFormat("\t\t\t// this.{0} = {1}.{2};\n", entryNameUpper, structureName, entryName);
                                sbcb.AppendFormat("\t\t\t// this._{0}.{1} = this.{2};\n", structureName, entryName, entryNameUpper);

                                offset += GetCSharpTypeSize(entry.DataType, entry.Length);
                            }
                            else
                            {
                                if (!sc.Contains(entry.ReferenceKey))
                                    sc.Add(entry.ReferenceKey);

                                sb.AppendFormat("\t\tpublic {0} {1}; // {2}  Key: {3}\n", GetSafeName(entry.ReferenceKey, entry.ReferenceTypeIndex), entryName, entry.DataOffset, GetString(entry.ReferenceKey));
                                sbc.AppendFormat("\t\tpublic {0} {1};\n", GetSafeName(entry.ReferenceKey, entry.ReferenceKey), entryNameUpper);

                                sbcp.AppendFormat("\t\t\tvar {0}Blocks = meta.FindBlocks(RageLib.Resources.GTA5.PC.Meta.{1});\n", entryName, GetMetaNameString(entry.ReferenceKey));
                                sbcp.AppendLine();
                                sbcp.AppendFormat("\t\t\tif({0}Blocks.Length > 0)\n", entryName);
                                sbcp.AppendLine("\t\t\t{");
                                sbcp.AppendFormat("\t\t\t\tvar {0} = MetaUtils.GetTypedData<RageLib.Resources.GTA5.PC.Meta.{1}>(meta, {2});\n", entryName, GetSafeName(entry.ReferenceKey, entry.ReferenceTypeIndex), GetMetaNameString(entry.ReferenceKey));
                                sbcp.AppendFormat("\t\t\t\tthis.{0} = new {1}();\n", entryNameUpper, GetSafeName(entry.ReferenceKey, entry.ReferenceTypeIndex));
                                sbcp.AppendFormat("\t\t\t\tthis.{0}.Parse(meta, {1});\n", entryNameUpper, entryName);
                                sbcp.AppendLine("\t\t\t}");
                                sbcp.AppendLine("\t\t\telse");
                                sbcp.AppendLine("\t\t\t{");
                                sbcp.AppendFormat("\t\t\t    this.{0} = null;\n", entryNameUpper);
                                sbcp.AppendLine("\t\t\t}\n");

                                sbcb.AppendFormat("\t\t\tif(this.{0} != null)\n", entryNameUpper);
                                sbcb.AppendLine("\t\t\t{");
                                sbcb.AppendFormat("\t\t\t\tthis.{0}.Build(mb);\n", entryNameUpper);
                                sbcb.AppendFormat("\t\t\t\tthis.MetaStructure.{0} = this.{1}.MetaStructure;\n", entryName, entryNameUpper);
                                sbcb.AppendLine("\t\t\t}\n");

                                offset += structureSize;
                            }
                        }
                    }
                    else if(entry.DataType == StructureEntryDataType.StructurePointer)
                    {
                    }
                    else
                    {
                        if(entry.EntryNameHash != 256)
                        {
                            var CSharpType = GetCSharpTypeName(entry.DataType);

                            if (CSharpType == "ArrayOfBytes")
                                CSharpType += Convert.ToString(entry.ReferenceKey);

                            if (matchingEnum != null)
                                CSharpType = GetSafeName(matchingEnum.EnumNameHash, matchingEnum.EnumKey);

                            sb.AppendFormat("\t\tpublic {0} {1}; // {2}  Key: {3}\n", CSharpType, entryName, entry.DataOffset, GetString(entry.ReferenceKey));

                            if (CSharpType == "CharPointer")
                            {
                                sbc.AppendFormat("\t\tpublic string {1} = \"\";\n", CSharpType, entryNameUpper);
                                sbcp.AppendFormat("\t\t\tthis.{0} = MetaUtils.GetString(Meta, {1}.{2});\n", entryNameUpper, structureName, entryName);
                                sbcb.AppendFormat("\t\t\tthis.MetaStructure.{0} = mb.AddStringPtr(this.{1});\n", entryName, entryNameUpper);
                            }
                            else
                            {
                                sbc.AppendFormat("\t\tpublic {0} {1};\n", CSharpType, entryNameUpper);
                                sbcp.AppendFormat("\t\t\tthis.{0} = {1}.{2};\n", entryNameUpper, structureName, entryName);
                                sbcb.AppendFormat("\t\t\tthis.MetaStructure.{0} = this.{1};\n", entryName, entryNameUpper);
                            }

                        }

                        offset += GetCSharpTypeSize(entry.DataType, entry.Length);

                    }
                }

                prevEntry = entry;
            }

            long remaining2 = def.Length - offset;

            while (remaining2 > 0)
            {
                if (remaining2 % 8 == 0)
                {
                    sb.AppendFormat("\t\tpublic {0} {1}; // {2}\n", "long", "Unused" + (unusedCount++), offset, 8);
                    offset += 8;
                    remaining2 -= 8;
                }
                else if (remaining2 % 4 == 0)
                {
                    sb.AppendFormat("\t\tpublic {0} {1}; // {2}\n", "uint", "Unused" + (unusedCount++), offset, 4);
                    offset += 4;
                    remaining2 -= 4;
                }
                else if (remaining2 % 2 == 0)
                {
                    sb.AppendFormat("\t\tpublic {0} {1}; // {2}\n", "short", "Unused" + (unusedCount++), offset, 2);
                    offset += 2;
                    remaining2 -= 2;
                }
                else
                {
                    sb.AppendFormat("\t\tpublic {0} {1}; // {2}\n", "byte", "Unused" + (unusedCount++), offset, 1);
                    offset += 1;
                    remaining2 -= 1;
                }
            }

            sb.AppendLine("\t}\n\n");

            sbc.AppendFormat("\n\t\tpublic {0}()\n", structureName);
            sbc.AppendLine("\t\t{");
            sbc.AppendFormat("\t\t\tthis.MetaName = {0};\n", GetMetaNameString(def.StructureNameHash));
            sbc.AppendFormat("\t\t\tthis.MetaStructure = new RageLib.Resources.GTA5.PC.Meta.{0}();\n", structureName);
            sbc.AppendLine("\t\t}");

            sbc.AppendLine();
            sbc.AppendFormat("\t\tpublic override void Parse(MetaFile meta, RageLib.Resources.GTA5.PC.Meta.{0} {0})\n", structureName);
            sbc.AppendLine("\t\t{");
            sbc.AppendLine("\t\t\tthis.Meta = meta;");
            sbc.AppendFormat("\t\t\tthis.MetaStructure = {0};\n", structureName);
            sbc.AppendLine();
            sbc.Append(sbcp);
            sbc.AppendLine("\t\t}\n");

            sbc.AppendLine("\t\tpublic override void Build(MetaBuilder mb, bool isRoot = false)");
            sbc.AppendLine("\t\t{");
            sbc.Append(sbcb);
            sbc.AppendLine();
            sbc.AppendLine("\t\t\tvar enumInfos = MetaInfo.GetStructureEnumInfo(this.MetaName);");
            sbc.AppendLine("\t\t\tvar structureInfo = MetaInfo.GetStructureInfo(this.MetaName);");
            sbc.AppendLine("\t\t\tvar childStructureInfos = MetaInfo.GetStructureChildInfo(this.MetaName);");
            sbc.AppendLine();
            sbc.AppendLine("\t\t\tfor (int i = 0; i < enumInfos.Length; i++)");
            sbc.AppendLine("\t\t\t\tmb.AddEnumInfo((MetaName) enumInfos[i].EnumNameHash);");
            sbc.AppendLine();
            sbc.AppendLine("\t\t\tmb.AddStructureInfo((MetaName) structureInfo.StructureNameHash);\n");
            sbc.AppendLine();
            sbc.AppendLine("\t\t\tfor (int i = 0; i < childStructureInfos.Length; i++)");
            sbc.AppendLine("\t\t\t\tmb.AddStructureInfo((MetaName) childStructureInfos[i].StructureNameHash);");
            sbc.AppendLine();
            sbc.AppendLine("\t\t\tif(isRoot)");
            sbc.AppendLine("\t\t\t{");
            sbc.AppendLine("\t\t\t\tmb.AddItem(this.MetaName, this.MetaStructure);\n");
            sbc.AppendLine("\t\t\t\tthis.Meta = mb.GetMeta();");
            sbc.AppendLine("\t\t\t}");
            sbc.AppendLine("\t\t}");

            sbc.AppendLine("\t}");
            sbc.AppendLine("}");

            File.WriteAllText( @".\MetaStructures\" + structureName + ".cs", sbc.ToString());

            return sb.ToString();
        }

        static string GenerateEnumInfos(EnumInfo def)
        {
            var sb = new StringBuilder();

            sb.AppendFormat("\t\t\t\tcase {0}:\n", GetMetaNameString(def.EnumNameHash));
            sb.AppendFormat("\t\t\t\t\treturn new EnumInfo({0}, {1}, {2}, {3}, new ResourceSimpleArray<EnumEntryInfo>() {{\n", def.EnumNameHash, def.EnumKey, def.EntriesPointer, def.Unknown_14h);

            for(int i=0; i<def.Entries.Count; i++)
            {
                sb.AppendFormat("\t\t\t\t\t\tnew EnumEntryInfo({0}, {1})", def.Entries[i].EntryNameHash, def.Entries[i].EntryValue);

                if (i < def.Entries.Count - 1)
                    sb.Append(",\n");
                else
                    sb.Append("\n");
            }

            sb.Append("\t\t\t\t\t});\n");

            sb.AppendLine();

            return sb.ToString();
        }

        static string GenerateStructureInfos(StructureInfo def)
        {
            var sb = new StringBuilder();

            sb.AppendFormat("\t\t\t\tcase {0}:\n", GetMetaNameString(def.StructureNameHash));
            sb.AppendFormat("\t\t\t\t\treturn new StructureInfo({0}, {1}, {2}, {3}, {4}, {5}, {6}, new ResourceSimpleArray<StructureEntryInfo>() {{\n", def.StructureNameHash, def.StructureKey, def.Unknown_8h, def.Unknown_Ch, def.EntriesPointer, def.StructureLength, def.Unknown_1Ch);

            for (int i = 0; i < def.Entries.Count; i++)
            {
                var entry = def.Entries[i];

                sb.AppendFormat("\t\t\t\t\t\tnew StructureEntryInfo({0}, {1}, StructureEntryDataType.{2}, {3}, {4}, {5})", entry.EntryNameHash, entry.DataOffset, entry.DataType, entry.Unknown_9h, entry.ReferenceTypeIndex, entry.ReferenceKey);

                if (i < def.Entries.Count - 1)
                    sb.Append(",\n");
                else
                    sb.Append("\n");
            }

            sb.Append("\t\t\t\t\t});\n");

            sb.AppendLine();

            return sb.ToString();
        }

        public static void GenerateStructureData(StringBuilder sbs, StringBuilder sbsi)
        {
            sbsi.AppendLine("\t\tpublic static StructureInfo GetStructureInfo(MetaName name)");
            sbsi.AppendLine("\t\t{");
            sbsi.AppendLine("\t\t\tswitch (name)");
            sbsi.AppendLine("\t\t\t{");

            for (int i = 0; i < StructureInfos.Count; i++)
            {
                if(!StructuresLengths.ContainsKey(StructureInfos[i].StructureNameHash))
                    continue;

                string code = GenerateStructureDefiniton(StructureInfos[i]);
                string code2 = GenerateStructureInfos(StructureInfos[i]);
                sbs.Append(code);
                sbsi.Append(code2);
            }

            sbsi.AppendLine("\t\t\t\t\tdefault: return null;");
            sbsi.AppendLine("\t\t\t}");
            sbsi.AppendLine("\t\t}");
        }

        public static void GenerateEnumData(StringBuilder sbei)
        {
            sbei.AppendLine("\t\tpublic static EnumInfo GetEnumInfo(MetaName name)");
            sbei.AppendLine("\t\t{");
            sbei.AppendLine("\t\t\tswitch (name)");
            sbei.AppendLine("\t\t\t{");

            for (int i = 0; i < EnumInfos.Count; i++)
            {
                string code = GenerateEnumInfos(EnumInfos[i]);
                sbei.Append(code);
            }

            sbei.AppendLine("\t\t\t\t\tdefault: return null;");
            sbei.AppendLine("\t\t\t}");
            sbei.AppendLine("\t\t}");
        }

        public static void GenerateStructureEnumInfos(StringBuilder sbsei)
        {
            sbsei.AppendLine("\t\tpublic static EnumInfo[] GetStructureEnumInfo(MetaName name)");
            sbsei.AppendLine("\t\t{");
            sbsei.AppendLine("\t\t\tvar enumInfos = new List<EnumInfo>();\n");
            sbsei.AppendLine("\t\t\tswitch (name)");
            sbsei.AppendLine("\t\t\t{");

            foreach(var entry in StructureEnums)
            {
                var code = GenerateStructureEnumInfoDefiniton(entry.Key.StructureNameHash, entry.Value);
                sbsei.Append(code);
            }

            sbsei.AppendLine("\t\t\t\t\tdefault: break;");
            sbsei.AppendLine("\t\t\t}");
            sbsei.AppendLine("\t\t\treturn enumInfos.ToArray();");
            sbsei.AppendLine("\t\t}");
        }

        public static void GenerateStructureChildInfos(StringBuilder sbsci)
        {
            sbsci.AppendLine("\t\tpublic static StructureInfo[] GetStructureChildInfo(MetaName name)");
            sbsci.AppendLine("\t\t{");
            sbsci.AppendLine("\t\t\tvar structureInfos = new List<StructureInfo>();\n");
            sbsci.AppendLine("\t\t\tswitch (name)");
            sbsci.AppendLine("\t\t\t{");

            foreach (var entry in StructureChildren)
            {
                var code = GenerateStructureChildInfoDefiniton(entry.Key, entry.Value);
                sbsci.Append(code);
            }

            sbsci.AppendLine("\t\t\t\t\tdefault: break;");
            sbsci.AppendLine("\t\t\t}");
            sbsci.AppendLine("\t\t\treturn structureInfos.ToArray();");
            sbsci.AppendLine("\t\t}");
        }

        public static string GenerateStructureEnumInfoDefiniton(int name, List<EnumInfo> enums)
        {
            lock(enums)
            {
                var sb = new StringBuilder();

                sb.AppendFormat("\t\t\t\tcase {0}: {{\n", GetMetaNameString(name));

                for (int i = 0; i < enums.Count; i++)
                    sb.AppendFormat("\t\t\t\t\tenumInfos.Add(MetaInfo.GetEnumInfo({0}));\n", GetMetaNameString(enums[i].EnumNameHash));

                sb.AppendLine("\t\t\t\t\tbreak;");
                sb.AppendLine("\t\t\t\t}");

                return sb.ToString();
            }
        }

        public static string GenerateStructureChildInfoDefiniton(StructureInfo si, List<int> nameHashes)
        {
            lock (nameHashes)
            {
                var sb = new StringBuilder();

                sb.AppendFormat("\t\t\t\tcase {0}: {{\n", GetMetaNameString(si.StructureNameHash));

                for (int i = 0; i < nameHashes.Count; i++)
                    sb.AppendFormat("\t\t\t\t\tstructureInfos.Add(MetaInfo.GetStructureInfo({0}));\n", GetMetaNameString(nameHashes[i]));

                sb.AppendLine("\t\t\t\t\tbreak;");
                sb.AppendLine("\t\t\t\t}");

                return sb.ToString();
            }
        }

        public static string CompileData(StringBuilder sbmt, StringBuilder sbei, StringBuilder sbsi, StringBuilder sbe, StringBuilder sbs, StringBuilder sbsei, StringBuilder sbsci)
        {
            var sb = new StringBuilder();

            sb.AppendLine("using System.Collections.Generic;");
            sb.AppendLine("using System.Runtime.InteropServices;");
            sb.AppendLine("using SharpDX;");
            sb.AppendLine("using RageLib.Resources.Common;");
            sb.AppendLine();

            sb.AppendLine("namespace RageLib.Resources.GTA5.PC.Meta");
            sb.AppendLine("{");

            sbmt.AppendLine("\tpublic class MetaInfo");
            sbmt.AppendLine("\t{");
            sbmt.Append(sbei);
            sbmt.AppendLine();
            sbmt.Append(sbsi);
            sbmt.Append(sbsei);
            sbmt.Append(sbsci);
            sbmt.AppendLine("\t}");

            sb.Append(sbmt);
            sb.AppendLine();

            sb.Append(sbe);
            sb.AppendLine();
            sb.Append(sbs);
            sb.AppendLine(); ;
            sb.AppendLine();

            sb.AppendLine("}");

            return sb.ToString();
        }
    }
}
