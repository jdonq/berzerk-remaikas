using Raylib_cs;

namespace berzerk
{
    class MainMenu
    {
        bool beginGame = false;
        public void DrawMenu()
        {
            Raylib.DrawText("BERZERK", Raylib.GetScreenWidth()/2 - 100, Raylib.GetScreenHeight()/2 - 100, 40, Color.RED);
            Raylib.DrawText("Press SpaceBar To Begin The Game", 125, Raylib.GetScreenHeight()/2, 30, Color.RED);
        }

        public void UpdateMenu()
        {
            if(Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE)){
                beginGame = true;
            }
        }

        public bool getGameState()
        {
            return beginGame;
        }

        public void setMenuState()
        {
            beginGame = false;
        }
    }
}
