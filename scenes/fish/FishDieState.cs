using Godot;
using System;

public partial class FishDieState : FishState
{
    [Export] float floatSpeed;
    [Export] PackedScene bloodVFX;
    [Export] RayCast2D wallCheck;
    [Export] Area2D areaToDisable;

    CpuParticles2D bloodParticles;
    protected override void EnterState()
    {
        fishNode.Rotation = 0;
        fishNode.Velocity = Vector2.Up * floatSpeed;

        fishNode.AnimationPlayer.Stop();
        fishNode.Sprite2D.Frame = 0;
        fishNode.Sprite2D.Scale = new Vector2(1, 1);
        fishNode.FlipV(true);

        bloodParticles = (CpuParticles2D)bloodVFX.Instantiate();
        fishNode.AddChild(bloodParticles);
        bloodParticles.Position = Vector2.Zero;
        if (areaToDisable != null)
        {
            areaToDisable.QueueFree();
        }
        //wallCheck.LookAt(fishNode.GlobalPosition + Vector2.Up);
    }

    public override void _PhysicsProcess(double delta)
    {
        fishNode.MoveAndSlide();
        if (wallCheck.IsColliding())
        {
            fishNode.StateMachine.SwitchState<FishGatherableState>();
        }
    }
}
