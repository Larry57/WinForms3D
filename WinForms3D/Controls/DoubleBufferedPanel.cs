using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinForms3D {
    public class DoubleBufferedPanel : Panel {
        public DoubleBufferedPanel() => this.DoubleBuffered = true;
    }
}
