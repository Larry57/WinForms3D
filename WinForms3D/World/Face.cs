using System.Linq;
using System.Numerics;

namespace WinForms3D {
    public class Face : Volume {

        public Face() : base(
            new[] {
                new Vector3(1, 1, 1) - new Vector3(.5f, .5f, .5f),
                new Vector3(0, 1, 1)- new Vector3(.5f, .5f, .5f),
                new Vector3(0, 0, 1)- new Vector3(.5f, .5f, .5f),
            },
            new[] { 0, 1, 2 }.BuildTriangleIndices().ToArray()) {
        }
    }
}
