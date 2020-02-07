using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinForms3D {

    // Slider

    public partial class Slider : UserControl {
        public event EventHandler ValueChanged {
            add {
                superSlider1.ValueChanged += value;
            }
            remove {
                superSlider1.ValueChanged -= value;
            }
        }

        public Slider() {
            InitializeComponent();
            textBox1.DataBindings.Add(nameof(TextBox.Text), superSlider1, "Value", false, DataSourceUpdateMode.OnPropertyChanged);
        }

        [EditorBrowsable(EditorBrowsableState.Always)]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Bindable(true)]
        public override string Text {
            get => label1.Text;
            set => label1.Text = value;
        }

        public float Value {
            get => superSlider1.Value;
            set => superSlider1.Value = value;
        }

        public float Min {
            get => superSlider1.Min;
            set => superSlider1.Min = value;
        }

        public float Max {
            get => superSlider1.Max;
            set => superSlider1.Max= value;
        }
    }
}
