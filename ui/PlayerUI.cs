using Godot;
using System;

public partial class PlayerUI : CanvasLayer
{
    [Export] ProgressBar airProgressBar;
    [Export] ProgressBar healthProgressBar;

    public override void _Ready()
    {
        airProgressBar.Value = 1.0;
        healthProgressBar.Value = 1.0;
        GameEvents.AirChanged += OnAirChanged;
        GameEvents.HealthChanged += OnHealthChanged;
    }

    public override void _ExitTree()
    {
        GameEvents.AirChanged -= OnAirChanged;
        GameEvents.HealthChanged -= OnHealthChanged;
    }

    private void OnAirChanged(object sender, (int, int) e)
    {
        airProgressBar.Value = (float)e.Item1 / (float)e.Item2;
    }

    private void OnHealthChanged(object sender, (int, int) e)
    {
        healthProgressBar.Value = (float)e.Item1 / (float)e.Item2;
    }




}
