using System.Drawing;

public class Triangle : Object
{
    private static Bitmap _image = null;
    private static Bitmap image {
        get {
            if (_image is null)
            {
                int width = 100;
                int height = 100;
                _image = new Bitmap(width, height);
                using (Graphics g = Graphics.FromImage(_image))
                {

                    Brush brush = Brushes.Orange;


                    Point[] points = {
                        new Point(width / 2, 0),
                        new Point(0, height),
                        new Point(width, height)
                    };

                    g.FillPolygon(brush, points);
                }
            }
            return _image;
        }
    }

    private static int _weight;
    public override int Weight => _weight;
    
    public Triangle(PointF position, int weight = 500)
    {
        this.Name = "Triangle";
        _weight = weight;
        this.Image = image;
        this.Position = position;
        this.Size = new SizeF(100, 100);
    }
    public Triangle(int weight = 500) : this(new PointF(0, 0), weight) {}

    // public override PointF Center => new PointF(
    //     Position.X + Size.Width / 2,
    //     (Position.Y + (Position.Y + Size.Height) * 2) / 3
    //     );

    public override Triangle Clone()
        => new Triangle(this.Position, this.Weight);
}