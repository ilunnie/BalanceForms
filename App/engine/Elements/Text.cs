using System.Drawing;
using System.Drawing.Drawing2D;
using BoschForms.Screen;

namespace BoschForms.Drawing;

public static partial class Elements
{
    public static void DrawString(this Graphics g, string text, PointF point, Font font, Brush brush, StringFormat format = null)
    {
        font = font.OnScreen();
        g.DrawString(text, font, brush, point.OnScreen());
    }

    public static void DrawString(this Graphics g, string text, RectangleF rect, Font font, Brush brush, StringFormat format = null)
    {
        font = font.OnScreen();
        g.DrawString(text, font, brush, rect.OnScreen(), format);
    }

    public static void DrawString(
        this Graphics g, RectangleF rect, string text, Font font, Brush brush, bool wrap = false,
        StringAlignment alignment = StringAlignment.Near, StringAlignment linealignment = StringAlignment.Center)
    {
        if (rect.Width <= 0 || rect.Height <= 0)
            return;

        RectangleF rectscreen = rect.OnScreen();

        using (GraphicsPath clipPath = new GraphicsPath())
        {
            clipPath.AddRectangle(rectscreen);

            g.SetClip(clipPath);

            using (StringFormat format = new StringFormat())
            {
                format.Alignment = alignment;
                format.LineAlignment = linealignment;
                if (!wrap) format.FormatFlags = StringFormatFlags.NoWrap;

                g.DrawString(text, rect, font, brush, format);
            }
        }

        g.ResetClip();
    }
}