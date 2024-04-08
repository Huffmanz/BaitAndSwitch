using Godot;
using System;

public partial class ScreenTransition : CanvasLayer
{
    [Signal] public delegate void ScreenTransitionHalfwayEventHandler();
    public static ScreenTransition Instance { get; private set; }

    [Export] public PackedScene TitleScene { get; private set; }
    [Export] public PackedScene WorldScene { get; private set; }
    [Export] AnimationPlayer animationPlayer;

    bool skipEmit = false;

    public override void _EnterTree()
    {
        if (Instance != null)
        {
            this.QueueFree();
        }
        Instance = this;
    }
    public void EmitScreenTransitionHalfway()
    {
        Instance.EmitSignal(SignalName.ScreenTransitionHalfway);
    }

    public async void transition()
    {
        animationPlayer.Play("transition");
        await ToSignal(this, "ScreenTransitionHalfway");
        skipEmit = true;
        animationPlayer.PlayBackwards("transition");
        await ToSignal(animationPlayer, "animation_finished");
        skipEmit = false;
    }

    public async void TransitionToScene(string scenePath)
    {
        transition();
        await ToSignal(this, "ScreenTransitionHalfway");
        GetTree().Paused = false;
        GetTree().ChangeSceneToFile(scenePath);
    }

    public async void TransitionToScene(PackedScene scene)
    {
        transition();
        await ToSignal(this, "ScreenTransitionHalfway");
        GetTree().Paused = false;
        GetTree().ChangeSceneToPacked(scene);
    }
}
