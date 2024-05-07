using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using BoschForms.Drawing;
using BoschForms.Screen;

public class Plate
{
    public Image image { get; set; } = Image.FromFile("assets/prato.png");
    public Balanca Balance { get; set; }
    public List<Object> Objects { get; set; } = new List<Object>();
    public int Count => Objects.Count;
    public int Weight => Objects.Sum(obj => obj.Weight);

    public PointF Position { get; set; }
    public float X => Position.X;
    public float Y => Position.Y;
    public SizeF Size { get; set; }
    public float Width => Size.Width;
    public float Height => Size.Height;
    public RectangleF Rectangle => new RectangleF(Position, Size);
    public RectangleF Area {
        get {
            float height = 2 * Width;
            return new RectangleF(X, Y - height, Width, height);
        }
    }
    public PointF Anchor { get; set; } = new PointF(0, 0);
    public PointF AnchorScreen => new PointF(Position.X + Anchor.X, Position.Y + Anchor.Y);

    public void Update(float angle)
    {
        float radius = angle * MathF.PI / 180;
        float x = Balance.Anchor.X + Balance.Position.X;
        float y = Balance.Anchor.Y + Balance.Position.Y;
        float d = Balance.Distance;
        this.Position = new PointF(
            x + d * MathF.Cos(radius) - Anchor.X,
            y + d * MathF.Sin(radius) - Anchor.Y
        );
    }

    public void Draw(Graphics g)
    {
        float r = 15 * Math.Max(Screen.ScaleX, Screen.ScaleY);
        g.FillEllipse(Position.X + Anchor.X - r, Position.Y + Anchor.Y - r, r*2, r*2, Brushes.White);
        Elements.DrawImage(g, this.image, this.Rectangle);

        g.DrawRectangle(this.Area, Pens.Blue);
    }
}