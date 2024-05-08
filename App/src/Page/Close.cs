using BoschForms;
using BoschForms.Forms;
using BoschForms.Drawing;
using KeyEventArgs = System.Windows.Forms.KeyEventArgs;
using Keys = System.Windows.Forms.Keys;
using System.Drawing;
using System.Collections.Generic;
using BoschForms.Screen;
using AForge.Imaging.Filters;
using System;
using Microsoft.VisualBasic;
using System.Linq;

public class Close : Page
{
    private Bitmap Background;
    public bool goBack;
    public IPage LastPage;

    public Close(IPage lastPage = null) => this.LastPage = lastPage;

    public override void Load()
    {
        App.Background = Color.White;

        var center = Screen.Center;
        float textInputWidth = Screen.Width * .3f;
        float textInputHeight = 50;
        float buttonWidth = 200;
        float buttonHeight = 80;

        void Submit(object obj)
        {
            Form form = (Form)obj;
            var body = form.Body;
            bool succes = true;

            if (body["user"].Value.ToString().Length == 0)
            {
                body["user"].Errors.Add("Digite seu usuário");
                succes = false;
            }

            if (body["password"].Value.ToString().Length == 0)
            {
                body["password"].Errors.Add("Digite sua senha");
                succes = false;
            }

            string user = "admin";
            string password = "123";

            if (body["user"].Value.ToString() != user)
            {
                body["user"].Errors.Add("Digite o usuário correta");
                succes = false;
            }

            if (body["password"].Value.ToString() != password)
            {
                body["password"].Errors.Add("Digite a senha correta");
                succes = false;
            }

            if (succes)
                App.Close();
        }

        void Voltar(object obj)
        {
            if (this.LastPage is not null)
                App.SetPage(LastPage, false);
        }

        Form close = new Form("close");
        #region 
        close.Add = new List<IInput>()
        {
            new TextInput(center.X - textInputWidth / 2, Screen.Height * .37f)
            {
                Name = "user",
                PlaceHolder = "Seu Usuário",
                Size = new SizeF(textInputWidth, textInputHeight),
                Style =
                {
                    BackgroundColor = Color.White,
                    BorderColor = Color.Black,
                    BorderWidth = 2,
                    BorderRadius = 10,
                },
                Selected = { BorderWidth = 2, BorderColor = Color.FromArgb(111, 111, 111), }
            },
            new TextInput(center.X - textInputWidth / 2, Screen.Height * .5f)
            {
                Name = "password",
                PlaceHolder = "Sua Senha",
                PasswordChar = true,
                Size = new SizeF(textInputWidth, textInputHeight),
                Style =
                {
                    BackgroundColor = Color.White,
                    BorderColor = Color.Black,
                    BorderWidth = 2,
                    BorderRadius = 10,
                },
                Selected = { BorderWidth = 2, BorderColor = Color.FromArgb(111, 111, 111), }
            },
        }
            .Concat(
                this.LastPage is null
                    ? new[]
                    {
                        new Button(center.X - buttonWidth / 2, Screen.Height * .62f)
                        {
                            Name = "Submit",
                            Value = close,
                            Label = "Enviar",
                            Size = new SizeF(buttonWidth, buttonHeight),
                            Style =
                            {
                                BackgroundColor = Color.FromArgb(0, 123, 192),
                                Color = Color.White,
                                BorderRadius = 40,
                            },
                            OnChange = Submit,
                        }
                    }
                    : new[]
                    {
                        new Button(center.X + 0.44f * buttonWidth, Screen.Height * .62f)
                        {
                            Name = "Submit",
                            Value = close,
                            Label = "Enviar",
                            Size = new SizeF(buttonWidth, buttonHeight),
                            Style =
                            {
                                BackgroundColor = Color.FromArgb(0,123,192),
                                Color = Color.White,
                                BorderRadius = 15,
                                BorderColor = Color.Black,
                                BorderWidth = 2
                            },
                            OnChange = Submit,
                        },
                        new Button(center.X - 1.44f * buttonWidth , Screen.Height * .62f)
                        {
                            Name = "Submit",
                            Value = close,
                            Label = "Voltar",
                            Size = new SizeF(buttonWidth, buttonHeight),
                            Style =
                            {
                                BackgroundColor = Color.FromArgb(0,123,192),
                                Color = Color.White,
                                BorderRadius = 15,
                                BorderColor = Color.Black,
                                BorderWidth = 2
                            },
                            OnChange = Voltar,
                        }
                    }
            )
            .ToList();
        #endregion
        Forms.Add(close);

        GaussianBlur filter = new GaussianBlur();
        Background = filter.Apply(new Bitmap("assets/bosch-entrada.jpg"));
    }

    public override void Update() { }

    public override void Draw(Graphics g)
    {
        g.DrawImage(Background, Screen.Dimension);

        PointF center = Screen.Center;
        float width = Screen.Width * .5f;
        float height = Screen.Height * .5f;
        float x = center.X - width / 2;
        float y = center.Y - height / 2;

        RectangleF shadow = new RectangleF(x * 1.02f, y * 1.02f, width, height);
        SolidBrush shadowbrush = new SolidBrush(Color.FromArgb(100, 100, 100));
        g.FillRectangle(shadow, 50, shadowbrush);

        RectangleF background = new RectangleF(x, y, width, height);
        SolidBrush backbrush = new SolidBrush(Color.FromArgb(239, 241, 242));
        g.FillRectangle(background, 50, backbrush);

        Font label = new Font("Arial", 15);
        SolidBrush labelbrush = new SolidBrush(Color.Black);
        g.DrawString(
            "Usuário:",
            new PointF(x + width * .22f, Screen.Height * .335f),
            label,
            labelbrush
        );
        g.DrawString(
            "Senha:",
            new PointF(x + width * .22f, Screen.Height * .465f),
            label,
            labelbrush
        );

        Forms.ForEach(form => form.Draw(g));
        shadowbrush.Dispose();
        backbrush.Dispose();
    }

    public override void KeyboardDown(object o, KeyEventArgs e)
    { 
        if ((e.Modifiers & Keys.Alt) == Keys.Alt && e.KeyCode == Keys.F4)
        {
            e.Handled = true;
            e.SuppressKeyPress = true;
        }
    }
}
