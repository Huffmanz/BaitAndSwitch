using Godot;
using System;

public partial class CoinUI : MarginContainer
{
    [Export] Label coinAmountLabel;

    int coinAmount = 0;

    public override void _Ready()
    {
        UpdateLabel();
        GameEvents.CoinCollected += OnCoinCollected;
        GameEvents.CoinsRemoved += OnCoinsRemoved;
    }


    private void OnCoinsRemoved(object sender, int e)
    {
        coinAmount -= Mathf.Max(0, e);
        UpdateLabel();
    }


    public override void _ExitTree()
    {
        GameEvents.CoinCollected -= OnCoinCollected;
        GameEvents.CoinsRemoved -= OnCoinsRemoved;
    }

    private void OnCoinCollected(object sender, EventArgs e)
    {
        coinAmount += 1;
        UpdateLabel();
    }

    private void UpdateLabel()
    {
        coinAmountLabel.Text = $"{coinAmount}";
    }
}
