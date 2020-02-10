using System;
using System.Numerics;

namespace WinForms3D {
    public interface ICamera {
        event EventHandler CameraChanged;
        Matrix4x4 ViewMatrix();
    }
}