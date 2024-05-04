using System.Drawing;

public class Styles
{
    // Rect Styles
    public Color BackgroundColor { get; set; } = Color.Empty;

    // Border Styles
    public Color BorderColor { get; set; } = Color.Empty;
    public int? BorderWidth { get; set; } = null;
    private (float, float, float, float)? _borderRays = null;
    public (float, float, float, float)? BorderRays { get => _borderRays; set => _borderRays = value; }
    public float BorderRadius { set => _borderRays = (value, value, value, value); }

    // Text Styles
    public Font Font { get; set; } = null;
    public Color Color { get; set; } = Color.Empty;
    public Font PlaceHolderFont { get; set; } = null;
    public Color PlaceHolderColor { get; set; } = Color.Empty;
}

public static class StylesExtension
{
    public static Styles Concat(this Styles a, Styles b)
        => new Styles() {
            BackgroundColor = a.BackgroundColor.IsEmpty ? b.BackgroundColor : a.BackgroundColor,
            BorderColor = a.BorderColor.IsEmpty ? b.BorderColor : a.BorderColor,
            BorderWidth = a.BorderWidth ?? b.BorderWidth,
            BorderRays = a.BorderRays ?? b.BorderRays,
            Font = a.Font ?? b.Font,
            Color = a.Color.IsEmpty ? b.Color : a.Color,
            PlaceHolderFont = a.PlaceHolderFont ?? b.PlaceHolderFont,
            PlaceHolderColor = a.PlaceHolderColor.IsEmpty ? b.PlaceHolderColor : a.PlaceHolderColor,
        };
}