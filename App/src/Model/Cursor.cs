using System.Drawing;
using BoschForms;

public static class Cursor
{
    public static Object Object { get; set; } = null;

    public static void Move()
    {
        PointF point = Client.Cursor;
        if (Object is not null)
            Object.Position = new PointF(point.X - Object.Width / 2, point.Y - Object.Weight / 2);
    }

    public static void Click()
    {
        //ToDo Get Table object
    }

    public static void Drop()
    {
        //ToDo Drop Object
    }
}