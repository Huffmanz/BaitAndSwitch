using Godot;
using System;

public partial class Spear : Node2D
{
    [Export] Sprite2D sprite;
    [Export] float speed = 250f;
    [Export] Area2D environmentCollision;
    [Export] Area2D airCollision;
    [Export] Hurtbox hurtbox;
    [Export] PackedScene bloodVFX;
    [Export] RayCast2D wallCheck;
    [Export] VisibleOnScreenNotifier2D screenNotifier2D;

    Vector2 velocity = Vector2.Zero;

    int maxHits = 0;
    int bounces = 0;


    public override void _Ready()
    {
        environmentCollision.BodyEntered += environmentCollisionOnBodyEntered;
        hurtbox.HurtBoxEntered += hurtboxEntered;
        airCollision.BodyEntered += AirCollisionOnBodyEntered;
        maxHits = MetaProgression.Instance.GetCount(GameConstants.UPGRADE_PIERCING_HARPOON);
        bounces = MetaProgression.Instance.GetCount(GameConstants.UPGRADE_BOUNCE_HARPOON);
        environmentCollision.Monitorable = false;
        environmentCollision.Monitoring = false;
        GameEvents.UpgradeAdded += onUpgradedAdded;
        screenNotifier2D.ScreenExited += () => QueueFree();
    }

    public override void _ExitTree()
    {
        GameEvents.UpgradeAdded -= onUpgradedAdded;
    }

    private void onUpgradedAdded(object sender, Upgrade e)
    {
        maxHits = MetaProgression.Instance.GetCount(GameConstants.UPGRADE_PIERCING_HARPOON);
        bounces = MetaProgression.Instance.GetCount(GameConstants.UPGRADE_BOUNCE_HARPOON);
    }


    private void AirCollisionOnBodyEntered(Node2D body)
    {
        if (IsInstanceValid(hurtbox)) hurtbox.QueueFree();
        if (IsInstanceValid(environmentCollision)) environmentCollision.QueueFree();
        wallCheck.Enabled = false;
    }


    private void hurtboxEntered(Node2D body)
    {
        Node2D newBlood = (Node2D)bloodVFX.Instantiate();
        newBlood.Rotation = Rotation;
        newBlood.GlobalPosition = body.GlobalPosition;
        GetTree().CurrentScene.AddChild(newBlood);
        if (maxHits > 0)
        {
            maxHits -= 1;
            return;
        }
        if (IsInstanceValid(hurtbox)) hurtbox.QueueFree();
        CallDeferred("ChangeParent", body);
        SetPhysicsProcess(false);
        hurtbox.HurtBoxEntered -= hurtboxEntered;
    }

    private void ChangeParent(Node hitTarget)
    {
        GetParent().RemoveChild(this);
        hitTarget.AddChild(this);
        Position = Vector2.Zero;
    }

    private void environmentCollisionOnBodyEntered(Node2D body)
    {
        if (bounces > 0) return;
        float direction = Mathf.Sign(velocity.X);
        velocity.X = sprite.GetRect().Size.X * 10 * direction;
        velocity.Y = sprite.GetRect().Size.X * 10 * Mathf.Sign(velocity.Y);
        GlobalPosition += velocity * (float)GetPhysicsProcessDeltaTime();
        if (IsInstanceValid(hurtbox)) hurtbox.QueueFree();
        SetPhysicsProcess(false);
    }

    public override void _PhysicsProcess(double delta)
    {
        if (wallCheck.IsColliding() && bounces > 0)
        {
            bounces--;
            Vector2 wallNormal = wallCheck.GetCollisionNormal();
            //Rotation = wallNormal.Angle() + wallCheck.GlobalPosition.Angle();
            GD.Print(wallNormal.Dot(GlobalPosition + velocity));
            velocity = (-2 * wallNormal.Dot(GlobalPosition + velocity)) * wallNormal + velocity;
            velocity = velocity.Normalized() * speed;
            LookAt(GlobalPosition + velocity);

            //velocity = new Vector2(speed, 0).Rotated(Rotation);
            sprite.LookAt(GlobalPosition + velocity);
        }
        else
        {
            if (IsInstanceValid(environmentCollision))
            {
                environmentCollision.Monitorable = true;
                environmentCollision.Monitoring = true;
            }
            if (velocity == Vector2.Zero)
            {
                velocity.X = speed;
                velocity = velocity.Rotated(Rotation);
            }
        }

        GlobalPosition += velocity * (float)delta;

    }


}
