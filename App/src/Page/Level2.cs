using System;
using System.Collections.Generic;
using System.Drawing;
using BoschForms;
using BoschForms.Forms;
using BoschForms.Screen;
using BoschForms.Drawing;
using System.Reflection;
using System.Linq;

public class Level2 : Game
{
    public static int Attempts { get; private set; } = 0;
    public static List<(int template, int response)> Weights { get; private set; }
    private RectangleF RightPanel;
    private RectangleF LeftPanel;
    private RectangleF BetweenLabels;
    private List<(Bitmap image, RectangleF rect)> labels;
    private bool ModalOn = false;
    private RectangleF ModalRect;
    private Form Modal;
    public override void Load()
    {
        App.Background = Color.White;

        //! 🆄🆂🅴🅵🆄🅻 🆂🅴🆃🆃🅸🅽🅶🆂
#region //! ■■■■■■■■■■■■■■■■■■■■■■■
        float width = Screen.Width;
        float height = Screen.Height;
        float RPwidth = width * .16f;
        RightPanel = new RectangleF(width - RPwidth, 0, RPwidth, height);
        float LPwidth = RPwidth / 2;
        LeftPanel = new RectangleF(0, 0, LPwidth, height);
        BetweenLabels = new RectangleF(LeftPanel.Right, 0, RightPanel.Left - LPwidth, height);
        //! ■■■■■■■■■■■■■■■■■■■■■■■
        #endregion

        Type[] shapes = { typeof(Circle), typeof(Hexagon), typeof(Square), typeof(Star), typeof(Triangle) };
        int[] weights = { 600, 675, 500, 50, 25 };
        GenerateRightPanel(shapes, weights);
        GenerateShapes(shapes, weights);
        GenerateGame();

        GenerateConfirmModal();
    }

    public override void Update()
    {
        RectangleF panel = LeftPanel;
        Dictionary<Type, List<Object>> objects = this.DictObjects;
        float gap = panel.Height / (objects.Count + 1);

        float line = gap;
        foreach (var type in objects)
        {
            foreach (var obj in type.Value)
            {
                if (obj == Cursor.Object) continue;
                float x = panel.Left + panel.Width / 2 - obj.Width / 2;
                float y = line - obj.Height / 2;

                obj.Position = new PointF(x, y);
            }
            line += gap;
        }
        Balances.ForEach(balance => balance.Update());

        this.FormsOn = !ModalOn;
        this.CursorOn = !ModalOn;
    }

    public override void Draw(Graphics g)
    {
        SolidBrush shadow = new SolidBrush(Color.FromArgb(100, 100, 100));
        SolidBrush panel = new SolidBrush(Color.FromArgb(239, 241, 242));

        RectangleF panelL = LeftPanel;
        g.FillRectangle(new RectangleF(3, 0, panelL.Width, panelL.Height), (0, 30, 30, 0), shadow);
        g.FillRectangle(panelL, (0, 30, 30, 0), panel);

        RectangleF panelR = RightPanel;
        g.FillRectangle(new RectangleF(panelR.X - 3, 0, panelR.Width, panelR.Height), (0, 30, 30, 0), shadow);
        g.FillRectangle(panelR, (0, 30, 30, 0), panel);

        foreach (var (image, rect) in labels)
            Elements.DrawImage(g, image, rect);

        Objects.ForEach(obj => obj.Draw(g));
        foreach (var type in DictObjects)
        {
            int count = type.Value.Count;
            Object obj = type.Value[0];
            bool inCursor = Cursor.Object is not null && obj.GetType() == Cursor.Object.GetType();
            string quant = (count - (inCursor ? 1:0)).ToString();

            Font font = new Font("Arial", 15);
            if (quant != "0") g.DrawString(obj.Rectangle, quant, font, Brushes.White, alignment: StringAlignment.Center);
        }

        shadow.Dispose();
        panel.Dispose();

        Balances.ForEach(balance => balance.Draw(g));
        Forms.ForEach(form => form.Draw(g));
        if (ModalOn) DrawModal(g);
    }

    public override void KeyboardDown(object o, System.Windows.Forms.KeyEventArgs e)
    {
        if (e.KeyCode == System.Windows.Forms.Keys.Escape)
            if (Client.Mode == "debug") App.Close();
            else App.SetPage(new Close(this));
        if ((e.Modifiers & System.Windows.Forms.Keys.Alt) == System.Windows.Forms.Keys.Alt && e.KeyCode == System.Windows.Forms.Keys.F4)
        {
            e.Handled = true;
            e.SuppressKeyPress = true;
        }
    }


    private void GenerateRightPanel(Type[] shapes, int[] weights, int y = 200, int gap = 120, int? correct_index = null)
    {
        //? Index do valor default
        int index;
        if (correct_index is null)
        {
            int[] arr = (int[])weights.Clone();
            Array.Sort(arr);

            int value = arr[arr.Length / 2];
            index = Array.IndexOf(weights, value);
        }
        else index = correct_index.Value;

        void Submit(object obj)
        {
            Form form = (Form)obj;
            var body = form.Body;

            Weights = new List<(int template, int response)>();
            for (int i = 0; i < weights.Length; i++)
            {
                int response;
                if (!int.TryParse(body[$"input_{i}"].Value.ToString(), out response))
                    response = 0;

                Weights.Add((weights[i], response));
            }

            List<string> weightStrings = Weights.Select(w => $"({w.template}, {w.response})").ToList();
            ModalOn = true;
        }

        RectangleF panel = RightPanel;
        float width = panel.Width * .5f;
        float height = 45;
        float x = panel.X + panel.Width * .6f - width / 2;
        float labelsize = height;
        float labelx = panel.X + panel.Width * .2f - labelsize / 2;

        Form form = new Form("Repostas");

        labels = new();
        for (int i = 0; i < shapes.Length; i++)
        {
            ConstructorInfo constructor = shapes[i].GetConstructor(new Type[] { typeof(int) });
            Object shape = (Object)constructor.Invoke(new object[] { weights[i] });
            Bitmap label = (Bitmap)shape.Image;

            IInput input = new TextInput(x, y + (i * gap), $"input_{i}")
            {
                Value = i == index ? weights[i] : "",
                Size = new SizeF(width, height),
                isDisabled = i == index,
                Style =
                    {
                        BackgroundColor = Color.White,
                        Color = Color.Black,
                        BorderRadius = 10,
                        BorderColor = Color.Black,
                        BorderWidth = 2
                    }
            };

            RectangleF rect = new RectangleF(labelx, y + (i * gap), labelsize, labelsize);

            form.Append(input);
            labels.Add((label, rect));
        }

        width = 200;
        height = 80;
        x = panel.X + panel.Width / 2 - width / 2;

        form.Append(
            new Button(x, Screen.Height * .82f)
            {
                Name = "Submit",
                Value = form,
                Label = "Responder",
                Size = new SizeF(width, height),
                Style =
                {
                    BackgroundColor = Color.FromArgb(0, 123, 192),
                    Color = Color.White,
                    BorderRadius = 10,
                    BorderColor = Color.Black,
                    BorderWidth = 2
                },
                OnChange = Submit,
            }
        );

        Forms.Add(form);
    }

    private void GenerateShapes(Type[] shapes, int[] weights, int[] quant = null)
    {
        if (quant is null)
            quant = new int[] { 5, 5, 5, 5, 5 };

        for (int i = 0; i < shapes.Length; i++)
        {
            ConstructorInfo constructor = shapes[i].GetConstructor(new Type[] { typeof(int) });
            foreach (var _ in quant)
                AddObject((Object)constructor.Invoke(new object[] { weights[i] }));
        }
    }

    private void GenerateGame(int y = 700, int width = 150)
    {
        RectangleF area = BetweenLabels;

        float widthpercent = area.Width * .25f - width / 2;
        Balances.Add(new Balance(area.Left + widthpercent, y));
        Balances.Add(new Balance(area.Right - widthpercent - width, y));

        void Submit(object obj)
        {
            Balances.ForEach(balance => balance.ToWeight());
            Attempts++;
        }

        float bwidth = 200;
        float bheight = 80;
        float bx = area.Left + area.Width / 2 - bwidth / 2;
        float by = area.Height * .8f;

        Form form = new Form("toWeight");

        form.Append(
            new Button(bx, by)
            {
                Label = "Pesar",
                Size = new SizeF(bwidth, bheight),
                Style =
                {
                    BackgroundColor = Color.FromArgb(0, 123, 192),
                    Color = Color.White,
                    BorderRadius = 10,
                    BorderColor = Color.Black,
                    BorderWidth = 2,
                },
                OnChange = Submit,
            }
        );

        Forms.Add(form);
    }

    private void GenerateConfirmModal()
    {
        RectangleF modal = ModalRect;
        Form form = new Form("Confirm");

        void cancel(object obj) => ModalOn = false;
        void submit(object obj)
        {
            //ToDo Move to the next level
        }

        float width = 150;
        float height = 75;
        float y = modal.Y + modal.Height * .6f;
        form.Add = new List<IInput>() {
            new Button(modal.X + modal.Width * .25f - width / 2, y)
            {
                Label = "Cancelar",
                Size = new SizeF(width, height),
                Style = {
                    BackgroundColor = Color.FromArgb(0,123,192),
                    Color = Color.White,
                    BorderRadius = 15,
                    BorderColor = Color.Black,
                    BorderWidth = 2
                },
                OnChange = cancel
            },
            new Button(modal.X + modal.Width * .75f - width / 2, y)
            {
                Label = "Enviar",
                Size = new SizeF(width, height),
                Style = {
                    BackgroundColor = Color.FromArgb(0,123,192),
                    Color = Color.White,
                    BorderRadius = 15,
                    BorderColor = Color.Black,
                    BorderWidth = 2
                },
                OnChange = submit
            },
        };

        this.Modal = form;
    }

    private void DrawModal(Graphics g)
    {
        RectangleF screen = new RectangleF(0, 0, Screen.Width, Screen.Height);
        SolidBrush background = new SolidBrush(Color.FromArgb(150, 0, 0, 0));
        g.FillRectangle(screen, background);

        SolidBrush shadow = new SolidBrush(Color.Black);
        SolidBrush color = new SolidBrush(Color.White);
        g.FillRectangle(new RectangleF(ModalRect.X + 5, ModalRect.Y + 5, ModalRect.Width, ModalRect.Height), 25, shadow);
        g.FillRectangle(ModalRect, 25, color);

        RectangleF text = new RectangleF(ModalRect.X, ModalRect.Y, ModalRect.Width, ModalRect.Height * .6f);
        Font font = new Font("Arial", 20);
        g.DrawString(text, "Deseja mesmo enviar?", font, Brushes.Black, alignment: StringAlignment.Center);

        shadow.Dispose();
        color.Dispose();
        background.Dispose();

        Modal.Draw(g);
    }
}
