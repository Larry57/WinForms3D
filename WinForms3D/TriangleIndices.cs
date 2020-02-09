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

        public bool IsBehindFarPlane(Vector3[] viewVertices) {
            return viewVertices[I1].Z > 0 && viewVertices[I2].Z > 0 && viewVertices[I3].Z > 0;
        }

        public bool IsFacingBack(Vector3[] viewVertices) {
            var p1 = viewVertices[I1]; var p2 = viewVertices[I2]; var p3 = viewVertices[I3];
            var vCentroid = Vector3.Normalize((p1 + p2 + p3) / 3);
            var vNormal = Vector3.Normalize(Vector3.Cross(p2 - p1, p3 - p1));
            return Vector3.Dot(vCentroid, vNormal) >= 0;
        }

        public bool isOutsideFrustum(Vector4[] projectionVertices) {
            var projectionP1 = projectionVertices[I1]; var projectionP2 = projectionVertices[I2]; var projectionP3 = projectionVertices[I3];

            if(projectionP1.W < 0 || projectionP2.W < 0 || projectionP3.W < 0)
                return true;

            if(projectionP1.X < -projectionP1.W && projectionP2.X < -projectionP2.W && projectionP3.X < -projectionP3.W)
                return true;

            if(projectionP1.X > projectionP1.W && projectionP2.X > projectionP2.W && projectionP3.X > projectionP3.W)
                return true;

            if(projectionP1.Y < -projectionP1.W && projectionP2.Y < -projectionP2.W && projectionP3.Y < -projectionP3.W)
                return true;

            if(projectionP1.Y > projectionP1.W && projectionP2.Y > projectionP2.W && projectionP3.Y > projectionP3.W)
                return true;

            if(projectionP1.Z > projectionP1.W && projectionP2.Z > projectionP2.W && projectionP3.Z > projectionP3.W)
                return true;

            // This last one is normally not necessary when a IsTriangleBehind check is done
            if(projectionP1.Z < 0 && projectionP2.Z < 0 && projectionP3.Z < 0)
                return true;

            return false;
        }
    }
}