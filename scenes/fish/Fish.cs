using Godot;
using System;

public partial class Fish : Character
{
    [Export] public FishResource fishResource { get; set; }

    public override void _Ready()
    {
        base._Ready();
        Sprite2D.Texture = fishResource.texture;
    }

    public void Gather()
    {
        CallDeferred("QueueFree");
    }

}
