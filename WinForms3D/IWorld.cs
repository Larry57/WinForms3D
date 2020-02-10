using System.Collections.Generic;

namespace WinForms3D {
    public interface IWorld {
        List<IVolume> Volumes { get; set; }
        List<ILightSource> LightSources { get; set; }
    }
}