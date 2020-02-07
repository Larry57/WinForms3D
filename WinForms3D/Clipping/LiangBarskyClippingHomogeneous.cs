using System;
using System.Numerics;

namespace WinForms3D {

    // Taken from https://fr.mathworks.com/matlabcentral/fileexchange/51550-3d-and-2d-homogeneous-space-line-clipping-using-liang-barsky-algorithm

    public class LiangBarskyClippingHomogeneous : IClippingHomogeneous {
        float _t0;
        float _t1;

        public bool Clip(ref Vector4 p0, ref Vector4 p1) {

            if(p0.W < 0 && p1.W < 0)
                return false;

            _t0 = 0;
            _t1 = 1;

            var delta = p1 - p0;

            if(!clip(p0.W - p0.X, -delta.W + delta.X)) return false;
            if(!clip(p0.W + p0.X, -delta.W - delta.X)) return false;

            if(!clip(p0.W - p0.Y, -delta.W + delta.Y)) return false;
            if(!clip(p0.W + p0.Y, -delta.W - delta.Y)) return false;

            if(!clip(p0.W - p0.Z, -delta.W + delta.Z)) return false;
            if(!clip(p0.W + p0.Z, -delta.W - delta.Z)) return false;

            if(_t1 < 1)
                p1 = p0 + _t1 * delta;
                
            if (_t0 > 0)
                p0 = p0 + _t0 * delta;

            return true;
        }

        bool clip(float q, float p) {
            if(Math.Abs(p) < float.Epsilon && q < 0)
                return false;

            var r = q / p;

            if(p < 0) {
                if(r > _t1) return false;
                if(r > _t0) _t0 = r;
            }
            else {
                if(r < _t0) return false;
                if(r < _t1) _t1 = r;
            }

            return true;
        }
    }
}
