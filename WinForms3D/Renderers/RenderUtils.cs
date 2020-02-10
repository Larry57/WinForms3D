using System.Numerics;

namespace WinForms3D {

    public class RenderUtils {

        public static void drawGrid(FrameBuffer surface, WireFramePainter wireFramePainter, Matrix4x4 world2Projection, float from, float to) {
            for(var xz = from; xz <= to; xz++) {
                drawLine(surface, wireFramePainter, world2Projection, new Vector3(xz, 0, from), new Vector3(xz, 0, to), xz == 0 ? ColorRGB.Red : ColorRGB.Green);
                drawLine(surface, wireFramePainter, world2Projection, new Vector3(from, 0, xz), new Vector3(to, 0, xz), ColorRGB.Green);
            }
        }

        public static void drawAxes(FrameBuffer surface, WireFramePainter wireFramePainter, Matrix4x4 world2Projection) {
            drawLine(surface, wireFramePainter, world2Projection, new Vector3(0, 0, 0), new Vector3(1, 0, 0), ColorRGB.Red);
            drawLine(surface, wireFramePainter, world2Projection, new Vector3(1, 0, 0), new Vector3(.75f, .25f, 0), ColorRGB.Red);
            drawLine(surface, wireFramePainter, world2Projection, new Vector3(1, 0, 0), new Vector3(.75f, -.25f, 0), ColorRGB.Red);
            drawLine(surface, wireFramePainter, world2Projection, new Vector3(1, 0, 0), new Vector3(.75f, 0, .25f), ColorRGB.Red);
            drawLine(surface, wireFramePainter, world2Projection, new Vector3(1, 0, 0), new Vector3(.75f, 0, -.25f), ColorRGB.Red);

            drawLine(surface, wireFramePainter, world2Projection, new Vector3(0, 0, 0), new Vector3(0, 1, 0), ColorRGB.Green);
            drawLine(surface, wireFramePainter, world2Projection, new Vector3(0, 1, 0), new Vector3(-.25f, .75f, 0), ColorRGB.Green);
            drawLine(surface, wireFramePainter, world2Projection, new Vector3(0, 1, 0), new Vector3(.25f, .75f, 0), ColorRGB.Green);
            drawLine(surface, wireFramePainter, world2Projection, new Vector3(0, 1, 0), new Vector3(0, .75f, -.25f), ColorRGB.Green);
            drawLine(surface, wireFramePainter, world2Projection, new Vector3(0, 1, 0), new Vector3(0, .75f, .25f), ColorRGB.Green);

            drawLine(surface, wireFramePainter, world2Projection, new Vector3(0, 0, 0), new Vector3(0, 0, 1), ColorRGB.Blue);
            drawLine(surface, wireFramePainter, world2Projection, new Vector3(0, 0, 1), new Vector3(-.25f, 0, .75f), ColorRGB.Blue);
            drawLine(surface, wireFramePainter, world2Projection, new Vector3(0, 0, 1), new Vector3(.25f, 0, .75f), ColorRGB.Blue);
            drawLine(surface, wireFramePainter, world2Projection, new Vector3(0, 0, 1), new Vector3(0, -.25f, .75f), ColorRGB.Blue);
            drawLine(surface, wireFramePainter, world2Projection, new Vector3(0, 0, 1), new Vector3(0, .25f, .75f), ColorRGB.Blue);
        }

        static void drawLine(FrameBuffer surface, WireFramePainter wireFramePainter, Matrix4x4 world2Projection, Vector3 worldP0, Vector3 worldP1, ColorRGB color) {
            var projectionP0 = Vector4.Transform(worldP0, world2Projection);
            var projectionP1 = Vector4.Transform(worldP1, world2Projection);

            wireFramePainter.DrawLine(surface, color, projectionP0, projectionP1);
        }
    }
}