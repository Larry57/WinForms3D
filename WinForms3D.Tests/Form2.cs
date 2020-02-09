using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinForms3D.Tests {

    public partial class Form2 : Form {

        ArcBallCam arcBallCam;
        FlyCam flyCam;

        public Form2() {
            InitializeComponent();

            lstDemos.DataSource = new[] {
                new { display = "Crane", id = "skull" },
                // new { display = "Teapot", id = "teapot" },
                new { display = "Cubes", id = "cubes" },
                new { display = "Spheres", id = "spheres" },
                new { display = "Little town", id = "littletown" },
                new { display = "Town", id = "town" },
                new { display = "Big town", id = "bigtown" },
                new { display = "Cube", id = "cube" },
                new { display = "Big cube", id = "bigcube" },
                new { display = "Empty", id = "empty" },
            };

            lstDemos.ValueMember = "id";
            lstDemos.DisplayMember = "display";

            lstDemos.DoubleClick += LstDemos_DoubleClick;

            rdbNoneShading.Checked = panel3D1.Painter == null;
            rdbClassicShading.Checked = panel3D1.Painter is ClassicPainter;
            rdbFlatShading.Checked = panel3D1.Painter is FlatPainter;
            rdbGouraudShading.Checked = panel3D1.Painter is GouraudPainter;

            rdbNoneShading.CheckedChanged += (s, e) => { if(!((RadioButton)s).Checked) return; panel3D1.Painter = null; panel3D1.Invalidate(); };
            rdbClassicShading.CheckedChanged += (s, e) => { if(!((RadioButton)s).Checked) return; panel3D1.Painter = new ClassicPainter(); panel3D1.Invalidate(); };
            rdbFlatShading.CheckedChanged += (s, e) => { if(!((RadioButton)s).Checked) return; panel3D1.Painter = new FlatPainter(); panel3D1.Invalidate(); };
            rdbGouraudShading.CheckedChanged += (s, e) => { if(!((RadioButton)s).Checked) return; panel3D1.Painter = new GouraudPainter(); panel3D1.Invalidate(); };

            rdbSimpleRendererLogic.Checked = panel3D1.Renderer is SimpleRenderer;
            rdbZSortRendererLogic.Checked = panel3D1.Renderer is ZSortRenderer;

            rdbSimpleRendererLogic.CheckedChanged += (s, e) => { if(!((RadioButton)s).Checked) return; panel3D1.Renderer = new SimpleRenderer(); panel3D1.Invalidate(); };
            rdbZSortRendererLogic.CheckedChanged += (s, e) => { if(!((RadioButton)s).Checked) return; panel3D1.Renderer = new ZSortRenderer(); panel3D1.Invalidate(); };

            chkShowTriangles.Checked = panel3D1.RendererSettings.ShowTriangles;
            chkShowBackFacesCulling.Checked = panel3D1.RendererSettings.BackFaceCulling;
            chkShowTrianglesNormals.Checked = panel3D1.RendererSettings.ShowTriangleNormals;
            chkShowXZGrid.Checked = panel3D1.RendererSettings.ShowXZGrid;
            chkShowAxes.Checked = panel3D1.RendererSettings.ShowAxes;

            chkShowTriangles.CheckedChanged += (s, e) => { panel3D1.RendererSettings.ShowTriangles = chkShowTriangles.Checked; panel3D1.Invalidate(); };
            chkShowBackFacesCulling.CheckedChanged += (s, e) => { panel3D1.RendererSettings.BackFaceCulling = chkShowBackFacesCulling.Checked; panel3D1.Invalidate(); };
            chkShowTrianglesNormals.CheckedChanged += (s, e) => { panel3D1.RendererSettings.ShowTriangleNormals = chkShowTrianglesNormals.Checked; panel3D1.Invalidate(); };
            chkShowXZGrid.CheckedChanged += (s, e) => { panel3D1.RendererSettings.ShowXZGrid = chkShowXZGrid.Checked; panel3D1.Invalidate(); };
            chkShowAxes.CheckedChanged += (s, e) => { panel3D1.RendererSettings.ShowAxes = chkShowAxes.Checked; panel3D1.Invalidate(); };

            btnBench.Click += (s, e) => {
                var sw = Stopwatch.StartNew();
                arcBallCam.Position = new Vector3(0, 0, -5);
                arcBallCam.Rotation = Quaternion.Identity;
                for(var i = 0; i < 10; i++) {
                    arcBallCam.Rotation *= Quaternion.CreateFromYawPitchRoll(.1f, .1f, .1f);
                    this.panel3D1.Render();
                }
                sw.Stop();
                lblSw.Text = sw.ElapsedMilliseconds.ToString();
            };

            var projection = new FovPerspectiveProjection(40f * (float)Math.PI / 180f, .01f, 500f);

            arcBallCam = new ArcBallCam { Position = new Vector3(0, 0, -60) };
            flyCam = new FlyCam { Position = new Vector3(0, 0, -60) };

            var arcBallCamHandler = new ArcBallCamHandler(this.panel3D1, arcBallCam);
            var flyCamHandler = new FlyCamHandler(this.panel3D2, flyCam);

            this.arcBallCamControl1.Camera = arcBallCam;

            this.panel3D1.Projection = projection;
            this.panel3D1.Camera = arcBallCam;

            this.panel3D2.Projection = projection;
            this.panel3D2.Camera = flyCam;
        }

        private void LstDemos_DoubleClick(object sender, EventArgs e) {
            var world = new World();

            switch(lstDemos.SelectedValue) {
                case "skull":
                    world.Volumes.AddRange(VolumeFactory.ImportCollada(@"models\skull.dae"));
                    break;

                case "teapot":
                    world.Volumes.AddRange(VolumeFactory.ImportCollada(@"models\teapot.dae"));
                    break;

                case "empty":
                    break;

                case "town": {
                        var d = 50; var s = 2;
                        for(var x = -d; x <= d; x += s)
                            for(var z = -d; z <= d; z += s) {
                                world.Volumes.Add(
                                    new Cube() {
                                        Position = new Vector3(x, 0, z),
                                        // Scale = new Vector3(1, r.Next(1, 50), 1)
                                    });
                            }
                        break;
                    }

                case "littletown": {
                        var d = 10; var s = 2;
                        for(var x = -d; x <= d; x += s)
                            for(var z = -d; z <= d; z += s) {
                                world.Volumes.Add(
                                    new Cube() {
                                        Position = new Vector3(x, 0, z),
                                        // Scale = new Vector3(1, r.Next(1, 50), 1)
                                    });
                            }
                        break;
                    }

                case "bigtown": {
                        var d = 200; var s = 2;
                        for(var x = -d; x <= d; x += s)
                            for(var z = -d; z <= d; z += s) {
                                world.Volumes.Add(
                                    new Cube() {
                                        Position = new Vector3(x, 0, z),
                                        // Scale = new Vector3(1, r.Next(1, 50), 1)
                                    });
                            }
                        break;
                    }

                case "cube":
                    world.Volumes.Add(new Cube());
                    break;

                case "bigcube":
                    world.Volumes.Add(new Cube() { Scale = new Vector3(100, 100, 100) });
                    break;

                case "spheres": {
                        var d = 5; var s = 2; var r = new Random();
                        for(var x = -d; x <= d; x += s)
                            for(var y = -d; y <= d; y += s)
                                for(var z = -d; z <= d; z += s) {
                                    world.Volumes.Add(
                                        new IcoSphere(2) {
                                            Position = new System.Numerics.Vector3(x, y, z),
                                            Rotation = new Rotation3D(
                                                (float)r.Next(-90, 90),
                                                (float)r.Next(-90, 90),
                                                (float)r.Next(-90, 90)).ToRad()
                                        });
                                }
                        break;
                    }

                case "cubes": {
                        var d = 20; var s = 2; var r = new Random();
                        for(var x = -d; x <= d; x += s)
                            for(var y = -d; y <= d; y += s)
                                for(var z = -d; z <= d; z += s) {
                                    world.Volumes.Add(
                                        new Cube() {
                                            Position = new System.Numerics.Vector3(x, y, z),
                                            Rotation = new Rotation3D(
                                                (float)r.Next(-90, 90),
                                                (float)r.Next(-90, 90),
                                                (float)r.Next(-90, 90)).ToRad()
                                        });
                                }
                        break;
                    }
            }

            world.LightSources.Add(new LightSource { Position = new Vector3(0, 0, 10) });

            // var camObject = new Cube() { Position = arcBallCam.Position };

            arcBallCam.CameraChanged -= MainCam_CameraChanged;
            arcBallCam.CameraChanged += MainCam_CameraChanged;

            // world.Volumes.Add(camObject);

            this.panel3D1.World = world;
            this.panel3D2.World = world;

            this.panel3D1.Invalidate();
            this.panel3D2.Invalidate();

            void MainCam_CameraChanged(object cam, EventArgs _1) {
                // camObject.Position = ((ArcBallCam)cam).Position;
                // this.panel3D2.Invalidate();
            }
        }
    }
}