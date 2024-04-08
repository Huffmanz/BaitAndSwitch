using Godot;

public partial class PlayerMoveState : CharacterState
{
    [Export(PropertyHint.Range, "0,150,1.0")] float speed = 40f;

    public override void _PhysicsProcess(double delta)
    {
        if (characterNode.direction == Vector2.Zero)
        {
            characterNode.StateMachine.SwitchState<PlayerIdleState>();
            return;
        }

        characterNode.Velocity = new(characterNode.direction.X, characterNode.direction.Y);
        characterNode.Velocity *= speed;

        characterNode.Flip();
        characterNode.MoveAndSlide();

    }

    protected override void EnterState()
    {
        //characterNode.AnimationPlayer.Play(GameConstants.ANIM_MOVE);
    }
}
