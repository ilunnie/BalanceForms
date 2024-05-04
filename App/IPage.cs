using System.Windows.Forms;

public interface IPage
{
    void Load();
    void Update();
    void Draw();
    void OnMouseMove(object o, MouseEventArgs e);
    void OnKeyDown(object o, KeyEventArgs e);
    void OnKeyUp(object o, KeyEventArgs e);
}