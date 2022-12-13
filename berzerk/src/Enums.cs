namespace berzerk
{
    enum GameState
    {
        Menu,
        Game
    }

    public enum Direction : int
    {
        Start = -1,
        Unvisited = 0,
        North = 1,
        East = 2,
        South = 3,
        West = 4
    }

    enum PlayerEnterPosition : int
    {
        Left = 0,
        Top = 1,
        Right = 2,
        Bottom = 3
    }
}