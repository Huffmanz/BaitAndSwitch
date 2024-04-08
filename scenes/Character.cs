using System;
using System.Linq;
using Godot;

public abstract partial class Character : CharacterBody2D
{
    [ExportGroup("Required Nodes")]
    [Export] public Sprite2D Sprite2D { get; private set; }
    [Export] public AnimationPlayer AnimationPlayer { get; private set; }
    [Export] public StateMachine StateMachine { get; private set; }
    [Export] public HealthComponent healthComponent { get; private set; }

    public Vector2 direction { get; protected set; } = new();

    public override void _Ready()
    {
        base._Ready();
    }

    public void Flip()
    {
        if (Velocity.X == 0) return;
        Sprite2D.FlipH = Velocity.X < 0 ? true : false;
    }

    public void FlipH(bool flip = true)
    {
        Sprite2D.FlipH = flip;
    }

    public void FlipV(bool flip = true)
    {
        Sprite2D.FlipV = flip;
    }
}