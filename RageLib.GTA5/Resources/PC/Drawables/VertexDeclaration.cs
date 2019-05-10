using RageLib.Resources;

namespace RageLib.Resources.GTA5.PC.Drawables
{
    public class VertexDeclaration : ResourceSystemBlock
    {
        public override long Length
        {
            get { return 16; }
        }

        // structure data
        public uint Flags { get; set; }
        public ushort Stride { get; set; }
        public byte Unknown_6h { get; set; }
        public byte Count { get; set; }
        public ulong Types { get; set; }

        /// <summary>
        /// Reads the data-block from a stream.
        /// </summary>
        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            // read structure data
            this.Flags = reader.ReadUInt32();
            this.Stride = reader.ReadUInt16();
            this.Unknown_6h = reader.ReadByte();
            this.Count = reader.ReadByte();
            this.Types = reader.ReadUInt64();
        }

        /// <summary>
        /// Writes the data-block to a stream.
        /// </summary>
        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            // write structure data
            writer.Write(this.Flags);
            writer.Write(this.Stride);
            writer.Write(this.Unknown_6h);
            writer.Write(this.Count);
            writer.Write(this.Types);
        }

        public ulong GetDeclarationId()
        {
            ulong res = 0;
            for (int i = 0; i < 16; i++)
            {
                if (((Flags >> i) & 1) == 1)
                {
                    res += (Types & (0xFu << (i * 4)));
                }
            }
            return res;
        }

        public override string ToString()
        {
            return Stride.ToString() + ": " + Count.ToString() + ": " + Flags.ToString() + ": " + Types.ToString();
        }
    }
}
