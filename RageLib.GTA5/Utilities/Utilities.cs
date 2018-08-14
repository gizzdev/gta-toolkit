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

using SharpDX;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RageLib.GTA5.Utilities
{
    public static class FloatUtil
    {
        public static bool TryParse(string s, out float f)
        {
            f = 0.0f;
            if (float.TryParse(s, NumberStyles.Any, CultureInfo.InvariantCulture, out f))
            {
                return true;
            }
            return false;
        }
        public static float Parse(string s)
        {
            float f;
            TryParse(s, out f);
            return f;
        }
        public static string ToString(float f)
        {
            var c = CultureInfo.InvariantCulture;
            return f.ToString(c);
        }


        public static string GetVector2String(Vector2 v)
        {
            var c = CultureInfo.InvariantCulture;
            return v.X.ToString(c) + ", " + v.Y.ToString(c);
        }
        public static string GetVector3String(Vector3 v)
        {
            var c = CultureInfo.InvariantCulture;
            return v.X.ToString(c) + ", " + v.Y.ToString(c) + ", " + v.Z.ToString(c);
        }
        public static string GetVector3String(Vector3 v, string format)
        {
            var c = CultureInfo.InvariantCulture;
            return v.X.ToString(format, c) + ", " + v.Y.ToString(format, c) + ", " + v.Z.ToString(format, c);
        }
        public static string GetVector3XmlString(Vector3 v)
        {
            var c = CultureInfo.InvariantCulture;
            return string.Format("x=\"{0}\" y=\"{1}\" z=\"{2}\"", v.X.ToString(c), v.Y.ToString(c), v.Z.ToString(c));
        }
        public static string GetVector4XmlString(Vector4 v)
        {
            var c = CultureInfo.InvariantCulture;
            return string.Format("x=\"{0}\" y=\"{1}\" z=\"{2}\" w=\"{3}\"", v.X.ToString(c), v.Y.ToString(c), v.Z.ToString(c), v.W.ToString(c));
        }
        public static string GetQuaternionXmlString(Quaternion q)
        {
            var c = CultureInfo.InvariantCulture;
            return string.Format("x=\"{0}\" y=\"{1}\" z=\"{2}\" w=\"{3}\"", q.X.ToString(c), q.Y.ToString(c), q.Z.ToString(c), q.W.ToString(c));
        }

        public static Vector3 ParseVector3String(string s)
        {
            Vector3 p = new Vector3(0.0f);
            string[] ss = s.Split(',');
            if (ss.Length > 0)
            {
                FloatUtil.TryParse(ss[0].Trim(), out p.X);
            }
            if (ss.Length > 1)
            {
                FloatUtil.TryParse(ss[1].Trim(), out p.Y);
            }
            if (ss.Length > 2)
            {
                FloatUtil.TryParse(ss[2].Trim(), out p.Z);
            }
            return p;
        }



        public static string GetVector4String(Vector4 v)
        {
            var c = CultureInfo.InvariantCulture;
            return v.X.ToString(c) + ", " + v.Y.ToString(c) + ", " + v.Z.ToString(c) + ", " + v.W.ToString(c);
        }
        public static Vector4 ParseVector4String(string s)
        {
            Vector4 p = new Vector4(0.0f);
            string[] ss = s.Split(',');
            if (ss.Length > 0)
            {
                FloatUtil.TryParse(ss[0].Trim(), out p.X);
            }
            if (ss.Length > 1)
            {
                FloatUtil.TryParse(ss[1].Trim(), out p.Y);
            }
            if (ss.Length > 2)
            {
                FloatUtil.TryParse(ss[2].Trim(), out p.Z);
            }
            if (ss.Length > 3)
            {
                FloatUtil.TryParse(ss[3].Trim(), out p.W);
            }
            return p;
        }


    }

}
