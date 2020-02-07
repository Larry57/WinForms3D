namespace WinForms3D {
    partial class ArcBallCamControl {
        /// <summary> 
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing) {
            if(disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur de composants

        /// <summary> 
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas 
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent() {
            this.panel1 = new System.Windows.Forms.Panel();
            this.slider1 = new WinForms3D.SliderIn();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(222, 222);
            this.panel1.TabIndex = 3;
            // 
            // slider1
            // 
            this.slider1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.slider1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.slider1.Location = new System.Drawing.Point(228, 3);
            this.slider1.Max = 500F;
            this.slider1.Min = 0F;
            this.slider1.Name = "slider1";
            this.slider1.NumberEvery = 25F;
            this.slider1.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.slider1.PixelStep = 1F;
            this.slider1.Size = new System.Drawing.Size(20, 222);
            this.slider1.TabIndex = 4;
            this.slider1.Text = "sliderIn1";
            this.slider1.TickEvery = 10F;
            this.slider1.Value = 0F;
            // 
            // ArcBallCamControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.Controls.Add(this.slider1);
            this.Controls.Add(this.panel1);
            this.Name = "ArcBallCamControl";
            this.Size = new System.Drawing.Size(251, 228);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private SliderIn slider1;
    }
}
