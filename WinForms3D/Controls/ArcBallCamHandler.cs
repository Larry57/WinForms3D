using System.Drawing;
using System.Numerics;
using System.Windows.Forms;

namespace WinForms3D {

    // Must adapt moving

    public class ArcBallCamHandler {
        ArcBallCam camera;

        Point oldMousePosition;
        Vector3 oldCameraPosition;

        Quaternion oldCameraRotation;

        float yCoeff = 10f;

        public ArcBallCamHandler(Control control, ArcBallCam camera) {
            Control = control;
            Camera = camera;
        }

        Control control;

        public Control Control {
            get => control;
            set {
                var oldControl = control;

                if(PropertyChangedHelper.ChangeValue(ref control, value)) {

                    if(oldControl != null) {
                        oldControl.MouseDown -= control_MouseDown;
                        oldControl.MouseMove -= control_MouseMove;
                        control.MouseUp -= Control_MouseUp;
                    }

                    if(control != null) {
                        control.MouseDown += control_MouseDown;
                        control.MouseMove += control_MouseMove;
                        control.MouseUp += Control_MouseUp;
                    }
                }
            }
        }

        private void Control_MouseUp(object sender, MouseEventArgs e) {
            left = false;
            right = false;
            control.Cursor = Cursors.Default;
        }

        bool right;
        bool left;

        private void control_MouseDown(object sender, MouseEventArgs e) {
            ControlHelper.getMouseButtons(out left, out right);
            oldMousePosition = e.Location;

            if(left && right) {
                oldCameraPosition = camera.Position;
                control.Cursor = Cursors.SizeNS ;
            }
            else if(left) {
                oldCameraRotation = camera.Rotation;
                control.Cursor = Cursors.NoMove2D;
            }
            else if(right) {
                oldCameraPosition = camera.Position;
                control.Cursor = Cursors.SizeAll;
            }
        }

        void control_MouseMove(object sender, MouseEventArgs e) {
            if(left && right) {
                var deltaY = oldMousePosition.Y - e.Location.Y;
                camera.Position = oldCameraPosition + new Vector3(0, 0, deltaY / yCoeff);
            }
            else if(left) {
                var oldNpc = control.NormalizePointClient(oldMousePosition);
                var oldVector = camera.MapToSphere(oldNpc);

                var curNpc = control.NormalizePointClient(e.Location);
                var curVector = camera.MapToSphere(curNpc);

                var deltaRotation = camera.CalculateQuaternion(oldVector, curVector);
                camera.Rotation = deltaRotation * oldCameraRotation;
            }
            else if(right) {
                var deltaPosition = new Vector3(e.Location.ToVector2() - oldMousePosition.ToVector2(), 0);
                camera.Position = oldCameraPosition + (deltaPosition * new Vector3(1, -1, 1)) / 100;
            }
        }

        public ArcBallCam Camera {
            get => camera;
            set => PropertyChangedHelper.ChangeValue(ref camera, value);
        }
    }
}
