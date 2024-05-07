using System.Drawing;
using BoschForms;

public static class Cursor
{
    public static Object Object { get; set; } = null;

    public static void Move()
    {
        PointF point = Client.Cursor;
        if (Object is not null)
            Object.Position = new PointF(point.X - Object.Width / 2, point.Y - Object.Height / 2);
    }

    public static void Click()
    {
        if (App.Page is not Game)
            return;
        Game game = App.Page as Game;

        foreach (var obj in game.Objects)
            if (obj.Contains(Client.Cursor))
                Object = obj;
    }

    public static void Drop()
    {
        if (App.Page is not Game || Object is null)
            return;
        Game game = App.Page as Game;

        Plate area = null;
        foreach (var balance in game.Balances)
            foreach (var plate in balance.Plates)
                if (plate.Area.Contains(Client.Cursor))
                    area = plate;

        if (area is not null)
        {
            area.Objects.Add(Object);
            game.RemoveObject(Object);
        }
        Object = null;
    }
}