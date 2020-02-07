using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace WinForms3D {
    static class MiscUtils {

        public static void Fill<T>(this T[] destinationArray, params T[] value) {
            Array.Copy(value, destinationArray, value.Length);

            int copyLength, nextCopyLength, destinationLength = destinationArray.Length;

            for(copyLength = value.Length; (nextCopyLength = copyLength << 1) < destinationLength; copyLength = nextCopyLength)
                Array.Copy(destinationArray, 0, destinationArray, copyLength, copyLength);

            Array.Copy(destinationArray, 0, destinationArray, copyLength, destinationLength - copyLength);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void Swap<T>(ref T a, ref T b) {
            var temp = b; b = a; a = temp;
        }

        public static void Benchmark(this Action action, string caption, int l = 10000) {
            var sw = Stopwatch.StartNew();

            for(var i = 0; i < l; i++)
                action();

            sw.Stop();
            Console.WriteLine(caption + ":" + sw.ElapsedMilliseconds + " ms");
        }
    }
}
