using System.Drawing;
using BoschForms;

using BoschForms.Screen;
using BoschForms.Forms;
public class Tutorial : Game
{
    public override void Load()
    {
        App.Background = Color.White;

        Balances.Add(new Balance(Screen.CenterX, Screen.CenterY));

        AddObject(new Circle());
        AddObject(new Circle());
        AddObject(new Circle());
        AddObject(new Circle());
        AddObject(new Circle());
        AddObject(new Triangle());
    }

    public override void Update()
    {
        Balances.ForEach(balance => balance.ToWeight());
        Balances.ForEach(balance => balance.Update());
    }

    public override void Draw(Graphics g)
    {
        Balances.ForEach(balance => balance.Draw(g));
        Objects.ForEach(obj => obj.Draw(g));
    }

    public override void KeyboardDown(object o, System.Windows.Forms.KeyEventArgs e)
    {
        if (Client.Mode == "debug" && e.KeyCode == System.Windows.Forms.Keys.Escape)
            App.Close();
        if (e.KeyCode == System.Windows.Forms.Keys.Escape)
            App.SetPage(new Close(this));
    }

    public override void MouseMove()
        => Cursor.Move();

    public override void MouseDown(System.Windows.Forms.MouseButtons button)
        => Cursor.Click();

    public override void MouseUp(System.Windows.Forms.MouseButtons button)
        => Cursor.Drop();
}
