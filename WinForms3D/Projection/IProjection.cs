using System;
using System.Numerics;

namespace WinForms3D {
    public interface IProjection {
        event EventHandler ProjectionChanged;
        Matrix4x4 ProjectionMatrix(float w, float h);
    }
}