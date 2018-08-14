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
using RageLib.GTA5.ResourceWrappers.PC.PSO;
using System.IO;
using System.Xml;

namespace RageLib.GTA5.PSO
{
    public enum PsoSection : uint
    {
        PSIN = 0x5053494E,
        PMAP = 0x504D4150,
        PSCH = 0x50534348,
        PSIG = 0x50534947,
        STRF = 0x53545246,
        STRS = 0x53545253,
        STRE = 0x53545245,
        CHKS = 0x43484B53,
    }


    public class PsoFile
    {
        public PsoDataSection DataSection { get; set; }
        public PsoDataMappingSection DataMappingSection { get; set; }
        public PsoDefinitionSection DefinitionSection { get; set; }
        public PsoSTRFSection STRFSection { get; set; }
        public PsoSTRSSection STRSSection { get; set; }
        public PsoPSIGSection PSIGSection { get; set; }
        public PsoSTRESection STRESection { get; set; }
        public PsoCHKSSection CHKSSection { get; set; }


        public void Load(string fileName)
        {
            using (var stream = new FileStream(fileName, FileMode.Open))
                Load(stream);
        }

        public virtual void Load(Stream stream)
        {
            stream.Position = 0;

            var reader = new DataReader(stream, Endianess.BigEndian);
            while (reader.Position < reader.Length)
            {
                var identInt = reader.ReadUInt32();
                var ident = (PsoSection)identInt;
                var length = reader.ReadInt32();

                reader.Position -= 8;

                var sectionData = reader.ReadBytes(length);
                var sectionStream = new MemoryStream(sectionData);
                var sectionReader = new DataReader(sectionStream, Endianess.BigEndian);

                switch (ident)
                {
                    case PsoSection.PSIN:  //0x5053494E  "PSIN"  - ID / data section
                        DataSection = new PsoDataSection();
                        DataSection.Read(sectionReader);
                        break;
                    case PsoSection.PMAP:  //0x504D4150  "PMAP"  //data mapping
                        DataMappingSection = new PsoDataMappingSection();
                        DataMappingSection.Read(sectionReader);
                        break;
                    case PsoSection.PSCH:  //0x50534348  "PSCH"  //schema
                        DefinitionSection = new PsoDefinitionSection();
                        DefinitionSection.Read(sectionReader);
                        break;
                    case PsoSection.STRF:  //0x53545246  "STRF"  //paths/STRINGS  (folder strings?)
                        STRFSection = new PsoSTRFSection();
                        STRFSection.Read(sectionReader);
                        break;
                    case PsoSection.STRS:  //0x53545253  "STRS"  //names/strings  (DES_)
                        STRSSection = new PsoSTRSSection();
                        STRSSection.Read(sectionReader);
                        break;
                    case PsoSection.STRE:  //0x53545245  "STRE"  //probably encrypted strings.....
                        STRESection = new PsoSTRESection();
                        STRESection.Read(sectionReader);
                        break;
                    case PsoSection.PSIG:  //0x50534947  "PSIG"  //signature?
                        PSIGSection = new PsoPSIGSection();
                        PSIGSection.Read(sectionReader);
                        break;
                    case PsoSection.CHKS:  //0x43484B53  "CHKS"  //checksum?
                        CHKSSection = new PsoCHKSSection();
                        CHKSSection.Read(sectionReader);
                        break;
                    default:
                        break;
                }
            }
        }

        public void Save(string fileName)
        {
            using (var stream = new FileStream(fileName, FileMode.Create))
                Save(stream);
        }

        public virtual void Save(Stream stream)
        {
            var writer = new DataWriter(stream, Endianess.BigEndian);
            if (DataSection != null) DataSection.Write(writer);
            if (DataMappingSection != null) DataMappingSection.Write(writer);
            if (DefinitionSection != null) DefinitionSection.Write(writer);
        }

        public static bool IsPSO(string fileName)
        {
            using (var stream = new FileStream(fileName, FileMode.Open))
                return !IsRBF(stream);
        }

        public static bool IsPSO(Stream stream)
        {
            return !IsRBF(stream);
        }

        public static bool IsRBF(Stream stream)
        {
            var reader = new DataReader(stream, Endianess.BigEndian);
            var identInt = reader.ReadUInt32();
            stream.Position = 0;
            return ((identInt & 0xFFFFFF00) == 0x52424600);
        }

        public PsoDataMappingEntry GetBlock(int id)
        {
            if (DataMappingSection == null) return null;
            if (DataMappingSection.Entries == null) return null;
            PsoDataMappingEntry block = null;
            var ind = id - 1;
            var blocks = DataMappingSection.Entries;
            if ((ind >= 0) && (ind < blocks.Count))
            {
                block = blocks[ind];
            }
            return block;
        }

        public static explicit operator XmlDocument(PsoFile pso)
        {
            var doc = new XmlDocument();
            var xml = PsoXml.GetXml(pso);

            doc.LoadXml(xml);

            return doc;
        }
    }

}
