using System.Numerics;
using System.Collections.Generic;
using Raylib_cs;


namespace berzerk
{
    enum GameState
    {
        Menu,
        Game
    }

    class Game
    {
        Player player;
        UI hud = new UI();
        Arena firstArena;
        Image horizontalGradient = Raylib.GenImageGradientH(Raylib.GetScreenWidth(), Raylib.GetScreenHeight(), Color.RED, Color.BLUE);
        Texture2D gradient;
        MainMenu menu = new MainMenu();
        Timer deathStateTimer = new Timer(2);

        GameState gameState = GameState.Menu;
        

        public void InitializeContent()
        {
            player = new Player(new Vector2 (180f, 200f));
            firstArena = new Arena(player, 0, hud);
            player.SetBulletManager(firstArena.ReturnArenaBulletManager());
            gradient = Raylib.LoadTextureFromImage(horizontalGradient);
        }
        
        public void Update()
        {
            if(gameState == GameState.Menu)
            {
                menu.UpdateMenu();
                if(menu.getGameState())
                {
                    gameState = GameState.Game;
                    InitializeContent();
                }
            } else {
                firstArena.UpdateEntity();
                player.UpdateEntity();

                if(firstArena.CheckIfPlayerLeftArena())
                {
                    int enterPos = firstArena.GetExitedZone();
                    
                    if(enterPos == 0)
                    {
                        player = new Player(new Vector2 (180f, 200f));
                    }
                    if(enterPos == 1)
                    {
                        player = new Player(new Vector2 (400f, 30f));
                    }
                    if(enterPos == 2)
                    {
                        player = new Player(new Vector2 (620f, 200f));
                    }
                    if(enterPos == 3)
                    {
                        player = new Player(new Vector2 (400f, 390f));
                    }

                    firstArena = new Arena(player, enterPos, hud);
                    player.SetBulletManager(firstArena.ReturnArenaBulletManager());
                }


                if(player.ReturnPlayerState() && deathStateTimer.UpdateTimer())
                {
                    gameState = GameState.Menu;
                    menu.setMenuState();
                }
            }
        }

        public void Draw()
        {
            Raylib.ClearBackground(Color.WHITE);
            Raylib.DrawTexture(gradient, 0, 0, new Color(125, 255, 0, 125));
            if(gameState == GameState.Menu)
            {
                menu.DrawMenu();
            } else {
                firstArena.DrawEntity();
                Raylib.DrawText("Berzerk", 12, 12, 25, Color.RED);
                hud.DrawEntity();
                player.DrawEntity();
            }
        }
    }
}
