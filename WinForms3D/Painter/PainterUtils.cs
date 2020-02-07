using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WinForms3D {
    class PainterUtils {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SortTrianglePoints(VertexBuffer vbx, FrameBuffer frameBuffer, IVolume v, int triangleIndices, out PaintedVertex v0, out PaintedVertex v1, out PaintedVertex v2) {
            var t = v.TriangleIndices[triangleIndices];

            v0 = new PaintedVertex(vbx.WorldNormVertices[t.I1], frameBuffer.ToScreen3(vbx.ProjectionVertices[t.I1]), vbx.WorldVertices[t.I1]);
            v1 = new PaintedVertex(vbx.WorldNormVertices[t.I2], frameBuffer.ToScreen3(vbx.ProjectionVertices[t.I2]), vbx.WorldVertices[t.I2]);
            v2 = new PaintedVertex(vbx.WorldNormVertices[t.I3], frameBuffer.ToScreen3(vbx.ProjectionVertices[t.I3]), vbx.WorldVertices[t.I3]);

            if(v0.ScreenPoint.Y > v1.ScreenPoint.Y) MiscUtils.Swap(ref v0, ref v1);
            if(v1.ScreenPoint.Y > v2.ScreenPoint.Y) MiscUtils.Swap(ref v1, ref v2);
            if(v0.ScreenPoint.Y > v1.ScreenPoint.Y) MiscUtils.Swap(ref v0, ref v1);
        }

        // https://www.geeksforgeeks.org/orientation-3-ordered-points/

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Cross2D(Vector3 p0, Vector3 p1, Vector3 p2) {
            return (p1.X - p0.X) * (p2.Y - p1.Y) - (p1.Y - p0.Y) * (p2.X - p1.X);
        }
    }
}
