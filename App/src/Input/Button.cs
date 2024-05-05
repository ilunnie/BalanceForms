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
    }
    public Button(float x, float y)
    : this(new PointF(x, y)) { }

    public bool Contains(PointF point)
        => new RectangleF(this.Position, this.Size).Contains(point);

    public void Draw(Graphics g)
    {
        RectangleF rect = new RectangleF(this.Position, this.Size);
        SolidBrush brush = new SolidBrush(Style.BackgroundColor);
        g.FillRectangle(rect, Style.BorderRays, brush);
        Pen pen = new Pen(Style.BorderColor, Style.BorderWidth);
        if (Style.BorderWidth != 0)
            g.DrawRectangle(rect, Style.BorderRays, pen);

        SolidBrush labelcolor = new SolidBrush(Style.Color);
        g.DrawString(rect, Label, Style.Font, labelcolor, alignment: StringAlignment.Center);

        labelcolor.Dispose();
    }

    public void KeyBoardDown(object o, System.Windows.Forms.KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
            onChange?.Invoke(_value);
    }

    public void KeyBoardUp(object o, System.Windows.Forms.KeyEventArgs e)
    {
        
    }

    public void MouseKeyDown(System.Windows.Forms.MouseButtons button)
    {
        if (button == MouseButtons.Left && Contains(Client.Cursor))
            onChange?.Invoke(_value);
    }

    public void MouseKeyUp(System.Windows.Forms.MouseButtons button)
    {
        
    }
}
