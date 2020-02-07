using System.Collections.Generic;
using System.Numerics;

namespace WinForms3D {
    public interface IVolume {
        Rotation3D Rotation { get; set; }
        Vector3 Position { get; set; }
        Vector3 Scale { get; }
        
        ColorRGB[] TriangleColors { get; }
        TriangleIndices[] TriangleIndices { get; }
        Vector3[] Vertices { get; }
        Vector3[] NormVertices { get; }

        Matrix4x4 WorldMatrix();
    }
}
