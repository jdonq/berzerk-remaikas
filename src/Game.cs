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
        BulletManager bulletManager;
        Image horizontalGradient = Raylib.GenImageGradientH(Raylib.GetScreenWidth(), Raylib.GetScreenHeight(), Color.RED, Color.BLUE);
        Texture2D gradient;
        List<Robot> robots = new List<Robot>();
        MainMenu menu = new MainMenu();
        Timer deathStateTimer = new Timer(2);

        GameState gameState = GameState.Menu;
        

        public void InitializeContent()
        {
            player = new Player(new Vector2 (200f, 200f));
            firstArena = new Arena(player);
            bulletManager = new BulletManager(player, firstArena.ReturnArenaWalls());
            player.setBulletManager(bulletManager);
            gradient = Raylib.LoadTextureFromImage(horizontalGradient);

            robots.Clear();

            robots.Add(new Robot(player, new Vector2(400f, 200f), bulletManager, firstArena.ReturnArenaWalls(), hud));
            //robots.Add(new Robot(player, new Vector2(200f, 100f), bulletManager, firstArena.ReturnArenaWalls(), hud));
            //robots.Add(new Robot(player, new Vector2(600f, 300f), bulletManager, firstArena.ReturnArenaWalls(), hud));
            //robots.Add(new Robot(player, new Vector2(400f, 100f), bulletManager, firstArena.ReturnArenaWalls(), hud));
            //robots.Add(new Robot(player, new Vector2(600f, 200f), bulletManager, firstArena.ReturnArenaWalls(), hud));
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
                bulletManager.UpdateEntity();

                foreach(Robot rs in robots)
                    rs.UpdateEntity();

                if(player.ReturnPlayerState() && deathStateTimer.UpdateTimer()){
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
                bulletManager.DrawEntity();

                foreach(Robot rs in robots)rs.DrawEntity();
            }
        }
    }
}
