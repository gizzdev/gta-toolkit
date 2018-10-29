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

using System.IO;
using RageLib.Resources;
using RageLib.Resources.GTA5;

namespace RageLib.Resources.GTA5.PC.GameFiles
{
    public interface IGameFile
    {
        void Load(string fileName);
        void Load(Stream stream);
        void Load(byte[] data);

        void Save(string fileName);
        void Save(Stream stream);
        byte[] Save();

        void Parse();
        void Build();
    }

    public abstract class GameFileBase : IGameFile
    {
        public Stream Stream;

        public GameFileBase()
        {
            this.Stream = null;
        }

        public void Load(string fileName)
        {
            byte[] data = File.ReadAllBytes(fileName);
            this.Load(data);
        }

        public void Load(Stream stream)
        {
            this.Stream = stream;
            this.Stream.Position = 0;
            this.Parse();
        }

        public void Load(byte[] data)
        {
            this.Stream = new MemoryStream();

            this.Stream.Write(data, 0, data.Length);
            this.Stream.Position = 0;
            this.Parse();
        }

        public void Save(string fileName)
        {
            this.Stream.Position = 0;
            this.Build();

            using (var fs = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None))
            {
                this.Stream.CopyTo(fs);
            }
        }

        public void Save(Stream stream)
        {
            this.Stream.Position = 0;
            this.Build();
            this.Stream.CopyTo(stream);
        }

        public byte[] Save()
        {
            this.Stream.Position = 0;
            this.Build();

            using (var ms = new MemoryStream())
            {
                this.Stream.CopyTo(ms);
                return ms.ToArray();
            }
        }

        public abstract void Parse();
        public abstract void Build();
    }

    public abstract class GameFileBase_Resource<T> : IGameFile where T : IResourceBlock, new()
    {
        public ResourceFile_GTA5_pc<T> ResourceFile;

        public GameFileBase_Resource()
        {
            this.ResourceFile = new ResourceFile_GTA5_pc<T>();
        }

        public void Load(string fileName)
        {
            this.ResourceFile.Load(fileName);
            this.Parse();
        }

        public void Load(Stream stream)
        {
            this.ResourceFile.Load(stream);
            this.Parse();
        }

        public void Load(byte[] data)
        {
            this.ResourceFile.Load(data);
            this.Parse();
        }

        public void Save(string fileName)
        {
            this.Build();
            this.ResourceFile.Save(fileName);
        }

        public void Save(Stream stream)
        {
            this.Build();
            this.ResourceFile.Save(stream);
        }

        public byte[] Save()
        {
            this.Build();
            return this.ResourceFile.Save();
        }

        public abstract void Parse();
        public abstract void Build();
    }
}
