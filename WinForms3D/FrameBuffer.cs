using System;
using System.Diagnostics;
using System.Numerics;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace WinForms3D {

    public class FrameBuffer {
        private readonly int[] emptyZBuffer;
        private readonly int[] emptyBuffer;
        
        private readonly RenderContext renderContext;
        private int[] zBuffer;

        public int[] Screen { get; }

        internal int Width { get; }
        internal int Height { get; }
        internal int Depth { get; set; } = 65535; // Build a true Z buffer based on Zfar/Znear planes

        private float widthMinus1By2 { get; }
        private float heightMinus1By2 { get; }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector3 ToScreen3(Vector4 p) => new Vector3(
            widthMinus1By2 * (p.X / p.W + 1),  // Using width - 1 to prevent overflow by -1 and 1 NDC coordinates
            -heightMinus1By2 * (p.Y / p.W - 1), // Using height - 1 to prevent overflow by -1 and 1 NDC coordinates
            Depth * p.Z / p.W);

        public FrameBuffer(int width, int height, RenderContext renderContext) {
            this.Screen = new int[width * height];
            this.zBuffer = new int[width * height];

            this.emptyBuffer = new int[width * height];
            this.emptyZBuffer = new int[width * height];
            this.emptyZBuffer.Fill(Depth);

            this.Width = width;
            this.Height = height;
            this.renderContext = renderContext;
            this.widthMinus1By2 = (width - 1) / 2f;
            this.heightMinus1By2 = (height - 1) / 2f;
        }

        public void Clear() {
            Array.Copy(emptyBuffer, Screen, Screen.Length);
            Array.Copy(emptyZBuffer, zBuffer, zBuffer.Length);
        }

        // Called to put a pixel on screen at a specific X,Y coordinates
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void PutPixel(int x, int y, int z, ColorRGB color) {
#if DEBUG
            if(x > Width - 1 || x < 0 || y > Height - 1 || y < 0) {
                throw new OverflowException($"PutPixel X={x}/{Width}: Y={y}/{Height}, Depth={z}");
            }
#endif
            var index = x + y * Width;
            if(z > zBuffer[index]) {
                renderContext.Stats.BehindZPixelCount++;
                return;
            }

            renderContext.Stats.DrawnPixelCount++;

            zBuffer[index] = z;

            Screen[index] = color.Color;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void DrawLine(Vector3 p0, Vector3 p1, ColorRGB color) {

            var x0 = (int)p0.X; var y0 = (int)p0.Y; var z0 = (int)p0.Z;
            var x1 = (int)p1.X; var y1 = (int)p1.Y; var z1 = (int)p1.Z;

            var dx = Math.Abs(x1 - x0); var dy = Math.Abs(y1 - y0); var dz = Math.Abs(z1 - z0);

            var sx = x0 < x1 ? 1 : -1; var sy = y0 < y1 ? 1 : -1; var sz = z0 < z1 ? 1 : -1;

            var ex = 0; var ey = 0; var ez = 0;

            var dmax = Math.Max(dx, dy);

            int i = 0;
            while(i++ < dmax) {
                ex += dx; if(ex >= dmax) { ex -= dmax; x0 += sx; PutPixel(x0, y0, z0, color); }
                ey += dy; if(ey >= dmax) { ey -= dmax; y0 += sy; PutPixel(x0, y0, z0, color); }
                ez += dz; if(ez >= dmax) { ez -= dmax; z0 += sz; PutPixel(x0, y0, z0, color); }
            }
        }
    }
}