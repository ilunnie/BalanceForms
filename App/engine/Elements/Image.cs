using System.Drawing;
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
}