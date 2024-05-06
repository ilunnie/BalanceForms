using BoschForms.Drawing;
using BoschForms.Forms;

using Graphics = System.Drawing.Graphics;
using PointF = System.Drawing.PointF;
using SizeF = System.Drawing.SizeF;
using RectangleF = System.Drawing.RectangleF;
using System;
using System.Drawing;
using System.Windows.Forms;
using BoschForms;
using System.Collections.Generic;

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
            onChange?.Invoke(_value);
        }
    }

    private bool _enable = false;

    public event Action<object> onChange;
    public Action<object> OnChange { set => onChange += value; }

    public bool Enable { get => _enable; set => _enable = value; }

    public PointF Position { get; set; }
    public SizeF Size { get; set; } = new SizeF(250, 50);

    public string PlaceHolder { get; set; } = "";
    private int? _cursor = null;
    private int Cursor
    {
        get
        {
            if (_cursor is null)
                _cursor = _value.Length;
            return _cursor.Value;
        }
        set => _cursor = value;
    }
    private bool DrawCursor = false;
    private int visible = 0;

    public float Margin = 10;

    public Styles Style { get; set; }
    public Styles Selected { get; set; }
    public Styles WithError { get; set; }
    public Styles Disable { get; set; }
    private Styles _style
    {
        get
        {
            if (this.isDisabled)
                return Disable.Concat(Style);
            Styles style = new Styles();
            if (errors.Count > 0)
                style = style.Concat(WithError);
            if (this.Enable)
                style = style.Concat(Selected);
            return style.Concat(Style);
        }
    }

    private List<string> errors = new();
    public List<string> Errors => errors;
    private void ClearErrors(object obj) => errors.Clear();

    private long Frame = 0;

    public bool isDisabled { get; set; } = false;

    public TextInput(PointF position, string name = "", string value = "")
    {
        this.Position = position;
        this.Name = name;
        this._value = value;

        this.Style = new Styles()
        {
            Font = new Font("Arial", 12),
            Color = Color.Black,
            PlaceHolderFont = new Font("Arial", 12),
            PlaceHolderColor = Color.FromArgb(160, 100, 100, 100),
        };
        this.Selected = new Styles()
        {
            CursorColor = Color.Gray
        };
        this.WithError = new Styles()
        {
            BackgroundColor = Color.FromArgb(255, 180, 180),
            BorderColor = Color.Red,
            BorderWidth = 2,
            Color = Color.Red,
            PlaceHolderColor = Color.Red,
            ErrorColor = Color.Red,
            ErrorFont = new Font("Arial", 10)
        };
        this.Disable = new Styles()
        {
            BackgroundColor = Color.FromArgb(225, 225, 225)
        };
        onChange += ClearErrors;
    }
    public TextInput(float x, float y, string name = "", string value = "")
    : this(new PointF(x, y), name, value) { }

    public bool Contains(PointF point)
        => new RectangleF(this.Position, this.Size).Contains(point);

    public void Draw(Graphics g)
    {
        Styles style = this._style;
        RectangleF rect = new RectangleF(this.Position, this.Size);
        RectangleF textrect = new RectangleF(rect.X + Margin, rect.Y, rect.Width - Margin * 2, rect.Height);
        while (true)
        {
            if (visible > this.Cursor)
                visible--;
            else if (g.MeasureString(_value.Substring(visible, this.Cursor - visible), style.Font).Width > textrect.Width)
                visible++;
            else
                break;
        }
        string text = _value.Substring(visible);
        SolidBrush brush = new SolidBrush(style.BackgroundColor);
        g.FillRectangle(rect, style.BorderRays, brush);
        Pen pen = new Pen(style.BorderColor, style.BorderWidth);
        if (style.BorderWidth != 0)
            g.DrawRectangle(rect, style.BorderRays, pen);

        SolidBrush fontcolor = new SolidBrush(style.Color);
        g.DrawString(textrect, text, style.Font, fontcolor);

        if (this.Enable && !this.isDisabled && DrawCursor)
        {
            float x = g.MeasureString(text.Substring(0, this.Cursor - visible), style.Font).Width;
            RectangleF cur = new RectangleF(x + textrect.X, textrect.Y, style.CursorWidth, textrect.Height);
            SolidBrush cursorcolor = new SolidBrush(style.CursorColor);
            g.FillRectangle(cur, cursorcolor);
            cursorcolor.Dispose();
        }

        if (_value.Length == 0)
        {
            SolidBrush phcolor = new SolidBrush(style.PlaceHolderColor);
            g.DrawString(textrect, PlaceHolder, style.PlaceHolderFont, phcolor);
            phcolor.Dispose();
        }

        if (Errors.Count > 0)
        {
            SolidBrush errorcolor = new SolidBrush(style.ErrorColor);
            g.DrawString(Errors[0], new PointF(rect.X, rect.Bottom), style.ErrorFont, errorcolor);
            errorcolor.Dispose();
        }

        fontcolor.Dispose();
        brush.Dispose();
        pen.Dispose();
        Frame = Math.Min(Frame + Client.Frame, style.CursorTick);
        if (Frame % style.CursorTick == 0)
        {
            DrawCursor = !DrawCursor;
            Frame = 0;
        }
    }

    public void KeyBoardDown(object o, System.Windows.Forms.KeyEventArgs e)
    {
        if (!this.Enable || this.isDisabled)
            return;
        if (char.IsLetterOrDigit((char)e.KeyCode))
        {
            bool capsLock = Control.IsKeyLocked(Keys.CapsLock);
            bool shiftPressed = (e.Modifiers & Keys.Shift) != 0;

            char character = (char)e.KeyValue;
            if (char.IsLetter(character))
            {
                if ((capsLock && !shiftPressed) || (!capsLock && shiftPressed))
                    character = char.ToUpper(character);
                else
                    character = char.ToLower(character);
            }

            Value = _value.Insert(this.Cursor, character.ToString());
            this.Cursor++;
        }
        else if(SpecialsChars.ContainsKey(e.KeyData))
        {
            Value = _value.Insert(this.Cursor, SpecialsChars[e.KeyData].ToString());
            this.Cursor++;
        }
        else
        {
            switch (e.KeyCode)
            {
                case Keys.Space:
                    Value = _value.Insert(this.Cursor, " ");
                    this.Cursor++;
                    break;

                case Keys.Back:
                    if (this.Cursor > 0)
                    {
                        this.Cursor--;
                        Value = _value.Remove(this.Cursor, 1);
                    }
                    break;

                case Keys.Delete:
                    if (this.Cursor < _value.Length)
                        Value = _value.Remove(this.Cursor, 1);
                    break;

                case Keys.Left:
                    if (this.Cursor > 0)
                        this.Cursor--;
                    break;

                case Keys.Right:
                    if (this.Cursor < _value.Length)
                        this.Cursor++;
                    break;
            }
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

    private Dictionary<Keys, char> SpecialsChars = new() {
        [Keys.OemQuestion] = '/',
        [Keys.LButton] = '/',
        [Keys.OemPeriod] = '.',
        [Keys.Oemcomma] = ',',
        [Keys.OemMinus] = '-',
        [Keys.Oemplus] = '+',
    };
}
