using System.Numerics;
using System.Runtime.CompilerServices;

namespace WinForms3D {
    public struct Triangle {
        public Triangle(int p0, int p1, int p2) {
            I0 = p0;
            I1 = p1;
            I2 = p2;
        }

        public int I0 { get; }
        public int I1 { get; }
        public int I2 { get; }

        public Vector3 CalculateCentroid(Vector3[] vertices) => (vertices[I0] + vertices[I1] + vertices[I2]) / 3;

        public Vector3 CalculateNormal(Vector3[] vertices) => Vector3.Normalize(Vector3.Cross(vertices[I1] - vertices[I0], vertices[I2] - vertices[I0]));

        public bool Contains(Vector3 vertex, Vector3[] vertices) => vertices[I0] == vertex || vertices[I1] == vertex || vertices[I2] == vertex;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsBehindFarPlane(VertexBuffer vbx) {
            var viewVertices = vbx.ViewVertices;
            return viewVertices[I0].Z > 0 && viewVertices[I1].Z > 0 && viewVertices[I2].Z > 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsFacingBack(VertexBuffer vbx) {
            var viewVertices = vbx.ViewVertices;
            var p0 = viewVertices[I0]; var p1 = viewVertices[I1]; var p2 = viewVertices[I2];
 
            var vCentroid = Vector3.Normalize((p0 + p1 + p2) / 3);
            var vNormal = Vector3.Normalize(Vector3.Cross(p1 - p0, p2 - p0));
            
            return Vector3.Dot(vCentroid, vNormal) >= 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool isOutsideFrustum(VertexBuffer vbx) {
            var projectionVertices = vbx.ProjectionVertices;
            var p0 = projectionVertices[I0]; var p1 = projectionVertices[I1]; var p2 = projectionVertices[I2];

            if(p0.W < 0 || p1.W < 0 || p2.W < 0)
                return true;

            if(p0.X < -p0.W && p1.X < -p1.W && p2.X < -p2.W)
                return true;

            if(p0.X > p0.W && p1.X > p1.W && p2.X > p2.W)
                return true;

            if(p0.Y < -p0.W && p1.Y < -p1.W && p2.Y < -p2.W)
                return true;

            if(p0.Y > p0.W && p1.Y > p1.W && p2.Y > p2.W)
                return true;

            if(p0.Z > p0.W && p1.Z > p1.W && p2.Z > p2.W)
                return true;

            // This last one is normally not necessary when a IsTriangleBehind check is done
            if(p0.Z < 0 && p1.Z < 0 && p2.Z < 0)
                return true;

            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void TransformProjection(VertexBuffer vbx, Matrix4x4 projectionMatrix) {
            var projectionVertices = vbx.ProjectionVertices;
            var viewVertices = vbx.ViewVertices;

            if(projectionVertices[I0] == Vector4.Zero)
                projectionVertices[I0] = Vector4.Transform(viewVertices[I0], projectionMatrix);

            if(projectionVertices[I1] == Vector4.Zero)
                projectionVertices[I1] = Vector4.Transform(viewVertices[I1], projectionMatrix);

            if(projectionVertices[I2] == Vector4.Zero)
                projectionVertices[I2] = Vector4.Transform(viewVertices[I2], projectionMatrix);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void TransformWorld(VertexBuffer vbx) {
            var worldMatrix = vbx.WorldMatrix;

            var worldNormVertices = vbx.WorldNormVertices;
            var normVertices = vbx.Volume.NormVertices;

            if(worldNormVertices[I0] == Vector3.Zero)
                worldNormVertices[I0] = Vector3.TransformNormal(normVertices[I0], worldMatrix);

            if(worldNormVertices[I1] == Vector3.Zero)
                worldNormVertices[I1] = Vector3.TransformNormal(normVertices[I1], worldMatrix);

            if(worldNormVertices[I2] == Vector3.Zero)
                worldNormVertices[I2] = Vector3.TransformNormal(normVertices[I2], worldMatrix);

            var worldVertices = vbx.WorldVertices;
            if(worldVertices[I0] == Vector3.Zero)
                worldVertices[I0] = Vector3.Transform(worldVertices[I0], worldMatrix);

            if(worldVertices[I1] == Vector3.Zero)
                worldVertices[I1] = Vector3.Transform(worldVertices[I1], worldMatrix);

            if(worldVertices[I2] == Vector3.Zero)
                worldVertices[I2] = Vector3.Transform(worldVertices[I2], worldMatrix);
        }
    }
}