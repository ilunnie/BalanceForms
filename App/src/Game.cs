using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

public abstract class Game : Page
{
    public List<Balance> Balances = new();
    private Dictionary<Type, List<Object>> _objects = new();
    public Dictionary<Type, List<Object>> DictObjects => _objects;
    public List<Object> Objects => GetObjects();

    public bool CursorOn = true;

    public Game AddObject(Object _object)
    {
        if (_objects.ContainsKey(_object.GetType()))
            _objects[_object.GetType()].Add(_object);
        else
            _objects.Add(_object.GetType(), new List<Object>() { _object });
        return this;
    }

    public Game RemoveObject(Object _object)
    {
        Type key = _object.GetType();
        if (_objects.ContainsKey(key))
        {
            _objects[key].Remove(_object);

            if (_objects[key].Count == 0)
                _objects.Remove(key);
        }
        return this;
    }

    public List<Object> GetObjects()
        => _objects
            .SelectMany(obj => obj.Value)
            .ToList();

    public override void OnMouseMove()
    {
        if (CursorOn) Cursor.Move();
        base.OnMouseMove();
    }

    public override void OnMouseDown(MouseButtons button)
    {
        if(CursorOn && button == MouseButtons.Left) Cursor.Click();
        base.OnMouseDown(button);
    }

    public override void OnMouseUp(MouseButtons button)
    {
        if(CursorOn && button == MouseButtons.Left) Cursor.Drop();
        base.OnMouseUp(button);
    }
}