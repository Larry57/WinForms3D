using System;
using System.Collections.Generic;
using System.Numerics;

namespace WinForms3D {

    public class SimpleRenderer : IRenderer {
        private RenderContext renderContext;

        public RenderContext RenderContext {
            get => renderContext;
            set {
                renderContext = value;
                wireFramePainter.RendererContext = value;
            }
        }

        public IPainter Painter { get; set; }

        private WireFramePainter wireFramePainter;

        public SimpleRenderer() {
            wireFramePainter = new WireFramePainter();
        }

        public int[] Render() {
            var stats = RenderContext.Stats;
            var surface = RenderContext.Surface;
            var camera = RenderContext.Camera;
            var projection = RenderContext.Projection;
            var world = RenderContext.World;
            var rendererSettings = RenderContext.RendererSettings;

            stats.Clear();

            stats.PaintTime();
            surface.Clear();

            stats.CalcTime();

            // model => worldMatrix => world => viewMatrix => view => projectionMatrix => projection => toNdc => ndc => toScreen => screen

            var viewMatrix = camera.ViewMatrix();
            var projectionMatrix = projection.ProjectionMatrix(surface.Width, surface.Height);
            var world2Projection = viewMatrix * projectionMatrix;

            // Allocate arrays to store transformed vertices
            using var worldBuffer = new WorldBuffer(world);
            renderContext.WorldBuffer = worldBuffer;

            var volumes = world.Volumes;
            var volumeCount = volumes.Count;
            for(var idxVolume = 0; idxVolume < volumeCount; idxVolume++) {

                var vbx = worldBuffer.VertexBuffer[idxVolume];
                var volume = volumes[idxVolume];

                var worldMatrix = volume.WorldMatrix();
                var modelViewMatrix = worldMatrix * viewMatrix;

                vbx.Volume = volume;
                vbx.WorldMatrix = worldMatrix;

                stats.TotalTriangleCount += volume.Triangles.Length;

                var vertices = volume.Vertices;
                var viewVertices = vbx.ViewVertices;

                // Transform and store vertices to View
                var vertexCount = vertices.Length;
                for(var idxVertex = 0; idxVertex < vertexCount; idxVertex++) {
                    viewVertices[idxVertex] = Vector3.Transform(vertices[idxVertex], modelViewMatrix);
                }

                var triangleCount = volume.Triangles.Length;
                for(var idxTriangle = 0; idxTriangle < triangleCount; idxTriangle++) {
                    var t = volume.Triangles[idxTriangle];

                    // Discard if behind far plane
                    if(t.IsBehindFarPlane(vbx)) {
                        stats.BehindViewTriangleCount++;
                        continue;
                    }

                    // Discard if back facing 
                    if(rendererSettings.BackFaceCulling && t.IsFacingBack(vbx)) {
                        stats.FacingBackTriangleCount++;
                        continue;
                    }

                    // Project in frustum
                    t.TransformProjection(vbx, projectionMatrix);

                    // Discard if outside view frustum
                    if(t.isOutsideFrustum(vbx)) {
                        stats.OutOfViewTriangleCount++;
                        continue;
                    }

                    stats.PaintTime();

                    var color = volume.TriangleColors[idxTriangle];

                    if(rendererSettings.ShowTriangles)
                        wireFramePainter.DrawTriangle(ColorRGB.Magenta, vbx, idxTriangle);

                    if(rendererSettings.ShowTriangleNormals) {
                        var worldCentroid = t.CalculateCentroid(vbx.WorldVertices);

                        var startPoint = Vector4.Transform(worldCentroid, world2Projection);
                        var endPoint = Vector4.Transform(worldCentroid + t.CalculateNormal(vbx.WorldVertices), world2Projection);

                        wireFramePainter.DrawLine(surface, ColorRGB.Red, startPoint, endPoint);
                    }

                    Painter?.DrawTriangle(color, vbx, idxTriangle);

                    stats.DrawnTriangleCount++;

                    stats.CalcTime();
                }
            }

            if(rendererSettings.ShowXZGrid)
                RenderUtils.drawGrid(surface, wireFramePainter, world2Projection, -10, 10);

            if(rendererSettings.ShowAxes)
                RenderUtils.drawAxes(surface, wireFramePainter, world2Projection);

            return surface.Screen;
        }
    }
}