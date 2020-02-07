using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace WinForms3D {

    public class FovPerspectiveProjection : IProjection {
        float zNear;
        float zFar;
        float fOV;

        public FovPerspectiveProjection(float fOV, float zNear, float zFar) {
            this.zNear = zNear;
            this.zFar = zFar;
            this.fOV = fOV;
        }

        public float FOV {
            get {
                return fOV;
            }

            set {
                if(PropertyChangedHelper.ChangeValue(ref fOV, value)) {
                    ProjectionChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        public float ZNear {
            get {
                return zNear;
            }

            set {
                if(PropertyChangedHelper.ChangeValue(ref zNear, value)) {
                    ProjectionChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        public float ZFar {
            get {
                return zFar;
            }

            set {
                if(PropertyChangedHelper.ChangeValue(ref zFar, value)) {
                    ProjectionChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        public event EventHandler ProjectionChanged;

        public Matrix4x4 ProjectionMatrix(float width, float height) => Matrix4x4.CreatePerspectiveFieldOfView(fOV, width / height, zNear, zFar);
    }
}
