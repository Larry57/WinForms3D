using System.Numerics;

namespace WinForms3D {
    public interface IClipping2D {
        bool Clip(ref Vector2 begin, ref Vector2 end);
    }
}