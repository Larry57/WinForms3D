using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinForms3D {

    // Taken from the highly efficient https://referencesource.microsoft.com/#System.Drawing/commonui/System/Drawing/Color.cs

    public struct ColorRGB {

        const int ARGBAlphaShift = 24;
        const int ARGBRedShift = 16;
        const int ARGBGreenShift = 8;
        const int ARGBBlueShift = 0;

        long value;

        ColorRGB(byte r, byte g, byte b) => value = (unchecked((uint)(r << ARGBRedShift | g << ARGBGreenShift | b << ARGBBlueShift | 255 << ARGBAlphaShift))) & 0xffffffff;

        public byte R { get => (byte)((value >> ARGBRedShift) & 0xFF); }
        public byte G { get => (byte)((value >> ARGBGreenShift) & 0xFF); }
        public byte B { get => (byte)((value >> ARGBBlueShift) & 0xFF); }

        public int Color { get => (int)value; }

        public static ColorRGB operator *(float f, ColorRGB color) => new ColorRGB((byte)(f * color.R), (byte)(f * color.G), (byte)(f * color.B));

        public static ColorRGB Yellow { get; } = new ColorRGB(255, 255, 0);
        public static ColorRGB Blue { get; } = new ColorRGB(0, 0, 255);
        public static ColorRGB Gray { get; } = new ColorRGB(127, 127, 127);
        public static ColorRGB Green { get; } = new ColorRGB(0, 255, 0);
        public static ColorRGB Red { get; } = new ColorRGB(255, 0, 0);
        public static ColorRGB Magenta { get; } = new ColorRGB(255, 0, 255);
    }
}
