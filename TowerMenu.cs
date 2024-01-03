using Godot;
using System;

public partial class TowerMenu : Control
{

    public override void _Ready()
    {

    }

    public override void _Process(double delta)
    {

    }

    public override Variant _GetDragData(Vector2 atPosition)
    {
        return base._GetDragData(atPosition);

        var test = new TowerMenu();
        SetDragPreview(test);

        
    }



}
