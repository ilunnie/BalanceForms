using System.Drawing;

public static class Client
{
    private static string _mode = null;
    public static string Mode { 
        get => _mode; 
        set{
            if (_mode is null)
                _mode = value;
        }
    }
    public static SizeF Screen { get; set;  }
    public static PointF Cursor { get; set; }
    public static long Frame { get; set; }
}