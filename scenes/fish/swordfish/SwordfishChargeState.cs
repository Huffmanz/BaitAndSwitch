using Godot;
using System;
using System.Formats.Asn1;

public partial class SwordfishChargeState : FishState
{
    [Export] RayCast2D wallCheck;
    [Export] float speed;
    [Export] float acceleration;
    [Export] Hurtbox hurtbox;

    Vector2 targetLocation;
    Vector2 direction;

    protected override void EnterState()
    {
        hurtbox.Monitorable = true;
        hurtbox.Monitoring = true;
        wallCheck.Enabled = false;
        if (playerNode != null || IsInstanceValid(playerNode))
        {
            targetLocation = playerNode.GlobalPosition;
        }
        else
        {
            targetLocation = ((Node2D)GetTree().GetNodesInGroup("fish_prey")[0]).GlobalPosition;
        }
        direction = fishNode.GlobalPosition.DirectionTo(targetLocation);
        fishNode.LookAt(fishNode.GlobalPosition.DirectionTo(targetLocation) * 1000);
        targetLocation += direction;
    }

    protected override void ExitState()
    {
        base.ExitState();
        targetLocation = Vector2.Zero;
        fishNode.Velocity = Vector2.Zero;
    }

    public override void _PhysicsProcess(double delta)
    {
        if (wallCheck.IsColliding())
        {
            fishNode.StateMachine.SwitchState<SwordfishStuckState>();
        }
        else
        {
            wallCheck.Enabled = true;
            Charge((float)delta);
        }
    }

    private void Charge(float delta)
    {

        if (fishNode.RotationDegrees < -100 && fishNode.RotationDegrees > -270)
        {
            fishNode.FlipV(true);
        }
        else if (fishNode.RotationDegrees > 90 && fishNode.RotationDegrees < 270)
        {
            fishNode.FlipV(true);
        }
        else
        {
            fishNode.FlipV(false);
        }

        fishNode.Velocity = fishNode.Velocity.MoveToward(direction * speed, acceleration * delta);
        fishNode.MoveAndSlide();
    }

}
