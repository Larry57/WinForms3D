using System.Numerics;

namespace WinForms3D {
    public struct TriangleIndices {
        public TriangleIndices(int p1, int p2, int p3) {
            I1 = p1;
            I2 = p2;
            I3 = p3;
        }

        public int I1 { get; }
        public int I2 { get; }
        public int I3 { get; }

        public Vector3 CalculateCentroid(Vector3[] vertices) => (vertices[I1] + vertices[I2] + vertices[I3]) / 3;

        public Vector3 CalculateNormal(Vector3[] vertices) => Vector3.Normalize(Vector3.Cross(vertices[I2] - vertices[I1], vertices[I3] - vertices[I1]));

        public bool Contains(Vector3 vertex, Vector3[] vertices) => vertices[I1] == vertex || vertices[I2] == vertex || vertices[I3] == vertex;
    }
}