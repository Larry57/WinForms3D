using System.Diagnostics;

namespace WinForms3D {

    public class Stats {
        public int TotalTriangleCount = 0;
        public int DrawnTriangleCount = 0;
        public int FacingBackTriangleCount = 0;
        public int OutOfViewTriangleCount = 0;
        public int BehindViewTriangleCount = 0;

        public int DrawnPixelCount = 0;
        public int BehindZPixelCount = 0;

        public long CalculationTimeMs { get => calcSw.ElapsedMilliseconds; }
        public long PainterTimeMs { get => paintSw.ElapsedMilliseconds; }

        readonly Stopwatch calcSw = new Stopwatch();
        readonly Stopwatch paintSw = new Stopwatch();

        public void PaintTime() {
            calcSw.Stop();
            paintSw.Start();
        }

        public void CalcTime() {
            paintSw.Stop();
            calcSw.Start();
        }

        public void StopTime() {
            paintSw.Stop();
            calcSw.Stop();
        }

        public void Clear() {
            paintSw.Reset();
            calcSw.Reset();
            TotalTriangleCount = 0;
            DrawnTriangleCount = 0;
            FacingBackTriangleCount = 0;
            OutOfViewTriangleCount = 0;
            BehindViewTriangleCount = 0;
            DrawnPixelCount = 0;
            BehindZPixelCount = 0;
        }
    }
}
