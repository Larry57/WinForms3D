using System.Linq;
using System.Numerics;

namespace WinForms3D {
    public class Volume : IVolume {
        public Volume(Vector3[] vertices, Triangle[] triangleIndices, Vector3[] vertexNormals = null, ColorRGB[] triangleColors = null) {
            Vertices = vertices;
            Triangles = triangleIndices;

            NormVertices = vertexNormals ?? this.CalculateVertexNormals().ToArray();
            TriangleColors = triangleColors ?? Enumerable.Repeat(ColorRGB.Gray, Triangles.Length).ToArray();

            Scale = Vector3.One;
        }

        public Vector3[] Vertices { get; }

        public ColorRGB[] TriangleColors { get; }

        public Triangle[] Triangles { get; }

        public Vector3 Centroid { get; }

        public Rotation3D Rotation { get; set; }

        public Vector3 Position { get; set; }

        public Vector3 Scale { get; set; }

        public Vector3[] NormVertices { get; set; }

        public Matrix4x4 WorldMatrix() =>
            Matrix4x4.CreateFromYawPitchRoll(Rotation.YYaw, Rotation.XPitch, Rotation.ZRoll) *
            Matrix4x4.CreateTranslation(Position) *
            Matrix4x4.CreateScale(Scale);
    }
}
