using System.Drawing;
// using System.Windows.Forms;
using BoschForms;
using BoschForms.Drawing;

using BoschForms.Screen;
using BoschForms.Forms;
using System.Security.Cryptography;
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
        float positionXButton = Screen.Width - width + (Screen.Width * .2f) / 2 - (buttonWidth / 2);
        float positionXInput = Screen.Width - width + (Screen.Width * .2f) / 2 - (textInputWidth / 2);

        GenerateRightPanel();
        GeneratePesarButton();
        GenerateLeftPanel();

    }

    private float angle = 0;
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
        string titutlo = "Bem vindo ao Tutorial!";
        string enunciado = "Aqui você vai aprender como funciona esse desafio das balanças. Quero que você arraste os blocos para a balança e tente descobrir o pesos dessas peças";
        
        // Left Panel
        #region 
        RectangleF shadowL = new RectangleF(3, 0, width, height);
        SolidBrush shadowLbrush = new SolidBrush(Color.FromArgb(100, 100, 100));
        g.FillRectangle(shadowL, (0, 30, 30, 0), shadowLbrush);

        RectangleF backgroundL = new RectangleF(0, y, width, height);
        SolidBrush backbrushL = new SolidBrush(Color.FromArgb(239, 241, 242));
        g.FillRectangle(backgroundL, (0, 30, 30, 0), backbrushL);
        #endregion
        
        // Right Panel
        #region 
        RectangleF shadowR = new RectangleF(x * 0.998f, y * -1.01f, width, height);
        SolidBrush shadowRbrush = new SolidBrush(Color.FromArgb(100, 100, 100));
        g.FillRectangle(shadowR, (30, 0, 0, 30), shadowRbrush);

        RectangleF backgroundR = new RectangleF(x, y, width, height);
        SolidBrush backbrushR = new SolidBrush(Color.FromArgb(239, 241, 242));
        g.FillRectangle(backgroundR, (30, 0, 0, 30), backbrushR);
        #endregion

        Font label = new Font("Arial", 15);
        SolidBrush labelbrush = new SolidBrush(Color.Black);

        StringFormat format = new StringFormat()
        {
            Alignment = StringAlignment.Center,
            LineAlignment = StringAlignment.Center
        };
        g.DrawString("Respostas", new RectangleF(x, 0, width, Screen.Height * 0.18f), label, labelbrush, format);

        Forms.ForEach(form => form.Draw(g));
        shadowRbrush.Dispose();
        backbrushR.Dispose();

    }

    public override void KeyboardDown(object o, System.Windows.Forms.KeyEventArgs e)
    {
        if (e.KeyCode == System.Windows.Forms.Keys.Escape)
            App.Close();

        if (e.KeyCode == System.Windows.Forms.Keys.Escape)
            App.SetPage(new Close(this));
    }

    private void GenerateLeftPanel()
    {
        var center = Screen.Center;
        float textInputWidth = Screen.Width * .15f;
        float textInputHeight = 50;
        float width = Screen.Width * .2f;
        float x = Screen.Width - width + 80;
        float y = 200;
        float buttonWidth = 200;
        float buttonHeight = 80;
        float positionXButton = Screen.Width - width + (Screen.Width * .2f) / 2 - (buttonWidth / 2);
        float positionXInput = Screen.Width - width + (Screen.Width * .2f) / 2 - (textInputWidth / 2);
        Form painel = new Form("Painel");
    }

    private void GeneratePesarButton()
    {
        float width = Screen.Width * .2f;
        float x = Screen.Width - width + 80;
        float textInputWidth = Screen.Width * .15f;

        void SubmitPesar(object obj)
        {
            Form form = (Form)obj;
            var body = form.Body;
            bool succes = true;
        }

        float buttonWidth = 200;
        float buttonHeight = 80;
        float positionXButton = Screen.Width - width + (Screen.Width * .2f) / 2 - (buttonWidth / 2);
        float positionXInput = Screen.Width - width + (Screen.Width * .2f) / 2 - (textInputWidth / 2);



        positionXButton = Screen.Width / 2 - buttonWidth;
        Form botao = new Form("botao");
        botao.Append(new Button(positionXButton, Screen.Height * 0.82f)
        {
            Name = "Submit",
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

    private void GenerateRightPanel()
    {
        var center = Screen.Center;
        float textInputWidth = Screen.Width * .15f;
        float textInputHeight = 50;
        float width = Screen.Width * .2f;
        float x = Screen.Width - width + 80;
        float y = 200;
        float buttonWidth = 200;
        float buttonHeight = 80;
        float positionXButton = Screen.Width - width + (Screen.Width * .2f) / 2 - (buttonWidth / 2);
        float positionXInput = Screen.Width - width + (Screen.Width * .2f) / 2 - (textInputWidth / 2);

        void SubmitRespostas(object obj)
        {
            Form form = (Form)obj;
            var body = form.Body;
            bool succes = true;
        }

        Form painel = new Form("Painel");

        for (int i = 0; i < 5; i++)
        {
            painel.Append(
              new TextInput(positionXInput, y + i * 120, "input" + i.ToString(), "")
              {
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

        painel.Append(new Button(positionXButton, Screen.Height * 0.82f)
        {
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


    }
}
