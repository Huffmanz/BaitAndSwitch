using Godot;
using System;

public partial class Cursor : Sprite2D
{
    [Export] AnimationPlayer animationPlayer;
    bool show = true;

    public override void _Ready()
    {
        Input.MouseMode = Input.MouseModeEnum.Hidden;
    }

    public override void _Process(double delta)
    {
        GlobalPosition = GetGlobalMousePosition();
        if (Input.IsActionJustPressed(GameConstants.INPUT_ATTACK))
        {
            animationPlayer.Play("fire");
        }
    }
}

