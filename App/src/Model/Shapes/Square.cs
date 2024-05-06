using System.Drawing;

public class Square : Object
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
                    Brush brush = Brushes.CadetBlue;

                    Point[] points = {
                        new Point(0, 0),
                        new Point(width, 0),
                        new Point(width, height),
                        new Point(0, height)
                    };

                    g.FillPolygon(brush, points);
                }
            }
            return _image;
        }
    }

    public Square(PointF position, int weight = 500)
    {
        this.Name = "Square";
        this.Weight = weight;
        this.Image = image;
        this.Position = position;
        this.Size = new SizeF(100, 100);
    }
    public Square(int weight = 500) : this(new PointF(0, 0), weight) {}

    public override Square Clone()
        => new Square(this.Position, this.Weight);
}