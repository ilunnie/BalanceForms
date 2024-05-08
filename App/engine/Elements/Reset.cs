using System.Drawing;
using BoschForms.Screen;

namespace BoschForms.Drawing;

public static partial class Elements
{
    public static void DrawRectangle(this Graphics g, RectangleF rect, Pen pen)
        => g.DrawRectangle(pen, rect.OnScreen());
    public static void FillRectangle(this Graphics g, RectangleF rect, Brush brush)
        => g.FillRectangle(brush, rect.OnScreen());

    public static void DrawEllipse(this Graphics g, float x, float y, float width, float height, Pen pen)
        => g.DrawEllipse(pen, Screen.Screen.Rectangle(x, y, width, height));
    public static void FillEllipse(this Graphics g, float x, float y, float width, float height, Brush brush)
        => g.FillEllipse(brush, Screen.Screen.Rectangle(x, y, width, height));

    public static void DrawArc(this Graphics g, RectangleF rect, float startAngle, float sweepAngle, Pen pen)
        => g.DrawArc(pen, rect.OnScreen(), startAngle, sweepAngle);

    public static void DrawLine(this Graphics g, float x1, float y1, float x2, float y2, Pen pen)
        => g.DrawLine(pen, Screen.Screen.Position(x1, y1), Screen.Screen.Position(x2, y2));
    public static void DrawLine(this Graphics g, PointF pt1, PointF pt2, Pen pen)
        => g.DrawLine(pen, pt1.OnScreen(), pt2.OnScreen());
}