using Godot;
using System;

public partial class PlayerIdleState : CharacterState
{
    public override void _PhysicsProcess(double delta)
    {
        if (characterNode.direction != Vector2.Zero)
        {
            characterNode.StateMachine.SwitchState<PlayerMoveState>();
        }
    }

    public override void _Input(InputEvent @event)
    {
        /*  CheckForAttackInput();

         if (@event.IsActionPressed(GameConstants.INPUT_DASH))
         {
             characterNode.StateMachine.SwitchState<PlayerDashState>();
         }
     */
    }

    protected override void EnterState()
    {
        //characterNode.AnimationPlayer.Play(GameConstants.ANIM_IDLE);
    }
}
