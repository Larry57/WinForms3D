using System.Collections.Generic;

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
