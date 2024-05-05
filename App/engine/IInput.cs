using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace BoschForms.Forms;

public interface IInput
{
    string Name { get; }
    Object Value { get; }
    public event Action<object> onChange;
    bool Enable { get; set; }
    bool Contains(PointF point);
    void Draw(Graphics g);
    void MouseKeyDown(MouseButtons button);
    void MouseKeyUp(MouseButtons button);
    void KeyBoardDown(object o, KeyEventArgs e);
    void KeyBoardUp(object o, KeyEventArgs e);
    List<string> Errors { get; }
}