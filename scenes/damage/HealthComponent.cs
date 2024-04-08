using Godot;
using System;

public partial class HealthComponent : Node
{
    [Signal] public delegate void DiedEventHandler();
    [Signal] public delegate void HitEventHandler(int damage, int currentHealth);

    [Export] public int MaxHealth { get; private set; }

    public int currentHealth { get; private set; }
    bool dead = false;

    public override void _Ready()
    {
        currentHealth = MaxHealth;
    }

    public void TakeDamage(int damage)
    {
        if (dead) return;
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, MaxHealth);
        EmitSignal(SignalName.Hit, damage, currentHealth);
        if (currentHealth == 0)
        {
            dead = true;
            EmitSignal(SignalName.Died);
        }
    }

    public void Heal(int healAmount)
    {
        if (dead) return;
        currentHealth = Mathf.Clamp(currentHealth + healAmount, 0, MaxHealth);
    }
}
