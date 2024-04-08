using Godot;
using System;

public partial class level : Node2D
{
    [Export] player player;

    public override void _Ready()
    {
        GameEvents.PlayerDeath += OnPlayerDeath;
    }

    public override void _ExitTree()
    {
        GameEvents.PlayerDeath -= OnPlayerDeath;
    }

    private void OnPlayerDeath(object sender, EventArgs e)
    {
        ScreenTransition.Instance.TransitionToScene(ScreenTransition.Instance.TitleScene);
    }
}
