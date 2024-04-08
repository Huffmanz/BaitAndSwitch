using Godot;
using System;

public partial class FishGatherableState : FishState
{
    [Export] Area2D gatherableArea;
    [Export] Timer gatherTimer;

    protected override void EnterState()
    {
        gatherableArea.Monitorable = true;
        gatherableArea.Monitoring = true;
        gatherTimer.Start();
        gatherTimer.Timeout += onGatherTimerTimeout;
        characterNode.AddToGroup("dead_fish");
    }

    protected override void ExitState()
    {
        base.ExitState();
        gatherableArea.Monitorable = false;
        gatherableArea.Monitoring = false;
        gatherTimer.Timeout -= onGatherTimerTimeout;
        gatherTimer.Stop();
    }

    private void onGatherTimerTimeout()
    {
        fishNode.QueueFree();
    }

}
