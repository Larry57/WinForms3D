
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;

namespace WinForms3D.Tests {
    public class RayTracer {
        int screenWidth;
        int screenHeight;
        const int MaxDepth = 5;

        public RayTracer(int screenWidth, int screenHeight) {
            this.screenWidth = screenWidth;
            this.screenHeight = screenHeight;
        }

        static IEnumerable<Intersection> Intersections(Ray ray, Scene scene) {
            return scene.Things
                .Select(obj => { obj.Intersect(ray, out var inter); return inter; })
                .Where(inter => !inter.IsNone)
                .OrderBy(inter => inter.Dist);
        }

        static float TestRay(Ray ray, Scene scene) {
            var isect = Intersections(ray, scene).DefaultIfEmpty(Intersection.None).First();
            return isect.Dist;
        }

        static Color4 Background = new Color4(0, 0, 0);
        static Color4 DefaultColor = new Color4(0, 0, 0);

        static Color4 TraceRay(Ray ray, Scene scene, int depth) {
            var isect = Intersections(ray, scene).DefaultIfEmpty(Intersection.None).First();
            if(isect.IsNone)
                return Background;
            else
                return Shade(isect, scene, depth);
        }

        static Color4 GetNaturalColor(SceneObject thing, Vector3 pos, Vector3 norm, Vector3 rd, Scene scene) {
            var ret = new Color4(0, 0, 0);

            foreach(var light in scene.Lights) {
                var ldis = light.Pos - pos;
                var livec = Vector3.Normalize(ldis);

                var neatIsect = TestRay(new Ray(pos, livec), scene);

                var notInShadow = (neatIsect > ldis.Length() || neatIsect == 0);
                if(notInShadow) {
                    float illum = Vector3.Dot(livec, norm);
                    float specular = Vector3.Dot(livec, Vector3.Normalize(rd));

                    ret += thing.Surface.Diffuse(pos) * (illum > 0 ? illum * light.Color : new Color4(0, 0, 0)) +
                           thing.Surface.Specular(pos) * (specular > 0 ? (float)Math.Pow(specular, thing.Surface.Roughness) * light.Color : new Color4(0, 0, 0));
                }
            }
            return ret;
        }

        static Color4 GetReflectionColor(SceneObject thing, Vector3 pos, Vector3 rd, Scene scene, int depth) {
            return thing.Surface.Reflect(pos) * TraceRay(new Ray(pos, rd), scene, depth + 1);
        }

        static Color4 Shade(Intersection isect, Scene scene, int depth) {
            var d = isect.Ray.Dir;
            var pos = isect.Dist * d + isect.Ray.Start;

            var normal = isect.Thing.Normal(pos);
            var reflectDir = d - 2 * Vector3.Dot(normal, d) * normal;
            var ret = DefaultColor + GetNaturalColor(isect.Thing, pos, normal, reflectDir, scene);
            
            if(depth >= MaxDepth)
                return ret + new Color4(.5f, .5f, .5f);
            else
                return ret + GetReflectionColor(isect.Thing, pos + .001f * reflectDir, reflectDir, scene, depth);
        }

        float RecenterX(float x) {
            return (x - (screenWidth / 2f)) / (2f * screenWidth);
        }
        float RecenterY(float y) {
            return -(y - (screenHeight / 2f)) / (2f * screenHeight);
        }

        Vector3 GetPoint(float x, float y, Camera camera) {
            return Vector3.Normalize(camera.Forward + (RecenterX(x) * camera.Right + RecenterY(y) * camera.Up));
        }

        internal byte[] Render(Scene scene) {
            var buffer = new FrameBuffer(this.screenWidth, this.screenHeight, null);
            for(var x = 0; x < screenWidth; x++) {
                for(var y = 0; y < screenHeight; y++) {
                    var r = new Ray(scene.Camera.Pos, GetPoint(x, y, scene.Camera));
                    var c = TraceRay(r, scene, 0);
                    buffer.PutPixel(x, y, 0, c);
                }
            }

            return buffer.Screen;
        }

        internal readonly Scene DefaultScene =
            new Scene() {
                Things = new SceneObject[]
                {
                    new Plane() {
                        Norm = new Vector3(0,1,0),
                        Offset = 0f,
                        Surface = Surfaces.CheckerBoard
                    },
                    new Sphere() {
                        Center = new Vector3(0,1,0),
                        Radius = 1f,
                        Surface = Surfaces.Shiny
                    },
                    new Sphere() {
                        Center = new Vector3(-1,.5f,1.5f),
                        Radius = .5f,
                        Surface = Surfaces.Shiny
                    }
                },
                Lights = new Light[]
                {
                    new Light(
                        new Vector3(-2,2.5f,0),
                        new Color4(.49f,.07f,.07f)
                    ),
                    new Light(
                        new Vector3(1.5f,2.5f,1.5f),
                        new Color4(.07f,.07f,.49f)
                    ),
                    new Light(
                        new Vector3(1.5f,2.5f,-1.5f),
                        new Color4(.07f,.49f,.071f)
                    ),
                    new Light(
                        new Vector3(0,3.5f,0),
                        new Color4(.21f,.21f,.35f)
                    )
                },
                Camera = Camera.Create(
                    new Vector3(3, 2, 4),
                    new Vector3(-1, .5f, 0)
                )
            };
    }

    class Surfaces {
        // Only works with X-Z plane.
        public static readonly Surface CheckerBoard =
            new Surface() {
                Diffuse = pos => ((Math.Floor(pos.Z) + Math.Floor(pos.X)) % 2 != 0) ? new Color4(1, 1, 1) : new Color4(0, 0, 0),
                Specular = pos => new Color4(1, 1, 1),
                Reflect = pos => ((Math.Floor(pos.Z) + Math.Floor(pos.X)) % 2 != 0) ? .1f : .7f,
                Roughness = 150
            };

        public static readonly Surface Shiny =
            new Surface() {
                Diffuse = pos => new Color4(1, 1, 1),
                Specular = pos => new Color4(.5f, .5f, .5f),
                Reflect = pos => .6f,
                Roughness = 50
            };
    }

    struct Ray {
        public Vector3 Start { get; }
        public Vector3 Dir { get; }

        public Ray(Vector3 start, Vector3 dir) {
            Start = start;
            Dir = dir;
        }
    }

    struct Intersection {
        bool exists;

        public SceneObject Thing { get; }
        public Ray Ray { get; }
        public float Dist { get; }
        public bool IsNone { get => !exists; }

        public static Intersection None = new Intersection();

        public Intersection(SceneObject thing, Ray ray, float dist) {
            exists = true;
            Thing = thing;
            Ray = ray;
            Dist = dist;
        }
    }

    class Surface {
        public Func<Vector3, Color4> Diffuse;
        public Func<Vector3, Color4> Specular;
        public Func<Vector3, float> Reflect;

        public float Roughness;
    }

    class Camera {
        public Vector3 Pos;
        public Vector3 Forward;
        public Vector3 Up;
        public Vector3 Right;

        public static Camera Create(Vector3 pos, Vector3 lookAt) {
            var forward = Vector3.Normalize(lookAt - pos);

            var right = 1.5f * Vector3.Normalize(Vector3.Cross(forward, -Vector3.UnitY));
            var up = 1.5f * Vector3.Normalize(Vector3.Cross(forward, right));

            return new Camera() { Pos = pos, Forward = forward, Up = up, Right = right };
        }
    }

    class Sphere : SceneObject {
        public Vector3 Center;
        public float Radius;

        public override bool Intersect(Ray ray, out Intersection intersection) {
            var eo = Center - ray.Start;
            var v = Vector3.Dot(eo, ray.Dir);
            float dist;

            if(v < 0) {
                dist = 0;
            }
            else {
                var disc = Radius * Radius - (Vector3.Dot(eo, eo) - v * v);
                dist = disc < 0 ? 0 : v - (float)Math.Sqrt(disc);
            }

            if(dist == 0) {
                intersection = Intersection.None;
                return false;
            }
            else {
                intersection = new Intersection(this, ray, dist);
                return true;
            }
        }

        public override Vector3 Normal(Vector3 pos) => Vector3.Normalize(pos - Center);
    }

    class Plane : SceneObject {
        public Vector3 Norm;
        public float Offset;

        public override bool Intersect(Ray ray, out Intersection intersection) {
            float denom = Vector3.Dot(Norm, ray.Dir);

            if(denom > 0) {
                intersection = Intersection.None;
                return false;
            }
            else {
                intersection = new Intersection(this, ray, (Vector3.Dot(Norm, ray.Start) + Offset) / -denom);
                return true;
            }
        }

        public override Vector3 Normal(Vector3 pos) { return Norm; }
    }

    class Scene {
        public SceneObject[] Things;
        public Light[] Lights;
        public Camera Camera;
    }

    struct Light {
        public Vector3 Pos { get; }
        public Color4 Color { get; }
        public Light(Vector3 pos, Color4 color) {
            Pos = pos;
            Color = color;
        }
    }

    abstract class SceneObject {
        public Surface Surface;
        public abstract bool Intersect(Ray ray, out Intersection inter);
        public abstract Vector3 Normal(Vector3 pos);
    }
}