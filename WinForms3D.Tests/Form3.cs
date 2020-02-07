using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinForms3D.Tests {
    public partial class Form3 : Form {
        public Form3() {
            InitializeComponent();
            this.BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void button1_Click(object sender, EventArgs e) {
            var sw = Stopwatch.StartNew();
            var rayTracer = new RayTracer(this.Width, this.Height);
            this.BackgroundImage?.Dispose();
            var bmp = rayTracer.Render(rayTracer.DefaultScene);
            this.BackgroundImage = ImageUtils.createBitmap(bmp, this.Width, this.Height);
            sw.Stop();
            this.Text = sw.ElapsedMilliseconds.ToString();
        }
    }
}
