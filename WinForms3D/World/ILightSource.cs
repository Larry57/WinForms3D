using System.Numerics;

namespace WinForms3D {
    public interface ILightSource {
        Vector3 Direction { get; set; }
        Vector3 Position { get; set; }
    }

    public class LightSource : ILightSource {
        public Vector3 Position { get; set; }
        public Vector3 Direction { get; set; }
    }
}
