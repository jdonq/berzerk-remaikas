using System.Numerics;
using System;
using System.IO;
using System.Collections.Generic;
using Raylib_cs;


namespace berzerk 
{
    class Arena : IEntity
    {
        //private Wall[] boundWalls = new Wall[12];
        private Wall[] hWalls = new Wall[10];
        private Wall[] vWalls = new Wall[12];
        private int exitedZone = 0;
        private List<Wall>arenaWalls = new List<Wall>();
        private Player player;
        private Random randomSeed = new Random();
        private BulletManager bulletManager;
        private List<Rectangle> exitZones = new List<Rectangle>();
        List<Robot> robots = new List<Robot>();

        public Arena(Player plr, int playerEntryPos, UI hud)
        {
            player = plr;

            GenerateArena(playerEntryPos);

            bulletManager = new BulletManager(player, arenaWalls);

            //robots.Clear();

            //robots.Add(new Robot(player, new Vector2(400f, 200f), bulletManager, arenaWalls, hud));
            //robots.Add(new Robot(player, new Vector2(200f, 100f), bulletManager, arenaWalls, hud));
            //robots.Add(new Robot(player, new Vector2(600f, 300f), bulletManager, arenaWalls, hud));
            //robots.Add(new Robot(player, new Vector2(400f, 100f), bulletManager, arenaWalls, hud));
            //robots.Add(new Robot(player, new Vector2(600f, 200f), bulletManager, arenaWalls, hud));

            //arenaWalls.Add(new Wall(player, 260f, 300f, false));
            //arenaWalls.Add(new Wall(player, 350f, 20f,  false));
            //arenaWalls.Add(new Wall(player, 560f, 160f, true));
            //arenaWalls.Add(new Wall(player, 360f, 300f, true));
            //arenaWalls.Add(new Wall(player, 460f, 160f, true));

            
            vWalls[0]  = new Wall(player, 260f, 20f,  false);
            vWalls[1]  = new Wall(player, 350f, 20f,  false);
            vWalls[2]  = new Wall(player, 460f, 20f,  false);
            vWalls[3]  = new Wall(player, 550f, 20f,  false);
            vWalls[4]  = new Wall(player, 260f, 160f, false);
            vWalls[5]  = new Wall(player, 350f, 160f, false);
            vWalls[6]  = new Wall(player, 460f, 160f, false);
            vWalls[7]  = new Wall(player, 550f, 160f, false);
            vWalls[8]  = new Wall(player, 260f, 300f, false);
            vWalls[9]  = new Wall(player, 350f, 300f, false);
            vWalls[10] = new Wall(player, 460f, 300f, false);
            vWalls[11] = new Wall(player, 550f, 300f, false);

            hWalls[0]  = new Wall(player, 160f, 160f, true);
            hWalls[1]  = new Wall(player, 260f, 160f, true);
            hWalls[2]  = new Wall(player, 360f, 160f, true);
            hWalls[3]  = new Wall(player, 460f, 160f, true);
            hWalls[4]  = new Wall(player, 560f, 160f, true);
            hWalls[5]  = new Wall(player, 160f, 300f, true);
            hWalls[6]  = new Wall(player, 260f, 300f, true);
            hWalls[7]  = new Wall(player, 360f, 300f, true);
            hWalls[8]  = new Wall(player, 460f, 300f, true);
            hWalls[9]  = new Wall(player, 560f, 300f, true);
            
        }

        public List<Wall> ReturnArenaWalls()
        {
            return arenaWalls;
        }

        public BulletManager ReturnArenaBulletManager()
        {
            return bulletManager;
        }

        public void UpdateEntity()
        {
            foreach(Wall wx in arenaWalls)
            wx.UpdateEntity();

            bulletManager.UpdateEntity();

            //foreach(Robot rs in robots)
            //rs.UpdateEntity();
            
            foreach(Wall wx in vWalls)wx.UpdateEntity();            
            foreach(Wall wx in hWalls)wx.UpdateEntity();

        }

        public void DrawEntity()
        {
            Raylib.DrawRectangle((Raylib.GetScreenWidth() - Raylib.GetScreenHeight()) / 2, 0, Raylib.GetScreenHeight(), Raylib.GetScreenHeight(), Color.BLACK);
            
            foreach(Wall wx in arenaWalls)
            wx.DrawEntity();

            
            bulletManager.DrawEntity();

            //foreach(Robot rs in robots)
            //rs.DrawEntity();
            
            foreach(Wall wx in vWalls)wx.DrawEntity();
            foreach(Wall wx in hWalls)wx.DrawEntity();
        }

        public int GetExitedZone()
        {
            return exitedZone;
        }

        public bool CheckIfPlayerLeftArena()
        {
            for(int i = 0; i < exitZones.Count; i++)
            {
                if(Raylib.CheckCollisionRecs(exitZones[i], player.GetPlayerCollision()))
                {
                    exitedZone = i;
                    return true;
                }
            }

            return false;
        }

        //playerEnterPos 0 - left, 1 - top, 2 - right, 3 - bottom
        private void GenerateArena(int playerEnterPos)
        {
            arenaWalls.Add( new Wall(player, 160f, 20f,  false));
            arenaWalls.Add( new Wall(player, 160f, 10f,  true));
            arenaWalls.Add( new Wall(player, 260f, 10f,  true));
            arenaWalls.Add( new Wall(player, 460f, 10f,  true));
            arenaWalls.Add( new Wall(player, 560f, 10f,  true));
            arenaWalls.Add( new Wall(player, 650f, 20f,  false));
            arenaWalls.Add( new Wall(player, 650f, 300f, false));
            arenaWalls.Add( new Wall(player, 560f, 440f, true));
            arenaWalls.Add( new Wall(player, 460f, 440f, true));
            arenaWalls.Add( new Wall(player, 260f, 440f, true));
            arenaWalls.Add( new Wall(player, 160f, 440f, true));
            arenaWalls.Add( new Wall(player, 160f, 300f, false));

            switch(playerEnterPos)
            {
                
                case 0:
                    arenaWalls.Add( new Wall(player, 160f, 160f,  false));
                    arenaWalls[arenaWalls.Count - 1].SetAsEntryWall();
                break;

                case 1:
                    arenaWalls.Add( new Wall(player, 360f, 10f,  true));
                    arenaWalls[arenaWalls.Count - 1].SetAsEntryWall();
                break;

                case 2:
                    arenaWalls.Add( new Wall(player, 650f, 160f,  false));
                    arenaWalls[arenaWalls.Count - 1].SetAsEntryWall();
                break;

                case 3:
                    arenaWalls.Add( new Wall(player, 360f, 440f,  true));
                    arenaWalls[arenaWalls.Count - 1].SetAsEntryWall();
                break;

                default:
                    arenaWalls.Add( new Wall(player, 160f, 160f,  false));
                    arenaWalls[arenaWalls.Count - 1].SetAsEntryWall();
                break;
            }

            exitZones.Add(new Rectangle(660f, 160f, 10f, 140f));
            exitZones.Add(new Rectangle(360f, 450f, 100f, 10f));
            exitZones.Add(new Rectangle(150f, 160f, 10f, 140f));
            exitZones.Add(new Rectangle(360f, 0f, 100f, 10f));
        } 
    }
}
