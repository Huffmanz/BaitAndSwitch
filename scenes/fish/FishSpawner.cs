using Godot;
using System;
using System.ComponentModel.DataAnnotations;

public partial class FishSpawner : Node
{
    [Export] ReferenceRect spawnArea;
    [Export] FishResource[] fishResources;
    [Export] PackedScene objectToSpawn;
    [Export] int maxAtOneTime = 10;
    [Export] string groupName;
    [Export] float timeBetweenSpawns = 0f;

    int totalSpawned = 0;

    RandomNumberGenerator rng;
    public override void _Ready()
    {
        rng = new RandomNumberGenerator();
        totalSpawned = GetTree().GetNodesInGroup(groupName).Count;
        SpawnFish();
    }

    private void onFishDied()
    {
        totalSpawned--;
        SpawnFish();
    }

    private async void SpawnFish()
    {
        while (totalSpawned < maxAtOneTime)
        {
            Fish chosenFish = (Fish)objectToSpawn.Instantiate();
            chosenFish.fishResource = fishResources[rng.RandiRange(0, fishResources.Length - 1)];
            Vector2 spawnLocation = spawnArea.Position + new Vector2(rng.Randf() * spawnArea.GetRect().Size.X, rng.Randf() * spawnArea.GetRect().Size.Y);
            chosenFish.GlobalPosition = spawnLocation;
            if (timeBetweenSpawns > 0)
            {
                SceneTreeTimer timer = GetTree().CreateTimer(timeBetweenSpawns);
                await ToSignal(timer, "timeout");
            }
            GetTree().CurrentScene.CallDeferred("add_child", chosenFish);
            chosenFish.healthComponent.Died += onFishDied;
            totalSpawned += 1;
        }
    }

}
