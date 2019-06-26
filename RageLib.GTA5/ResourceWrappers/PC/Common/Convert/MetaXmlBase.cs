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

using RageLib.GTA5.Utilities;
using RageLib.Hash;
using RageLib.Resources.GTA5.PC.Meta;
using SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RageLib.GTA5.ResourceWrappers.PC
{
    public class MetaXmlBase
    {

        public const string XmlHeader = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>";



        public static void Indent(StringBuilder sb, int indent)
        {
            for (int i = 0; i < indent; i++)
            {
                sb.Append(" ");
            }
        }
        public static void ErrorXml(StringBuilder sb, int indent, string msg)
        {
            Indent(sb, indent);
            sb.Append("<error>");
            sb.Append(msg);
            sb.Append("</error>");
            sb.AppendLine();
        }
        public static void OpenTag(StringBuilder sb, int indent, string name, bool appendLine = true)
        {
            Indent(sb, indent);
            sb.Append("<");
            sb.Append(name);
            sb.Append(">");
            if (appendLine) sb.AppendLine();
        }
        public static void CloseTag(StringBuilder sb, int indent, string name, bool appendLine = true)
        {
            Indent(sb, indent);
            sb.Append("</");
            sb.Append(name);
            sb.Append(">");
            if (appendLine) sb.AppendLine();
        }
        public static void ValueTag(StringBuilder sb, int indent, string name, string val)
        {
            Indent(sb, indent);
            sb.Append("<");
            sb.Append(name);
            sb.Append(" value=\"");
            sb.Append(val);
            sb.Append("\" />");
            sb.AppendLine();
        }
        public static void OneLineTag(StringBuilder sb, int indent, string name, string text)
        {
            Indent(sb, indent);
            sb.Append("<");
            sb.Append(name);
            sb.Append(">");
            sb.Append(text);
            sb.Append("</");
            sb.Append(name);
            sb.Append(">");
            sb.AppendLine();
        }
        public static void SelfClosingTag(StringBuilder sb, int indent, string val)
        {
            Indent(sb, indent);
            sb.Append("<");
            sb.Append(val);
            sb.Append(" />");
            sb.AppendLine();
        }
        public static void StringTag(StringBuilder sb, int indent, string name, string text)
        {
            if (!string.IsNullOrEmpty(text)) OneLineTag(sb, indent, name, text);
            else SelfClosingTag(sb, indent, name);
        }

        public static void WriteRawArray<T>(StringBuilder sb, T[] arr, int ind, string name, string typeName, Func<T, string> formatter = null, int arrRowSize = 10) where T : struct
        {
            var aCount = arr?.Length ?? 0;
            //var arrRowSize = 10;
            var aind = ind + 1;
            var arrTag = name;// + " itemType=\"" + typeName + "\"";
            if (aCount > 0)
            {
                if (aCount <= arrRowSize)
                {
                    OpenTag(sb, ind, arrTag, false);
                    for (int n = 0; n < aCount; n++)
                    {
                        if (n > 0) sb.Append(" ");
                        string str = (formatter != null) ? formatter(arr[n]) : arr[n].ToString();
                        sb.Append(str);
                    }
                    CloseTag(sb, 0, name);
                }
                else
                {
                    OpenTag(sb, ind, arrTag);
                    for (int n = 0; n < aCount; n++)
                    {
                        var col = n % arrRowSize;
                        if (col == 0) Indent(sb, aind);
                        if (col > 0) sb.Append(" ");
                        string str = (formatter != null) ? formatter(arr[n]) : arr[n].ToString();
                        sb.Append(str);
                        bool lastcol = (col == (arrRowSize - 1));
                        bool lastn = (n == (aCount - 1));
                        if (lastcol || lastn) sb.AppendLine();
                    }
                    CloseTag(sb, ind, name);
                }
            }
            else
            {
                SelfClosingTag(sb, ind, arrTag);
            }
        }

        public static void WriteItemArray<T>(StringBuilder sb, T[] arr, int ind, string name, string typeName, Func<T, string> formatter) where T : struct
        {
            var aCount = arr?.Length ?? 0;
            var arrTag = name;// + " itemType=\"Hash\"";
            var aind = ind + 1;
            if (aCount > 0)
            {
                OpenTag(sb, ind, arrTag);
                for (int n = 0; n < aCount; n++)
                {
                    Indent(sb, aind);
                    sb.Append("<Item>");
                    sb.Append(formatter(arr[n]));
                    sb.AppendLine("</Item>");
                }
                CloseTag(sb, ind, name);
            }
            else
            {
                SelfClosingTag(sb, ind, arrTag);
            }
        }

        public static string FormatHash(MetaName h) //for use with WriteItemArray
        {
            return HashString(h);// "hash_" + h.Hex;
        }
        public static string FormatVector2(Vector2 v) //for use with WriteItemArray
        {
            return FloatUtil.GetVector2String(v);
        }
        public static string FormatVector3(Vector3 v) //for use with WriteItemArray
        {
            return FloatUtil.GetVector3String(v);
        }

        public static string FormatVector4(Vector3 v) //for use with WriteItemArray
        {
            return FloatUtil.GetVector4String((Vector4)v);
        }

        public static string FormatVector4(Vector4 v) //for use with WriteItemArray
        {
            return FloatUtil.GetVector4String(v);
        }

        public static string FormatHashSwap(MetaName h) //for use with WriteItemArray, swaps endianness
        {
            return MetaUtils.SwapBytes((uint)h).ToString();
        }
        public static string FormatVector2Swap(Vector2 v) //for use with WriteItemArray, swaps endianness
        {
            return FloatUtil.GetVector2String(MetaUtils.SwapBytes(v));
        }
        public static string FormatVector3Swap(Vector3 v) //for use with WriteItemArray, swaps endianness
        {
            return FloatUtil.GetVector3String(MetaUtils.SwapBytes(v));
        }
        public static string FormatVector4Swap(Vector4 v) //for use with WriteItemArray, swaps endianness
        {
            return FloatUtil.GetVector4String(MetaUtils.SwapBytes(v));
        }
        public static string FormatVector4SwapXYZOnly(Vector4 v) //for use with WriteItemArray, swaps endianness, and outputs only XYZ components
        {
            return FloatUtil.GetVector3String(MetaUtils.SwapBytes((Vector3)v));
        }

        public static string HashString(MetaName h)
        {
            if (Enum.IsDefined(typeof(MetaName), h))
            {
                return h.ToString();
            }

            uint uh = (uint)h;
            if (uh == 0) return "";

            var str = Jenkins.TryGetString(uh);
            if (!string.IsNullOrEmpty(str)) return str;

            return "hash_" + uh.ToString("X").PadLeft(8, '0');

        }

        public static string UintString(uint h)
        {
            if (Enum.IsDefined(typeof(MetaName), h))
            {
                return ((MetaName)h).ToString();
            }

            return "0x" + h.ToString("X");

        }



        public enum XmlTagMode
        {
            None = 0,
            Structure = 1,
            Item = 2,
            ItemAndType = 3,
        }
    }

}
