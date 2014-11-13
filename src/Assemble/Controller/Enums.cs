namespace Assemble.Controller
{
    public enum BestMove
    {
        MoveUp,
        MoveDown,
        MoveRight,
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
        MoveUp = -1,
        MoveDown = -1,
        MoveRight = -1,
        MoveLeft = -1,
        TurnRight = -1,
        TurnLeft = -1, 
        GetAmmo = 10,
        Attack = -10,
        Cockroach = -10000,
        Hole = -10000,
        FixBug = 100,
        Debug = 0
    };

    public enum Facing
    { 
        East,
        West,
        North,
        South
    };
}
