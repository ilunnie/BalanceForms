using BoschForms.Drawing;
using BoschForms.Screen;
using BoschForms.Forms;

using Graphics = System.Drawing.Graphics;
using PointF = System.Drawing.PointF;
using SizeF = System.Drawing.SizeF;
using RectangleF = System.Drawing.RectangleF;
using System;
using System.Drawing;
using System.Windows.Forms;

public class TextInput : IInput
{
    private string _name;
    public string Name { get => _name; set => _name = value; }

    private string _value;
    public object Value
    {
        get => _value;
        set
        {
            _value = value.ToString();
            OnChange?.Invoke(_value);
        }
    }

    private bool _enable = false;

    public event Action<string> OnChange;

    public bool Enable { get => _enable; set => _enable = value; }

    public PointF Position { get; set; }
    public SizeF Size { get; set; } = new SizeF(250, 100);

    public string PlaceHolder { get; set; } = "";

    public Styles Style { get; set; }
    public Styles Hover { get; set; }
    private Styles _style
    {
        get
        {
            if (this.Enable)
                return Hover.Concat(Style);
            return Style;
        }
    }

    public TextInput(PointF position, string name = "", string value = "")
    {
        this.Position = position;
        this.Name = name;
        this._value = value;
    }
    public TextInput(float x, float y, string name = "", string value = "")
    : this(new PointF(x, y), name, value)
    {
    }

    public bool Contains(PointF point)
        => new RectangleF(this.Position, this.Size).Contains(point);

    public void Draw(Graphics g)
    {
        RectangleF rect = new RectangleF(this.Position, this.Size);
        SolidBrush brush = new SolidBrush(_style.BackgroundColor);
        g.FillRectangle(rect, _style.BorderRays.Value, brush);
        Pen pen = new Pen(_style.BorderColor, _style.BorderWidth.Value);
        if (_style.BorderWidth != 0)
            g.DrawRectangle(rect, _style.BorderRays.Value, pen);

        pen.Dispose();
    }

    public void KeyBoardDown(object o, System.Windows.Forms.KeyEventArgs e)
    {
        if (!this.Enable)
            return;
        if (char.IsLetterOrDigit((char)e.KeyCode))
        {
            bool capsLock = Control.IsKeyLocked(Keys.CapsLock);
            bool shiftPressed = (e.Modifiers & Keys.Shift) != 0;

            if ((capsLock && !shiftPressed) || (!capsLock && shiftPressed))
                _value += char.ToUpper((char)e.KeyCode);
            else
                _value += char.ToLower((char)e.KeyCode);
        }
    }

    public void KeyBoardUp(object o, System.Windows.Forms.KeyEventArgs e)
    {

    }

    public void MouseKeyDown(System.Windows.Forms.MouseButtons button)
    {

    }

    public void MouseKeyUp(System.Windows.Forms.MouseButtons button)
    {

    }
}
