using Godot;
using System;

public partial class Blood : CpuParticles2D
{
    [Export] Timer timer;

    public override void _Ready()
    {
        Emitting = true;
        timer.Timeout += onTimerTimeout;
    }

    private void onTimerTimeout()
    {
        QueueFree();
    }

}
