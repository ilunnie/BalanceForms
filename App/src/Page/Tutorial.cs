using System.Drawing;
using BoschForms;

using BoschForms.Screen;
using BoschForms.Forms;
public class Tutorial : Page
{
    private Balanca Balanca = new Balanca(Screen.CenterX, Screen.CenterY);
    public override void Load()
    {
        App.Background = Color.White;
    }

    public override void Update()
    {
        Balanca.ToWeight();
        Balanca.Update();
    }

    public override void Draw(Graphics g)
    {
        Balanca.Draw(g);
    }

    public override void KeyboardDown(object o, System.Windows.Forms.KeyEventArgs e)
    {
        if (Client.Mode == "debug" && e.KeyCode == System.Windows.Forms.Keys.Escape)
            App.Close();
        if (e.KeyCode == System.Windows.Forms.Keys.Escape)
            App.SetPage(new Close(this));
    }
}
