using System;
using System.Drawing;
using BoschForms.Forms;
using BoschForms.Drawing;
using Keys = System.Windows.Forms.Keys;
using MouseButtons = System.Windows.Forms.MouseButtons;
using System.Collections.Generic;
using BoschForms;

public class Button : IInput
{
    private string _name;
    public string Name { get => _name; set => _name = value; }

    private Form _value = null;
    public object Value { get => _value; set => _value = (Form)value; }

    private bool _enable = false;
    public bool Enable { get => _enable; set => _enable = value; }

    public event Action<object> onChange;
    public Action<object> OnChange { set => onChange += value; }

    public PointF Position { get; set; }
    public SizeF Size { get; set; } = new SizeF(250, 100);
    public string Label = "";

    public Styles Style { get; set; }
    public Styles Disable { get; set; }
    private Styles _style {
        get
        {
            if (this.isDisabled)
                return Disable.Concat(Style);
            return Style;
        }
    }

    public bool isDisabled { get; set; } = false;

    private List<string> errors = new();
    public List<string> Errors => errors;

    public Button(PointF position)
    {
        this.Position = position;
        this.Style = new Styles()
        {
            Font = new Font("Arial", 12),
            Color = Color.Black,
        };
        this.Disable = new Styles()
        {
            BackgroundColor = Color.FromArgb(225, 225, 225)
        };
    }
    public Button(float x, float y)
    : this(new PointF(x, y)) { }

    public bool Contains(PointF point)
        => new RectangleF(this.Position, this.Size).Contains(point);

    public void Draw(Graphics g)
    {
        Styles style = _style;
        RectangleF rect = new RectangleF(this.Position, this.Size);
        SolidBrush brush = new SolidBrush(style.BackgroundColor);
        g.FillRectangle(rect, style.BorderRays, brush);
        Pen pen = new Pen(style.BorderColor, style.BorderWidth);
        if (style.BorderWidth != 0)
            g.DrawRectangle(rect, style.BorderRays, pen);

        SolidBrush labelcolor = new SolidBrush(style.Color);
        g.DrawString(rect, Label, style.Font, labelcolor, alignment: StringAlignment.Center);

        labelcolor.Dispose();
    }

    public void KeyBoardDown(object o, System.Windows.Forms.KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter && !isDisabled)
            onChange?.Invoke(_value);
    }

    public void KeyBoardUp(object o, System.Windows.Forms.KeyEventArgs e)
    {
        
    }

    public void MouseKeyDown(System.Windows.Forms.MouseButtons button)
    {
        if (button == MouseButtons.Left && Contains(Client.Cursor) && !isDisabled)
            onChange?.Invoke(_value);
    }

    public void MouseKeyUp(System.Windows.Forms.MouseButtons button)
    {
        
    }
}
