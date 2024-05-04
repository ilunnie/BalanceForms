using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace BoschForms.Forms;

public class Form
{
    public string Name;
    public List<IInput> Inputs { get; private set; }
    private IInput Selected = null;

    public Form(string name = "")
    {
        this.Name = name;
        this.Inputs = new();
    }

    public void Draw(Graphics g)
        => this.Inputs.ForEach(input => input.Draw(g));

    public virtual void OnMouseDown(MouseButtons button)
    {
        this.Inputs.ForEach(input => {
            if (input.Contains(Client.Cursor) && button == MouseButtons.Left)
                Selected = input;
            input.MouseKeyDown(button);
        });
    }

    public virtual void OnMouseUp(MouseButtons button)
    {
        this.Inputs.ForEach(input => {
            input.MouseKeyUp(button);
            input.Enable = false;
        });
        if (this.Selected is not null)
        {
            this.Selected.Enable = this.Selected.Contains(Client.Cursor);
            this.Selected = null;
        }
    }

    public virtual void OnKeyDown(object o, KeyEventArgs e)
    {
        IInput selected = this.Inputs.LastOrDefault(input => input.Enable);

        switch (e.KeyCode)
        {
            case Keys.Tab:
                selected.Enable = false;
                this.Inputs[(this.Inputs.IndexOf(selected) + 1) % this.Inputs.Count]
                    .Enable = true;
            break;
            
            case Keys.Enter:
                int index = (this.Inputs.IndexOf(selected) + 1) % this.Inputs.Count;
                if (index != 0)
                {
                    selected.Enable = false;
                    this.Inputs[index].Enable = true;
                }
            break;
        }
        this.Inputs.ForEach(input => input.KeyBoardDown(o, e));
    }

    public virtual void OnKeyUp(object o,  KeyEventArgs e)
        => this.Inputs.ForEach(input => input.KeyBoardUp(o, e));


    public List<IInput> Add { set {
        foreach (var input in value)
            this.Inputs.Add(input);
    }}

    public virtual Form Append(IInput input)
    {
        this.Inputs.Add(input);
        return this;
    }
}