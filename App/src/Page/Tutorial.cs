using System.Drawing;
using System.Windows.Forms;
using BoschForms;

public class Tutorial : Page
{
    public override void Load()
    {
        App.Background = Color.White;
    }

    public override void Update()
    {

    }

    public override void Draw(Graphics g)
    {
        
    }

    public override void KeyboardDown(object o, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Escape)
            App.Close();
        if (e.KeyCode == Keys.Enter)
            App.SetPage(new Close());
    }
}
