using Godot;
using System.Collections.Generic;
using System.Linq;

public partial class SharkMoveState : FishMoveState
{
    [Export] Area2D eatArea;
    Fish chosenFish;


    protected override void EnterState()
    {
        base.EnterState();
        fishNode.AnimationPlayer.Play(GameConstants.ANIM_IDLE);
        eatArea.AreaEntered += onEatAreaEntered;
    }

    protected override void ExitState()
    {
        base.ExitState();
        eatArea.AreaEntered -= onEatAreaEntered;
        chosenFish = null;
    }

    private void onEatAreaEntered(Area2D area)
    {
        fishNode.StateMachine.SwitchState<SharkEatState>();
    }


    public override void _PhysicsProcess(double delta)
    {
        if (chosenFish == null || !IsInstanceValid(chosenFish))
        {
            chosenFish = FindClosestFish();
            if (chosenFish == null)
            {
                Move();
            }
        }
        else
        {
            MoveToFish();
        }
    }

    private void Move()
    {
        fishNode.Sprite2D.Rotation = 0;
        fishNode.FlipV(false);
        if (rayCast2D.IsColliding())
        {
            Vector2 wallNormal = rayCast2D.GetCollisionNormal();
            Vector2 dirToNormal = wallNormal - fishNode.GlobalPosition;
            rayCast2D.Rotation = wallNormal.Angle() - rayCast2D.GlobalPosition.Angle(); //* Mathf.Sign(wallNormal.X);
            direction = new Vector2(1, 0).Rotated(rayCast2D.Rotation).Normalized();
        }
        fishNode.Velocity = new Vector2(direction.X, direction.Y) * speed;
        fishNode.Flip();
        fishNode.MoveAndSlide();
    }

    private void MoveToFish()
    {
        Vector2 prev_direction = direction;
        rayCast2D.LookAt(chosenFish.GlobalPosition);
        fishNode.Sprite2D.Rotation = rayCast2D.Rotation;
        direction = new Vector2(1, 0).Rotated(rayCast2D.Rotation).Normalized();

        if (fishNode.Sprite2D.RotationDegrees < -100 && fishNode.Sprite2D.RotationDegrees > -270)
        {
            fishNode.FlipV(true);
        }
        else if (fishNode.Sprite2D.RotationDegrees > 90 && fishNode.Sprite2D.RotationDegrees < 270)
        {
            fishNode.FlipV(true);
        }
        else
        {
            fishNode.FlipV(false);
        }

        //Flip if previusly going left and now going right, because reasons
        if (Mathf.Sign(prev_direction.X) == -1 && Mathf.Sign(direction.X) == 1)
        {
            fishNode.Flip();
        }
        else
        {
            fishNode.FlipH(false);
        }

        fishNode.Velocity = new Vector2(direction.X, direction.Y) * speed;
        fishNode.MoveAndSlide();
    }

    private Fish FindClosestFish()
    {
        List<Node> deadFish = GetTree().GetNodesInGroup("dead_fish").Where(x => x is Fish).ToList();
        Fish returnFish = null;
        float minDistance = Mathf.Inf;
        if (deadFish.Count > 0)
        {
            foreach (Node fish in deadFish)
            {
                Fish checkFish = (Fish)fish;
                float distanceToFish = fishNode.GlobalPosition.DistanceSquaredTo(checkFish.GlobalPosition);
                if (distanceToFish < minDistance)
                {
                    returnFish = checkFish;
                    minDistance = distanceToFish;
                }

            }
        }
        return returnFish;
    }
}
