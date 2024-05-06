using System;
using System.Drawing;

public abstract class Object
{
    public string Name { get; protected set; }
    public int Weight { get; protected set; }
    public Image Image { get; protected set; }
    public SizeF Size { get; set; }
    public PointF Position { get; set; }

    public float X => Position.X;
    public float Y => Position.Y;
    public float Width => Size.Width;
    public float Height => Size.Height;
    public RectangleF HitBox => new RectangleF(Position, Size);

    public virtual float CenterX => X + Width / 2;
    public virtual float CenterY => Y + Height / 2;
    public virtual PointF Center => new PointF(CenterX, CenterY);

    public virtual void Draw(Graphics g)
        => g.DrawImage(Image, HitBox);

    public abstract Object Clone();
}