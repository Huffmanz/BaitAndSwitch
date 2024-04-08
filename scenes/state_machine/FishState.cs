using Godot;
using System;

public abstract partial class FishState : CharacterState
{
    protected Fish fishNode;
    protected player playerNode;


    public override void _Ready()
    {
        base._Ready();
        SetPhysicsProcess(false);
        SetProcessInput(false);
        fishNode = GetOwner<Fish>();
        fishNode.healthComponent.Died += onDied;
        if (GetTree().GetNodesInGroup("player").Count > 0)
        {
            playerNode = (player)GetTree().GetNodesInGroup("player")[0];
        }
    }

    private void onDied()
    {
        fishNode.StateMachine.SwitchState<FishDieState>();
    }
}
