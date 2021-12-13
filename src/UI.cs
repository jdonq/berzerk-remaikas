using Raylib_cs;


namespace berzerk
{
    class UI : IEntity
    {
        private int score = 0;

        public void UpdateEntity()
        {
            
        }

        public void AddPoints(){
            score += 50;
        }

        public void ResetPoints(){
            score = 0;
        }

        public void DrawEntity()
        {
            Raylib.DrawText("Score: " + score.ToString(), (Raylib.GetScreenWidth() - Raylib.GetScreenHeight())/2 + 20, Raylib.GetScreenHeight() - 40, 20, Color.GREEN);
        }
    }
}
