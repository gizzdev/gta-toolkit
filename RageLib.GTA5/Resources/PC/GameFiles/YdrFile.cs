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

using System;
using System.Collections.Generic;
using RageLib.Resources.GTA5.PC.Meta;
using SharpDX;

namespace RageLib.Resources.GTA5.PC.GameFiles
{
    public class YdrFile : GameFileBase_Resource<Drawables.Drawable>
    {
        Drawables.Drawable Drawable;

        public ulong ShaderGroupPointer { get => Drawable.ShaderGroupPointer; set => Drawable.ShaderGroupPointer = value; }
        public ulong SkeletonPointer { get => Drawable.SkeletonPointer; set => Drawable.SkeletonPointer = value; }
        public Vector3 BoundingCenter { get => (Vector3) Drawable.BoundingCenter; set => Drawable.BoundingCenter = (RAGE_Vector3) value; }
        public float BoundingSphereRadius { get => Drawable.BoundingSphereRadius; set => Drawable.BoundingSphereRadius = value; }
        public Vector4 BoundingBoxMin { get => (Vector4) Drawable.BoundingBoxMin; set => Drawable.BoundingBoxMin = (RAGE_Vector4) value; }
        public Vector4 BoundingBoxMax { get => (Vector4) Drawable.BoundingBoxMax; set => Drawable.BoundingBoxMax = (RAGE_Vector4) value; }
        public ulong DrawableModelsHighPointer { get => Drawable.DrawableModelsHighPointer; set => Drawable.DrawableModelsHighPointer = value; }
        public ulong DrawableModelsMediumPointer { get => Drawable.DrawableModelsMediumPointer; set => Drawable.DrawableModelsMediumPointer = value; }
        public ulong DrawableModelsLowPointer { get => Drawable.DrawableModelsLowPointer; set => Drawable.DrawableModelsLowPointer = value; }
        public ulong DrawableModelsVeryLowPointer { get => Drawable.DrawableModelsVeryLowPointer; set => Drawable.DrawableModelsVeryLowPointer = value; }
        public float LodGroupHigh { get => Drawable.LodGroupHigh; set => Drawable.LodGroupHigh = value; }
        public float LodGroupMed { get => Drawable.LodGroupMed; set => Drawable.LodGroupMed = value; }
        public float LodGroupLow { get => Drawable.LodGroupLow; set => Drawable.LodGroupLow = value; }
        public float LodGroupVlow { get => Drawable.LodGroupVlow; set => Drawable.LodGroupVlow = value; }
        public uint Unknown_80h { get => Drawable.Unknown_80h; set => Drawable.Unknown_80h = value; }
        public uint Unknown_84h { get => Drawable.Unknown_84h; set => Drawable.Unknown_84h = value; }
        public uint Unknown_88h { get => Drawable.Unknown_88h; set => Drawable.Unknown_88h = value; }
        public uint Unknown_8Ch { get => Drawable.Unknown_8Ch; set => Drawable.Unknown_8Ch = value; }
        public ulong JointsPointer { get => Drawable.JointsPointer; set => Drawable.JointsPointer = value; }
        public uint Unknown_98h { get => Drawable.Unknown_98h; set => Drawable.Unknown_98h = value; }
        public uint Unknown_9Ch { get => Drawable.Unknown_9Ch; set => Drawable.Unknown_9Ch = value; }
        public ulong DrawableModelsXPointer { get => Drawable.DrawableModelsXPointer; set => Drawable.DrawableModelsXPointer = value; }

        public YdrFile()
        {
            
        }

        public override void Parse()
        {
            this.Drawable = this.ResourceFile.ResourceData;
        }

        public override void Build()
        {

        }

    }

}