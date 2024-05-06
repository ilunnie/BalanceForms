using BoschForms;
using BoschForms.Drawing;
using KeyEventArgs = System.Windows.Forms.KeyEventArgs;
using Keys = System.Windows.Forms.Keys;
using System.Drawing;
using BoschForms.Screen;
using BoschForms.Forms;
using System.Collections.Generic;
public class Tutorial : Page
{

    public override void Load()
    {
        App.Background = Color.White;

        var center = Screen.Center;
        float textInputWidth = Screen.Width * .15f;
        float textInputHeight = 50;
        float width = Screen.Width * .2f;
        float x = Screen.Width - width + 80;
        float y = 200;
        float buttonWidth = 200;
        float buttonHeight = 80;
        float positionXButton =  Screen.Width - width+ ( Screen.Width * .2f) / 2  - ( buttonWidth / 2);
        float positionXInput =  Screen.Width - width+ ( Screen.Width * .2f) / 2  - ( textInputWidth / 2);

        void Submit(object obj)
        {
            Form form = (Form)obj;
            var body = form.Body;
            bool succes = true;


        }

        Form painel = new Form("Painel");

        for (int i = 0; i < 5; i++)
        {
              painel.Append(
                new TextInput(positionXInput, y + i * 120, "input" + i.ToString(), "") {
                    Size = new SizeF(textInputWidth, textInputHeight),
                    Style = {
                       BackgroundColor = Color.White,
                       Color = Color.Black,
                       BorderRadius = 10,
                       BorderColor = Color.Black,
                       BorderWidth = 2
                    }
                });
        }

        painel.Append(new Button(positionXButton, Screen.Height * 0.82f){
            Name = "Submit",
                Value = painel,
                Label = "Responder",
                Size = new SizeF(buttonWidth, buttonHeight),
                Style = {
                    BackgroundColor = Color.FromArgb(0,123,192),
                    Color = Color.White,
                    BorderRadius = 40,
                    BorderColor = Color.Black,
                    BorderWidth = 2
                },
                OnChange = Submit,
        });

        Forms.Add(painel);
       

    }

    public override void Update()
    {

    }

    public override void Draw(Graphics g)
    {
        

        PointF center = Screen.Center;
        float width = Screen.Width * .2f;
        float height = Screen.Height;
        float x = Screen.Width - width;
        float y = 0;
        

        RectangleF shadow = new RectangleF(x * 0.998f, y * -1.01f, width, height);
        SolidBrush shadowbrush = new SolidBrush(Color.FromArgb(100, 100, 100));
        g.FillRectangle(shadow, 50, shadowbrush);


        RectangleF background = new RectangleF(x, y, width, height);
        SolidBrush backbrush = new SolidBrush(Color.FromArgb(239, 241, 242));
        g.FillRectangle(background, (50, 0, 0, 50), backbrush);


        Font label = new Font("Arial", 15);
        SolidBrush labelbrush = new SolidBrush(Color.Black);

        StringFormat format = new StringFormat();
        format.Alignment = StringAlignment.Center;
        format.LineAlignment = StringAlignment.Center;
        g.DrawString("Respostas", new RectangleF(x , 0, width, Screen.Height * 0.18f ), label, labelbrush, format);

        Forms.ForEach(form => form.Draw(g));
        shadowbrush.Dispose();
        backbrush.Dispose();
    }

    public override void KeyboardDown(object o, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Escape)
            App.Close();
        if (e.KeyCode == Keys.Enter)
            App.SetPage(new Close());
    }
}
