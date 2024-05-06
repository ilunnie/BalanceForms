using System.Drawing;

public class Balanca
{
    public Image Image { get; set; }
    public PointF Anchor { get; set; }

    public PointF Position { get; set; }
    public SizeF Size { get; set; }
    public RectangleF Rectangle => new RectangleF(Position, Size);
    public float Distance { get; set; }
}