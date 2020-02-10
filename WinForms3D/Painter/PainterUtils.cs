using System.Numerics;
using System.Runtime.CompilerServices;

namespace WinForms3D {
    class PainterUtils {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SortTrianglePoints(VertexBuffer vbx, FrameBuffer frameBuffer, int triangleIndices, out PaintedVertex v0, out PaintedVertex v1, out PaintedVertex v2) {
            var t = vbx.Volume.Triangles[triangleIndices];

            var worldNormVertices = vbx.WorldNormVertices;
            var projectionVertices = vbx.ProjectionVertices;
            var worldVertices = vbx.WorldVertices;

            v0 = new PaintedVertex(worldNormVertices[t.I0], frameBuffer.ToScreen3(projectionVertices[t.I0]), worldVertices[t.I0]);
            v1 = new PaintedVertex(worldNormVertices[t.I1], frameBuffer.ToScreen3(projectionVertices[t.I1]), worldVertices[t.I1]);
            v2 = new PaintedVertex(worldNormVertices[t.I2], frameBuffer.ToScreen3(projectionVertices[t.I2]), worldVertices[t.I2]);

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
