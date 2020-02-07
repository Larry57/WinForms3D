namespace WinForms3D.Tests {
    partial class Form2 {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if(disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            this.rdbClassicShading = new System.Windows.Forms.RadioButton();
            this.rdbFlatShading = new System.Windows.Forms.RadioButton();
            this.rdbGouraudShading = new System.Windows.Forms.RadioButton();
            this.chkShowTriangles = new System.Windows.Forms.CheckBox();
            this.chkShowVerticesNormals = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rdbNoneShading = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.chkShowAxes = new System.Windows.Forms.CheckBox();
            this.chkShowXZGrid = new System.Windows.Forms.CheckBox();
            this.chkShowBackFacesCulling = new System.Windows.Forms.CheckBox();
            this.chkShowTrianglesNormals = new System.Windows.Forms.CheckBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.rdoPerspectiveProjection = new System.Windows.Forms.RadioButton();
            this.lstDemos = new System.Windows.Forms.ListBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.arcBallCamControl1 = new WinForms3D.ArcBallCamControl();
            this.panel3D1 = new WinForms3D.Panel3D();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.panel3D2 = new WinForms3D.Panel3D();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.rdbSimpleRendererLogic = new System.Windows.Forms.RadioButton();
            this.rdbZSortRendererLogic = new System.Windows.Forms.RadioButton();
            this.btnBench = new System.Windows.Forms.Button();
            this.lblSw = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // rdbClassicShading
            // 
            this.rdbClassicShading.AutoSize = true;
            this.rdbClassicShading.Location = new System.Drawing.Point(15, 42);
            this.rdbClassicShading.Name = "rdbClassicShading";
            this.rdbClassicShading.Size = new System.Drawing.Size(58, 17);
            this.rdbClassicShading.TabIndex = 1;
            this.rdbClassicShading.TabStop = true;
            this.rdbClassicShading.Text = "Classic";
            this.rdbClassicShading.UseVisualStyleBackColor = true;
            // 
            // rdbFlatShading
            // 
            this.rdbFlatShading.AutoSize = true;
            this.rdbFlatShading.Location = new System.Drawing.Point(15, 65);
            this.rdbFlatShading.Name = "rdbFlatShading";
            this.rdbFlatShading.Size = new System.Drawing.Size(42, 17);
            this.rdbFlatShading.TabIndex = 2;
            this.rdbFlatShading.TabStop = true;
            this.rdbFlatShading.Text = "Flat";
            this.rdbFlatShading.UseVisualStyleBackColor = true;
            // 
            // rdbGouraudShading
            // 
            this.rdbGouraudShading.AutoSize = true;
            this.rdbGouraudShading.Location = new System.Drawing.Point(15, 88);
            this.rdbGouraudShading.Name = "rdbGouraudShading";
            this.rdbGouraudShading.Size = new System.Drawing.Size(66, 17);
            this.rdbGouraudShading.TabIndex = 4;
            this.rdbGouraudShading.TabStop = true;
            this.rdbGouraudShading.Text = "Gouraud";
            this.rdbGouraudShading.UseVisualStyleBackColor = true;
            // 
            // chkShowTriangles
            // 
            this.chkShowTriangles.AutoSize = true;
            this.chkShowTriangles.Location = new System.Drawing.Point(15, 19);
            this.chkShowTriangles.Name = "chkShowTriangles";
            this.chkShowTriangles.Size = new System.Drawing.Size(69, 17);
            this.chkShowTriangles.TabIndex = 3;
            this.chkShowTriangles.Text = "Triangles";
            this.chkShowTriangles.UseVisualStyleBackColor = true;
            // 
            // chkShowVerticesNormals
            // 
            this.chkShowVerticesNormals.AutoSize = true;
            this.chkShowVerticesNormals.Location = new System.Drawing.Point(15, 42);
            this.chkShowVerticesNormals.Name = "chkShowVerticesNormals";
            this.chkShowVerticesNormals.Size = new System.Drawing.Size(103, 17);
            this.chkShowVerticesNormals.TabIndex = 4;
            this.chkShowVerticesNormals.Text = "Vertices normals";
            this.chkShowVerticesNormals.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.rdbNoneShading);
            this.groupBox1.Controls.Add(this.rdbGouraudShading);
            this.groupBox1.Controls.Add(this.rdbClassicShading);
            this.groupBox1.Controls.Add(this.rdbFlatShading);
            this.groupBox1.Location = new System.Drawing.Point(918, 279);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(135, 133);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Shading";
            // 
            // rdbNoneShading
            // 
            this.rdbNoneShading.AutoSize = true;
            this.rdbNoneShading.Location = new System.Drawing.Point(15, 19);
            this.rdbNoneShading.Name = "rdbNoneShading";
            this.rdbNoneShading.Size = new System.Drawing.Size(51, 17);
            this.rdbNoneShading.TabIndex = 0;
            this.rdbNoneShading.TabStop = true;
            this.rdbNoneShading.Text = "None";
            this.rdbNoneShading.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.chkShowAxes);
            this.groupBox2.Controls.Add(this.chkShowXZGrid);
            this.groupBox2.Controls.Add(this.chkShowBackFacesCulling);
            this.groupBox2.Controls.Add(this.chkShowTrianglesNormals);
            this.groupBox2.Controls.Add(this.chkShowTriangles);
            this.groupBox2.Controls.Add(this.chkShowVerticesNormals);
            this.groupBox2.Location = new System.Drawing.Point(918, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(135, 158);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Display";
            // 
            // chkShowAxes
            // 
            this.chkShowAxes.AutoSize = true;
            this.chkShowAxes.Location = new System.Drawing.Point(15, 134);
            this.chkShowAxes.Name = "chkShowAxes";
            this.chkShowAxes.Size = new System.Drawing.Size(49, 17);
            this.chkShowAxes.TabIndex = 8;
            this.chkShowAxes.Text = "Axes";
            this.chkShowAxes.UseVisualStyleBackColor = true;
            // 
            // chkShowXZGrid
            // 
            this.chkShowXZGrid.AutoSize = true;
            this.chkShowXZGrid.Location = new System.Drawing.Point(15, 111);
            this.chkShowXZGrid.Name = "chkShowXZGrid";
            this.chkShowXZGrid.Size = new System.Drawing.Size(60, 17);
            this.chkShowXZGrid.TabIndex = 7;
            this.chkShowXZGrid.Text = "XZ grid";
            this.chkShowXZGrid.UseVisualStyleBackColor = true;
            // 
            // chkShowBackFacesCulling
            // 
            this.chkShowBackFacesCulling.AutoSize = true;
            this.chkShowBackFacesCulling.Location = new System.Drawing.Point(15, 88);
            this.chkShowBackFacesCulling.Name = "chkShowBackFacesCulling";
            this.chkShowBackFacesCulling.Size = new System.Drawing.Size(113, 17);
            this.chkShowBackFacesCulling.TabIndex = 6;
            this.chkShowBackFacesCulling.Text = "Back faces culling";
            this.chkShowBackFacesCulling.UseVisualStyleBackColor = true;
            // 
            // chkShowTrianglesNormals
            // 
            this.chkShowTrianglesNormals.AutoSize = true;
            this.chkShowTrianglesNormals.Location = new System.Drawing.Point(15, 65);
            this.chkShowTrianglesNormals.Name = "chkShowTrianglesNormals";
            this.chkShowTrianglesNormals.Size = new System.Drawing.Size(106, 17);
            this.chkShowTrianglesNormals.TabIndex = 5;
            this.chkShowTrianglesNormals.Text = "Trangles normals";
            this.chkShowTrianglesNormals.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.rdoPerspectiveProjection);
            this.groupBox4.Location = new System.Drawing.Point(918, 451);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(135, 53);
            this.groupBox4.TabIndex = 7;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Projection";
            // 
            // rdoPerspectiveProjection
            // 
            this.rdoPerspectiveProjection.AutoSize = true;
            this.rdoPerspectiveProjection.Location = new System.Drawing.Point(15, 19);
            this.rdoPerspectiveProjection.Name = "rdoPerspectiveProjection";
            this.rdoPerspectiveProjection.Size = new System.Drawing.Size(81, 17);
            this.rdoPerspectiveProjection.TabIndex = 0;
            this.rdoPerspectiveProjection.TabStop = true;
            this.rdoPerspectiveProjection.Text = "Perspective";
            this.rdoPerspectiveProjection.UseVisualStyleBackColor = true;
            // 
            // lstDemos
            // 
            this.lstDemos.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lstDemos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstDemos.IntegralHeight = false;
            this.lstDemos.Location = new System.Drawing.Point(8, 21);
            this.lstDemos.Name = "lstDemos";
            this.lstDemos.Size = new System.Drawing.Size(144, 565);
            this.lstDemos.TabIndex = 8;
            this.toolTip1.SetToolTip(this.lstDemos, "Use double click to select");
            // 
            // groupBox5
            // 
            this.groupBox5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox5.Controls.Add(this.lstDemos);
            this.groupBox5.Location = new System.Drawing.Point(12, 12);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Padding = new System.Windows.Forms.Padding(8);
            this.groupBox5.Size = new System.Drawing.Size(160, 594);
            this.groupBox5.TabIndex = 9;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Worlds (select=2x click)";
            // 
            // toolTip1
            // 
            this.toolTip1.IsBalloon = true;
            // 
            // groupBox6
            // 
            this.groupBox6.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox6.Controls.Add(this.arcBallCamControl1);
            this.groupBox6.Controls.Add(this.panel3D1);
            this.groupBox6.Location = new System.Drawing.Point(0, 0);
            this.groupBox6.Margin = new System.Windows.Forms.Padding(0);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Padding = new System.Windows.Forms.Padding(8);
            this.groupBox6.Size = new System.Drawing.Size(734, 297);
            this.groupBox6.TabIndex = 10;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Camera view";
            // 
            // arcBallCamControl1
            // 
            this.arcBallCamControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.arcBallCamControl1.BackColor = System.Drawing.Color.Gainsboro;
            this.arcBallCamControl1.Camera = null;
            this.arcBallCamControl1.Location = new System.Drawing.Point(588, 16);
            this.arcBallCamControl1.Name = "arcBallCamControl1";
            this.arcBallCamControl1.Size = new System.Drawing.Size(139, 117);
            this.arcBallCamControl1.TabIndex = 1;
            // 
            // panel3D1
            // 
            this.panel3D1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3D1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel3D1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.panel3D1.Location = new System.Drawing.Point(11, 16);
            this.panel3D1.Name = "panel3D1";
            this.panel3D1.Size = new System.Drawing.Size(571, 269);
            this.panel3D1.TabIndex = 0;
            // 
            // groupBox7
            // 
            this.groupBox7.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox7.Controls.Add(this.panel3D2);
            this.groupBox7.Location = new System.Drawing.Point(0, 297);
            this.groupBox7.Margin = new System.Windows.Forms.Padding(0);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Padding = new System.Windows.Forms.Padding(8);
            this.groupBox7.Size = new System.Drawing.Size(734, 297);
            this.groupBox7.TabIndex = 11;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Control view";
            // 
            // panel3D2
            // 
            this.panel3D2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3D2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel3D2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.panel3D2.Location = new System.Drawing.Point(11, 16);
            this.panel3D2.Name = "panel3D2";
            this.panel3D2.Size = new System.Drawing.Size(571, 270);
            this.panel3D2.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.groupBox6, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBox7, 0, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(178, 12);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(734, 594);
            this.tableLayoutPanel1.TabIndex = 12;
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.rdbSimpleRendererLogic);
            this.groupBox3.Controls.Add(this.rdbZSortRendererLogic);
            this.groupBox3.Location = new System.Drawing.Point(918, 188);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(135, 85);
            this.groupBox3.TabIndex = 13;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Renderer logic";
            // 
            // rdbSimpleRendererLogic
            // 
            this.rdbSimpleRendererLogic.AutoSize = true;
            this.rdbSimpleRendererLogic.Location = new System.Drawing.Point(15, 19);
            this.rdbSimpleRendererLogic.Name = "rdbSimpleRendererLogic";
            this.rdbSimpleRendererLogic.Size = new System.Drawing.Size(56, 17);
            this.rdbSimpleRendererLogic.TabIndex = 3;
            this.rdbSimpleRendererLogic.TabStop = true;
            this.rdbSimpleRendererLogic.Text = "Simple";
            this.rdbSimpleRendererLogic.UseVisualStyleBackColor = true;
            // 
            // rdbZSortRendererLogic
            // 
            this.rdbZSortRendererLogic.AutoSize = true;
            this.rdbZSortRendererLogic.Location = new System.Drawing.Point(15, 42);
            this.rdbZSortRendererLogic.Name = "rdbZSortRendererLogic";
            this.rdbZSortRendererLogic.Size = new System.Drawing.Size(52, 17);
            this.rdbZSortRendererLogic.TabIndex = 0;
            this.rdbZSortRendererLogic.TabStop = true;
            this.rdbZSortRendererLogic.Text = "Z sort";
            this.rdbZSortRendererLogic.UseVisualStyleBackColor = true;
            // 
            // btnBench
            // 
            this.btnBench.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBench.Location = new System.Drawing.Point(918, 510);
            this.btnBench.Name = "btnBench";
            this.btnBench.Size = new System.Drawing.Size(135, 24);
            this.btnBench.TabIndex = 14;
            this.btnBench.Text = "Bench";
            this.btnBench.UseVisualStyleBackColor = true;
            // 
            // lblSw
            // 
            this.lblSw.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSw.AutoSize = true;
            this.lblSw.Location = new System.Drawing.Point(1014, 537);
            this.lblSw.Name = "lblSw";
            this.lblSw.Size = new System.Drawing.Size(0, 13);
            this.lblSw.TabIndex = 15;
            this.lblSw.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1065, 618);
            this.Controls.Add(this.lblSw);
            this.Controls.Add(this.btnBench);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.MinimumSize = new System.Drawing.Size(435, 334);
            this.Name = "Form2";
            this.Text = "Form2";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.groupBox7.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Panel3D panel3D1;
        private ArcBallCamControl arcBallCamControl1;
        private System.Windows.Forms.RadioButton rdbGouraudShading;
        private System.Windows.Forms.RadioButton rdbFlatShading;
        private System.Windows.Forms.RadioButton rdbClassicShading;
        private System.Windows.Forms.CheckBox chkShowTriangles;
        private System.Windows.Forms.CheckBox chkShowVerticesNormals;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox chkShowTrianglesNormals;
        private System.Windows.Forms.CheckBox chkShowBackFacesCulling;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.RadioButton rdoPerspectiveProjection;
        private System.Windows.Forms.ListBox lstDemos;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.RadioButton rdbNoneShading;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.GroupBox groupBox7;
        private Panel3D panel3D2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.CheckBox chkShowXZGrid;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton rdbSimpleRendererLogic;
        private System.Windows.Forms.RadioButton rdbZSortRendererLogic;
        private System.Windows.Forms.CheckBox chkShowAxes;
        private System.Windows.Forms.Button btnBench;
        private System.Windows.Forms.Label lblSw;
    }
}