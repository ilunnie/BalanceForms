using System.Drawing;
using System.Drawing.Drawing2D;
using BoschForms.Screen;

namespace BoschForms.Drawing;

public static partial class Elements
{
    public static void DrawRectangle(
        this Graphics g,
        RectangleF rect,
        (float, float, float, float) radius,
        Pen pen)
    {
        var (topLeftRadius, topRightRadius, bottomRightRadius, bottomLeftRadius) = radius;

        var topLeft = Screen.Screen.Rectangle(rect.Location, new SizeF(2 * topLeftRadius, 2 * topLeftRadius));
        var topRight = Screen.Screen.Rectangle(new PointF(rect.Right - 2 * topRightRadius, rect.Top), new SizeF(2 * topRightRadius, 2 * topRightRadius));
        var bottomRight = Screen.Screen.Rectangle(new PointF(rect.Right - 2 * bottomRightRadius, rect.Bottom - 2 * bottomRightRadius), new SizeF(2 * bottomRightRadius, 2 * bottomRightRadius));
        var bottomLeft = Screen.Screen.Rectangle(new PointF(rect.Left, rect.Bottom - 2 * bottomLeftRadius), new SizeF(2 * bottomLeftRadius, 2 * bottomLeftRadius));

        if (topLeftRadius > 0) g.DrawArc(topLeft, 180, 90, pen);
        if (topRightRadius > 0) g.DrawArc(topRight, 270, 90, pen);
        if (bottomRightRadius > 0) g.DrawArc(bottomRight, 0, 90, pen);
        if (bottomLeftRadius > 0) g.DrawArc(bottomLeft, 90, 90, pen);

        g.DrawLine(rect.Left + topLeftRadius, rect.Top, rect.Right - topRightRadius, rect.Top, pen);
        g.DrawLine(rect.Right, rect.Top + topRightRadius, rect.Right, rect.Bottom - bottomRightRadius, pen);
        g.DrawLine(rect.Left + bottomLeftRadius, rect.Bottom, rect.Right - bottomRightRadius, rect.Bottom, pen);
        g.DrawLine(rect.Left, rect.Top + topLeftRadius, rect.Left, rect.Bottom - bottomLeftRadius, pen);
    }
    public static void DrawRectangle(this Graphics g, RectangleF rect, float radius, Pen pen)
        => DrawRectangle(g, rect, (radius, radius, radius, radius), pen);

    public static void FillRectangle(
        this Graphics g,
        RectangleF rect,
        (float, float, float, float) radius,
        Brush brush)
    {
        var (topLeftRadius, topRightRadius, bottomRightRadius, bottomLeftRadius) = radius;

        var topLeft = Screen.Screen.Rectangle(rect.Location, new SizeF(2 * topLeftRadius, 2 * topLeftRadius));
        var topRight = Screen.Screen.Rectangle(new PointF(rect.Right - 2 * topRightRadius, rect.Top), new SizeF(2 * topRightRadius, 2 * topRightRadius));
        var bottomRight = Screen.Screen.Rectangle(new PointF(rect.Right - 2 * bottomRightRadius, rect.Bottom - 2 * bottomRightRadius), new SizeF(2 * bottomRightRadius, 2 * bottomRightRadius));
        var bottomLeft = Screen.Screen.Rectangle(new PointF(rect.Left, rect.Bottom - 2 * bottomLeftRadius), new SizeF(2 * bottomLeftRadius, 2 * bottomLeftRadius));

        GraphicsPath path = new GraphicsPath();
        if (topLeftRadius > 0)
            path.AddArc(topLeft, 180, 90);
        else
        {
            path.AddLine(topLeft.X, topLeft.Y + topLeft.Height, topLeft.X, topLeft.Y);
            path.AddLine(topLeft.X, topLeft.Y, topLeft.X + topLeft.Width, topLeft.Y);
        }
        if (topRightRadius > 0)
            path.AddArc(topRight, 270, 90);
        else
        {
            path.AddLine(topRight.X, topRight.Y, topRight.X + topRight.Width, topRight.Y);
            path.AddLine(topRight.X + topRight.Width, topRight.Y, topRight.X + topRight.Width, topRight.Y + topRight.Height);
        }
        if (bottomRightRadius > 0)
            path.AddArc(bottomRight, 0, 90);
        else
        {
            path.AddLine(bottomRight.X + bottomRight.Width, bottomRight.Y, bottomRight.X + bottomRight.Width, bottomRight.Y + bottomRight.Height);
            path.AddLine(bottomRight.X, bottomRight.Y, bottomRight.X + bottomRight.Width, bottomRight.Y + bottomRight.Height);
        }
        if (bottomLeftRadius > 0)
            path.AddArc(bottomLeft, 90, 90);
        else
        {
            path.AddLine(bottomLeft.X + bottomLeft.Width, bottomLeft.Y + bottomLeft.Height, bottomLeft.X, bottomLeft.Y + bottomLeft.Height);
            path.AddLine(bottomLeft.X, bottomLeft.Y + bottomLeft.Height, bottomLeft.X, bottomLeft.Y);
        }

        path.CloseFigure();

        g.FillPath(brush, path);
    }
    public static void FillRectangle(this Graphics g, RectangleF rect, float radius, Brush brush)
        => FillRectangle(g, rect, (radius, radius, radius, radius), brush);
}