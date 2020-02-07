using System;
using System.Buffers;
using System.Numerics;

namespace WinForms3D {

    public class WorldBuffer : IDisposable {
        private static ArrayPool<VertexBuffer> vertexBuffer3bag = ArrayPool<VertexBuffer>.Shared;

        public VertexBuffer[] VertexBuffers { get; }

        public WorldBuffer(IWorld w) {
            var nv = w.Volumes.Count;
            VertexBuffers = vertexBuffer3bag.Rent(w.Volumes.Count);

            for(var i = 0; i < nv; i++) {
                VertexBuffers[i] = new VertexBuffer(w.Volumes[i].Vertices.Length);
            }
        }

        public void Dispose() {
            var nv = VertexBuffers.Length;
            for(var i = 0; i < nv; i++) {
                VertexBuffers[i]?.Dispose();
            }
            vertexBuffer3bag.Return(VertexBuffers, true);
        }
    }

    public class VertexBuffer : IDisposable {
        private static ArrayPool<Vector3> vector3bag = ArrayPool<Vector3>.Shared;
        private static ArrayPool<Vector4> vector4bag = ArrayPool<Vector4>.Shared;

        public Vector3[] ViewVertices;
        public Vector3[] WorldVertices;
        public Vector3[] WorldNormVertices;
        public Vector4[] ProjectionVertices;
        public Matrix4x4 WorldMatrix;
        public Matrix4x4 Model2ViewMatrix;
        public float Z;

        public VertexBuffer(int vertexCount) {
            ViewVertices = vector3bag.Rent(vertexCount);
            WorldVertices = vector3bag.Rent(vertexCount);
            WorldNormVertices = vector3bag.Rent(vertexCount);
            ProjectionVertices = vector4bag.Rent(vertexCount);
        }

        public void Dispose() {
            vector3bag.Return(ViewVertices, true);
            vector3bag.Return(WorldVertices, true);
            vector3bag.Return(WorldNormVertices, true);
            vector4bag.Return(ProjectionVertices, true);
        }
    }
}