using System;
using System.Drawing;
using System.Linq;
using BoschForms.Drawing;
using Screen = BoschForms.Screen.Screen;

public class Balance
{
    public Image Image { get; set; } = Image.FromFile("assets/Base (1).png");

    public PointF Position { get; set; }
    public SizeF Size { get; set; }
    public RectangleF Rectangle => new RectangleF(Position, Size);

    public PointF Anchor { get; set; }
    public float Distance { get; set; }
    public Plate[] Plates{ get; private set; }
    public Plate Left => Plates[0];
    public Plate Right => Plates[1];

    public Weight Weight { get; private set; } = Weight.Balanced;
    public int Count => Plates.Sum(plate => plate.Count);

    public Balance(float x, float y, float width = 150, float height = 150, float distance = 200)
    {
        this.Position = new PointF(x, y);
        this.Size = new SizeF(width, height);
        this.Anchor = new PointF(width * .5f, height * .27f);
        this.Distance = distance;

        this.Plates = new Plate[] { new Plate(), new Plate() };
        this.Left.Balance = this;
        this.Right.Balance = this;

        SizeF plateSize = new SizeF(width * 1.43f, height * .65f);
        this.Left.Size = plateSize;
        this.Right.Size = plateSize;

        PointF platePoint = new PointF(plateSize.Width * .5f, plateSize.Height * .77f);
        this.Left.Anchor = platePoint;
        this.Right.Anchor = platePoint;

        this.Left.Update(180);
        this.Right.Update(0);
    }

    public void Update()
    {
        int Movement = 20;
        int angle = 180 + (-(int)Weight * Movement);
        this.Left.Update(angle);
        this.Right.Update(angle + 180);
    }

    public void Draw(Graphics g)
    {
        Pen pen = new Pen(Color.FromArgb(57, 74, 92), 20 * Math.Max(Screen.ScaleX, Screen.ScaleY));
        g.DrawLine(Left.AnchorScreen, Right.AnchorScreen, pen);
        float rx = this.Size.Width * .125f * Screen.ScaleX;
        float ry = this.Size.Height * .125f * Screen.ScaleY;
        g.FillEllipse(Position.X + Anchor.X - rx, Position.Y + Anchor.Y - ry, rx*2, ry*2, Brushes.White);
        Elements.DrawImage(g, this.Image, this.Rectangle);
        this.Left.Draw(g);
        this.Right.Draw(g);
    }

    public void ToWeight()
    {
        int left = this.Left.Weight;
        int right = this.Right.Weight;
        this.Weight = (Weight)left.CompareTo(right);
    }
}