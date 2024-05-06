using System.Drawing;
using System.Windows.Forms;
using BoschForms;
using BoschForms.Drawing;

using BoschForms.Screen;
using BoschForms.Forms;
using System.Collections.Generic;
public class Tutorial : Page
{
    public override void Load()
    {
        App.Background = Color.White;
    }

    private float angle = 0;
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
    }
}
