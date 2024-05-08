using System;
using System.Drawing;
using System.Drawing.Imaging;
using BoschForms.Screen;

namespace BoschForms.Drawing;

public static partial class Elements
{
    public static void DrawImage(this Graphics g, Bitmap img, RectangleF rect)
        => g.DrawImage(img, rect.OnScreen());
    public static void DrawImage(this Graphics g, Bitmap img, PointF point, SizeF size)
    {
        point = point.OnScreen();
        size = size.OnScreen();
        g.DrawImage(img, point.X, point.Y, size.Width, size.Height);
    }
    public static void DrawImage(this Graphics g, Bitmap img, SizeF size)
        => DrawImage(g, img, new PointF(0, 0), size);

    public static void DrawImage(this Graphics g, Image img, RectangleF rect)
        => g.DrawImage(img, rect.OnScreen());
    public static void DrawImage(this Graphics g, Image img, PointF point, SizeF size)
    {
        point = point.OnScreen();
        size = size.OnScreen();
        g.DrawImage(img, point.X, point.Y, size.Width, size.Height);
    }
    public static void DrawImage(this Graphics g, Image img, SizeF size)
        => DrawImage(g, img, new PointF(0, 0), size);


    public static Bitmap SetOpacity(this Bitmap bitmap, int alpha)
    {
        Bitmap bmp = new Bitmap(bitmap.Width, bitmap.Height, PixelFormat.Format32bppArgb);
        for (int y = 0; y < bitmap.Height; y++)
            for (int x = 0; x < bitmap.Width; x++)
            {
                Color pixel = bitmap.GetPixel(x, y);
                Color transparent = Color.FromArgb(Math.Max(0, pixel.A - alpha), pixel.R, pixel.G, pixel.B);
                bmp.SetPixel(x, y, transparent);
            }

        return bmp;
    }
}