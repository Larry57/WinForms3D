using System;
using System.Collections.Generic;
using System.Numerics;

namespace WinForms3D {
    public class IcoSphere : Volume {

        IcoSphere(sphere sphere) : base(sphere.points.ToArray(), sphere.faces.ToArray()) {
        }

        public IcoSphere(int recursionLevel) : this(new sphere(recursionLevel)) {
        }

        class sphere {
            public sphere(int recursionLevel) {
                create(recursionLevel);
            }

            // return index of point in the middle of p1 and p2
            int getMiddlePoint(int p1, int p2) {

                // first check if we have it already
                var firstIsSmaller = p1 < p2;
                long smallerIndex = firstIsSmaller ? p1 : p2;
                long greaterIndex = firstIsSmaller ? p2 : p1;
                var key = (smallerIndex << 32) + greaterIndex;

                if(middlePointIndexCache.TryGetValue(key, out var ret)) {
                    return ret;
                }

                // not in cache, calculate it
                var point1 = points[p1];
                var point2 = points[p2];
                var middle = new Vector3(
                    (point1.X + point2.X) / 2f,
                    (point1.Y + point2.Y) / 2f,
                    (point1.Z + point2.Z) / 2f);

                // add vertex makes sure point is on unit sphere
                var i = addVertex(middle);

                // store it, return index
                middlePointIndexCache.Add(key, i);
                return i;
            }

            int index;

            Dictionary<long, int> middlePointIndexCache;
            public List<Vector3> points { get;  } = new List<Vector3>();
            public List<Triangle> faces { get; private set; } = new List<Triangle>();

            int addVertex(Vector3 p) {
                points.Add(Vector3.Normalize(p));
                return index++;
            }

            void create(int recursionLevel) {
                middlePointIndexCache = new Dictionary<long, int>();
                index = 0;

                // create 12 vertices of a icosahedron
                var t = (1f + (float)Math.Sqrt(5f)) / 2f;

                addVertex(new Vector3(-1, t, 0));
                addVertex(new Vector3(1, t, 0));
                addVertex(new Vector3(-1, -t, 0));
                addVertex(new Vector3(1, -t, 0));

                addVertex(new Vector3(0, -1, t));
                addVertex(new Vector3(0, 1, t));
                addVertex(new Vector3(0, -1, -t));
                addVertex(new Vector3(0, 1, -t));

                addVertex(new Vector3(t, 0, -1));
                addVertex(new Vector3(t, 0, 1));
                addVertex(new Vector3(-t, 0, -1));
                addVertex(new Vector3(-t, 0, 1));


                // create 20 triangles of the icosahedron
                faces = new List<Triangle>();
                
                // 5 faces around point 0
                faces.Add(new Triangle(0, 11, 5));
                faces.Add(new Triangle(0, 5, 1));
                faces.Add(new Triangle(0, 1, 7));
                faces.Add(new Triangle(0, 7, 10));
                faces.Add(new Triangle(0, 10, 11));

                // 5 adjacent faces 
                faces.Add(new Triangle(1, 5, 9));
                faces.Add(new Triangle(5, 11, 4));
                faces.Add(new Triangle(11, 10, 2));
                faces.Add(new Triangle(10, 7, 6));
                faces.Add(new Triangle(7, 1, 8));

                // 5 faces around point 3
                faces.Add(new Triangle(3, 9, 4));
                faces.Add(new Triangle(3, 4, 2));
                faces.Add(new Triangle(3, 2, 6));
                faces.Add(new Triangle(3, 6, 8));
                faces.Add(new Triangle(3, 8, 9));

                // 5 adjacent faces 
                faces.Add(new Triangle(4, 9, 5));
                faces.Add(new Triangle(2, 4, 11));
                faces.Add(new Triangle(6, 2, 10));
                faces.Add(new Triangle(8, 6, 7));
                faces.Add(new Triangle(9, 8, 1));


                // refine triangles
                for(var r = 0; r < recursionLevel; r++) {
                    var faces2 = new List<Triangle>();
                    foreach(var tri in faces) {
                        // replace triangle by 4 triangles
                        var a = getMiddlePoint(tri.I0, tri.I1);
                        var b = getMiddlePoint(tri.I1, tri.I2);
                        var c = getMiddlePoint(tri.I2, tri.I0);

                        faces2.Add(new Triangle(tri.I0, a, c));
                        faces2.Add(new Triangle(tri.I1, b, a));
                        faces2.Add(new Triangle(tri.I2, c, b));
                        faces2.Add(new Triangle(a, b, c));
                    }

                    faces = faces2;
                }
            }
        }
    }
}