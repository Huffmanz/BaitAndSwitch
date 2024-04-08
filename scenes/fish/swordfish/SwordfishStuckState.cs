using Godot;
using System;

public partial class SwordfishStuckState : FishState
{
    [Export] Timer stuckTimer;
    [Export] Hurtbox hurtbox;

    protected override void EnterState()
    {
        stuckTimer.Start();
        stuckTimer.Timeout += OnStuckTimerTimeout;
        hurtbox.Monitorable = false;
        hurtbox.Monitoring = false;
    }

    protected override void ExitState()
    {
        stuckTimer.Timeout -= OnStuckTimerTimeout;
    }

    private void OnStuckTimerTimeout()
    {
        fishNode.StateMachine.SwitchState<SwordfishChargeState>();
    }
}
