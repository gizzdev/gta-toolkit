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

using RageLib.Data;
using RageLib.Resources.GTA5.PC.Meta;
using System;
using System.Collections.Generic;
using System.IO;

namespace RageLib.GTA5.PSO
{
    public enum DataType : byte
    {
        Bool = 0x00,
        SByte = 0x01,
        UByte = 0x02,
        SShort = 0x03,
        UShort = 0x04,
        SInt = 0x05,
        UInt = 0x06,
        Float = 0x07,
        Float2 = 0x08,
        Float3 = 0x09,
        Float4 = 0x0a,
        String = 0x0b,
        Structure = 0x0c,
        Array = 0x0d,
        Enum = 0x0e,
        Flags = 0x0f,
        Map = 0x10,
        Float3a = 0x14,
        Float4a = 0x15,
        HFloat = 0x1e,
        Long = 0x20,
    }

    public class PsoDefinitionSection
    {
        public int Ident { get; private set; } = 0x50534348;
        public int Length { get; set; }
        public uint Count;

        public List<PsoElementIndexInfo> EntriesIdx;
        public List<PsoElementInfo> Entries;

        public void Read(DataReader reader)
        {
            this.Ident = reader.ReadInt32();
            this.Length = reader.ReadInt32();
            this.Count = reader.ReadUInt32();

            this.EntriesIdx = new List<PsoElementIndexInfo>();
            for (int i = 0; i < Count; i++)
            {
                var entry = new PsoElementIndexInfo();
                entry.Read(reader);
                EntriesIdx.Add(entry);
            }

            this.Entries = new List<PsoElementInfo>();
            for (int i = 0; i < Count; i++)
            {
                reader.Position = EntriesIdx[i].Offset;
                var type = reader.ReadByte();

                reader.Position = EntriesIdx[i].Offset;
                if (type == 0)
                {
                    var entry = new PsoStructureInfo();
                    entry.Read(reader);
                    entry.IndexInfo = EntriesIdx[i];
                    Entries.Add(entry);
                }
                else if (type == 1)
                {
                    var entry = new PsoEnumInfo();
                    entry.Read(reader);
                    entry.IndexInfo = EntriesIdx[i];
                    Entries.Add(entry);
                }
                else
                    throw new Exception("unknown type!");
            }
        }

        public void Write(DataWriter writer)
        {

            var entriesStream = new MemoryStream();
            var entriesWriter = new DataWriter(entriesStream, Endianess.BigEndian);
            for (int i = 0; i < Entries.Count; i++)
            {
                EntriesIdx[i].Offset = 12 + 8 * Entries.Count + (int)entriesWriter.Position;
                Entries[i].Write(entriesWriter);
            }



            var indexStream = new MemoryStream();
            var indexWriter = new DataWriter(indexStream, Endianess.BigEndian);
            foreach (var entry in EntriesIdx)
                entry.Write(indexWriter);




            writer.Write(Ident);
            writer.Write((int)(12 + entriesStream.Length + indexStream.Length));
            writer.Write((int)(Entries.Count));

            // write entries index data
            var buf1 = new byte[indexStream.Length];
            indexStream.Position = 0;
            indexStream.Read(buf1, 0, buf1.Length);
            writer.Write(buf1);

            // write entries data
            var buf2 = new byte[entriesStream.Length];
            entriesStream.Position = 0;
            entriesStream.Read(buf2, 0, buf2.Length);
            writer.Write(buf2);


        }
    }

    public class PsoElementIndexInfo
    {
        public int NameHash;
        public int Offset;

        public void Read(DataReader reader)
        {
            this.NameHash = reader.ReadInt32();
            this.Offset = reader.ReadInt32();
        }

        public void Write(DataWriter writer)
        {
            writer.Write(NameHash);
            writer.Write(Offset);
        }
    }

    public abstract class PsoElementInfo
    {
        public PsoElementIndexInfo IndexInfo { get; set; }

        public abstract void Read(DataReader reader);

        public abstract void Write(DataWriter writer);
    }

    public class PsoStructureInfo : PsoElementInfo
    {
        public byte Type { get; set; } = 0;
        public short EntriesCount { get; private set; }
        public byte Unk { get; set; }
        public int StructureLength { get; set; }
        public uint Unk_Ch { get; set; } = 0x00000000;
        public List<PsoStructureEntryInfo> Entries { get; set; } = new List<PsoStructureEntryInfo>();

        public PsoStructureInfo()
        {

        }

        public PsoStructureInfo(byte type, byte unk, int structureLength, uint unk_ch, List<PsoStructureEntryInfo> entries)
        {
            this.Type = type;
            this.Unk = unk;
            this.StructureLength = structureLength;
            this.Unk_Ch = unk_ch;
            this.Entries = entries;
            this.EntriesCount = (short) entries.Count;
        }

        public override void Read(DataReader reader)
        {
            uint x = reader.ReadUInt32();
            this.Type = (byte)((x & 0xFF000000) >> 24);
            this.EntriesCount = (short)(x & 0xFFFF);
            this.Unk = (byte)((x & 0x00FF0000) >> 16);
            this.StructureLength = reader.ReadInt32();
            this.Unk_Ch = reader.ReadUInt32();

            Entries = new List<PsoStructureEntryInfo>();
            for (int i = 0; i < EntriesCount; i++)
            {
                var entry = new PsoStructureEntryInfo();
                entry.Read(reader);
                Entries.Add(entry);
            }
        }

        public override void Write(DataWriter writer)
        {
            Type = 0;
            EntriesCount = (short)Entries.Count;

            uint typeAndEntriesCount = (uint)(Type << 24) | (uint)(Unk << 16) | (ushort)EntriesCount;
            writer.Write(typeAndEntriesCount);
            writer.Write(StructureLength);
            writer.Write(Unk_Ch);

            foreach (var entry in Entries)
            {
                entry.Write(writer);
            }
        }

        public PsoStructureEntryInfo FindEntry(MetaName name)
        {
            if (Entries != null)
            {
                foreach (var entry in Entries)
                {
                    if ((MetaName) entry.EntryNameHash == name) return entry;
                }
            }
            return null;
        }

        public PsoStructureEntryInfo GetEntry(int id)
        {
            if ((Entries != null) && (id >= 0) && (id < Entries.Count))
            {
                return Entries[id];
            }
            return null;
        }
    }

    public class PsoStructureEntryInfo
    {
        public int EntryNameHash;
        public DataType Type;
        public byte Unk_5h;
        public short DataOffset;
        public int ReferenceKey; // when array -> entry index with type

        public PsoStructureEntryInfo()
        { }

        public PsoStructureEntryInfo(int nameHash, DataType type, byte unk5, short dataOffset, int referenceKey)
        {
            this.EntryNameHash = nameHash;
            this.Type = type;
            this.Unk_5h = unk5;
            this.DataOffset = dataOffset;
            this.ReferenceKey = referenceKey;
        }

        public void Read(DataReader reader)
        {
            this.EntryNameHash = reader.ReadInt32();
            this.Type = (DataType)reader.ReadByte();
            this.Unk_5h = reader.ReadByte();
            this.DataOffset = reader.ReadInt16();
            this.ReferenceKey = reader.ReadInt32();
        }

        public void Write(DataWriter writer)
        {
            writer.Write(EntryNameHash);
            writer.Write((byte)Type);
            writer.Write(Unk_5h);
            writer.Write(DataOffset);
            writer.Write(ReferenceKey);
        }
    }

    public class PsoEnumInfo : PsoElementInfo
    {
        public byte Type { get; private set; } = 1;
        public int EntriesCount { get; private set; }
        public List<PsoEnumEntryInfo> Entries { get; set; }

        public PsoEnumInfo()
        {

        }

        public PsoEnumInfo(byte type, List<PsoEnumEntryInfo> entries)
        {
            this.Type = type;
            this.Entries = entries;
            this.EntriesCount = entries.Count;
        }

        public override void Read(DataReader reader)
        {
            uint x = reader.ReadUInt32();
            this.Type = (byte)((x & 0xFF000000) >> 24);
            this.EntriesCount = (int)(x & 0x00FFFFFF);

            Entries = new List<PsoEnumEntryInfo>();
            for (int i = 0; i < EntriesCount; i++)
            {
                var entry = new PsoEnumEntryInfo();
                entry.Read(reader);
                Entries.Add(entry);
            }
        }

        public override void Write(DataWriter writer)
        {
            // update...
            Type = 1;
            EntriesCount = Entries.Count;

            uint typeAndEntriesCount = (uint)(Type << 24) | (uint)EntriesCount;
            writer.Write(typeAndEntriesCount);

            foreach (var entry in Entries)
            {
                entry.Write(writer);
            }
        }

        public PsoEnumEntryInfo FindEntry(int val)
        {
            if (Entries == null) return null;
            for (int i = 0; i < Entries.Count; i++)
            {
                var entry = Entries[i];
                if (entry.EntryKey == val)
                {
                    return entry;
                }
            }
            return null;
        }

    }

    public class PsoEnumEntryInfo
    {
        public int EntryNameHash { get; set; }
        public int EntryKey { get; set; }

        public PsoEnumEntryInfo()
        {

        }

        public PsoEnumEntryInfo(int nameHash, int key)
        {
            this.EntryNameHash = nameHash;
            this.EntryKey = key;
        }

        public void Read(DataReader reader)
        {
            this.EntryNameHash = reader.ReadInt32();
            this.EntryKey = reader.ReadInt32();
        }

        public void Write(DataWriter writer)
        {
            writer.Write(EntryNameHash);
            writer.Write(EntryKey);
        }
    }
}
