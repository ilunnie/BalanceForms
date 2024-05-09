using BoschForms;
using BoschForms.Drawing;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;

public static class TutorialAnimations
{
    public static float Opacity { get; set; } = .5f;
    private static Bitmap _cursorImage = null;
    public static Bitmap CursorImage
    {
        get
        {
            if (_cursorImage is null)
            {
                _cursorImage = (Bitmap)(Bitmap.FromFile("assets/cursor.png"));
                _cursorImage = _cursorImage.SetOpacity((int)(255 * (1 - Opacity)));
            }
            return _cursorImage;
        }
        set => _cursorImage = value.SetOpacity((int)(255 * (1 - Opacity)));
    }

    private static Object ObjectRef = null;
    private static Image ImageRef = null;

    private static Bitmap _image = null;
    private static Bitmap Image
    {
        get
        {
            if (ObjectRef.Image != ImageRef || _image is null)
            {
                ImageRef = ObjectRef.Image;
                Bitmap original = (Bitmap)ImageRef;

                int alpha = (int)(255 * (1 - Opacity));
                _image = original.SetOpacity(alpha);
            }
            return _image;
        }
    }

    private static VirtualCursor cursor = null;
    private static long frame = 0;

    private static bool withObj = false;
    private static Object obj = null;
    private static Plate plate = null;
    public static bool DraginHold(this Graphics g, Object obj, Plate plate)
    {
        ObjectRef = obj;
        if (cursor is null)
        {
            float size = 50;
            cursor = new VirtualCursor(obj.Center, new SizeF(size, size))
            {
                Anchor = new PointF(size * .423f, size * .118f)
            };
        }

        RectangleF area = plate.Area;
        if (!withObj) cursor.Destiny = obj.Center;
        else cursor.Destiny = new PointF(area.Left + area.Width / 2, area.Top + area.Height * .75f);

        if (cursor.AtTheDestiny)
        {
            if (withObj)
            {
                TutorialAnimations.plate = null;
                TutorialAnimations.obj = null;
            }
            withObj = !withObj;
        }

        cursor.Move();

        if (withObj)
        {
            SizeF size = obj.Size;
            PointF point = new PointF(cursor.Position.X - size.Width / 2 + cursor.Anchor.X, cursor.Position.Y - size.Height / 2 + cursor.Anchor.Y);
            g.DrawImage(Image, point, size);
        }
        g.DrawImage(CursorImage, cursor.Rectangle);
        return cursor.AtTheDestiny;
    }
    public static bool DraginHold(Graphics g)
    {
        Game game = (Game)App.Page;
        var random = new Random();
        if (obj is null)
        {
            var objetcs = game.Objects;
            obj = objetcs.ElementAt(random.Next(objetcs.Count));
        }

        if (plate is null)
        {
            var plates = game.Balances.SelectMany(balance => balance.Plates);
            plate = plates.ElementAt(random.Next(plates.Count()));
        }

        return DraginHold(g, obj, plate);
    }

    private static int clicktick = 1000;
    private static bool click = false;
    public static bool clickButton(Graphics g, Button button, float clicksize = 10, float clickline = 5)
    {
        if (cursor is null)
        {
            float size = 50;
            cursor = new VirtualCursor(obj.Center, new SizeF(size, size))
            {
                Anchor = new PointF(size * .423f, size * .118f)
            };
        }

        cursor.Destiny = new PointF(button.Position.X + button.Size.Width / 2, button.Position.Y + button.Size.Height / 2);
        cursor.Move();

        if (click)
        {
            PointF point = new PointF(cursor.Position.X + cursor.Anchor.X, cursor.Position.Y + cursor.Anchor.Y);
            Pen pen = new Pen(Color.FromArgb((int)(255 * (1 - Opacity)), 255, 255, 255), clickline);
            g.DrawEllipse(point.X - clicksize, point.Y - clicksize, clicksize * 2, clicksize * 2, pen);
            pen.Dispose();
        }
        g.DrawImage(CursorImage, cursor.Rectangle);

        frame = Math.Min(frame + Client.Frame, clicktick);
        if (frame % clicktick == 0 && cursor.AtTheDestiny)
        {
            click = !click;
            frame = 0;
        }
        return cursor.AtTheDestiny;
    }
}