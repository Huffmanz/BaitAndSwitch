using Godot;
using System;

public partial class AudioButton : Button
{
    [Export] RandomAudioPlayer clickAudioPlayer;
    [Export] bool animate = true;
    [Export] AnimationPlayer animationPlayer;


    public override void _EnterTree()
    {
        PivotOffset = new Vector2(Size.X / 2, Size.Y / 2);
    }

    public override void _Ready()
    {
        MouseEntered += OnMouseEntered;
    }

    private void OnMouseEntered()
    {
        clickAudioPlayer.PlayRandom();
        if (animate && !Disabled)
        {
            animationPlayer.Play("hover");
        }
    }

}
