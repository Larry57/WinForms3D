using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing.Design;
using System.Globalization;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace WinForms3D {
    public class World : IWorld {
        public List<IVolume> Volumes { get; set; }
        
        public List<ILightSource> LightSources { get; set; }        

        public World() {
            this.Volumes = new List<IVolume>();
            this.LightSources = new List<ILightSource>();
        }
    }
}
