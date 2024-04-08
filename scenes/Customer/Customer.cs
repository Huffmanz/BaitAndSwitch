using Godot;
using System;

public partial class Customer : Node2D
{
    [Export] Area2D gatherArea;
    [Export] RayCast2D rayCast2D;
    [Export] Sprite2D fishIconSprite;
    [Export] FishResource[] fishResources;
    [Export] float speed;
    [Export] Sprite2D boatSprite;
    [Export] AnimatedSprite2D customerSprite;
    [Export] Timer gatherTimer;
    [Export] PackedScene coin;
    [Export] RandomAudioPlayer randomAudioPlayer;

    FishResource chosenFish;
    Vector2 direction;
    RandomNumberGenerator rng;
    WeightedTable<FishResource> weightedTable;

    public override void _Ready()
    {
        weightedTable = new WeightedTable<FishResource>();
        foreach (FishResource fishResource in fishResources)
        {
            weightedTable.AddItem(fishResource, fishResource.Weight);
        }
        rng = new RandomNumberGenerator();
        gatherArea.AreaEntered += OnGatherAreaEntered;
        gatherTimer.Timeout += onGatherTimerTimeout;
        PickFish();
    }

    public override void _Process(double delta)
    {
        if (!rayCast2D.IsColliding())
        {
            rayCast2D.Rotate(Mathf.DegToRad(180));
        }

        direction = new Vector2(1, 0).Rotated(rayCast2D.Rotation).Normalized();
        GlobalPosition += direction * speed * (float)delta;
        Flip();

    }

    public void Flip()
    {
        if (direction.X == 0) return;
        boatSprite.FlipH = direction.X < 0 ? true : false;
        customerSprite.FlipH = direction.X < 0 ? true : false;
    }

    private void PickFish()
    {
        chosenFish = weightedTable.PickItem();
        fishIconSprite.Texture = chosenFish.icon;
    }

    private void onGatherTimerTimeout()
    {
        PickFish();
    }


    private void OnGatherAreaEntered(Area2D area)
    {
        if (area.GetParent() is Fish)
        {
            Fish fish = (Fish)area.GetParent();
            if (fish.fishResource.Name == chosenFish.Name)
            {
                for (int i = 0; i < rng.RandiRange(chosenFish.minCoins, chosenFish.maxCoins); i++)
                {
                    RigidBody2D newCoin = (RigidBody2D)coin.Instantiate();
                    newCoin.GlobalPosition = GlobalPosition + new Vector2(rng.RandfRange(-10, 10), 0);
                    GetTree().CurrentScene.CallDeferred("add_child", newCoin);
                }
                randomAudioPlayer.PlayRandom();
                fish.QueueFree();
                PickFish();
            }
        }
    }

}
