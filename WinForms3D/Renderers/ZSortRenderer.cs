using System;
using System.Collections.Generic;
using System.Numerics;

namespace WinForms3D {

    // Renderer that sort volume by Z to see if drawing near object first increases performances by early Z buffer elimination
    // Well... Not that much

    public class ZSortRenderer : IRenderer {
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

        private static Comparer<VertexBuffer> zComparer = Comparer<VertexBuffer>.Create((v1, v2) => v2.Z.CompareTo(v1.Z));

        public ZSortRenderer() {
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

            var vc = world.Volumes.Count;
            using var worldBuffer = new WorldBuffer(world);

            renderContext.WorldBuffer = worldBuffer;

            // Sort volumes from the nearest 0 to the farest -Z
            var volumes = world.Volumes;

            for(var i = 0; i < vc; i++) {
                var volume = volumes[i];

                var worldMatrix = volume.WorldMatrix();
                var model2View = worldMatrix * viewMatrix;

                var vbx = worldBuffer.VertexBuffers[i];
                vbx.WorldMatrix = worldMatrix;
                vbx.Model2ViewMatrix = model2View;
                vbx.Z = Vector3.Transform(volume.Position, viewMatrix).Z;
            }

            // VertexBuffers array might be longer than vc
            Array.Sort(worldBuffer.VertexBuffers, 0, vc, zComparer);

            for(var iv = 0; iv < vc; iv++) {
                var volume = volumes[iv];
                var vbx = worldBuffer.VertexBuffers[iv];

                var worldMatrix = vbx.WorldMatrix;
                var model2View = vbx.Model2ViewMatrix;

                // Transform volume vertices from model xyz to view xyz

                var nv = volume.Vertices.Length;
                for(var i = 0; i < nv; i++) {
                    vbx.ViewVertices[i] = Vector3.Transform(volume.Vertices[i], model2View);
                }

                stats.TotalTriangleCount += volume.TriangleIndices.Length;

                var nt = volume.TriangleIndices.Length;
                for(var triangleIndice = 0; triangleIndice < nt; triangleIndice++) {
                    var t = volume.TriangleIndices[triangleIndice];

                    var viewP1 = vbx.ViewVertices[t.I1];
                    var viewP2 = vbx.ViewVertices[t.I2];
                    var viewP3 = vbx.ViewVertices[t.I3];

                    if(RenderUtils.isTriangleBehindFarPlane(viewP1, viewP2, viewP3)) {
                        stats.BehindViewTriangleCount++;
                        continue;
                    }

                    // Discard back facing triangle

                    if(rendererSettings.BackFaceCulling && RenderUtils.isTriangleFacingBack(viewP1, viewP2, viewP3)) {
                        stats.FacingBackTriangleCount++;
                        continue;
                    }

                    // Project visible vertices

                    foreach(var j in new[] { t.I1, t.I2, t.I3 }) {
                        if(vbx.ProjectionVertices[j] == Vector4.Zero)
                            vbx.ProjectionVertices[j] = Vector4.Transform(vbx.ViewVertices[j], projectionMatrix);
                    }

                    var projectionP1 = vbx.ProjectionVertices[t.I1];
                    var projectionP2 = vbx.ProjectionVertices[t.I2];
                    var projectionP3 = vbx.ProjectionVertices[t.I3];

                    if(RenderUtils.isTriangleOutsideFrustum(projectionP1, projectionP2, projectionP3)) {
                        stats.OutOfViewTriangleCount++;
                        continue;
                    }

                    foreach(var j in new[] { t.I1, t.I2, t.I3 }) {
                        if(vbx.WorldNormVertices[j] == Vector3.Zero)
                            vbx.WorldNormVertices[j] = Vector3.TransformNormal(volume.NormVertices[j], worldMatrix);

                        if(vbx.WorldVertices[j] == Vector3.Zero)
                            vbx.WorldVertices[j] = Vector3.Transform(volume.Vertices[j], worldMatrix);
                    }

                    stats.PaintTime();

                    var color = volume.TriangleColors[triangleIndice];

                    if(rendererSettings.ShowTriangles)
                        wireFramePainter.DrawTriangle(ColorRGB.Magenta, vbx, volume, triangleIndice);

                    if(rendererSettings.ShowTriangleNormals) {
                        var worldCentroid = t.CalculateCentroid(vbx.WorldVertices);

                        var startPoint = Vector4.Transform(worldCentroid, world2Projection);
                        var endPoint = Vector4.Transform(worldCentroid + t.CalculateNormal(vbx.WorldVertices), world2Projection);

                        wireFramePainter.DrawLine(surface, ColorRGB.Red, startPoint, endPoint);
                    }

                    Painter?.DrawTriangle(color, vbx, volume, triangleIndice);

                    stats.DrawnTriangleCount++;

                    stats.CalcTime();
                }
            }

            // drawLine(world2Projection, new Vector3(-20, 0, 0), new Vector3(20, 0, 0), Color4.Red);
            // drawAxes(world2Projection);

            if(rendererSettings.ShowXZGrid)
                RenderUtils.drawGrid(surface, wireFramePainter, world2Projection, -10, 10);

            if(rendererSettings.ShowAxes)
                RenderUtils.drawAxes(surface, wireFramePainter, world2Projection);

            return surface.Screen;
        }
    }
}