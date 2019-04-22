using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RageLib.GTA5.Cryptography;

namespace RageLib.GTA5.Helpers
{
    public class RandomGauss
    {
        public static bool[] Solve(uint[][] tables, int inByte0, int inByte1, int inByte2, int inByte3, int outByte, int outBit)
        {
            uint[] key = new uint[4];
            Random random = new Random();
            List<RandomGaussRow> list = new List<RandomGaussRow>();
            RandomGaussRow randomGaussRow = new RandomGaussRow();
            randomGaussRow.SetA(0);
            randomGaussRow.SetB();
            list.Add(randomGaussRow);
            byte[] array = new byte[16];
            for (int i = 1; i < 1024; i++)
            {
                RandomGaussRow randomGaussRow2;
                do
                {
                    random.NextBytes(array);
                    byte[] array2 = GTA5Crypto.DecryptRoundA(array, key, tables);
                    randomGaussRow2 = new RandomGaussRow();
                    randomGaussRow2.SetA((int)array2[inByte0]);
                    randomGaussRow2.SetA(256 + (int)array2[inByte1]);
                    randomGaussRow2.SetA(512 + (int)array2[inByte2]);
                    randomGaussRow2.SetA(768 + (int)array2[inByte3]);
                    if (((int)array[outByte] & 1 << outBit) != 0)
                    {
                        randomGaussRow2.SetB();
                    }
                    if (i == 767)
                    {
                        randomGaussRow2 = new RandomGaussRow();
                        randomGaussRow2.SetA(767);
                        randomGaussRow2.SetB();
                    }
                    if (i == 1023)
                    {
                        randomGaussRow2 = new RandomGaussRow();
                        randomGaussRow2.SetA(1023);
                        randomGaussRow2.SetB();
                    }
                    for (int j = 0; j < i; j++)
                    {
                        if (randomGaussRow2.GetA(j))
                        {
                            randomGaussRow2 ^= list[j];
                        }
                    }
                }
                while (!randomGaussRow2.GetA(i));
                list.Add(randomGaussRow2);
            }
            bool[] array3 = new bool[1024];
            for (int k = 1023; k >= 0; k--)
            {
                bool b = list[k].GetB();
                array3[k] = b;
                for (int l = 0; l < k; l++)
                {
                    if (list[l].GetA(k))
                    {
                        list[l].B ^= b;
                    }
                }
            }
            return array3;
        }

        public static uint[][] Solve(uint[][] tables)
        {
            uint[][] result = new uint[16][];
            for (int i = 0; i < 16; i++)
            {
                result[i] = new uint[256];
            }
            Parallel.For(0, 128, delegate (int bitIdx)
            {
                int num = bitIdx / 8;
                int num2 = num / 4;
                int num3 = 4 * num2;
                int num4 = 4 * num2 + 1;
                int num5 = 4 * num2 + 2;
                int num6 = 4 * num2 + 3;
                int outBit = bitIdx % 8;
                int num7 = bitIdx % 32;
                bool[] array = RandomGauss.Solve(tables, num3, num4, num5, num6, num, outBit);
                uint[][] currResult = result;
                lock (currResult)
                {
                    for (int j = 0; j < 256; j++)
                    {
                        if (array[j])
                        {
                            result[num3][j] |= 1u << num7;
                        }
                        if (array[256 + j])
                        {
                            result[num4][j] |= 1u << num7;
                        }
                        if (array[512 + j])
                        {
                            result[num5][j] |= 1u << num7;
                        }
                        if (array[768 + j])
                        {
                            result[num6][j] |= 1u << num7;
                        }
                    }
                }
            });
            return result;
        }

        private const int TEST_ITERATIONS = 100000;
    }
}