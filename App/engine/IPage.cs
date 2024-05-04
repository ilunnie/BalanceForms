using System.Drawing;
using System.Windows.Forms;

public interface IPage
{
    void Load();
    void Update();
    void Draw(Graphics g);
    void OnKeyDown(object o, KeyEventArgs e);
    void OnKeyUp(object o, KeyEventArgs e);
    void OnMouseMove();
    void OnMouseDown(MouseButtons button);
    void OnMouseUp(MouseButtons button);
}