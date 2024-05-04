using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;

public static class App
{
    public static IPage Page { get; private set; }

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
            Client.Cursor = e.Location;
            Page.OnMouseMove(o, e);
        };

        pb.MouseWheel += (o, e) =>
        {
            Page.OnMouseMove(o, e);
        };

        pb.MouseDown += (o, e) =>
        {
            Page.OnMouseMove(o, e);
        };

        pb.MouseUp += (o, e) =>
        {
            Page.OnMouseMove(o, e);
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
            g.Clear(Color.Black);

            Page.Update();
            Page.Draw();
            pb.Refresh();

            Client.Frame = stopwatch.ElapsedMilliseconds;
            stopwatch.Restart();
        };

         System.Windows.Forms.Application.Run(form);
    }

    public static void SetPage(IPage page)
    {
        Page = page;
        page.Load();
    }

    public static void Close() => form.Close();
}