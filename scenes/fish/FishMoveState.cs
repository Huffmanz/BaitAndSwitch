using Godot;
using System;

public partial class FishMoveState : FishState
{
    [Export] protected RayCast2D rayCast2D;
    [Export] protected float speed;

    protected Vector2 direction = Vector2.Zero;

    protected override void EnterState()
    {
        Random r = new Random();

        rayCast2D.Rotation = ((float)r.NextDouble()) * Mathf.DegToRad(360);
        direction = new Vector2(1, 0).Rotated(rayCast2D.Rotation).Normalized();
    }

    public override void _PhysicsProcess(double delta)
    {
        if (rayCast2D.IsColliding())
        {
            Vector2 wallNormal = rayCast2D.GetCollisionNormal();
            rayCast2D.Rotation = wallNormal.Angle() - rayCast2D.GlobalPosition.Angle(); //* Mathf.Sign(wallNormal.X);
            direction = new Vector2(1, 0).Rotated(rayCast2D.Rotation).Normalized();
        }
        fishNode.Velocity = new Vector2(direction.X, direction.Y) * speed;

        fishNode.Flip();
        fishNode.MoveAndSlide();
    }

}
