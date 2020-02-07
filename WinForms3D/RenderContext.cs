using System.Numerics;

namespace WinForms3D {

    public class RenderContext {
        public ICamera Camera { get; set; }
        public IWorld World { get; set; }
        public IProjection Projection { get; set; }
        public RendererSettings RendererSettings { get; set; }
        public Stats Stats { get; set; }

        internal FrameBuffer Surface { get; set; }
        internal WorldBuffer WorldBuffer { get; set; }
    }
}