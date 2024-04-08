using Godot;
using System;
using System.Linq;

public partial class StateMachine : Node
{
    [Export] private Node currentState;
    [Export] private Node[] states;

    public override void _Ready()
    {
        currentState.Notification(GameConstants.ENTER_STATE);
    }

    public void SwitchState<T>()
    {
        Node newState = states.Where(x => x is T).FirstOrDefault();

        if (newState != null)
        {
            currentState.Notification(GameConstants.EXIT_STATE);
            currentState = newState;
            currentState.Notification(GameConstants.ENTER_STATE);
        }
    }

}
