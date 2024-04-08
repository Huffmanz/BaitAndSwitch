using Godot;
using System;
using System.Reflection.Metadata.Ecma335;

public partial class UpgradeMenu : CanvasLayer
{
    [Export] Upgrade[] upgrades;
    [Export] PackedScene upgradeCard;
    [Export] GridContainer gridContainer;
    [Export] AudioButton backButton;

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
        backButton.Pressed += onBackPressed;
        foreach (var child in gridContainer.GetChildren())
        {
            child.QueueFree();
        }

        foreach (Upgrade upgrade in upgrades)
        {
            UpgradeCard newUpgradeCard = (UpgradeCard)upgradeCard.Instantiate();
            gridContainer.AddChild(newUpgradeCard);
            newUpgradeCard.SetUpgrade(upgrade);
        }
    }

    private void onBackPressed()
    {
        paused = false;
    }

    public override void _Process(double delta)
    {
        if (Input.IsActionJustPressed("upgrade"))
        {
            paused = !paused;
        }
    }
}

