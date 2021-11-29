using System.Numerics;
using Raylib_cs;


namespace berzerk
{
    class UI : IEntity
    {
        private int score = 0;
        private int lives = 3;
        public void UpdateEntity()
        {
            
        }

        public void addPoints(){
            score += 50;
        }

        public void DrawEntity()
        {
            Raylib.DrawText("Score: " + score.ToString() + "\tlives: " + lives.ToString(), (Raylib.GetScreenWidth() - Raylib.GetScreenHeight())/2 + 20, Raylib.GetScreenHeight() - 40, 20, Color.RED);
        }
    }
}
