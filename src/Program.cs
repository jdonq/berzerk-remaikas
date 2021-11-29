using Raylib_cs;

namespace berzerk
{
    class Program
    {
        static void Main(string[] args)
        {

            InitializeWindow();
            
            Game berzerk = new Game();
            berzerk.InitializeContent();

            while (!Raylib.WindowShouldClose())
            {
                berzerk.Update();

                Raylib.BeginDrawing();
                berzerk.Draw();
                Raylib.EndDrawing();
            }

            Raylib.CloseWindow();
        }

        static void InitializeWindow()
        {
            Raylib.InitWindow(820, 520, "Berzerk");
            //Raylib.SetWindowState(ConfigFlags.FLAG_WINDOW_RESIZABLE);
            Raylib.SetWindowMinSize(520, 520);
            Raylib.SetTargetFPS(60);
        }
    }
}
