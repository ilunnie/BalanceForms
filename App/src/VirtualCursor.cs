using System;
using System.Collections.Generic;
using System.Drawing;

public class VirtualCursor
{
    public PointF Destiny { get; set; } = PointF.Empty;
    public PointF Position { get; private set; }
    public PointF Anchor { get; set; }
    public SizeF Size { get; private set; }
    public float Speed { get; set; }

    public RectangleF Rectangle => new RectangleF(Position, Size);

    public float Distance
    {
        get
        {
            PointF point = new PointF(Position.X + Anchor.X, Position.Y + Anchor.Y);
            float x = Destiny.X - point.X;
            float y = Destiny.Y - point.Y;
            return MathF.Sqrt(x * x + y * y);
        }
    }
    public bool AtTheDestiny => Distance <= Speed || Destiny.IsEmpty;
    public VirtualCursor(PointF position, SizeF size, float speed = 10)
    {
        this.Position = position;
        this.Size = size;
        this.Speed = speed;
        this.Anchor = new PointF(size.Width / 2, size.Height / 2);
    }

    public void Move()
    {
        if (AtTheDestiny) this.Position = new PointF(Destiny.X - Anchor.X, Destiny.Y - Anchor.Y);
        if (!Destiny.IsEmpty && !AtTheDestiny)
        {
            float deltaX = Destiny.X - (Position.X + Anchor.X);
            float deltaY = Destiny.Y - (Position.Y + Anchor.Y);

            float totalDistance = MathF.Sqrt(deltaX * deltaX + deltaY * deltaY);

            float ratio = Speed / totalDistance;

            this.Position = new PointF(Position.X + deltaX * ratio, Position.Y + deltaY * ratio);
        }
    }
}