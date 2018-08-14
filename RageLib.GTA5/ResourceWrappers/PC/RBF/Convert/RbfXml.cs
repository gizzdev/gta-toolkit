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
using RageLib.GTA5.RBF;
using RageLib.GTA5.RBF.Types;
using RageLib.GTA5.Utilities;
using System.IO;
using System.Text;

namespace RageLib.GTA5.ResourceWrappers.PC.RBF
{
    public class RbfXml : MetaXmlBase
    {

        public static string GetXml(RbfFile rbf)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(XmlHeader);

            WriteNode(sb, 0, rbf.current);

            return sb.ToString();
        }

        private static void WriteNode(StringBuilder sb, int indent, RbfStructure rs)
        {
            if (rs.Children.Count == 0)
            {
                SelfClosingTag(sb, indent, rs.Name);
                return;
            }

            int cind = indent + 1;

            bool oneline = ((rs.Children.Count == 1) && (rs.Children[0].Name == null));

            OpenTag(sb, indent, rs.Name, !oneline);


            foreach (var child in rs.Children)
            {
                if (child is RbfBytes)
                {
                    var bytesChild = (RbfBytes)child;
                    var contentField = rs.FindChild("content") as RbfString;//TODO: fix this to output nicer XML!
                    if (contentField != null)
                    {
                        OpenTag(sb, cind, "value");
                        var aind = cind + 1;

                        if (contentField.Value == "char_array")
                        {
                            foreach (byte k in bytesChild.Value)
                            {
                                Indent(sb, aind);
                                sb.AppendLine(k.ToString());
                            }
                        }
                        else if (contentField.Value.Equals("short_array"))
                        {
                            var valueReader = new DataReader(new MemoryStream(bytesChild.Value));
                            while (valueReader.Position < valueReader.Length)
                            {
                                Indent(sb, aind);
                                var y = valueReader.ReadUInt16();
                                sb.AppendLine(y.ToString());
                            }
                        }
                        else
                        {
                            ErrorXml(sb, aind, "Unexpected content type: " + contentField.Value);
                        }

                        CloseTag(sb, cind, "value");
                    }
                    else
                    {
                        string stringValue = Encoding.ASCII.GetString(bytesChild.Value);
                        string str = stringValue.Substring(0, stringValue.Length - 1); //removes null terminator

                        sb.Append(str);
                    }
                }
                if (child is RbfFloat)
                {
                    var floatChild = (RbfFloat)child;
                    ValueTag(sb, cind, child.Name, FloatUtil.ToString(floatChild.Value));
                }
                if (child is RbfString)
                {
                    var stringChild = (RbfString)child;
                    StringTag(sb, cind, stringChild.Name, stringChild.Value);

                    //if (stringChild.Name.Equals("content"))
                    //else if (stringChild.Name.Equals("type"))
                    //else throw new Exception("Unexpected string content");
                }
                if (child is RbfStructure)
                {
                    WriteNode(sb, cind, child as RbfStructure);
                }
                if (child is RbfUint32)
                {
                    var intChild = (RbfUint32)child;
                    ValueTag(sb, cind, intChild.Name, UintString(intChild.Value));
                }
                if (child is RbfBoolean)
                {
                    var booleanChild = (RbfBoolean)child;
                    ValueTag(sb, cind, booleanChild.Name, booleanChild.Value.ToString());
                }
                if (child is RbfFloat3)
                {
                    var v3 = child as RbfFloat3;
                    SelfClosingTag(sb, cind, v3.Name + " x=\"" + FloatUtil.ToString(v3.X) + "\" y=\"" + FloatUtil.ToString(v3.Y) + "\" z=\"" + FloatUtil.ToString(v3.Z) + "\"");
                }


            }

            CloseTag(sb, oneline ? 0 : indent, rs.Name);
        }



    }
}
