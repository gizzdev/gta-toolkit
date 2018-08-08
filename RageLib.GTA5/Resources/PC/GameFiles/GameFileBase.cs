using System.IO;
using RageLib.Resources;
using RageLib.Resources.GTA5;

namespace RageLib.Resources.GTA5.PC.GameFiles
{
    public abstract class GameFileBase<T> where T : IResourceBlock, new()
    {
        protected ResourceFile_GTA5_pc<T> ResourceFile;

        public GameFileBase()
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
