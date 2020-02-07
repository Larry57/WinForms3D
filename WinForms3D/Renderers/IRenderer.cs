namespace WinForms3D {
    public interface IRenderer {
        RenderContext RenderContext { get; set; }
        IPainter Painter { get; set; }
        int[] Render();
    }
}