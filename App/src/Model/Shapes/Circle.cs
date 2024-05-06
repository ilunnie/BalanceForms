using System.Drawing;

public class Circle : Object
{
    private static Bitmap _image = null;
    private static Bitmap image
    {
        get
        {
            if (_image is null)
            {
                int diameter = 100;
                _image = new Bitmap(diameter, diameter);
                using (Graphics g = Graphics.FromImage(_image))
                {
                    Brush brush = Brushes.Red;

                    g.FillEllipse(brush, 0, 0, diameter, diameter);
                }
            }
            return _image;
        }
    }

    public Circle(PointF position, int weight = 500)
    {
        this.Name = "Circle";
        this.Weight = weight;
        this.Image = image;
        this.Position = position;
        this.Size = new SizeF(100, 100);
    }
    public Circle(int weight = 500) : this(new PointF(0, 0), weight) {}

    public override Circle Clone()
        => new Circle(this.Position, this.Weight);
}