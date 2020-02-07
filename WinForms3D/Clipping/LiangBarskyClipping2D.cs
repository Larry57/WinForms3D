using System;
using System.Numerics;

namespace WinForms3D {

    // Taken from https://github.com/stil/graf3d/blob/master/Engine/Algorytmy/LineClippingAlgorithm.cs
    // Thank you Stil (https://github.com/stil)

    public class LiangBarskyClipping2D : IClipping2D {
        float _t0; float _t1;
        float xMin; float xMax; float yMin; float yMax;

        public LiangBarskyClipping2D(float xMin, float xMax, float yMin, float yMax) {
            this.xMin = xMin;
            this.xMax = xMax;
            this.yMin = yMin;
            this.yMax = yMax;
        }

        public bool Clip(ref Vector2 begin, ref Vector2 end) {
            var delta = end - begin;
            _t0 = 0;
            _t1 = 1;

            if(!clip(-delta.X, -xMin + begin.X)) return false;
            if(!clip(delta.X, xMax - begin.X)) return false;
            if(!clip(-delta.Y, -yMin + begin.Y)) return false;
            if(!clip(delta.Y, yMax - begin.Y)) return false;

            if(_t1 < 1) {
                end = begin + delta * _t1;
            }
            if(_t0 > 0) {
                begin += delta * _t0;
            }

            return true;
        }

        bool clip(float p, float q) {
            if(Math.Abs(p) < float.Epsilon) {
                if(q < 0) return false;
            }
            else {
                var r = q / p;

                if(p < 0) {
                    if(r > _t1) return false;
                    if(r > _t0) _t0 = r;
                }
                else {
                    if(r < _t0) return false;
                    if(r < _t1) _t1 = r;
                }
            }
            return true;
        }
    }
}
