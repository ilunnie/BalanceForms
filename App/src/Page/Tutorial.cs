using System.Drawing;
// using System.Windows.Forms;
using BoschForms;
using BoschForms.Drawing;

using BoschForms.Screen;
using BoschForms.Forms;
using System.Security.Cryptography;
using System.Collections.Generic;

public class Tutorial : Page
{
    public Dictionary<Object, int> Formas = new();

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
        float positionXInput =
            Screen.Width - width + (Screen.Width * .2f) / 2 - (textInputWidth / 2);

        GenerateLeftPanel();
        GeneratePesarButton();
        GenerateRightPanel();
    }

    private float angle = 0;

    public override void Update() { }

    public override void Draw(Graphics g)
    {
        PointF center = Screen.Center;
        float width = Screen.Width * 0.16f;
        float height = Screen.Height;
        float x = Screen.Width - width;
        float y = 0;
        string titutlo = "Bem vindo ao Tutorial!";
        string enunciado =
            "Aqui você vai aprender como funciona esse desafio das balanças. Quero que você arraste os blocos para a balança e tente descobrir o pesos dessas peças";
        float shapeWidth = Screen.Width * 0.16f;
        float shapeHeight = 40;
        float shapeY = 200;
        float textInputWidth = Screen.Width * .08f;
        float positionXInput = Screen.Width - shapeWidth + (shapeWidth) / 3 - (textInputWidth / 2);

        // Left Panel
        #region
        RectangleF shadowL = new RectangleF(3, 0, width / 2, height);
        SolidBrush shadowLbrush = new SolidBrush(Color.FromArgb(100, 100, 100));
        g.FillRectangle(shadowL, (0, 30, 30, 0), shadowLbrush);

        RectangleF backgroundL = new RectangleF(0, y, width / 2, height);
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
        g.DrawString(
            "Respostas",
            new RectangleF(x, 0, width, Screen.Height * 0.18f),
            label,
            labelbrush,
            format
        );

        // Playable Shapes
        foreach (var shape in Formas)
        {
            var s = shape.Key;
            g.DrawImage(
                new Bitmap(s.Image),
                new PointF(s.Position.X, s.Position.Y),
                new SizeF(100, 100)
            );
        }

        // Display Shapes
        Object[] shapes =
        {
            new Square(new PointF(positionXInput, shapeHeight)),
            new Circle(new PointF(positionXInput, shapeHeight)),
            new Triangle(new PointF(positionXInput, shapeHeight)),
            new Hexagon(new PointF(positionXInput, shapeHeight)),
            new Star(new PointF(positionXInput, shapeHeight)),
        };

        for (int i = 0; i < 5; i++)
        {
            g.DrawImage(
                new Bitmap(shapes[i].Image),
                new PointF(shapes[i].Position.X, shapeY + i * 120 ),
                new SizeF(45, 45)
            );
        }

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
        float textInputWidth = Screen.Width * .16f;
        float textInputHeight = 50;
        float width = Screen.Width * .08f;
        float height = (Screen.Height / 2) - 50;
        float x = Screen.Width - width + 80;
        float y = 200;
        float buttonHeight = 80;

        float positionXInput = (width / 2) - 50;

        float gap = 170;
        Formas[new Square(new PointF(positionXInput, height - 2 * gap), 1000)] = 5;
        Formas[new Circle(new PointF(positionXInput, height - gap), 750)] = 5;
        Formas[new Triangle(new PointF(positionXInput, height), 500)] = 5;
        Formas[new Hexagon(new PointF(positionXInput, height + gap), 100)] = 5;
        Formas[new Star(new PointF(positionXInput, height - 5 + 2 * gap), 200)] = 5; // 110 x 110
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
        float positionXInput =
            Screen.Width - width + (Screen.Width * .2f) / 2 - (textInputWidth / 2);

        positionXButton = Screen.Width / 2 - buttonWidth;
        Form botao = new Form("botao");
        botao.Append(
            new Button(positionXButton, Screen.Height * 0.82f)
            {
                Name = "Submit",
                Label = "Pesar",
                Size = new SizeF(buttonWidth, buttonHeight),
                Style =
                {
                    BackgroundColor = Color.FromArgb(0, 123, 192),
                    Color = Color.White,
                    BorderRadius = 10,
                    BorderColor = Color.Black,
                    BorderWidth = 2,
                },
                OnChange = SubmitPesar,
            }
        );

        Forms.Add(botao);
    }

    private void GenerateRightPanel()
    {
        var center = Screen.Center;
        float textInputWidth = Screen.Width * .08f;
        float textInputHeight = 45;
        float width = Screen.Width * .14f;
        float x = Screen.Width - width + 80;
        float y = 200;
        float buttonWidth = 200;
        float buttonHeight = 80;
        float positionXButton = Screen.Width - width + (width * 0.875f) / 2 - (buttonWidth / 2);
        float positionXInput = Screen.Width - width + (width) / 2 - (textInputWidth / 2);

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
                    Style =
                    {
                        BackgroundColor = Color.White,
                        Color = Color.Black,
                        BorderRadius = 10,
                        BorderColor = Color.Black,
                        BorderWidth = 2,
                    }
                }
            );
        }

        painel.Append(
            new Button(positionXButton, Screen.Height * 0.82f)
            {
                Name = "Submit",
                Value = painel,
                Label = "Responder",
                Size = new SizeF(buttonWidth, buttonHeight),
                Style =
                {
                    BackgroundColor = Color.FromArgb(0, 123, 192),
                    Color = Color.White,
                    BorderRadius = 10,
                    BorderColor = Color.Black,
                    BorderWidth = 2
                },
                OnChange = SubmitRespostas,
            }
        );

        Forms.Add(painel);
    }
}
