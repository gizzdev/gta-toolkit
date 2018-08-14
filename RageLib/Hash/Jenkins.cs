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
using System.Linq;

namespace RageLib.Hash
{
    public class Jenkins
    {
        public static Dictionary<uint, string> Index = new Dictionary<uint, string>();

        // source: http://en.wikipedia.org/wiki/Jenkins_hash_function
        public static uint Hash(string key)
        {
            uint hash = 0;
            for (int i = 0; i < key.Length; ++i)
            {
                hash += key[i];
                hash += (hash << 10);
                hash ^= (hash >> 6);
            }
            hash += (hash << 3);
            hash ^= (hash >> 11);
            hash += (hash << 15);
            return hash;
        }

        public static void Clear()
        {
            Index.Clear();
        }

        public static bool Ensure(string str)
        {
            uint hash = Hash(str);
            if (hash == 0) return true;

            if (!Index.ContainsKey(hash))
            {
                Index.Add(hash, str);
                return false;
            }

            return true;
        }

        public static string GetString(uint hash)
        {
            string res;

            if (!Index.TryGetValue(hash, out res))
            {
                res = hash.ToString();
            }

            return res;
        }
        public static string TryGetString(uint hash)
        {
            string res;

            if (!Index.TryGetValue(hash, out res))
            {
                res = string.Empty;
            }

            return res;
        }

    }
}
