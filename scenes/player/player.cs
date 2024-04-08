using Godot;
using System;

public partial class player : Character
{
    [Export] SpearGun spearGun;
    [Export] Area2D collectableArea;
    [Export] int maxAirLevel;
    [Export] Timer airTimer;
    [Export] Area2D airDetection;
    [Export] RandomAudioPlayer coinAudioPlayer;

    int coins = 0;

    int currentAirLevel;
    bool isSubmerged = true;

    public override void _EnterTree()
    {
        base._EnterTree();
    }

    public override void _Ready()
    {
        base._Ready();
        collectableArea.AreaEntered += onCollectableAreaEntered;
        currentAirLevel = maxAirLevel;
        airTimer.Timeout += OnAirTimerTimeout;
        airDetection.BodyEntered += (Node2D body) => isSubmerged = false;
        airDetection.BodyExited += (Node2D body) => isSubmerged = true; ;
        healthComponent.Hit += OnPlayerHit;
        healthComponent.Died += OnPlayerDied;
        GameEvents.CoinsRemoved += OnRemoveCoins;
        GameEvents.UpgradeAdded += OnUpgradeAdded;
    }

    private void OnUpgradeAdded(object sender, Upgrade e)
    {
        if (e.id == GameConstants.UPGRADE_HEAL)
        {
            healthComponent.Heal(healthComponent.MaxHealth);
            GameEvents.EmitHealthChanged(healthComponent.currentHealth, healthComponent.MaxHealth);
        }
        else if (e.id == GameConstants.UPGRADE_AIR)
        {
            airTimer.Stop();
            maxAirLevel += 2;
            currentAirLevel = maxAirLevel;
            airTimer.Start();
        }
    }


    private void OnRemoveCoins(object sender, int e)
    {
        coins -= Mathf.Max(0, e);
    }


    private void OnPlayerDied()
    {
        GameEvents.EmitPlayerDeath();
    }


    private void OnPlayerHit(int damage, int currentHealth)
    {
        GameEvents.EmitHealthChanged(currentHealth, healthComponent.MaxHealth);
    }


    private void OnAirTimerTimeout()
    {
        if (isSubmerged)
        {
            currentAirLevel = Mathf.Max(0, currentAirLevel - 1);
        }
        else
        {
            currentAirLevel = Mathf.Min(currentAirLevel + 2, maxAirLevel);
        }
        if (currentAirLevel == 0)
        {
            healthComponent.TakeDamage(1);
        }
        GameEvents.EmitAirChanged(currentAirLevel, maxAirLevel);
    }


    private void onCollectableAreaEntered(Area2D area)
    {
        if (area.GetParent() is Collectable)
        {
            Collectable collectable = (Collectable)area.GetParent();
            collectable.Collect(this);
        }
    }

    public override void _Input(InputEvent @event)
    {
        if (Input.IsActionJustPressed(GameConstants.INPUT_ATTACK))
        {
            spearGun.Attack();
        }
        direction = Input.GetVector(GameConstants.INPUT_MOVE_LEFT, GameConstants.INPUT_MOVE_RIGHT, GameConstants.INPUT_MOVE_UP, GameConstants.INPUT_MOVE_DOWN);
    }

    public void AddCoins(int value)
    {
        coinAudioPlayer.PlayRandom();
        coins += value;
    }
}
