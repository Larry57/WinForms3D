using System;
using System.Numerics;

namespace WinForms3D {

    // Buggy

    public class FlyCam : ICamera {
        private Quaternion rotation = Quaternion.Identity;
        private Vector3 position;

        public event EventHandler CameraChanged;

        public Matrix4x4 ViewMatrix() => Matrix4x4.CreateTranslation(position) * Matrix4x4.CreateFromQuaternion(rotation);

        public Vector3 Position {
            get => position;
            set {
                if(position == value)
                    return;

                position = value;

                CameraChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public Quaternion Rotation {
            get => rotation;
            set {
                if(rotation == value)
                    return;

                rotation = value;

                CameraChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public override string ToString() {
            return $"Camera: P: {Position},R: {Rotation}";
        }
    }
}