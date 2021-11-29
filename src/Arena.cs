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
        //private Wall[] hWalls = new Wall[10];
        //private Wall[] vWalls = new Wall[12];

        private List<Wall>arenaWalls = new List<Wall>();
        private Player player;

        public Arena(Player plr)
        {
            player = plr;

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

            //arenaWalls.Add(new Wall(player, 260f, 300f, false));
            //arenaWalls.Add(new Wall(player, 350f, 20f,  false));
            //arenaWalls.Add(new Wall(player, 560f, 160f, true));
            //arenaWalls.Add(new Wall(player, 360f, 300f, true));
            //arenaWalls.Add(new Wall(player, 460f, 160f, true));

            /********************************************
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
            ********************************************/

        }

        public void UpdateEntity()
        {
            foreach(Wall wx in arenaWalls)wx.UpdateEntity();
            
            //foreach(Wall wx in vWalls)wx.UpdateEntity();            
            //foreach(Wall wx in hWalls)wx.UpdateEntity();

        }
        public void DrawEntity()
        {
            Raylib.DrawRectangle((Raylib.GetScreenWidth() - Raylib.GetScreenHeight()) / 2, 0, Raylib.GetScreenHeight(), Raylib.GetScreenHeight(), Color.BLACK);
            
            foreach(Wall wx in arenaWalls)wx.DrawEntity();
            
            //foreach(Wall wx in vWalls)wx.DrawEntity();
            //foreach(Wall wx in hWalls)wx.DrawEntity();
        }

        public List<Wall> ReturnArenaWalls()
        {
            return arenaWalls;
        }

        private void ReadLevelFile(string lvlFile)
        {
            string fullPath = Directory.GetCurrentDirectory() + "\\data\\" + lvlFile;

            if(File.Exists(fullPath))
            {
                string[] lines = System.IO.File.ReadAllLines(fullPath);
                
                if(lines.Length == 7)
                {
                    for(int i = 0; i < lines.Length; i += 2)
                    {
                        lines[i] = lines[i].Trim(' ');
                        lines[i + 1] = lines[i + 1].Trim(' ');


                    }
                }
                else
                {
                    Environment.Exit(-1);
                }
            } 
            else 
            {
                Environment.Exit(-1);
            }
            
            //Environment.Exit(0);
        }

        private void CreateWalls(int wallID)
        {
            switch(wallID)
            {

            case 0:
                arenaWalls.Add(new Wall(player, 260f, 20f,  false));
                break;
            case 1:    
                arenaWalls.Add(new Wall(player, 350f, 20f,  false));
                break;
            case 2: 
                arenaWalls.Add(new Wall(player, 460f, 20f,  false));
                break;
            case 3: 
                arenaWalls.Add(new Wall(player, 550f, 20f,  false));
                break;
            case 4: 
                arenaWalls.Add(new Wall(player, 260f, 160f, false));
                break;
            case 5: 
                arenaWalls.Add(new Wall(player, 350f, 160f, false));
                break;
            case 6: 
                arenaWalls.Add(new Wall(player, 460f, 160f, false));
                break;
            case 7: 
                arenaWalls.Add(new Wall(player, 550f, 160f, false));
                break;
            case 8: 
                arenaWalls.Add(new Wall(player, 260f, 300f, false));
                break;
            case 9: 
                arenaWalls.Add(new Wall(player, 350f, 300f, false));
                break;
            case 10: 
                arenaWalls.Add(new Wall(player, 460f, 300f, false));
                break;
            case 11: 
                arenaWalls.Add(new Wall(player, 550f, 300f, false));
                break;
            case 12:
                arenaWalls.Add(new Wall(player, 160f, 160f, true));
                break;
            case 13:
                arenaWalls.Add(new Wall(player, 260f, 160f, true));
                break;
            case 14:
                arenaWalls.Add(new Wall(player, 360f, 160f, true));
                break;
            case 15:
                arenaWalls.Add(new Wall(player, 460f, 160f, true));
                break;
            case 16:
                arenaWalls.Add(new Wall(player, 560f, 160f, true));
                break;
            case 17:
                arenaWalls.Add(new Wall(player, 160f, 300f, true));
                break;
            case 18:
                arenaWalls.Add(new Wall(player, 260f, 300f, true));
                break;
            case 19:
                arenaWalls.Add(new Wall(player, 360f, 300f, true));
                break;
            case 20:
                arenaWalls.Add(new Wall(player, 460f, 300f, true));
                break;
            case 21:
                arenaWalls.Add(new Wall(player, 560f, 300f, true));
                break;

            }
            
            //arenaWalls.Add
        }
    }
}
