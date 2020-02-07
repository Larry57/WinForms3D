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

namespace WinForms3D
{
    // Must adapt move

    public partial class ArcBallCamControl : UserControl
    {
        ArcBallCam camera;

        Point oldMousePosition;
        Quaternion oldRotation;

        public ArcBallCamControl()
        {
            InitializeComponent();
            this.DoubleBuffered = true;

            this.panel1.MouseDown += Panel1_MouseDown;
            this.panel1.MouseMove += Panel1_MouseMove;

            this.slider1.ValueChanged += (s, e) => camera.Position = new Vector3(camera.Position.X, camera.Position.Y, -this.slider1.Value);

            this.panel1.Paint += Panel1_Paint;
        }

        private void Panel1_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            drawGlobe(g);
            drawZIndex(g);
        }

        private void drawZIndex(Graphics g)
        {
        }

        void drawGlobe(Graphics g)
        {
            var nR = camera?.Radius ?? 0f;

            var rectF = new RectangleF(
                (.5f - nR) * panel1.Width,
                (.5f - nR) * panel1.Height,
                nR * 2f * panel1.Width,             // width
                nR * 2f * panel1.Height             // height
            );

            g.DrawEllipse(Pens.LightBlue, rectF);
        }

        private void Panel1_MouseDown(object sender, MouseEventArgs e)
        {
            oldMousePosition = e.Location;
            oldRotation = camera.Rotation;
        }

        private void Panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if(e.Button != MouseButtons.Left)
                return;

            var oldNpc = panel1.NormalizePointClient(oldMousePosition);
            var oldVector = camera.MapToSphere(oldNpc);

            var curNpc = panel1.NormalizePointClient(e.Location);
            var curVector = camera.MapToSphere(curNpc);

            var q = camera.CalculateQuaternion(oldVector, curVector);
            curRotation = q * oldRotation;
            camera.Rotation = curRotation;
        }

        Quaternion curRotation;

        public ArcBallCam Camera
        {
            get
            {
                return camera;
            }
            set
            {
                var oldCamera = camera;

                if(PropertyChangedHelper.ChangeValue(ref camera, value))
                {

                    if(oldCamera != null)
                        oldCamera.CameraChanged -= Camera_CameraChanged;

                    if(camera != null)
                        camera.CameraChanged += Camera_CameraChanged;

                    slider1.Value = -camera.Position.Z;
                }
            }
        }

        private void Camera_CameraChanged(object sender, EventArgs e)
        {
            if (-camera.Position.Z != slider1.Value)
                slider1.Value = -camera.Position.Z;

            if(camera.Rotation != curRotation) {
                this.panel1.Invalidate();
                curRotation = camera.Rotation;
            }
        }
    }
}
