using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Numerics;
using System.Xml.Linq;

namespace WinForms3D {
    public class VolumeFactory {

        // Collada import is a brittle hack and need a serious work

        public static IEnumerable<Volume> ImportCollada(string fileName) {
            var ns = "http://www.collada.org/2005/11/COLLADASchema";
            var xdoc = XDocument.Load(fileName);
            var xvolumes = xdoc.Descendants(XName.Get("geometry", ns));
            foreach(var xvolume in xvolumes) {
                var xid = xvolume.Attribute("id").Value;

                string svertices = xvolume.Descendants().First(e => e.Attribute("id")?.Value == $"{xid}-positions-array").Value;
                var vertices = svertices.Split(" ".ToArray(), StringSplitOptions.RemoveEmptyEntries).Select(x => float.Parse(x, CultureInfo.InvariantCulture)).ToArray().BuildVector3s().ToArray();

                string snormals = xvolume.Descendants().First(e => e.Attribute("id")?.Value == $"{xid}-normals-array").Value;
                var normals = snormals.Split(" ".ToArray(), StringSplitOptions.RemoveEmptyEntries).Select(x => float.Parse(x, CultureInfo.InvariantCulture)).ToArray().BuildVector3s().ToArray();

                string striangles = xvolume.Descendants(XName.Get("p", ns)).First().Value;
                var triangles = striangles.Split(" ".ToArray(), StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x, CultureInfo.InvariantCulture)).ToArray().BuildTriangleIndices().ToArray();

                yield return new Volume(vertices, triangles, normals, null);
            }
        }

        // Work in progress

        public static IEnumerable<Volume> NewImportCollada(string fileName) {
            XNamespace ns = "http://www.collada.org/2005/11/COLLADASchema";

            var xdoc = XDocument.Load(fileName);

            var geometries = xdoc.Root.Element(ns + "library_geometries").Elements(ns + "geometry");

            foreach(var geometry in geometries) {
                var mesh = geometry.Element(ns + "mesh");

                var polylist = mesh.Element(ns + "polylist");
                if(polylist != null) {
                    var polylist_vcount = parseArray<int>(polylist.Element(ns + "vcount")?.Value);

                    if(!polylist_vcount.Any() || polylist_vcount.Distinct().Any(v => v != 3))
                        throw new Exception();

                    var polylist_p = parseArray<int>(polylist.Element(ns + "p")?.Value);
                    getSource(polylist, "VERTEX", out var polylist_vertex_id, out _);

                    var vertices = mesh.Elements(ns + "vertices").FirstOrDefault(e => e.Attribute("id")?.Value == polylist_vertex_id);
                    getSource(vertices, "POSITION", out var vertices_position_id, out _);
                    getSource(vertices, "NORMAL", out var vertices_normal_id, out _);

                    var vertices_position = getArraySource<Vector3>(mesh, vertices_position_id);
                    var vertices_normal = getArraySource<Vector3>(mesh, vertices_normal_id);

                    yield return new Volume(
                        vertices_position.ToArray(),
                        polylist_p.ToArray().BuildTriangleIndices().ToArray(),
                        vertices_normal.ToArray(),
                        null);
                }

                var triangles = mesh.Element(ns + "triangles");
                if(triangles != null) {
                    var triangles_count = int.Parse(triangles.Attribute("count").Value);
                    var triangles_p = parseArray<int>(triangles.Element(ns + "p")?.Value).ToArray();

                    var stride = triangles_p.Count() / triangles_count;

                    getSource(triangles, "VERTEX", out var triangles_vertex_id, out var triangle_vertex_offset);
                    // getSource(triangles, "NORMAL", out var triangles_normal_id, out _);

                    var vertices = mesh.Elements(ns + "vertices").FirstOrDefault(e => e.Attribute("id")?.Value == triangles_vertex_id);
                    getSource(vertices, "POSITION", out var vertices_position_id, out _);

                    var vertices_position = getArraySource<Vector3>(mesh, vertices_position_id);
                    // var triangles_normal = getArraySource<Vector3>(mesh, triangles_normal_id);

                    yield return new Volume(
                        vertices_position.ToArray(),
                        getTriangles(triangles_p, stride).ToArray(),
                        null, // triangles_normal.ToArray(),
                        null);
                }
            }
        }

        static IEnumerable<Triangle> getTriangles(int[] array, int stride, int offset = 0) {
            var l = array.Length / stride;
            for(var i = 0; i < l; i++)
                yield return new Triangle(array[i * stride + offset], array[i * stride + 4 + offset], array[i * stride + 8 + offset]);
        }

        static IEnumerable<T> parseArray<T>(string value) {
            return value?.Split(new[] { ' ', '\n' }, StringSplitOptions.RemoveEmptyEntries)?.Select(v => (T)Convert.ChangeType(v, typeof(T), CultureInfo.InvariantCulture)) ?? Enumerable.Empty<T>();
        }

        static void getSource(XElement element, string semantic, out string id, out int offset) {
            var e = element?.Elements(element?.GetDefaultNamespace() + "input")?.FirstOrDefault(e => string.Equals(e.Attribute("semantic")?.Value, semantic));
            id = e?.Attribute("source")?.Value?.TrimStart('#');
            offset = int.Parse(e?.Attribute("offset")?.Value ?? "0");
        }

        static IEnumerable<T> getArraySource<T>(XElement mesh, string id) {
            var ns = mesh.GetDefaultNamespace();

            var data = mesh
                .Elements(ns + "source")
                .FirstOrDefault(e => e.Attribute("id").Value == id)
                .Element(ns + "float_array")
                .Value;

            var floats = parseArray<float>(data).ToArray();

            if(typeof(T) == typeof(Vector3)) {
                for(var i = 0; i < floats.Length; i += 3)
                    yield return (T)(object)new Vector3(floats[i], floats[i + 1], floats[i + 2]);
            }
        }
    }
}
