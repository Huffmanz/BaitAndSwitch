using Godot;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;

[GlobalClass]
public partial class MetaProgression : Node
{
    public static MetaProgression Instance { get; private set; }
    [Export] Upgrade[] debugUpgrades;

    Dictionary<Upgrade, int> upgrades = new Dictionary<Upgrade, int>();

    public override void _Ready()
    {
        if (Instance != null)
        {
            this.QueueFree();
        }
        Instance = this;
        upgrades = new Dictionary<Upgrade, int>();
        if (debugUpgrades != null)
        {
            foreach (Upgrade upgrade in debugUpgrades)
            {
                AddUpgrade(upgrade);
            }
        }
    }

    public void AddUpgrade(Upgrade upgrade)
    {
        if (!upgrades.ContainsKey(upgrade))
        {
            upgrades.Add(upgrade, 0);
        }
        int newCount = Mathf.Min(upgrades[upgrade] + 1, upgrade.maxQuantity);
        upgrades[upgrade] = newCount;
        GameEvents.EmitUpgradeAdded(upgrade);
    }

    public int GetCount(string upgradeId)
    {
        Upgrade upgrade = upgrades.Keys.Where(x => x.id == upgradeId).SingleOrDefault();
        if (upgrade == null)
        {
            return 0;
        }
        else
        {
            return upgrades[upgrade];
        }
    }

}
