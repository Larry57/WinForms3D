namespace WinForms3D {
    public interface IPainter {
        RenderContext RendererContext { get; set; }
        void DrawTriangle(ColorRGB color, VertexBuffer vbx, int triangleIndice);
    }
}
