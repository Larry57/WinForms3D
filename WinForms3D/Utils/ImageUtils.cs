using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace WinForms3D {
    public static class ImageUtils {
        
        // Taken from https://stackoverflow.com/a/11740297/24472

        public static Bitmap FillBitmap(Bitmap bmp, int[] buffer, int width, int height) {
            var bmpData = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.WriteOnly, bmp.PixelFormat);
            Marshal.Copy(buffer, 0, bmpData.Scan0, buffer.Length);
            bmp.UnlockBits(bmpData);
            return bmp;
        }
    }
}
