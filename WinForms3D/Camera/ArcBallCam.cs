using System.Numerics;
using System;

namespace WinForms3D {

    // Adapted from https://github.com/dwmkerr/sharpgl/blob/master/source/SharpGL/Core/SharpGL.SceneGraph/Core/ArcBall.cs

    public class ArcBallCam : ICamera {

        Vector3 position;
        Quaternion rotation = Quaternion.Identity;

        public float Radius = .3f;

        float radius_squared { get => Radius * Radius; }

        public event EventHandler CameraChanged;

        public Matrix4x4 ViewMatrix() => Matrix4x4.CreateFromQuaternion(rotation) * Matrix4x4.CreateTranslation(position);

        public void SetPosition(Vector3 position, Quaternion rotation) {
            if(PropertyChangedHelper.ChangeValue(ref this.rotation, rotation) || PropertyChangedHelper.ChangeValue(ref this.position, position)) {
                CameraChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public Quaternion Rotation {
            get => rotation;
            set {
                if(PropertyChangedHelper.ChangeValue(ref rotation, value))
                    CameraChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public Vector3 Position {
            get => position;
            set {
                if(PropertyChangedHelper.ChangeValue(ref position, value))
                    CameraChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        // Hyperboloid mapping taken from https://www.opengl.org/wiki/Object_Mouse_Trackball

        internal Vector3 MapToSphere(Vector2 v) {

            var P = new Vector3(v.X, -v.Y, 0);

            var XY_squared = P.LengthSquared();

            if(XY_squared <= .5f * radius_squared)
                P.Z = (float)Math.Sqrt(radius_squared - XY_squared);  // Pythagore
            else
                P.Z = 0.5f * radius_squared / (float)Math.Sqrt(XY_squared);  // Hyperboloid

            return Vector3.Normalize(P);
        }

        internal Quaternion CalculateQuaternion(Vector3 startV, Vector3 currentV) {

            var cross = Vector3.Cross(startV, currentV);

            if(cross.Length() > MathUtils.Epsilon)
                return new Quaternion(cross, Vector3.Dot(startV, currentV));
            else
                return Quaternion.Identity;
        }
    }
}