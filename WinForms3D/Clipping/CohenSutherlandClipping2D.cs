using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace WinForms3D {

    // Buggy

    class CohenSutherlandClipping2D : IClipping2D {

        float xMin; float xMax; float yMin; float yMax;

        public CohenSutherlandClipping2D(float xMin, float xMax, float yMin, float yMax) {
            this.xMin = xMin;
            this.xMax = xMax;
            this.yMin = yMin;
            this.yMax = yMax;
        }

        // https://en.wikipedia.org/wiki/Cohen%E2%80%93Sutherland_algorithm

        [Flags]
        enum OutCode {
            INSIDE = 0, // 0000
            LEFT = 1,   // 0001
            RIGHT = 2,  // 0010
            BOTTOM = 4, // 0100
            TOP = 8     // 1000
        }

        // Compute the bit code for a point (x, y) using the clip rectangle
        // bounded diagonally by (xmin, ymin), and (xmax, ymax)

        OutCode computeOutCode(float x, float y) {
            var code = OutCode.INSIDE;          // initialised as being inside of [[clip window]]

            if(x < xMin)           // to the left of clip window
                code |= OutCode.LEFT;
            else if(x > xMax)      // to the right of clip window
                code |= OutCode.RIGHT;
            if(y < yMin)           // below the clip window
                code |= OutCode.TOP;
            else if(y > yMax)      // above the clip window
                code |= OutCode.BOTTOM;

            return code;
        }

        // Cohen–Sutherland clipping algorithm clips a line from
        // P0 = (x0, y0) to P1 = (x1, y1) against a rectangle with 
        // diagonal from (xmin, ymin) to (xmax, ymax).
        public bool Clip(ref Vector2 p1, ref Vector2 p2) {
            var x0 = p1.X; var x1 = p2.X;
            var y0 = p1.Y; var y1 = p2.Y;

            // compute outcodes for P0, P1, and whatever point lies outside the clip rectangle
            var outcode0 = computeOutCode(x0, y0);
            var outcode1 = computeOutCode(x1, y1);

            while(true) {
                if((outcode0 | outcode1) == 0) {
                    p1 = new Vector2(x0, y0);
                    p2 = new Vector2(x1, y1);
                    // bitwise OR is 0: both points inside window; trivially accept and exit loop
                    return true;
                }
                else if((outcode0 & outcode1) != 0) {
                    // bitwise AND is not 0: both points share an outside zone (LEFT, RIGHT, TOP,
                    // or BOTTOM), so both must be outside window; exit loop (accept is false)
                    return false;
                }
                else {
                    // failed both tests, so calculate the line segment to clip
                    // from an outside point to an intersection with clip edge
                    float x = 0;
                    float y = 0;

                    // At least one endpoint is outside the clip rectangle; pick it.
                    var outcodeOut = outcode0 != 0 ? outcode0 : outcode1;

                    // Now find the intersection point;
                    // use formulas:
                    //   slope = (y1 - y0) / (x1 - x0)
                    //   x = x0 + (1 / slope) * (ym - y0), where ym is ymin or ymax
                    //   y = y0 + slope * (xm - x0), where xm is xmin or xmax

                    // No need to worry about divide-by-zero because, in each case, the
                    // outcode bit being tested guarantees the denominator is non-zero
                    if(outcodeOut.HasFlag(OutCode.TOP)) {           // point is above the clip window
                        x = x0 + (x1 - x0) * (yMin - y0) / (y1 - y0);
                        y = yMin;
                    }
                    else if(outcodeOut.HasFlag(OutCode.BOTTOM)) { // point is below the clip window
                        x = x0 + (x1 - x0) * (yMax - y0) / (y1 - y0);
                        y = yMax;
                    }
                    else if(outcodeOut.HasFlag(OutCode.RIGHT)) {  // point is to the right of clip window
                        y = y0 + (y1 - y0) * (xMax - x0) / (x1 - x0);
                        x = xMax;
                    }
                    else if(outcodeOut.HasFlag(OutCode.LEFT)) {   // point is to the left of clip window
                        y = y0 + (y1 - y0) * (xMin - x0) / (x1 - x0);
                        x = xMin;
                    }

                    // Now we move outside point to intersection point to clip
                    // and get ready for next pass.
                    if(outcodeOut == outcode0) {
                        x0 = x;
                        y0 = y;
                        outcode0 = computeOutCode(x0, y0);
                    }
                    else {
                        x1 = x;
                        y1 = y;
                        outcode1 = computeOutCode(x1, y1);
                    }
                }
            }
        }
    }
}
