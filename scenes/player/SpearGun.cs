using Godot;
using System;

public partial class SpearGun : Node2D
{
    [Export] Timer attackTimer;
    [Export] Marker2D spearSpawnLocation;
    [Export] PackedScene projectile;
    [Export] AnimationPlayer animationPlayer;
    [Export] RandomAudioPlayer randomAudioPlayer;

    bool canAttack = true;

    int shotSpread = 1;

    public override void _Ready()
    {
        base._Ready();
        attackTimer.Timeout += onAttackTimerTimeout;
        shotSpread = shotSpread + MetaProgression.Instance.GetCount(GameConstants.UPGRADE_SPREAD_HARPOON);
        GameEvents.UpgradeAdded += OnUpgradeAdded;
    }

    public override void _ExitTree()
    {
        GameEvents.UpgradeAdded -= OnUpgradeAdded;
    }

    private void OnUpgradeAdded(object sender, Upgrade e)
    {
        shotSpread = MetaProgression.Instance.GetCount(GameConstants.UPGRADE_SPREAD_HARPOON);
        if (shotSpread == 0) shotSpread = 1;
    }


    public override void _Process(double delta)
    {
        base._Process(delta);
        Vector2 mouseDirection = GetGlobalMousePosition() - this.GlobalPosition;
        Rotation = mouseDirection.Angle();
        if (Scale.Y == 1 && mouseDirection.X < 0)
        {
            Scale = new Vector2(Scale.X, -1);
        }
        if (Scale.Y == -1 && mouseDirection.X > 0)
        {
            Scale = new Vector2(Scale.X, 1);
        }
    }

    public void Attack()
    {
        if (!canAttack) return;
        float angle = 0f;
        for (int i = 0; i < shotSpread; i++)
        {
            Node2D newSpear = (Node2D)projectile.Instantiate();
            GetTree().CurrentScene.AddChild(newSpear);
            newSpear.GlobalPosition = spearSpawnLocation.GlobalPosition;
            newSpear.RotationDegrees = spearSpawnLocation.GlobalRotationDegrees + Mathf.RadToDeg(angle);
            if (angle == 0)
            {
                angle = .2f;
            }
            else
            {
                angle = -MathF.Sign(angle) * (i * .2f);
            }
        }

        canAttack = false;
        animationPlayer.Play(GameConstants.ANIM_GUN_SHOOT);
        randomAudioPlayer.PlayRandom();
        attackTimer.Start();
    }

    private void onAttackTimerTimeout()
    {
        canAttack = true;
        animationPlayer.Play(GameConstants.ANIM_GUN_IDLE);
    }


}
