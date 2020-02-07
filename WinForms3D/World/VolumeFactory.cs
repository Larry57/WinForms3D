using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
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
            var ns = "http://www.collada.org/2005/11/COLLADASchema";
            var xdoc = XDocument.Load(fileName);

            var library_geometries = xdoc.Element("library_geometries");

            var meshes = library_geometries.Descendants(XName.Get("geometry", ns));
            foreach(var mesh in meshes) {

                var verticesNode = mesh.Descendants(XName.Get("vertices", ns));
                var inputNodes = verticesNode.Descendants(XName.Get("input", ns));

                var positionsId = inputNodes.FirstOrDefault(n => n.Attribute("semantic").Value == "POSITION")?.Attribute("source")?.Value;
                var normalsId = inputNodes.FirstOrDefault(n => n.Attribute("semantic").Value == "NORMAL")?.Attribute("source")?.Value;

                var xid = mesh.Attribute("id").Value;

                string svertices = mesh.Descendants().First(e => e.Attribute("id")?.Value == $"{xid}-positions-array").Value;
                var vertices = svertices.Split(" ".ToArray(), StringSplitOptions.RemoveEmptyEntries).Select(x => float.Parse(x, CultureInfo.InvariantCulture)).ToArray().BuildVector3s().ToArray();

                string snormals = mesh.Descendants().First(e => e.Attribute("id")?.Value == $"{xid}-normals-array").Value;
                var normals = snormals.Split(" ".ToArray(), StringSplitOptions.RemoveEmptyEntries).Select(x => float.Parse(x, CultureInfo.InvariantCulture)).ToArray().BuildVector3s().ToArray();
                
                string striangles = mesh.Descendants(XName.Get("p", ns)).First().Value;
                var triangles = striangles.Split(" ".ToArray(), StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x, CultureInfo.InvariantCulture)).ToArray().BuildTriangleIndices().ToArray();

                yield return new Volume(vertices, triangles, normals, null);
            }
        }
    }
}
