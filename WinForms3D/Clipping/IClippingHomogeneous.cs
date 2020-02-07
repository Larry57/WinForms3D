using System.Numerics;

namespace WinForms3D {
    public interface IClippingHomogeneous {
        bool Clip(ref Vector4 begin, ref Vector4 end);
    }
}