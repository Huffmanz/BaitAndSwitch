using Godot;
using System;

public partial class Hurtbox : Area2D
{
    [Signal] public delegate void HurtBoxEnteredEventHandler(Node2D node);

    [Export] public int damage { get; set; } = 1;
    [Export] PackedScene bloodVFX;

    public override void _Ready()
    {
        base._Ready();
        AreaEntered += onAreaEntered;
    }

    private void onAreaEntered(Node2D body)
    {
        EmitSignal(SignalName.HurtBoxEntered, body);
        if (bloodVFX != null)
        {
            Node2D newBlood = (Node2D)bloodVFX.Instantiate();
            newBlood.GlobalPosition = body.GlobalPosition;
            newBlood.Rotation = ((Node2D)GetParent()).Rotation;
            GetTree().CurrentScene.AddChild(newBlood);
        }


        //QueueFree();
    }
}
