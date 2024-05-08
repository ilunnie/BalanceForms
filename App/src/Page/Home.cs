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

public class Home : Page
{
    private Bitmap Background;
    private AltTabInterceptor _interceptor;
    public static string Name;
    public static DateTime Date;
    public override void Load()
    {
        App.SetPage(new Tutorial()); // To remove
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

            if (body["name"].Value.ToString().Length == 0)
            {
                body["name"].Errors.Add("Digite seu nome completo");
                succes = false;
            }

            if (body["date"].Value.ToString().Length == 0)
            {
                body["date"].Errors.Add("Digite sua data de nascimento");
                succes = false;
            }

            DateTime date;
            var dateTimeStyles = System.Globalization.DateTimeStyles.None;
            if (!DateTime.TryParseExact(body["date"].Value.ToString(), "dd/MM/yyyy", null, dateTimeStyles, out date))
            {
                body["date"].Errors.Add("Digite uma data válida");
                succes = false;
            }

            if (succes)
            {
                Name = body["name"].Value.ToString();
                Date = date;
                App.SetPage(new Tutorial());
            }

        }

        Form login = new Form("login");
        login.Add = new List<IInput>(){
            new TextInput(center.X - textInputWidth / 2, Screen.Height * .37f) {
                Name = "name",
                PlaceHolder = "Seu Nome",
                Size = new SizeF(textInputWidth, textInputHeight),
                Style = {
                    BackgroundColor = Color.White,
                    BorderColor = Color.Black,
                    BorderWidth = 2,
                    BorderRadius = 10,
                },
                Selected = {
                    BorderWidth = 2,
                    BorderColor = Color.FromArgb(111, 111, 111),
                },
            },
            new TextInput(center.X - textInputWidth / 2, Screen.Height * .5f) {
                Name = "date",
                PlaceHolder = "dd/mm/yyyy",
                Size = new SizeF(textInputWidth, textInputHeight),
                Style = {
                    BackgroundColor = Color.White,
                    BorderColor = Color.Black,
                    BorderWidth = 2,
                    BorderRadius = 10,
                },
                Selected = {
                    BorderWidth = 2,
                    BorderColor = Color.FromArgb(111, 111, 111),
                }
            },
            new Button(center.X - buttonWidth / 2, Screen.Height * .62f) {
                Name = "Submit",
                Value = login,
                Label = "Começar",
                Size = new SizeF(buttonWidth, buttonHeight),
                Style = {
                    BackgroundColor = Color.FromArgb(0,123,192),
                    Color = Color.White,
                    BorderRadius = 15,
                    BorderColor = Color.Black,
                    BorderWidth = 2
                },
                OnChange = Submit,
            }
        };

        Forms.Add(login);

        GaussianBlur filter = new GaussianBlur();
        Background = filter.Apply(new Bitmap("assets/bosch-entrada.jpg"));
        _interceptor = new AltTabInterceptor();
    }

    public override void Update()
    {
        
    }

    public override void Draw(Graphics g)
    {
        g.DrawImage(Background, Screen.Dimension);

        PointF center = Screen.Center;
        float width = Screen.Width * .5f;
        float height = Screen.Height * .5f;
        float x = center.X - width / 2;
        float y = center.Y - height / 2;

        RectangleF shadow = new RectangleF(x * 1.01f, y * 1.01f, width, height);
        SolidBrush shadowbrush = new SolidBrush(Color.FromArgb(100, 100, 100));
        g.FillRectangle(shadow, 50, shadowbrush);

        RectangleF background = new RectangleF(x, y, width, height);
        SolidBrush backbrush = new SolidBrush(Color.FromArgb(239, 241, 242));
        g.FillRectangle(background, 50, backbrush);

        Font label = new Font("Arial", 15);
        SolidBrush labelbrush = new SolidBrush(Color.Black);
        g.DrawString("Nome Completo:", new PointF(x + width * .22f, Screen.Height * .335f), label, labelbrush);
        g.DrawString("Data de Nascimento:", new PointF(x + width * .22f, Screen.Height * .465f), label, labelbrush);

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
    
        if (e.KeyCode == Keys.Escape)
            App.SetPage(new Close(this));

        if ((e.Modifiers & Keys.Alt) == Keys.Alt && e.KeyCode == Keys.Tab)
        {
            e.Handled = true;
            e.SuppressKeyPress = true;
        }
    }

}

