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

        void SubmitRespostas(object obj)
        {
            Form form = (Form)obj;
            var body = form.Body;
            bool succes = true;
        }

        void SubmitPesar(object obj)
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
                       BorderWidth = 2,
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
                    BorderRadius = 10,
                    BorderColor = Color.Black,
                    BorderWidth = 2
                },
                OnChange = SubmitRespostas,
        });

        Forms.Add(painel);

        positionXButton =  Screen.Width / 2 - buttonWidth;
        Form botao = new Form("botao");
        botao.Append(new Button(positionXButton, Screen.Height * 0.82f){
            Name = "Submit",
                Value = painel,
                Label = "Pesar",
                Size = new SizeF(buttonWidth, buttonHeight),
                Style = {
                    BackgroundColor = Color.FromArgb(0,123,192),
                    Color = Color.White,
                    BorderRadius = 10,
                    BorderColor = Color.Black,
                    BorderWidth = 2,
                },
                OnChange = SubmitPesar,
        });

        Forms.Add(botao);
    }

    public override void Update()
    {
        Balanca.Update();
    }

    public override void Draw(Graphics g)
    {
        
    }

    public override void KeyboardDown(object o, System.Windows.Forms.KeyEventArgs e)
    {
        if (e.KeyCode == System.Windows.Forms.Keys.Escape)
            App.Close();

        if (e.KeyCode == Keys.Escape )
            App.SetPage(new Close(this));
    }
}
