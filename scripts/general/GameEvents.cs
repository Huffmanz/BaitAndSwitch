using Godot;
using System;

public static class GameEvents
{
    public static event EventHandler CoinCollected;
    public static event EventHandler<(int, int)> AirChanged;
    public static event EventHandler<(int, int)> HealthChanged;
    public static event EventHandler PlayerDeath;
    public static event EventHandler<Upgrade> UpgradeAdded;
    public static event EventHandler<int> CoinsRemoved;

    public static void EmitCoinCollected()
    {
        CoinCollected?.Invoke(null, EventArgs.Empty);
    }

    public static void EmitAirChanged(int current, int max)
    {
        AirChanged?.Invoke(null, (current, max));
    }

    public static void EmitHealthChanged(int current, int max)
    {
        HealthChanged?.Invoke(null, (current, max));
    }

    public static void EmitPlayerDeath()
    {
        PlayerDeath?.Invoke(null, EventArgs.Empty);
    }


    public static void EmitUpgradeAdded(Upgrade upgrade)
    {
        UpgradeAdded?.Invoke(null, upgrade);
    }

    public static void EmitCoinsRemoved(int coinsToRemove)
    {
        CoinsRemoved?.Invoke(null, coinsToRemove);
    }
}


