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

using RageLib.GTA5.PSO;
using RageLib.Resources.GTA5.PC.Meta;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RageLib.Resources.GTA5.PC.PSO
{
    public class PsoBuilder
    {
        private Dictionary<MetaName, PsoStructureInfo> StructureInfos = new Dictionary<MetaName, PsoStructureInfo>();
        private Dictionary<MetaName, PsoEnumInfo> EnumInfos = new Dictionary<MetaName, PsoEnumInfo>();

        public PsoFile GetPso()
        {
            var pso = new PsoFile();

            pso.DefinitionSection = new PsoDefinitionSection();
            pso.DataMappingSection = new PsoDataMappingSection();
            pso.DataSection = new PsoDataSection();

            // DefinitionSection
            foreach (var si in this.StructureInfos)
            {
                PsoElementIndexInfo indexInfo = new PsoElementIndexInfo();
                indexInfo.Offset = 0;
                indexInfo.NameHash = si.Value.IndexInfo.NameHash;

                pso.DefinitionSection.EntriesIdx.Add(indexInfo);
                pso.DefinitionSection.Entries.Add(si.Value);
            }

            foreach (var ei in this.EnumInfos)
            {
                PsoElementIndexInfo indexInfo = new PsoElementIndexInfo();
                indexInfo.Offset = 0;
                indexInfo.NameHash = ei.Value.IndexInfo.NameHash;

                pso.DefinitionSection.EntriesIdx.Add(indexInfo);
                pso.DefinitionSection.Entries.Add(ei.Value);
            }

            pso.DefinitionSection.Count = (uint)pso.DefinitionSection.Entries.Count;

            // DataMappingSection
            pso.DataMappingSection.Entries = new List<PsoDataMappingEntry>();
            MemoryStream memoryStream = new MemoryStream();
            memoryStream.Position = 16L;

            byte[] array2 = new byte[memoryStream.Length];
            memoryStream.Position = 0L;
            memoryStream.Read(array2, 0, array2.Length);
            pso.DataSection.Data = array2;

            pso.DataMappingSection.RootIndex = 1;


            return pso;
        }

        public void AddStructureInfo(MetaName name)
        {
            if (!StructureInfos.ContainsKey(name))
            {
                PsoStructureInfo si = PsoInfo.GetStructureInfo(name);
                if (si != null)
                {
                    StructureInfos[name] = si;
                }
            }
        }
        public void AddEnumInfo(MetaName name)
        {
            if (!EnumInfos.ContainsKey(name))
            {
                PsoEnumInfo ei = null; PsoInfo.GetEnumInfo(name);
                if (ei != null)
                {
                    EnumInfos[name] = ei;
                }
            }
        }

    }
}
