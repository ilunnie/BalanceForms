using System.Drawing;

public class Styles
{
    // Rect Styles
    public Color BackgroundColor { get; set; } = Color.Empty;

    // Border Styles
    public Color BorderColor { get; set; } = Color.Empty;
    public int? _borderWidth { get; set; } = null;
    public int BorderWidth { get => _borderWidth ?? 0; set => _borderWidth = value; }
    public (float, float, float, float)? _borderRays { get; set; } = null;
    public (float, float, float, float) BorderRays { get => _borderRays ?? (0, 0, 0, 0); set => _borderRays = value; }
    public float BorderRadius { set => _borderRays = (value, value, value, value); }

    // Text Styles
    public Font Font { get; set; } = null;
    public Color Color { get; set; } = Color.Empty;
    public Font PlaceHolderFont { get; set; } = null;
    public Color PlaceHolderColor { get; set; } = Color.Empty;
    public Font ErrorFont { get; set; } = null;
    public Color ErrorColor { get; set; } = Color.Empty;

    // Cursor
    public Color CursorColor { get; set; } = Color.Empty;
    public float CursorWidth { get; set; } = 2;
    public int CursorTick { get; set; } = 120;
}

public static class StylesExtension
{
    public static Styles Concat(this Styles a, Styles b)
        => new Styles() {
            BackgroundColor = a.BackgroundColor.IsEmpty ? b.BackgroundColor : a.BackgroundColor,
            BorderColor = a.BorderColor.IsEmpty ? b.BorderColor : a.BorderColor,
            _borderWidth = a._borderWidth ?? b._borderWidth,
            _borderRays = a._borderRays is null ? b._borderRays : a._borderRays,
            Font = a.Font ?? b.Font,
            Color = a.Color.IsEmpty ? b.Color : a.Color,
            PlaceHolderFont = a.PlaceHolderFont ?? b.PlaceHolderFont,
            PlaceHolderColor = a.PlaceHolderColor.IsEmpty ? b.PlaceHolderColor : a.PlaceHolderColor,
            CursorColor = a.CursorColor.IsEmpty ? b.CursorColor : a.CursorColor,
            CursorWidth = a.CursorWidth,
            CursorTick = a.CursorTick,
            ErrorFont = a.ErrorFont ?? b.ErrorFont,
            ErrorColor = a.ErrorColor.IsEmpty ? b.ErrorColor : a.ErrorColor,
        };
}