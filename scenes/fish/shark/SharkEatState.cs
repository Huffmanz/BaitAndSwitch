using Godot;
using System.Collections.Generic;
using System.Linq;

public partial class SharkEatState : FishState
{
    [Export] Area2D eatArea;
    [Export] PackedScene bloodVfx;
    [Export] RandomAudioPlayer randomAudioPlayer;

    Fish currentFish;
    protected override async void EnterState()
    {
        List<Area2D> fishToEat = eatArea.GetOverlappingAreas().Where(x => x.GetParent().IsInGroup("dead_fish")).ToList();
        if (fishToEat.Count > 0)
        {
            foreach (Area2D area in fishToEat)
            {
                fishNode.AnimationPlayer.Play(GameConstants.ANIM_ATTACK);
                currentFish = (Fish)area.GetParent();
                Node2D newBlood = (Node2D)bloodVfx.Instantiate();
                newBlood.GlobalPosition = currentFish.GlobalPosition;
                newBlood.LookAt(newBlood.GlobalPosition * Vector2.Down * 1000);
                GetTree().CurrentScene.AddChild(newBlood);
                randomAudioPlayer.PlayRandom();
                await ToSignal(fishNode.AnimationPlayer, "animation_finished");
            }
            fishNode.StateMachine.SwitchState<SharkMoveState>();
        }
        else
        {
            fishNode.StateMachine.SwitchState<SharkMoveState>();
        }
    }


    public void EatFish()
    {
        if (IsInstanceValid(currentFish)) currentFish.QueueFree();

    }

}
