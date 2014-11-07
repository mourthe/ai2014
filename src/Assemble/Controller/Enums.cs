namespace Assemble.Controller
{
    public enum BestMove
    {
        MoveRight,
        MoveUp,
        MoveDown,
        MoveLeft,
        TurnRight,
        TurnLeft,
        AStar, 
        GetAmmo,
        Attack,
        FixBug,
        JumpVortex,
        Debug
    };

    public enum MoveCosts
    {
        MoveRight = -1,
        MoveUp = -1,
        MoveDown = -1,
        MoveLeft = -1,
        TurnRight = -1,
        TurnLeft = -1, 
        Ammo = 10,
        Attack = -10,
        Cockroach = -10000,
        FixBug = 100,
        Debug = 0
    };
}
