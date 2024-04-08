

using Godot;

public abstract partial class Collectable : RigidBody2D
{
    public abstract void Collect(player player);
}