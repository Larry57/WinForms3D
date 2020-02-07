using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace WinForms3D {
    public class WireFramePainter : IPainter {
        public RenderContext RendererContext { get; set; }

        IClippingHomogeneous clipperH = new LiangBarskyClippingHomogeneous();

        public void DrawTriangle(ColorRGB color, VertexBuffer vbx, IVolume v, int triangleIndice) {
            var surface = RendererContext.Surface;
            var t = v.TriangleIndices[triangleIndice];

            var projectionP0 = vbx.ProjectionVertices[t.I1];
            var projectionP1 = vbx.ProjectionVertices[t.I2];
            var projectionP2 = vbx.ProjectionVertices[t.I3];

            var l0p0 = projectionP0; var l0p1 = projectionP1;
            var l1p0 = projectionP1; var l1p1 = projectionP2;
            var l2p0 = projectionP0; var l2p1 = projectionP2;

            var l0 = clipperH.Clip(ref l0p0, ref l0p1);
            var l1 = clipperH.Clip(ref l1p0, ref l1p1);
            var l2 = clipperH.Clip(ref l2p0, ref l2p1);

            if(l0) surface.DrawLine(surface.ToScreen3(l0p0), surface.ToScreen3(l0p1), color);
            if(l1) surface.DrawLine(surface.ToScreen3(l1p0), surface.ToScreen3(l1p1), color);
            if(l2) surface.DrawLine(surface.ToScreen3(l2p0), surface.ToScreen3(l2p1), color);
        }

        public void DrawLine(FrameBuffer surface, ColorRGB color, Vector4 projectionP0, Vector4 projectionP1) {
            if(!clipperH.Clip(ref projectionP0, ref projectionP1))
                return;

            var p0 = surface.ToScreen3(projectionP0);
            var p1 = surface.ToScreen3(projectionP1);

            surface.DrawLine(p0, p1, color);
        }
    }
}
