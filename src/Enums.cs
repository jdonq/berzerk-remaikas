namespace berzerk 
{
    enum GameState
    {
        Menu,
        Game
    }

    enum Direction : int
    {
        Start = -1,
        Unvisited = 0,
        North = 1,
        East = 2,
        South = 3,
        West = 4
    }

}