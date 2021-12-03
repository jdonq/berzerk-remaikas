using System.Numerics;
using System;
using System.IO;
using System.Collections.Generic;
using Raylib_cs;


namespace berzerk 
{
    class Arena : IEntity
    {
        private int exitedZone = 0;
        private List<Wall>arenaWalls = new List<Wall>();
        private Player player;
        private Random randomSeed = new Random();
        private BulletManager bulletManager;
        private List<Rectangle> exitZones = new List<Rectangle>();
        private List<Robot> robots = new List<Robot>();
        private Direction[,] cells = new Direction[3, 5] {{0, 0, 0, 0, 0}, {0, 0, 0, 0, 0}, {0, 0, 0, 0, 0}};

        public Arena(Player plr, int playerEntryPos, UI hud)
        {
            player = plr;

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

            GenerateArena(playerEntryPos);   
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
        }

        public void DrawEntity()
        {
            Raylib.DrawRectangle((Raylib.GetScreenWidth() - Raylib.GetScreenHeight()) / 2, 0, Raylib.GetScreenHeight(), Raylib.GetScreenHeight(), Color.BLACK);
            
            foreach(Wall wx in arenaWalls)
            wx.DrawEntity();

            
            bulletManager.DrawEntity();

            //foreach(Robot rs in robots)
            //rs.DrawEntity();
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
            //Arena outside walls
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

            //Vertical inside arena walls
            arenaWalls.Add(new Wall(player, 260f, 20f,  false,  1));
            arenaWalls.Add(new Wall(player, 350f, 20f,  false,  2));
            arenaWalls.Add(new Wall(player, 460f, 20f,  false,  3));
            arenaWalls.Add(new Wall(player, 550f, 20f,  false,  4));
            arenaWalls.Add(new Wall(player, 260f, 160f, false,  6));
            arenaWalls.Add(new Wall(player, 350f, 160f, false,  7));
            arenaWalls.Add(new Wall(player, 460f, 160f, false,  8));
            arenaWalls.Add(new Wall(player, 550f, 160f, false,  9));
            arenaWalls.Add(new Wall(player, 260f, 300f, false, 11));
            arenaWalls.Add(new Wall(player, 350f, 300f, false, 12));
            arenaWalls.Add(new Wall(player, 460f, 300f, false, 13));
            arenaWalls.Add(new Wall(player, 550f, 300f, false, 14));

            //Horizontal inside arena walls
            arenaWalls.Add(new Wall(player, 160f, 160f, true, 1));
            arenaWalls.Add(new Wall(player, 260f, 160f, true, 2));
            arenaWalls.Add(new Wall(player, 360f, 160f, true, 3));
            arenaWalls.Add(new Wall(player, 460f, 160f, true, 4));
            arenaWalls.Add(new Wall(player, 560f, 160f, true, 5));
            arenaWalls.Add(new Wall(player, 160f, 300f, true, 6));
            arenaWalls.Add(new Wall(player, 260f, 300f, true, 7));
            arenaWalls.Add(new Wall(player, 360f, 300f, true, 8));
            arenaWalls.Add(new Wall(player, 460f, 300f, true, 9));
            arenaWalls.Add(new Wall(player, 560f, 300f, true, 10));

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

            GenerateMaze(0, 0, Direction.Start);

            exitZones.Add(new Rectangle(660f, 160f, 10f, 140f));
            exitZones.Add(new Rectangle(360f, 450f, 100f, 10f));
            exitZones.Add(new Rectangle(150f, 160f, 10f, 140f));
            exitZones.Add(new Rectangle(360f, 0f, 100f, 10f));
        }

        private void GenerateMaze(int x, int y, Direction from)
        {
            
            cells[x, y] = from;

            List<Vector2>neighbours = new List<Vector2>();

            if(x != 0)
            {
                if(cells[x - 1, y] == Direction.Unvisited)neighbours.Add(new Vector2(x - 1, y));
            }

            if(y != 0)
            {
                if(cells[x, y - 1] == Direction.Unvisited)neighbours.Add(new Vector2(x, y - 1));
            }

            if(y != 4)
            {
                if(cells[x, y + 1] == Direction.Unvisited)neighbours.Add(new Vector2(x, y + 1));
            }

            if(x != 2)
            {
                if(cells[x + 1, y] == Direction.Unvisited)neighbours.Add(new Vector2(x + 1, y));
            }

            while(neighbours.Count != 0)
            {
                int index = randomSeed.Next(neighbours.Count);

                Vector2 destination = neighbours[index];
                neighbours.RemoveAt(index);
                Direction dir = Direction.Unvisited;
                int cellIndex = x * 5 + y;

                if(cells[(int)destination.X, (int)destination.Y] != Direction.Unvisited)
                continue;

                if(destination.X > x)
                {
                    dir = Direction.North;
                    DeleteWall(cellIndex + 1, false);
                }
                else if(destination.X < x)
                {
                    dir = Direction.South;
                    DeleteWall(cellIndex - 4, false);
                }
                else if(destination.Y > y)
                {
                    dir = Direction.West;
                    DeleteWall(cellIndex + 1, true);
                }
                else if(destination.Y < y)
                {
                    dir = Direction.East;
                    DeleteWall(cellIndex, true);
                }

                GenerateMaze((int)destination.X, (int)destination.Y, dir);
            }            

        }

        private void DeleteWall(int cellIndex, bool vwall)
        {
            if(cellIndex == 0)
            {
                return;
            }

            for(int i = 0; i < arenaWalls.Count; i++)
            {

                if(arenaWalls[i].ReturnWallID() == cellIndex && vwall == arenaWalls[i].IsWallVertical())
                {

                    arenaWalls.RemoveAt(i);
                    i--;
                }
            }
        } 
    }
}
