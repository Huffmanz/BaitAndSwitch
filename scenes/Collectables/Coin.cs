using Godot;
using System;

public partial class Coin : Collectable
{
    private const float UpwardForce = -100f;


    public override void _Ready()
    {
        // Call a method to apply the upward force on spawn
        ApplyUpwardForce();
    }

    private void ApplyUpwardForce()
    {
        // Calculate the upward force vector (in this case, pointing upwards)
        Vector2 upwardVector = new Vector2(0, UpwardForce);

        // Apply the upward force to the object's center of mass
        ApplyCentralImpulse(upwardVector);
    }

    public override void Collect(player player)
    {
        player.AddCoins(1);
        GameEvents.EmitCoinCollected();
        QueueFree();
    }

}
