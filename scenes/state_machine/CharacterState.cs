using Godot;
using System;

public abstract partial class CharacterState : Node
{
    protected Character characterNode;

    public override void _Ready()
    {
        SetPhysicsProcess(false);
        SetProcessInput(false);
        characterNode = GetOwner<Character>();
    }

    public override void _Notification(int what)
    {
        base._Notification(what);
        switch (what)
        {
            case GameConstants.ENTER_STATE:
                EnterState();
                SetPhysicsProcess(true);
                SetProcessInput(true);
                break;
            case GameConstants.EXIT_STATE:
                ExitState();
                SetPhysicsProcess(false);
                SetProcessInput(false);
                break;
        }
    }

    protected abstract void EnterState();

    protected virtual void ExitState()
    {

    }
}
