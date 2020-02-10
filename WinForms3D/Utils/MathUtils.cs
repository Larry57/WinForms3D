using System;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace WinForms3D {

    // Some code from https://www.davrous.com/2013/06/13/tutorial-series-learning-how-to-write-a-3d-soft-engine-from-scratch-in-c-typescript-or-javascript/

    public static class MathUtils {

        public const float PI_Deg = (float)Math.PI * 1f / 180f;
        public const float Epsilon = 1E-5f;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float ToRad(this float angleDegree) => angleDegree * PI_Deg;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 ToRad(this Vector2 v) => v * PI_Deg;

        // Interpolating the value between 2 vertices 
        // min is the starting point, max the ending point
        // and gradient the % between the 2 points
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static float Lerp(float start, float end, float amount) {
            return start + (end - start) * amount;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        // Compute the cosine of the angle between the light vector and the normal vector
        // Returns a value between 0 and 1
        internal static float ComputeNDotL(Vector3 vertexCenter, Vector3 normal, Vector3 lightPosition) => 
            Math.Max(0, Vector3.Dot(
                Vector3.Normalize(normal),
                Vector3.Normalize(lightPosition - vertexCenter)));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        // Clamping values to keep them between 0 and 1
        public static float Clamp(this float value, float min = 0, float max = 1) => Math.Max(min, Math.Min(value, max));
    }
}
