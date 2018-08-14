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
using RageLib.GTA5.RBF;
using RageLib.Resources.GTA5;
using RageLib.GTA5.ResourceWrappers.PC.Meta;
using RageLib.GTA5.ResourceWrappers.PC.PSO;
using RageLib.GTA5.ResourceWrappers.PC.RBF;
using RageLib.Resources.GTA5.PC.Meta;
using System;
using System.IO;
using System.Xml;
using System.Reflection;
using RageLib.Hash;

namespace MetaTool
{
    public class Program
    {
        private string[] arguments;

        public static void Main(string[] args)
        {
            new Program(args).Run();
        }

        public Program(string[] arguments)
        {
            this.arguments = arguments;
        }

        public void Run()
        {
            if (arguments.Length > 0)
            {


                Convert();
            }
            else
            {
                Console.WriteLine("No input file specified.");
                Console.ReadLine();
            }
        }

        public void Convert()
        {
            if (this.arguments[0].EndsWith(".ymap.xml") || this.arguments[0].EndsWith(".ytyp.xml") || this.arguments[0].EndsWith(".ymt.xml"))
            {
                this.ConvertToMetaResource();
                return;
            }
            if (this.arguments[0].EndsWith(".pso.xml"))
            {
                this.ConvertToMetaPso();
                return;
            }
            if (this.arguments[0].EndsWith(".ymap") || this.arguments[0].EndsWith(".ytyp") || this.arguments[0].EndsWith(".ymt"))
            {
                this.ConvertResourceToXml();
                return;
            }
            if (this.arguments[0].EndsWith(".ymf"))
            {
                this.ConvertPsoToXml();
                return;
            }
            Console.WriteLine("Unsupported file extension.");
            Console.ReadLine();
        }

        public void ConvertToMetaPso()
        {
            throw new NotImplementedException();
        }

        private void ConvertToMetaResource()
        {
            string inputFileName = arguments[0];
            string outputFileName = inputFileName.Replace(".xml", "");

            var xmlDoc = new XmlDocument();
            xmlDoc.Load(inputFileName);

            var res = new ResourceFile_GTA5_pc<MetaFile>();

            res.Version = 2;
            res.ResourceData = XmlMeta.GetMeta(xmlDoc);

            res.Save(outputFileName);
        }
        
        private void ConvertResourceToXml()
        {
            string inputFileName = arguments[0];
            string outputFileName = inputFileName + ".xml";

            var res = new ResourceFile_GTA5_pc<MetaFile>();
            res.Load(inputFileName);

            AddHashForStrings("MetaTool.Lists.FileNames.txt");
            AddHashForStrings("MetaTool.Lists.MetaNames.txt");

            var xml = MetaXml.GetXml(res.ResourceData);

            File.WriteAllText(outputFileName, xml);
        }

        private void ConvertPsoToXml()
        {
            string inputFileName = arguments[0];
            string outputFileName = inputFileName + ".pso.xml";

            var pso = new PsoFile();
            pso.Load(inputFileName);

            AddHashForStrings("MetaTool.Lists.PsoTypeNames.txt");
            AddHashForStrings("MetaTool.Lists.PsoFieldNames.txt");
            AddHashForStrings("MetaTool.Lists.PsoEnumValues.txt");
            AddHashForStrings("MetaTool.Lists.PsoCommon.txt");
            AddHashForStrings("MetaTool.Lists.FileNames.txt");
            AddHashForStrings("MetaTool.Lists.PsoCollisions.txt");

            var xml = PsoXml.GetXml(pso);

            File.WriteAllText(outputFileName, xml);
        }

        private void ConvertRbfToXml()
        {
            string inputFileName = arguments[0];
            string outputFileName = inputFileName + ".rbf.xml";

            var rbf = new RbfFile();

            rbf.Load(inputFileName);

            var xml = RbfXml.GetXml(rbf);

            File.WriteAllText(outputFileName, xml);
        }

        private void AddHashForStrings(string resourceFileName)
        {
            var assembly = Assembly.GetExecutingAssembly();
            using (Stream namesStream = assembly.GetManifestResourceStream(resourceFileName))
            {
                using (StreamReader namesReader = new StreamReader(namesStream))
                {
                    while (!namesReader.EndOfStream)
                    {
                        string name = namesReader.ReadLine();
                        Jenkins.Ensure(name);
                    }
                }
            }
        }
    }
}
