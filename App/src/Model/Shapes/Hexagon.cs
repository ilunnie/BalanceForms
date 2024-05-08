using System.Drawing;

public class Hexagon : Object
{
    private static Bitmap _image = null;
    private static Bitmap image
    {
        get
        {
            if (_image is null)
            {
                int width = 100;
                int height = 100;
                _image = new Bitmap(width, height);
                using (Graphics g = Graphics.FromImage(_image))
                {
                    Brush brush = Brushes.Green;

                    Point[] points = {
                        new Point(width / 4, 0),
                        new Point(width * 3 / 4, 0),
                        new Point(width, height / 2),
                        new Point(width * 3 / 4, height),
                        new Point(width / 4, height),
                        new Point(0, height / 2)
                    };

                    g.FillPolygon(brush, points);
                }
            }
            return _image;
        }
    }

    private static int _weight;
    public override int Weight => _weight;

    public Hexagon(PointF position, int weight = 500)
    {
        this.Name = "Hexagon";
        _weight = weight;
        this.Image = image;
        this.Position = position;
        this.Size = new SizeF(100, 100);
    }
    public Hexagon(int weight = 500) : this(new PointF(0, 0), weight) { }

    public override Hexagon Clone()
        => new Hexagon(this.Position, this.Weight);
}