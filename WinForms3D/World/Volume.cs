using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace WinForms3D {
    public class Volume : IVolume {
        public Volume(Vector3[] vertices, TriangleIndices[] triangleIndices, Vector3[] vertexNormals = null, ColorRGB[] triangleColors = null) {
            Vertices = vertices;
            TriangleIndices = triangleIndices;

            // TriangleNormals = triangleNormals ?? this.CalculateTriangleNormals().ToArray();
            NormVertices = vertexNormals ?? this.CalculateVertexNormals().ToArray();
            TriangleColors = triangleColors ?? Enumerable.Repeat(ColorRGB.Gray, TriangleIndices.Length).ToArray();

            Scale = Vector3.One;
        }

        public Vector3[] Vertices { get; }

        public ColorRGB[] TriangleColors { get; }

        public TriangleIndices[] TriangleIndices { get; }

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
