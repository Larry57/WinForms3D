using System;
using System.Numerics;

namespace WinForms3D {

    // Buggy : this code has a bounce case and shall not be used

    class CohenSutherlandClippingHomogeneous : IClippingHomogeneous {

        [Flags]
        enum OutCode {
            INSIDE = 0,
            LEFT = 1,
            RIGHT = 2,
            BOTTOM = 4,
            TOP = 8,
            FAR = 16,
            NEAR = 32,
            OUT = 64
        }

        static OutCode computeOutCode(float x, float y, float z, float w) {
            var code = OutCode.INSIDE;

            if(w < 0f) code = OutCode.OUT;

            if(y > w) code |= OutCode.TOP;
            else if(y < -w) code |= OutCode.BOTTOM;

            if(x < -w) code |= OutCode.LEFT;
            else if(x > w) code |= OutCode.RIGHT;

            if(z < -w) code |= OutCode.FAR;
            else if(z > w) code |= OutCode.NEAR;

            return code;
        }

        public bool Clip(ref Vector4 p1, ref Vector4 p2) {
            var x1 = p1.X;
            var y1 = p1.Y;
            var z1 = p1.Z;
            var w1 = p1.W;

            var x2 = p2.X;
            var y2 = p2.Y;
            var z2 = p2.Z;
            var w2 = p2.W;

            var outcode1 = computeOutCode(x1, y1, z1, w1);
            var outcode2 = computeOutCode(x2, y2, z2, w2);

            while(true) {
                if((outcode1 | outcode2) == 0) {
                    p1 = new Vector4(x1, y1, z1, w1);
                    p2 = new Vector4(x2, y2, z2, w2);
                    return true;
                }
                else if((outcode1 & outcode2) != 0) {
                    return false;
                }
                else {
                    var x = 0f;
                    var y = 0f;
                    var z = 0f;
                    var w = 0f;

                    var outcodeOut = outcode1 == 0 ? outcode2 : outcode1;

                    if(outcodeOut.HasFlag(OutCode.OUT)) {
                        var t = w1 / (w1 - w2);
                        w = MathUtils.Epsilon;                // Clip at the closest as possible to W = 0
                        x = x1 + t * (x2 - x1);
                        y = y1 + t * (y2 - y1);
                        z = z1 + t * (z2 - z1);
                    }
                    else if(outcodeOut.HasFlag(OutCode.TOP)) {
                        var v = w1 - y1;
                        var t = v / (v - (w2 - y2));
                        w = w1 + t * (w2 - w1);
                        x = x1 + t * (x2 - x1);
                        y = w;
                        z = z1 + t * (z2 - z1);
                    }
                    else if(outcodeOut.HasFlag(OutCode.BOTTOM)) {
                        var v = w1 + y1;
                        var t = v / (v - (w2 + y2));
                        w = w1 + t * (w2 - w1);
                        x = x1 + t * (x2 - x1);
                        y = -w;
                        z = z1 + t * (z2 - z1);
                    }
                    else if(outcodeOut.HasFlag(OutCode.RIGHT)) {
                        var v = w1 - x1;
                        var t = v / (v - (w2 - x2));
                        w = w1 + t * (w2 - w1);
                        x = w;
                        y = y1 + t * (y2 - y1);
                        z = z1 + t * (z2 - z1);
                    }
                    else if(outcodeOut.HasFlag(OutCode.LEFT)) {
                        var v = w1 + x1;
                        var t = v / (v - (w2 + x2));
                        w = w1 + t * (w2 - w1);
                        x = -w;
                        y = y1 + t * (y2 - y1);
                        z = z1 + t * (z2 - z1);
                    }
                    else if(outcodeOut.HasFlag(OutCode.FAR)) {
                        var v = w1 - z1;
                        var t = v / (v - (w2 - z2));
                        w = w1 + t * (w2 - w1);
                        x = x1 + t * (x2 - x1);
                        y = y1 + t * (y2 - y1);
                        z = -w;
                    }
                    else if(outcodeOut.HasFlag(OutCode.NEAR)) {
                        var v = w1 + z1;
                        var t = v / (v - (w2 + z2));
                        w = w1 + t * (w2 - w1);
                        x = x1 + t * (x2 - x1);
                        y = y1 + t * (y2 - y1);
                        z = w;
                    }

                    // Now we move outside point to intersection point to clip
                    // and get ready for next pass.
                    if(outcodeOut == outcode1) {
                        x1 = x;
                        y1 = y;
                        z1 = z;
                        w1 = w;
                        outcode1 = computeOutCode(x, y, z, w);
                    }
                    else {
                        x2 = x;
                        y2 = y;
                        z2 = z;
                        w2 = w;
                        outcode2 = computeOutCode(x, y, z, w);
                    }
                }
            }
        }
    }
}
