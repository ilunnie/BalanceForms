using BoschForms;
using BoschForms.Forms;
using KeyEventArgs = System.Windows.Forms.KeyEventArgs;
using Keys = System.Windows.Forms.Keys;
using System.Drawing;
using System.Collections.Generic;


public class Home : Page
{
    public override void Load()
    {
        App.Background = Color.White;

        Form login = new Form("login");
        login.Add = new List<IInput>(){
            new TextInput(0, 0) {
                Style = new Styles {
                    BackgroundColor = Color.FromArgb(239, 241, 242),
                    BorderColor = Color.Black,
                    BorderWidth = 1,
                    BorderRadius = 10,
                },
                Hover = new Styles {
                    BorderWidth = 5,
                }
            },
        };

        Forms.Add(login);
    }

    public override void Update()
    {

    }

    public override void Draw(Graphics g)
    {
        Forms.ForEach(form => form.Draw(g));
    }

    public override void KeyboardDown(object o, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Escape)
            App.Close();
    }
}
