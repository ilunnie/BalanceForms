using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

public abstract class Page : IPage
{
    protected List<BoschForms.Forms.Form> Forms = new();
    public abstract void Update();
    public abstract void Load();
    public abstract void Draw(Graphics g);

    public void OnMouseMove()
        => this.MouseMove();
    public virtual void MouseMove(){}

    public void OnMouseDown(MouseButtons button)
    {
        this.Forms.ForEach(form => form.OnMouseDown(button));
        this.MouseDown(button);
    }
    public virtual void MouseDown(MouseButtons button){}

    public void OnMouseUp(MouseButtons button)
    {
        this.Forms.ForEach(form => form.OnMouseUp(button));
        this.MouseUp(button);
    }
    public virtual void MouseUp(MouseButtons button){}

    public void OnKeyDown(object o, KeyEventArgs e)
    {
        this.Forms.ForEach(form => form.OnKeyDown(o, e));
        this.KeyboardDown(o, e);
    }
    public virtual void KeyboardDown(object o, KeyEventArgs e){}

    public void OnKeyUp(object o, KeyEventArgs e)
    {
        this.Forms.ForEach(form => form.OnKeyUp(o, e));
        this.KeyboardUp(o, e);
    }
    public virtual void KeyboardUp(object o, KeyEventArgs e){}
}