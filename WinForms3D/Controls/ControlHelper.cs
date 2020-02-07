using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinForms3D {
    static class ControlHelper {

        public static Vector2 NormalizePointClient(this Control control, Point position) => new Vector2(position.X * (2f / control.Width) - 1.0f, position.Y * (2f / control.Height) - 1.0f);
      
        public static void getMouseButtons(out bool left, out bool right) {
            left = Control.MouseButtons.HasFlag(MouseButtons.Left);
            right = Control.MouseButtons.HasFlag(MouseButtons.Right);
        }
    }
}
