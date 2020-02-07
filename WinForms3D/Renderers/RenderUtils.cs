using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace WinForms3D {

    public class RenderUtils {

        public static bool isTriangleFacingBack(Vector3 p1, Vector3 p2, Vector3 p3) {
            var vCentroid = Vector3.Normalize((p1 + p2 + p3) / 3);
            var vNormal = Vector3.Normalize(Vector3.Cross(p2 - p1, p3 - p1));
            return Vector3.Dot(vCentroid, vNormal) >= 0;
        }

        public static bool isTriangleBehindFarPlane(Vector3 viewP1, Vector3 viewP2, Vector3 viewP3) {
            return viewP1.Z > 0 && viewP2.Z > 0 && viewP3.Z > 0;
        }

        public static bool isTriangleOutsideFrustum(Vector4 projectionP1, Vector4 projectionP2, Vector4 projectionP3) {
            if(projectionP1.W < 0 || projectionP2.W < 0 || projectionP3.W < 0)
                return true;

            if(projectionP1.X < -projectionP1.W && projectionP2.X < -projectionP2.W && projectionP3.X < -projectionP3.W)
                return true;

            if(projectionP1.X > projectionP1.W && projectionP2.X > projectionP2.W && projectionP3.X > projectionP3.W)
                return true;

            if(projectionP1.Y < -projectionP1.W && projectionP2.Y < -projectionP2.W && projectionP3.Y < -projectionP3.W)
                return true;

            if(projectionP1.Y > projectionP1.W && projectionP2.Y > projectionP2.W && projectionP3.Y > projectionP3.W)
                return true;

            if(projectionP1.Z > projectionP1.W && projectionP2.Z > projectionP2.W && projectionP3.Z > projectionP3.W)
                return true;

            // This last one is normally not necessary when a IsTriangleBehind check is done
            if(projectionP1.Z < 0 && projectionP2.Z < 0 && projectionP3.Z < 0)
                return true;

            return false;
        }

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