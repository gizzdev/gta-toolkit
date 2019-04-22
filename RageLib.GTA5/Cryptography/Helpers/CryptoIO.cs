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
using System.IO;

namespace RageLib.GTA5.Cryptography.Helpers
{
    public class CryptoIO
    {
        public static byte[][] ReadNgKeys(byte[] data)
        {
            byte[][] result;

            var ms = new MemoryStream(data);
            var rd = new DataReader(ms);

            result = new byte[101][];
            for (int i = 0; i < 101; i++)
            {
                result[i] = rd.ReadBytes(272);
            }

            ms.Close();

            return result;
        }

        public static byte[][] ReadNgKeys(string fileName)
        {
            byte[][] result;

            var fs = new FileStream(fileName, FileMode.Open);
            var rd = new DataReader(fs);

            result = new byte[101][];
            for (int i = 0; i < 101; i++)
            {
                result[i] = rd.ReadBytes(272);
            }

            fs.Close();

            return result;
        }

        public static void WriteNgKeys(string fileName, byte[][] keys)
        {
            var fs = new FileStream(fileName, FileMode.Create);
            var wr = new DataWriter(fs);

            for (int i = 0; i < 101; i++)
            {
                wr.Write(keys[i]);
            }

            fs.Close();
        }

        public static uint[][][] ReadNgTables(byte[] data)
        {
            uint[][][] result;

            var ms = new MemoryStream(data);
            var rd = new DataReader(ms);

            // 17 rounds...
            result = new uint[17][][];
            for (int i = 0; i < 17; i++)
            {
                // 16 bytes...
                result[i] = new uint[16][];
                for (int j = 0; j < 16; j++)
                {
                    // 256 entries...
                    result[i][j] = new uint[256];
                    for (int k = 0; k < 256; k++)
                    {
                        result[i][j][k] = rd.ReadUInt32();
                    }
                }
            }

            ms.Close();

            return result;
        }

        public static uint[][][] ReadNgTables(string fileName)
        {
            uint[][][] result;

            var fs = new FileStream(fileName, FileMode.Open);
            var rd = new DataReader(fs);

            // 17 rounds...
            result = new uint[17][][];
            for (int i = 0; i < 17; i++)
            {
                // 16 bytes...
                result[i] = new uint[16][];
                for (int j = 0; j < 16; j++)
                {
                    // 256 entries...
                    result[i][j] = new uint[256];
                    for (int k = 0; k < 256; k++)
                    {
                        result[i][j][k] = rd.ReadUInt32();
                    }
                }
            }

            fs.Close();

            return result;
        }

        public static void WriteNgTables(string fileName, uint[][][] tableData)
        {
            var fs = new FileStream(fileName, FileMode.Create);
            var wr = new DataWriter(fs);

            // 17 rounds...
            for (int i = 0; i < 17; i++)
            {
                // 16 bytes...
                for (int j = 0; j < 16; j++)
                {
                    // 256 entries...
                    for (int k = 0; k < 256; k++)
                    {
                        wr.Write(tableData[i][j][k]);
                    }
                }
            }

            fs.Close();
        }

        public static GTA5NGLUT[][] ReadNgLuts(byte[] data)
        {
            var ms = new MemoryStream(data);
            var rd = new DataReader(ms);

            GTA5NGLUT[][] array = new GTA5NGLUT[17][];
            for (int i = 0; i < 17; i++)
            {
                array[i] = new GTA5NGLUT[16];
                for (int j = 0; j < 16; j++)
                {
                    array[i][j] = new GTA5NGLUT();
                    array[i][j].LUT0 = new byte[256][];
                    for (int k = 0; k < 256; k++)
                    {
                        array[i][j].LUT0[k] = rd.ReadBytes(256);
                    }
                    array[i][j].LUT1 = new byte[256][];
                    for (int l = 0; l < 256; l++)
                    {
                        array[i][j].LUT1[l] = rd.ReadBytes(256);
                    }
                    array[i][j].Indices = rd.ReadBytes(65536);
                }
            }
            ms.Close();
            return array;
        }


        public static GTA5NGLUT[][] ReadNgLuts(string fileName)
        {
            return ReadNgLuts(File.ReadAllBytes(fileName));
        }

        public static void WriteLuts(string fileName, GTA5NGLUT[][] lutData)
        {
            FileStream fileStream = new FileStream(fileName, FileMode.Create);
            DataWriter dataWriter = new DataWriter(fileStream, Endianess.LittleEndian);
            for (int i = 0; i < 17; i++)
            {
                for (int j = 0; j < 16; j++)
                {
                    GTA5NGLUT gta5NGLUT = lutData[i][j];
                    for (int k = 0; k < 256; k++)
                    {
                        for (int l = 0; l < 256; l++)
                        {
                            dataWriter.Write(gta5NGLUT.LUT0[k][l]);
                        }
                    }
                    for (int m = 0; m < 256; m++)
                    {
                        for (int n = 0; n < 256; n++)
                        {
                            dataWriter.Write(gta5NGLUT.LUT1[m][n]);
                        }
                    }
                    for (int num = 0; num < 65536; num++)
                    {
                        dataWriter.Write(gta5NGLUT.Indices[num]);
                    }
                }
            }
            fileStream.Close();
        }

    }
}