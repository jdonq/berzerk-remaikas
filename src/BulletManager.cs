using System.Collections.Generic;
using Raylib_cs;

namespace berzerk
{
    class BulletManager : IEntity
    {
        private List<Bullet> bullets = new List<Bullet>();
        private List<Wall> arenaWalls;
        private List<Robot> robots = new List<Robot>();
        private Player player;

        public BulletManager(Player plr, List<Wall> walls)
        {
            player = plr;
            arenaWalls = walls;
        }

        public void AddBullet(Bullet blt)
        {
            bullets.Add(blt);
        }

        public void AddRobot(Robot robot)
        {
            robots.Add(robot);
        }

        public void UpdateEntity()
        {
            if(bullets.Count != 0)
            {
                foreach(Bullet blt in bullets)
                {
                    blt.UpdateEntity();
                    //if(blt.BulletStatus())bullets.Remove(blt);
                }

            }

            CheckBulletCollisions();
        }

        public void DrawEntity()
        {
            if(bullets.Count != 0)
            {
                foreach(Bullet blt in bullets)blt.DrawEntity();
            }
        }

        private void CheckBulletCollisions()
        {
            foreach(Bullet bl in bullets)
            {
                foreach(Wall wx in arenaWalls)
                {
                    if(Raylib.CheckCollisionRecs(wx.returnWallCollision(), bl.ReturnBulletCollision()))
                    {
                        bl.DestroyBullet();
                    }

                    if(Raylib.CheckCollisionRecs(bl.ReturnBulletCollision(), player.GetPlayerCollision()))
                    {
                        if(bl.returnBulletStatus())player.KillPlayer();
                        bl.DestroyBullet();
                    }
                }

                foreach(Robot rb in robots)
                {
                    if(Raylib.CheckCollisionRecs(bl.ReturnBulletCollision(), rb.ReturnRobotCollision()) && !rb.GetRobotStatus())
                    {
                        bl.DestroyBullet();
                        rb.KillRobot();
                    }
                }
            }
        }
    }
}
