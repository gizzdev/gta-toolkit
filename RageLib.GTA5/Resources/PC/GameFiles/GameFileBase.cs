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
        void Load(string fileName, object[] parameters = null);
        void Load(Stream stream, object[] parameters = null);
        void Load(byte[] data, object[] parameters = null);

        void Save(string fileName, object[] parameters = null);
        void Save(Stream stream, object[] parameters = null);
        byte[] Save(object[] parameters = null);

        void Parse(object[] parameters = null);
        void Build(object[] parameters = null);
    }

    public abstract class GameFileBase : IGameFile
    {
        public Stream Stream;

        public GameFileBase()
        {
            this.Stream = null;
        }

        public void Load(string fileName, object[] parameters = null)
        {
            byte[] data = File.ReadAllBytes(fileName);
            this.Load(data, parameters);
        }

        public void Load(Stream stream, object[] parameters = null)
        {
            this.Stream = stream;
            this.Stream.Position = 0;
            this.Parse(parameters);
        }

        public void Load(byte[] data, object[] parameters = null)
        {
            this.Stream = new MemoryStream();

            this.Stream.Write(data, 0, data.Length);
            this.Stream.Position = 0;
            this.Parse(parameters);
        }

        public void Save(string fileName, object[] parameters = null)
        {
            this.Stream.Position = 0;
            this.Build(parameters);

            using (var fs = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None))
            {
                this.Stream.CopyTo(fs);
            }
        }

        public void Save(Stream stream, object[] parameters = null)
        {
            this.Stream.Position = 0;
            this.Build(parameters);
            this.Stream.CopyTo(stream);
        }

        public byte[] Save(object[] parameters = null)
        {
            this.Stream.Position = 0;
            this.Build(parameters);

            using (var ms = new MemoryStream())
            {
                this.Stream.CopyTo(ms);
                return ms.ToArray();
            }
        }

        public abstract void Parse(object[] parameters = null);
        public abstract void Build(object[] parameters = null);
    }

    public abstract class GameFileBase_Resource<T> : IGameFile where T : IResourceBlock, new()
    {
        public ResourceFile_GTA5_pc<T> ResourceFile;

        public GameFileBase_Resource()
        {
            this.ResourceFile = new ResourceFile_GTA5_pc<T>();
        }

        public void Load(string fileName, object[] parameters = null)
        {
            this.ResourceFile.Load(fileName);
            this.Parse(parameters);
        }

        public void Load(Stream stream, object[] parameters = null)
        {
            this.ResourceFile.Load(stream);
            this.Parse(parameters);
        }

        public void Load(byte[] data, object[] parameters = null)
        {
            this.ResourceFile.Load(data);
            this.Parse(parameters);
        }

        public void Save(string fileName, object[] parameters = null)
        {
            this.Build(parameters);
            this.ResourceFile.Save(fileName);
        }

        public void Save(Stream stream, object[] parameters = null)
        {
            this.Build(parameters);
            this.ResourceFile.Save(stream);
        }

        public byte[] Save(object[] parameters = null)
        {
            this.Build(parameters);
            return this.ResourceFile.Save();
        }

        public abstract void Parse(object[] parameters = null);
        public abstract void Build(object[] parameters = null);
    }
}
