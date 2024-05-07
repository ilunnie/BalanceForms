using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using BoschForms.Drawing;
using BoschForms.Screen;

public class Plate
{
    public Image image { get; set; } = Image.FromFile("assets/prato.png");
    public Balance Balance { get; set; }
    public List<Object> Objects { get; set; } = new List<Object>();
    public SizeF ObjectsSize { get; set; } = new SizeF(75, 75);
    public bool ClassifyObject { get; set; } = true;
    public int Count => Objects.Count;
    public int Weight => Objects.Sum(obj => obj.Weight);

    public PointF Position { get; set; }
    public float X => Position.X;
    public float Y => Position.Y;
    public SizeF Size { get; set; }
    public float Width => Size.Width;
    public float Height => Size.Height;
    public RectangleF Rectangle => new RectangleF(Position, Size);
    public RectangleF Area
    {
        get
        {
            float height = 2 * Width;
            return new RectangleF(X, Y - height, Width, height);
        }
    }
    public PointF Anchor { get; set; } = new PointF(0, 0);
    public PointF AnchorScreen => new PointF(Position.X + Anchor.X, Position.Y + Anchor.Y);
    public float Speed { get; set; } = 3;

    private float? lastangle = null;
    public void Update(float angle)
    {
        if (lastangle.HasValue)
        {
            float angleDifference = angle - lastangle.Value;

            if (angleDifference > 180)
                angleDifference -= 360;
            else if (angleDifference < -180)
                angleDifference += 360;

            angle = lastangle.Value + MathF.Sign(angleDifference) * Math.Min(MathF.Abs(angleDifference), Speed);
        }

        lastangle = angle;
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
        g.FillEllipse(Position.X + Anchor.X - r, Position.Y + Anchor.Y - r, r * 2, r * 2, Brushes.White);
        Elements.DrawImage(g, this.image, this.Rectangle);

        DrawShapes(g);

        g.DrawRectangle(this.Area, this.Area.Contains(BoschForms.Client.Cursor) ? Pens.Red : Pens.Blue);
    }

    private void DrawShapes(Graphics g)
    {
        RectangleF area = this.Area;
        SizeF size = this.ObjectsSize;

        float x = area.X;
        float y = area.Bottom - size.Height;

        int max_column = (int)(area.Width / size.Width);
        float error = area.Width % size.Width;

        Dictionary<Type, (RectangleF, int)> classes = new Dictionary<Type, (RectangleF, int)>();

        float column = 0;
        float line = 0;
        foreach (var item in Objects)
        {
            if (ClassifyObject && classes.ContainsKey(item.GetType()))
            {
                (RectangleF, int) value = classes[item.GetType()];
                classes[item.GetType()] = (value.Item1, value.Item2 + 1);
                continue;
            }

            PointF position = new PointF(x + (size.Width * column) + error / 2, y - (size.Height * line));
            var obj = item.Clone();
            obj.Size = size;
            obj.Position = position;
            obj.Draw(g);

            if (ClassifyObject)
                classes.Add(item.GetType(), (obj.Rectangle, 1));

            column++;
            if (!(column < max_column))
            {
                column = 0;
                line++;
            }
        }

        foreach (var type in classes)
        {
            Font font = new Font("Arial", 10);
            g.DrawString(type.Value.Item1, type.Value.Item2.ToString(), font, Brushes.White, alignment: StringAlignment.Center);
        }
    }
}