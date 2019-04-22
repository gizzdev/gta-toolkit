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

using RageLib.Cryptography;
using RageLib.Data;
using RageLib.GTA5.Cryptography;
using System;
using System.Collections.Generic;
using System.IO;

namespace RageLib.GTA5.Archives
{
    public interface IRageArchiveEntry7
    {
        uint NameOffset { get; set; }
        string Name { get; set; }

        void Read(DataReader reader);
        void Write(DataWriter writer);
    }

    public interface IRageArchiveFileEntry7 : IRageArchiveEntry7
    {
        uint FileOffset { get; set; }
        uint FileSize { get; set; }
    }

    public enum RageArchiveEncryption7
    {
        None,
        AES,
        NG
    }

    /// <summary>
    /// Represents an RPFv7 archive.
    /// </summary>
    public class RageArchive7 : IDisposable
    {
        private const uint IDENT = 0x52504637;

        public RageArchiveEncryption7 Encryption { get; set; }

        private bool LeaveOpen;
        public Stream BaseStream { get; private set; }

        public RageArchiveDirectory7 Root { get; set; }

        /// <summary>
        /// Creates an RPFv7 archive.
        /// </summary>
        public RageArchive7(Stream fileStream, bool leaveOpen = false)
        {
            BaseStream = fileStream;
            LeaveOpen = leaveOpen;
        }

        /// <summary>
        /// Reads the archive header.
        /// </summary>
        public void ReadHeader(byte[] aesKey = null, byte[] ngKey = null)
        {
            DataReader dataReader = new DataReader(this.BaseStream, Endianess.LittleEndian);
            long position = dataReader.Position;
            dataReader.Position = 0L;
            uint num = dataReader.ReadUInt32();
            if (num != 0x52504637)
            {
                throw new Exception("The identifier " + num.ToString("X8") + " did not match the expected value of 0x52504637");
            }
            uint num2 = dataReader.ReadUInt32();
            uint count = dataReader.ReadUInt32();
            uint num3 = dataReader.ReadUInt32();
            byte[] buffer;
            byte[] buffer2;
            if (num3 == 0x4E45504F)
            {
                this.Encryption = RageArchiveEncryption7.None;
                buffer = dataReader.ReadBytes(16 * (int) num2);
                buffer2 = dataReader.ReadBytes((int)count);
            }
            else if (num3 == 0x0FFFFFF9)
            {
                this.Encryption = RageArchiveEncryption7.AES;
                byte[] data = dataReader.ReadBytes(16 * (int)num2);
                buffer = AesEncryption.DecryptData(data, aesKey, 1);
                byte[] data2 = dataReader.ReadBytes((int)count);
                buffer2 = AesEncryption.DecryptData(data2, aesKey, 1);
            }
            else
            {
                this.Encryption = RageArchiveEncryption7.NG;
                byte[] data3 = dataReader.ReadBytes(16 * (int)num2);
                buffer = GTA5Crypto.Decrypt(data3, ngKey);
                byte[] data4 = dataReader.ReadBytes((int)count);
                buffer2 = GTA5Crypto.Decrypt(data4, ngKey);
            }
            DataReader dataReader2 = new DataReader(new MemoryStream(buffer), Endianess.LittleEndian);
            DataReader dataReader3 = new DataReader(new MemoryStream(buffer2), Endianess.LittleEndian);
            List<IRageArchiveEntry7> list = new List<IRageArchiveEntry7>();
            int num4 = 0;
            while (num4 < num2)
            {
                dataReader2.Position += 4L;
                int num5 = dataReader2.ReadInt32();
                dataReader2.Position -= 8L;
                if (num5 == 0x7FFFFF00)
                {
                    RageArchiveDirectory7 rageArchiveDirectory = new RageArchiveDirectory7();
                    rageArchiveDirectory.Read(dataReader2);
                    dataReader3.Position = (long)((ulong)rageArchiveDirectory.NameOffset);
                    rageArchiveDirectory.Name = dataReader3.ReadString();
                    list.Add(rageArchiveDirectory);
                }
                else if ((num5 & 0x80000000) == 0L)
                {
                    RageArchiveBinaryFile7 rageArchiveBinaryFile = new RageArchiveBinaryFile7();
                    rageArchiveBinaryFile.Read(dataReader2);
                    dataReader3.Position = rageArchiveBinaryFile.NameOffset;
                    rageArchiveBinaryFile.Name = dataReader3.ReadString();
                    list.Add(rageArchiveBinaryFile);
                }
                else
                {
                    RageArchiveResourceFile7 rageArchiveResourceFile = new RageArchiveResourceFile7();
                    rageArchiveResourceFile.Read(dataReader2);
                    if (rageArchiveResourceFile.FileSize == 0x00FFFFFF)
                    {
                        dataReader.Position = 512 * rageArchiveResourceFile.FileOffset;
                        byte[] array = dataReader.ReadBytes(16);
                        rageArchiveResourceFile.FileSize = (uint)(array[7] | array[14] << 8 | array[5] << 16 | array[2] << 24);
                    }
                    dataReader3.Position = rageArchiveResourceFile.NameOffset;
                    rageArchiveResourceFile.Name = dataReader3.ReadString();
                    list.Add(rageArchiveResourceFile);
                }
                num4++;
            }
            Stack<RageArchiveDirectory7> stack = new Stack<RageArchiveDirectory7>();
            stack.Push((RageArchiveDirectory7)list[0]);
            this.Root = (RageArchiveDirectory7)list[0];
            while (stack.Count > 0)
            {
                RageArchiveDirectory7 rageArchiveDirectory2 = stack.Pop();
                int num6 = (int)rageArchiveDirectory2.EntriesIndex;
                while ((long)num6 < (long)((ulong)(rageArchiveDirectory2.EntriesIndex + rageArchiveDirectory2.EntriesCount)))
                {
                    if (list[num6] is RageArchiveDirectory7)
                    {
                        rageArchiveDirectory2.Directories.Add(list[num6] as RageArchiveDirectory7);
                        stack.Push(list[num6] as RageArchiveDirectory7);
                    }
                    else
                    {
                        rageArchiveDirectory2.Files.Add(list[num6]);
                    }
                    num6++;
                }
            }
            dataReader.Position = position;
        }

        /// <summary>
        /// Writes the archive header.
        /// </summary>
        public void WriteHeader(byte[] aesKey = null, byte[] ngKey = null)
        {
            long position = this.BaseStream.Position;
            DataWriter dataWriter = new DataWriter(this.BaseStream, Endianess.LittleEndian);
            List<IRageArchiveEntry7> list = new List<IRageArchiveEntry7>();
            Stack<RageArchiveDirectory7> stack = new Stack<RageArchiveDirectory7>();
            int num = 1;
            list.Add(this.Root);
            stack.Push(this.Root);
            Dictionary<string, uint> dictionary = new Dictionary<string, uint>();
            dictionary.Add("", 0);
            while (stack.Count > 0)
            {
                RageArchiveDirectory7 rageArchiveDirectory = stack.Pop();
                rageArchiveDirectory.EntriesIndex = (uint)list.Count;
                rageArchiveDirectory.EntriesCount = (uint)(rageArchiveDirectory.Directories.Count + rageArchiveDirectory.Files.Count);
                List<IRageArchiveEntry7> list2 = new List<IRageArchiveEntry7>();
                foreach (RageArchiveDirectory7 rageArchiveDirectory2 in rageArchiveDirectory.Directories)
                {
                    if (!dictionary.ContainsKey(rageArchiveDirectory2.Name))
                    {
                        dictionary.Add(rageArchiveDirectory2.Name, (uint)num);
                        num += rageArchiveDirectory2.Name.Length + 1;
                    }
                    rageArchiveDirectory2.NameOffset = dictionary[rageArchiveDirectory2.Name];
                    list2.Add(rageArchiveDirectory2);
                }
                foreach (IRageArchiveEntry7 rageArchiveEntry in rageArchiveDirectory.Files)
                {
                    if (!dictionary.ContainsKey(rageArchiveEntry.Name))
                    {
                        dictionary.Add(rageArchiveEntry.Name, (uint)num);
                        num += rageArchiveEntry.Name.Length + 1;
                    }
                    rageArchiveEntry.NameOffset = dictionary[rageArchiveEntry.Name];
                    list2.Add(rageArchiveEntry);
                }
                list2.Sort((IRageArchiveEntry7 a, IRageArchiveEntry7 b) => string.CompareOrdinal(a.Name, b.Name));
                foreach (IRageArchiveEntry7 item in list2)
                {
                    list.Add(item);
                }
                list2.Reverse();
                foreach (IRageArchiveEntry7 rageArchiveEntry2 in list2)
                {
                    if (rageArchiveEntry2 is RageArchiveDirectory7)
                    {
                        stack.Push((RageArchiveDirectory7)rageArchiveEntry2);
                    }
                }
            }
            foreach (IRageArchiveEntry7 rageArchiveEntry3 in list)
            {
                if (rageArchiveEntry3 is RageArchiveResourceFile7)
                {
                    RageArchiveResourceFile7 rageArchiveResourceFile = rageArchiveEntry3 as RageArchiveResourceFile7;
                    if (rageArchiveResourceFile.FileSize > 0x00FFFFFF)
                    {
                        byte[] array = new byte[16];
                        array[7] = (byte)(rageArchiveResourceFile.FileSize & 255);
                        array[14] = (byte)(rageArchiveResourceFile.FileSize >> 8 & 255);
                        array[5] = (byte)(rageArchiveResourceFile.FileSize >> 16 & 255);
                        array[2] = (byte)(rageArchiveResourceFile.FileSize >> 24 & 255);
                        if (dataWriter.Length > (512 * rageArchiveResourceFile.FileOffset))
                        {
                            dataWriter.Position = (512 * rageArchiveResourceFile.FileOffset);
                            dataWriter.Write(array);
                        }
                        rageArchiveResourceFile.FileSize = 0x00FFFFFF;
                    }
                }
            }
            MemoryStream memoryStream = new MemoryStream();
            DataWriter writer = new DataWriter(memoryStream, Endianess.LittleEndian);
            foreach (IRageArchiveEntry7 rageArchiveEntry4 in list)
            {
                rageArchiveEntry4.Write(writer);
            }
            memoryStream.Flush();
            byte[] array2 = new byte[memoryStream.Length];
            memoryStream.Position = 0L;
            memoryStream.Read(array2, 0, array2.Length);
            if (this.Encryption == RageArchiveEncryption7.AES)
            {
                array2 = AesEncryption.EncryptData(array2, aesKey, 1);
            }
            if (this.Encryption == RageArchiveEncryption7.NG)
            {
                array2 = GTA5Crypto.Encrypt(array2, ngKey);
            }
            MemoryStream memoryStream2 = new MemoryStream();
            DataWriter dataWriter2 = new DataWriter(memoryStream2, Endianess.LittleEndian);
            foreach (KeyValuePair<string, uint> keyValuePair in dictionary)
            {
                dataWriter2.Write(keyValuePair.Key);
            }
            byte[] value = new byte[16L - dataWriter2.Length % 16L];
            dataWriter2.Write(value);
            memoryStream2.Flush();
            byte[] array3 = new byte[memoryStream2.Length];
            memoryStream2.Position = 0L;
            memoryStream2.Read(array3, 0, array3.Length);
            if (this.Encryption == RageArchiveEncryption7.AES)
            {
                array3 = AesEncryption.EncryptData(array3, aesKey, 1);
            }
            if (this.Encryption == RageArchiveEncryption7.NG)
            {
                array3 = GTA5Crypto.Encrypt(array3, ngKey);
            }
            dataWriter.Position = 0L;
            dataWriter.Write(0x52504637);
            dataWriter.Write((uint)list.Count);
            dataWriter.Write((uint)array3.Length);
            switch (this.Encryption)
            {
                case RageArchiveEncryption7.None:
                    dataWriter.Write(0x4E45504F);
                    break;
                case RageArchiveEncryption7.AES:
                    dataWriter.Write(0x0FFFFFF9);
                    break;
                case RageArchiveEncryption7.NG:
                    dataWriter.Write(0x0FEFFFFF);
                    break;
            }
            dataWriter.Write(array2);
            dataWriter.Write(array3);
            this.BaseStream.Position = position;
        }

        /// <summary>
        /// Releases all resources used by the archive.
        /// </summary>
        public void Dispose()
        {
            if (BaseStream != null)
                BaseStream.Dispose();

            BaseStream = null;
            Root = null;
        }
    }

    /// <summary>
    /// Represents a directory in an RPFv7 archive.
    /// </summary>
    public class RageArchiveDirectory7 : IRageArchiveEntry7
    {
        public uint NameOffset { get; set; }
        public uint EntriesIndex { get; set; }
        public uint EntriesCount { get; set; }

        public string Name { get; set; }
        public List<RageArchiveDirectory7> Directories = new List<RageArchiveDirectory7>();
        public List<IRageArchiveEntry7> Files = new List<IRageArchiveEntry7>();

        /// <summary>
        /// Reads the directory entry.
        /// </summary>
        public void Read(DataReader reader)
        {
            this.NameOffset = reader.ReadUInt32();

            uint ident = reader.ReadUInt32();
            if (ident != 0x7FFFFF00)
                throw new Exception("Error in RPF7 directory entry.");

            this.EntriesIndex = reader.ReadUInt32();
            this.EntriesCount = reader.ReadUInt32();
        }

        /// <summary>
        /// Writes the directory entry.
        /// </summary>
        public void Write(DataWriter writer)
        {
            writer.Write(this.NameOffset);
            writer.Write((uint)0x7FFFFF00);
            writer.Write(this.EntriesIndex);
            writer.Write(this.EntriesCount);
        }
    }

    /// <summary>
    /// Represents a binary file in an RPFv7 archive.
    /// </summary>
    public class RageArchiveBinaryFile7 : IRageArchiveFileEntry7
    {
        public uint NameOffset { get; set; }
        public uint FileSize { get; set; }
        public uint FileOffset { get; set; }
        public uint FileUncompressedSize { get; set; }
        public bool IsEncrypted { get; set; }

        public string Name { get; set; }

        /// <summary>
        /// Reads the binary file entry.
        /// </summary>
        public void Read(DataReader reader)
        {
            NameOffset = reader.ReadUInt16();

            var buf1 = reader.ReadBytes(3);
            FileSize = (uint)buf1[0] + (uint)(buf1[1] << 8) + (uint)(buf1[2] << 16);

            var buf2 = reader.ReadBytes(3);
            FileOffset = (uint)buf2[0] + (uint)(buf2[1] << 8) + (uint)(buf2[2] << 16);

            FileUncompressedSize = reader.ReadUInt32();

            switch (reader.ReadUInt32())
            {
                case 0: IsEncrypted = false; break;
                case 1: IsEncrypted = true; break;
                default:
                    throw new Exception("Error in RPF7 file entry.");
            }
        }

        /// <summary>
        /// Writes the binary file entry.
        /// </summary>
        public void Write(DataWriter writer)
        {
            writer.Write((ushort)NameOffset);

            var buf1 = new byte[] {
                (byte)((FileSize >> 0) & 0xFF),
                (byte)((FileSize >> 8) & 0xFF),
                (byte)((FileSize >> 16) & 0xFF)
            };
            writer.Write(buf1);

            var buf2 = new byte[] {
                (byte)((FileOffset >> 0) & 0xFF),
                (byte)((FileOffset >> 8) & 0xFF),
                (byte)((FileOffset >> 16) & 0xFF)
            };
            writer.Write(buf2);

            writer.Write(FileUncompressedSize);

            if (IsEncrypted)
                writer.Write((uint)1);
            else
                writer.Write((uint)0);
        }
    }

    /// <summary>
    /// Represents a resource file in an RPFv7 archive.
    /// </summary>
    public class RageArchiveResourceFile7 : IRageArchiveFileEntry7
    {
        public uint NameOffset { get; set; }
        public uint FileSize { get; set; }
        public uint FileOffset { get; set; }
        public uint SystemFlags { get; set; }
        public uint GraphicsFlags { get; set; }

        public string Name { get; set; }

        /// <summary>
        /// Reads the resource file entry.
        /// </summary>
        public void Read(DataReader reader)
        {
            NameOffset = reader.ReadUInt16();

            var buf1 = reader.ReadBytes(3);
            FileSize = (uint)buf1[0] + (uint)(buf1[1] << 8) + (uint)(buf1[2] << 16);

            var buf2 = reader.ReadBytes(3);
            FileOffset = ((uint)buf2[0] + (uint)(buf2[1] << 8) + (uint)(buf2[2] << 16)) & 0x7FFFFF;

            SystemFlags = reader.ReadUInt32();
            GraphicsFlags = reader.ReadUInt32();
        }

        /// <summary>
        /// Writes the resource file entry.
        /// </summary>
        public void Write(DataWriter writer)
        {
            writer.Write((ushort)NameOffset);

            var buf1 = new byte[] {
                (byte)((FileSize >> 0) & 0xFF),
                (byte)((FileSize >> 8) & 0xFF),
                (byte)((FileSize >> 16) & 0xFF)
            };
            writer.Write(buf1);

            var buf2 = new byte[] {
                (byte)((FileOffset >> 0) & 0xFF),
                (byte)((FileOffset >> 8) & 0xFF),
                (byte)(((FileOffset >> 16) & 0xFF) | 0x80)
            };
            writer.Write(buf2);

            writer.Write(SystemFlags);
            writer.Write(GraphicsFlags);
        }
    }
}