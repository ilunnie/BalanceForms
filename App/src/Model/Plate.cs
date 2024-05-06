using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using BoschForms.Screen;

public class Plate
{
    public Image image { get; set; }
    public Balanca Balanca { get; set; }
    public List<Object> Objects { get; set; } = new List<Object>();
    public int Count => Objects.Count;
    public int Weight => Objects.Sum(obj => obj.Weight);

    public PointF Position { get; set; }
    public SizeF Size { get; set; }
    public RectangleF Area {
        get {
            //ToDo Calcular area do prato
            return RectangleF.Empty;
        }
    }
    public PointF Anchor { get; set; } = new PointF(0, 0);

    public void Update(float angle)
    {
        float radius = angle * MathF.PI / 180;
        float x = Screen.CenterX;
        float y = Screen.CenterY;
        float d = 500;
        this.Position = new PointF(
            (x + d * MathF.Cos(radius)) - Anchor.X,
            (y + d * MathF.Sin(radius)) - Anchor.Y
        );
    }

    public void Draw(Graphics g)
    {

    }
}