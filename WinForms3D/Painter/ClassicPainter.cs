using System;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace WinForms3D {

    public class ClassicPainter : IPainter {
        public RenderContext RendererContext { get; set; }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void DrawTriangle(ColorRGB color, VertexBuffer vbx, int triangleIndice) {
            var surface = RendererContext.Surface;
            PainterUtils.SortTrianglePoints(vbx, surface, triangleIndice, out var v0, out var v1, out var v2);

            var p0 = v0.ScreenPoint; var p1 = v1.ScreenPoint; var p2 = v2.ScreenPoint;

            var yStart = (int)Math.Max(p0.Y, 0);
            var yEnd = (int)Math.Min(p2.Y, surface.Height - 1);

            // Out if clipped
            if(yStart > yEnd) return;

            var yMiddle = MathUtils.Clamp((int)p1.Y, yStart, yEnd);

            if(PainterUtils.Cross2D(p0, p1, p2) > 0) {
                // P0
                //   P1
                // P2
                paintHalfTriangle(yStart, (int)yMiddle - 1, color, p0, p2, p0, p1);
                paintHalfTriangle((int)yMiddle, yEnd, color, p0, p2, p1, p2);
            }
            else {
                //   P0
                // P1
                //   P2
                paintHalfTriangle(yStart, (int)yMiddle - 1, color, p0, p1, p0, p2);
                paintHalfTriangle((int)yMiddle, yEnd, color, p1, p2, p0, p2);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        void paintHalfTriangle(int yStart, int yEnd, ColorRGB color, Vector3 pa, Vector3 pb, Vector3 pc, Vector3 pd) {
            var mg1 = pa.Y == pb.Y ? 1f : 1 / (pb.Y - pa.Y);
            var mg2 = pd.Y == pc.Y ? 1f : 1 / (pd.Y - pc.Y);

            for(var y = yStart; y <= yEnd; y++) {
                var gradient1 = ((y - pa.Y) * mg1).Clamp();
                var gradient2 = ((y - pc.Y) * mg2).Clamp();

                var sx = MathUtils.Lerp(pa.X, pb.X, gradient1);
                var ex = MathUtils.Lerp(pc.X, pd.X, gradient2);

                if(sx >= ex) continue;

                var sz = MathUtils.Lerp(pa.Z, pb.Z, gradient1);
                var ez = MathUtils.Lerp(pc.Z, pd.Z, gradient2);

                ProcessScanLine(y, sx, ex, sz, ez, color);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        void ProcessScanLine(float y, float sx, float ex, float sz, float ez, ColorRGB color) {
            var surface = RendererContext.Surface;

            var minX = Math.Max(sx, 0);
            var maxX = Math.Min(ex, surface.Width);

            var mx = 1 / (ex - sx);

            for(var x = minX; x < maxX; x++) {
                var gradient = (x - sx) * mx;

                var z = MathUtils.Lerp(sz, ez, gradient);

                surface.PutPixel((int)x, (int)y, (int)z, color);
            }
        }
    }
}
