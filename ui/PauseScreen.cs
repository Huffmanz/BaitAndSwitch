using Godot;
using System;
using System.Reflection.Metadata.Ecma335;

public partial class PauseScreen : CanvasLayer
{
    [Export] Button resumeButton;
    [Export] Button mainMenuButton;

    bool _paused = false;
    bool paused
    {
        get => _paused;
        set
        {
            _paused = value;
            Visible = value;
            GetTree().Paused = value;
            if (value)
            {
                Input.MouseMode = Input.MouseModeEnum.Visible;
            }
            else
            {
                Input.MouseMode = Input.MouseModeEnum.Hidden;
            }

        }
    }

    public override void _Ready()
    {
        paused = false;
        resumeButton.Pressed += OnResumePressed;
        mainMenuButton.Pressed += OnMainMenuPressed;
    }

    private void OnResumePressed()
    {
        paused = false;
    }

    private void OnMainMenuPressed()
    {
        ScreenTransition.Instance.TransitionToScene(ScreenTransition.Instance.TitleScene);
    }



    public override void _Process(double delta)
    {
        if (Input.IsActionJustPressed("ui_cancel"))
        {
            paused = !paused;
        }
    }

}
