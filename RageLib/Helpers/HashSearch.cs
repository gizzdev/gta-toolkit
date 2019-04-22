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

using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace RageLib.Helpers
{
    public static class HashSearch
    {

        private const int BLOCK_LENGTH = 1048576;
        private const int ALIGN_LENGTH = 1;

        public static byte[] SearchHash(Stream stream, byte[] hash, int length = 32)
        {
            return HashSearch.SearchHashes(stream, new List<byte[]>
            {
                hash
            }, length)[0];
        }

        public static byte[][] SearchHashes(Stream stream, IList<byte[]> hashes, int length = 32)
        {
            byte[][] result = new byte[hashes.Count][];
            Parallel.For(0, (int)(stream.Length / 1048576L), delegate (int k)
            {
                SHA1CryptoServiceProvider sha1CryptoServiceProvider = new SHA1CryptoServiceProvider();
                byte[] array = new byte[length];
                for (int i = 0; i < 1048576; i++)
                {
                    int num = k * 1048576 + i;
                    if ((long)num < stream.Length)
                    {
                        Stream stream2 = stream;
                        lock (stream2)
                        {
                            stream.Position = (long)num;
                            stream.Read(array, 0, length);
                        }
                        if (result.Count((byte[] a) => a == null) == 0)
                        {
                            break;
                        }
                        byte[] first = sha1CryptoServiceProvider.ComputeHash(array);
                        for (int j = 0; j < hashes.Count; j++)
                        {
                            if (first.SequenceEqual(hashes[j]))
                            {
                                result[j] = (byte[])array.Clone();
                            }
                        }
                    }
                }
            });
            return result;
        }

    }
}