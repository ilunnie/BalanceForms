using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

public class DateInput : TextInput
{
    public DateInput(PointF position, string name = "", string value = "")
        : base(position, name, value) { }
    public DateInput(float x, float y, string name = "", string value = "")
    : this(new PointF(x, y), name, value) { }

    public override void KeyBoardDown(object o, KeyEventArgs e)
    {
        char character = '\0';
        if (e.KeyCode >= Keys.NumPad0 && e.KeyCode <= Keys.NumPad9)
        {
            character = (char)('0' + (e.KeyCode - Keys.NumPad0));
        }
        else if (e.KeyCode >= Keys.D0 && e.KeyCode <= Keys.D9)
        {
            character = (char)('0' + (e.KeyCode - Keys.D0));
        }
        else
        {
            switch (e.KeyCode)
            {
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

        if (character != '\0')
        {
            if (_value.Length > 2 && _value[2] != '/')
            {
                _value = _value.Insert(2, "/");
                if (this.Cursor > 2) this.Cursor++;
            }

            if (_value.Length > 5 && _value[5] != '/')
            {
                _value = _value.Insert(5, "/");
                if (this.Cursor > 5) this.Cursor++;
            }

            Value = _value.Insert(this.Cursor, character.ToString());
            this.Cursor++;

            if (_value.Length == 2 || _value.Length == 5)
            {
                _value += "/";
                this.Cursor++;
            }
        }
    }
}
