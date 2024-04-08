using Godot;
using System;

public partial class Hitbox : Area2D
{
    [Export] HealthComponent healthComponent;
    [Export] RandomAudioPlayer randomAudioPlayer;

    public override void _Ready()
    {
        AreaEntered += OnAreaEntered;
    }

    private void OnAreaEntered(Area2D area)
    {
        if (area is Hurtbox)
        {
            healthComponent.TakeDamage(((Hurtbox)area).damage);
            if (randomAudioPlayer != null)
            {
                randomAudioPlayer.PlayRandom();
            }
        }
    }
}
