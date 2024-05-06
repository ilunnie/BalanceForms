using System.Drawing;

public class Balanca
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

    public Balanca(float x, float y, float width = 200, float height = 200, float distance = 300)
    {
        this.Position = new PointF(x, y);
        this.Size = new SizeF(width, height);
        this.Anchor = new PointF(width * .5f, y * .27f);
        this.Distance = distance;

        this.Plates = new Plate[] { new Plate(), new Plate() };
        this.Left.Balance = this;
        this.Right.Balance = this;

        this.Left.Size = new SizeF(width * 1.43f, height * .65f);
        this.Right.Size = new SizeF(width * 1.43f, height * .65f);
    }

    private float angle = 0;
    public void Update()
    {
        this.Left.Update(angle);
        this.Right.Update(angle + 180);
        angle++;
    }

    public void Draw(Graphics g)
    {
        g.DrawImage(this.Image, this.Rectangle);
        this.Left.Draw(g);
        this.Right.Draw(g);
    }
}