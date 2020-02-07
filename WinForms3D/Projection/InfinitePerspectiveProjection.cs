using System;
using System.Numerics;

namespace WinForms3D {

    // Buggy

    public class InfinitePerspectiveProjection : IProjection {
        public event EventHandler ProjectionChanged;

        float zNear;
        float fOV;

        public float ZNear {
            get {
                return zNear;
            }

            set {
                if(zNear == value)
                    return;

                zNear = value;

                ProjectionChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public float FOV {
            get {
                return fOV;
            }
            set {
                if(fOV == value)
                    return;

                fOV = value;

                ProjectionChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public InfinitePerspectiveProjection(float fovDegree, float zNear) {
            this.zNear = zNear;
            this.fOV = fovDegree;
        }

        public Matrix4x4 ProjectionMatrix(float w, float h) {
            var f = fOV.ToRad();
            return new Matrix4x4(
                f / (w / h), 0, 0, 0,
                0, f, 0, 0,
                0, 0, 0, -zNear,
                0, 0, -1, 0
           );
        }
    }
}
