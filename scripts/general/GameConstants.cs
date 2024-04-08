using System.Data.Common;

public class GameConstants
{
    public const string ANIM_IDLE = "idle";
    public const string ANIM_MOVE = "Move";
    public const string ANIM_GUN_SHOOT = "shoot";
    public const string ANIM_GUN_IDLE = "idle";
    public const string ANIM_ATTACK = "attack";

    public const string INPUT_MOVE_LEFT = "move_left";
    public const string INPUT_MOVE_RIGHT = "move_right";
    public const string INPUT_MOVE_UP = "move_up";
    public const string INPUT_MOVE_DOWN = "move_down";
    public const string INPUT_ATTACK = "attack";
    public const string INPUT_UPGRADE = "upgrade";

    public const int ENTER_STATE = 5001;
    public const int EXIT_STATE = 5002;

    //Upgrades
    public const string UPGRADE_PIERCING_HARPOON = "upgrade_piercing_harpoon";
    public const string UPGRADE_SPREAD_HARPOON = "upgrade_spread_harpoon";
    public const string UPGRADE_BOUNCE_HARPOON = "upgrade_bounce_harpoon";
    public const string UPGRADE_HEAL = "heal";
    public const string UPGRADE_AIR = "air";
}