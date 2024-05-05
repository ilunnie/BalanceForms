using System.Drawing;

namespace BoschForms.Screen;

public static class Screen
{
    public static SizeF Dimension { get; set; } = new SizeF(1920, 1080);
    public static float Width => Dimension.Width;
    public static float Height => Dimension.Height;
    public static PointF Center => new PointF(Width / 2, Height / 2);
    public static float CenterX => Width / 2;
    public static float CenterY => Height / 2;

    private static float ScaleX => Client.Screen.Width / Dimension.Width;
    private static float ScaleY => Client.Screen.Height / Dimension.Height;

    public static PointF Position(float x, float y)
        =>  new PointF(x * ScaleX, y * ScaleY);
    public static PointF Position(PointF point)
        => Position(point.X, point.Y);

    public static SizeF Size(float width, float height)
        => new SizeF(width * ScaleX, height * ScaleY);
    public static SizeF Size(SizeF size)
        => Size(size.Width, size.Height);

    public static RectangleF Rectangle(float x, float y, float width, float height)
        => new RectangleF(
            Position(x, y),
            Size(width, height)
        );
    public static RectangleF Rectangle(PointF point, SizeF size)
        => Rectangle(point.X, point.Y, size.Width, size.Height);
    public static RectangleF Rectangle(RectangleF rect)
        => Rectangle(rect.X, rect.Y, rect.Width, rect.Height);

    public static Font Font(string name, float size, FontStyle style = FontStyle.Regular)
        => new Font(name, size * ScaleY, style);
    public static Font Font(Font font)
        => Font(font.Name, font.Size, font.Style);

    public static PointF OnScreen(this PointF point)
        => Position(point);
    public static SizeF OnScreen(this SizeF size)
        => Size(size);
    public static RectangleF OnScreen(this RectangleF rect)
        => Rectangle(rect);
    public static Font OnScreen(this Font font)
        => Font(font);
}