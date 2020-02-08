using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Numerics;

namespace WinForms3D {
    // Must adapt move

    public partial class ArcBallCamControl : UserControl {
        ArcBallCamHandler handler;

        ArcBallCam camera;
        Quaternion curRotation;

        public ArcBallCamControl() {
            InitializeComponent();

            this.slider1.ValueChanged += (s, e) => camera.Position = new Vector3(camera.Position.X, camera.Position.Y, -this.slider1.Value);
            this.panel1.Paint += Panel1_Paint;
        }

        private void Panel1_Paint(object sender, PaintEventArgs e) {
            var g = e.Graphics;
            drawGlobe(g);
            drawZIndex(g);
        }

        private void drawZIndex(Graphics g) {
        }

        void drawGlobe(Graphics g) {
            var nR = camera?.Radius ?? 0f;

            var rectF = new RectangleF(
                (.5f - nR) * panel1.Width,
                (.5f - nR) * panel1.Height,
                nR * 2f * panel1.Width,             // width
                nR * 2f * panel1.Height             // height
            );

            g.DrawEllipse(Pens.LightBlue, rectF);
        }

        public ArcBallCam Camera {
            get {
                return camera;
            }
            set {
                var oldCamera = camera;

                if(PropertyChangedHelper.ChangeValue(ref camera, value)) {

                    if(oldCamera != null) {
                        oldCamera.CameraChanged -= Camera_CameraChanged;
                        handler = null;
                    }

                    if(camera != null) {
                        camera.CameraChanged += Camera_CameraChanged;
                        handler = new ArcBallCamHandler(this.panel1, Camera);
                    }

                    slider1.Value = -camera.Position.Z;
                }
            }
        }

        private void Camera_CameraChanged(object sender, EventArgs e) {
            if(-camera.Position.Z != slider1.Value)
                slider1.Value = -camera.Position.Z;

            if(camera.Rotation != curRotation) {
                curRotation = camera.Rotation;
                this.panel1.Invalidate();
            }
        }
    }
}
