namespace Assemble.Controller
{
    public enum BestMove
    {
        Move,
        TurnRight,
        TurnLeft,
        TurnBack,
        AStar, 
        GetAmmo,
        KillCockroach,
        KillBug,
        JumpVortex,
        Joker
    };

    public enum Costs
    {
        Move = -1,
        Turn = -1,
        Attack = -1,
        Ammo = 10,
        KillBug = 100,
        Hole = -10000,
        Cockroach = -10000
    };
}
