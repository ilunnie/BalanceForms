using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using BoschForms.Screen;

namespace BoschForms;

public static class App
{
    public static IPage Page { get; private set; }
    public static Color Background { get; set; }

    private static System.Windows.Forms.Form form = null;
    private static Bitmap bmp = null;
    private static Graphics g = null;
    private static  System.Windows.Forms.PictureBox pb = null;

    public static void Run()
    {
        Stopwatch stopwatch = new Stopwatch();
        ApplicationConfiguration.Initialize();

        pb = new  System.Windows.Forms.PictureBox()
        {
            Dock =  System.Windows.Forms.DockStyle.Fill
        };

        var timer = new  System.Windows.Forms.Timer
        {
            Interval = 20,
        };

        form = new System.Windows.Forms.Form
        {
            WindowState = System.Windows.Forms.FormWindowState.Maximized,
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.None,
            Controls = { pb }
        };
        pb.MouseMove += (o, e) =>
        {
            PointF position = new PointF(e.Location.X, e.Location.Y);
            float scaleX = Screen.Screen.Width / Client.Screen.Width;
            float scaleY = Screen.Screen.Height / Client.Screen.Height;
            Client.Cursor = new PointF(position.X * scaleX, position.Y * scaleY);
            Page.OnMouseMove();
        };

        pb.MouseDown += (o, e) =>
        {
            Page.OnMouseDown(e.Button);
        };

        pb.MouseUp += (o, e) =>
        {
            Page.OnMouseUp(e.Button);
        };

        form.Load += delegate
        {
            bmp = new Bitmap(pb.Width, pb.Height);
            pb.Image = bmp;

            g = Graphics.FromImage(bmp);
            g.InterpolationMode = InterpolationMode.NearestNeighbor;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;

            g.Clear(Color.DarkGray);
            pb.Refresh();

            Client.Screen = new SizeF(bmp.Width, bmp.Height);

            timer.Start();
            stopwatch.Start();
        };

        form.KeyDown += (o, e) =>
        {
            Page.OnKeyDown(o, e);
        };

        form.KeyUp += (o, e) =>
        {
            Page.OnKeyUp(o, e);
        };

        timer.Tick += delegate
        {
            g.Clear(Background);

            Page.Update();
            Page.Draw(g);
            pb.Refresh();

            Client.Frame = stopwatch.ElapsedMilliseconds;
            stopwatch.Restart();
        };

         System.Windows.Forms.Application.Run(form);
    }

    public static void SetPage(IPage page, bool load = true)
    {
        Page = page;
        if (load) page.Load();
    }

    public static void Close() => form.Close();
}