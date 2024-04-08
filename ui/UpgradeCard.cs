using Godot;
using System;

public partial class UpgradeCard : PanelContainer
{
    [Export] Label nameLabel;
    [Export] Label descriptionLabel;
    [Export] ProgressBar progressBar;
    [Export] AudioButton purchaseButton;
    [Export] Label countLabel;
    [Export] Label progressLabel;
    [Export] AnimationPlayer animationPlayer;

    private Upgrade upgrade;
    private int currency;
    private int upgradeCost;

    public override void _Ready()
    {
        purchaseButton.Pressed += onPurchasePressed;
        GameEvents.CoinCollected += onCoinCollected;
        GameEvents.CoinsRemoved += OnCoinsRemoved;
    }

    public override void _ExitTree()
    {
        purchaseButton.Pressed -= onPurchasePressed;
        GameEvents.CoinCollected -= onCoinCollected;
        GameEvents.CoinsRemoved -= OnCoinsRemoved;
    }

    private void OnCoinsRemoved(object sender, int e)
    {
        currency -= Mathf.Max(0, e);
    }


    private void onCoinCollected(object sender, EventArgs e)
    {
        currency += 1;
        UpdateProgress();
    }


    private void SelectCard()
    {
        animationPlayer.Play("selected");
    }

    public void SetUpgrade(Upgrade newUpgrade)
    {
        upgrade = newUpgrade;
        nameLabel.Text = upgrade.title;
        descriptionLabel.Text = upgrade.description;
        UpdateProgress();
    }

    private void UpdateProgress()
    {
        int currentQuantity = MetaProgression.Instance.GetCount(upgrade.id);
        int costIncrease = currentQuantity * upgrade.costIncreasePerLevel;
        upgradeCost = upgrade.cost + costIncrease;
        float percent = (float)currency / upgradeCost;
        bool isMaxed = currentQuantity >= upgrade.maxQuantity;
        percent = (float)Mathf.Min((double)percent, 1.0);
        progressBar.Value = percent;
        purchaseButton.Disabled = percent < 1 || isMaxed;
        if (isMaxed)
        {
            purchaseButton.Text = "Max";
        }
        progressLabel.Text = $"{currency} / {upgradeCost}";
        countLabel.Text = $"x{currentQuantity}";
    }

    private void onPurchasePressed()
    {
        if (upgrade == null) return;

        MetaProgression.Instance.AddUpgrade(upgrade);
        GameEvents.EmitCoinsRemoved(upgradeCost);
        GetTree().CallGroup("meta_upgrade_card", "UpdateProgress");
        animationPlayer.Play("selected");
    }

}
