using Godot;
using System;

public partial class TitleScreen : Node2D
{
    [Export] PackedScene level;
    [Export] AudioButton playButton;
    [Export] AudioButton howToPlayButton;
    [Export] AudioButton quitButton;
    [Export] Control howToPlayScreen;
    [Export] Button howToPlayBack;

    public override void _Ready()
    {
        playButton.Pressed += () => ScreenTransition.Instance.TransitionToScene(ScreenTransition.Instance.WorldScene);
        quitButton.Pressed += () => GetTree().Quit();
        howToPlayButton.Pressed += onHowToPlayPressed;
        howToPlayBack.Pressed += () => howToPlayScreen.Visible = false;
        Input.MouseMode = Input.MouseModeEnum.Visible;
        DisplayServer.WindowSetMode(DisplayServer.WindowMode.Fullscreen);
        howToPlayScreen.Visible = false;
    }

    private void onHowToPlayPressed()
    {
        howToPlayScreen.Visible = true;
    }


    public override void _UnhandledInput(InputEvent @event)
    {
        if (@event.IsActionPressed("ui_accept"))
        {
            ScreenTransition.Instance.TransitionToScene(ScreenTransition.Instance.WorldScene);
        }
        //if (@event.IsActionPressed("ui_cancel"))
        // {
        //    GetTree().Quit();
        // }
    }
}
