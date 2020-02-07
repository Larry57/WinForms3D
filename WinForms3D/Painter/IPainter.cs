﻿using System.Numerics;

namespace WinForms3D {
    public interface IPainter {
        RenderContext RendererContext { get; set; }
        void DrawTriangle(ColorRGB color, VertexBuffer vbx, IVolume v, int triangleIndice);
    }
}