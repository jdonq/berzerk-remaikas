using System.Numerics;
using System;
using System.Collections.Generic;
using Raylib_cs;


namespace berzerk
{
    class Robot : IEntity
    {
        private Player player;
        private Rectangle robotCollision;
        private Rectangle robotAnimationFrame;
        private BulletManager bulletManager;
        private Timer moveTimer = new Timer(1);
        private Timer coolDownTimer = new Timer(2);
        private Timer shootingTimer = new Timer(1);
        private static Texture2D robotTex = Raylib.LoadTexture("data/robot.png");
        private List<Wall> walls;
        private UI hud;
        private bool canRobotMove = false;
        private bool isRobotDead = false;
        private bool isOnLeftSide = false;
        private bool lastSide = false;
        private int currentRobotFrame = 0;
        private int framesCounter = 0;
        private int framesSpeed = 13;

        public Robot(Player plr, Vector2 startingPos, BulletManager blt, List<Wall> wls, UI ui)
        {
            hud = ui;
            walls = wls;
            player = plr;
            bulletManager = blt;
            robotCollision = new Rectangle(startingPos.X, startingPos.Y, 22f, 36f);
            robotAnimationFrame = new Rectangle(0f, 0f, (float)robotTex.width/13f, (float)robotTex.height);
            blt.AddRobot(this);
        }

        public void UpdateEntity()
        {
            if(coolDownTimer.UpdateTimer() && canRobotMove == false && !isRobotDead)
            {
                canRobotMove = true;
                if(isOnLeftSide)
                {
                    currentRobotFrame = 7;
                } else {
                    currentRobotFrame = 10;
                }
                moveTimer.ResetTimer();
            }

            if(moveTimer.UpdateTimer() && canRobotMove == true && !isRobotDead)
            {
                robotCollision.x = MathF.Round(robotCollision.x);
                robotCollision.y = MathF.Round(robotCollision.y);
                canRobotMove = false;
                coolDownTimer.ResetTimer();
            }

            if(canRobotMove && !isRobotDead)
            {
                MoveRobot();
            }

            if(shootingTimer.UpdateTimer() && !isRobotDead)
            {
                ShootBullet();
            }

            if(framesCounter >= (60/framesSpeed))
            {
                framesCounter = 0;
                currentRobotFrame++;

                if(!canRobotMove)
                AnimateRobotIdle();

                AnimateRobotMovement();
            }

            framesCounter++;

            CheckCollision();
        }

        public void KillRobot()
        {
            isRobotDead = true;
            hud.addPoints();
        }

        public bool GetRobotStatus()
        {
            return isRobotDead;
        }

        public Rectangle ReturnRobotCollision()
        {
            return robotCollision;
        }

        public void DrawEntity()
        {
            if(!isRobotDead)
            Raylib.DrawTextureRec(robotTex, robotAnimationFrame, new Vector2(robotCollision.x, robotCollision.y), Color.GREEN);
        }

        private void MoveRobot()
        {
            if(player.GetPlayerCollision().x - robotCollision.x < 0)
            {
                isOnLeftSide = true;
                CheckLastSide();
                robotCollision.x -= 0.5f;
            }
            if(player.GetPlayerCollision().x - robotCollision.x > 0)
            {
                isOnLeftSide = false;
                CheckLastSide();
                robotCollision.x += 0.5f;
            }
            if(player.GetPlayerCollision().y - robotCollision.y < 0)
            {
                robotCollision.y -= 0.5f;
            }
            if(player.GetPlayerCollision().y - robotCollision.y > 0)
            {
                robotCollision.y += 0.5f;
            }

            lastSide = isOnLeftSide;
        }

        private void ShootBullet()
        {
            float xDist = player.GetPlayerCollision().x - robotCollision.x;
            float yDist = player.GetPlayerCollision().y - robotCollision.y;
            bool isxDistSmaller = true;
            
            if(Math.Abs(xDist) > Math.Abs(yDist))isxDistSmaller = false;

            if(xDist > 0 && xDist < 150 && !isxDistSmaller)
            {
                Bullet blt = new Bullet(new Vector2(1f, 0f), new Vector2(robotCollision.x + 18f, robotCollision.y + 20f), false);
                bulletManager.AddBullet(blt);
            } 
            else if(xDist < 0 && xDist > -150 && !isxDistSmaller)
            {
                Bullet blt = new Bullet(new Vector2(-1f, 0f), new Vector2(robotCollision.x - 6f, robotCollision.y + 20f), false);
                bulletManager.AddBullet(blt);
            } 
            else if(yDist < 0 && xDist > -150 && isxDistSmaller)
            {
                Bullet blt = new Bullet(new Vector2(0f, -1f), new Vector2(robotCollision.x + 8f, robotCollision.y - 6f), true);
                bulletManager.AddBullet(blt);
            } 
            else if(xDist > 0 && xDist < 150 && isxDistSmaller)
            {
                Bullet blt = new Bullet(new Vector2(0f, 1f), new Vector2(robotCollision.x + 8f, robotCollision.y + 40f), true);
                bulletManager.AddBullet(blt);
            }
        }

        private void CheckCollision()
        {
            foreach(Wall wx in walls)
            {
                if(Raylib.CheckCollisionRecs(robotCollision, wx.ReturnWallCollision()))
                {
                    if(!isRobotDead)KillRobot();
                }
            }

            if(Raylib.CheckCollisionRecs(robotCollision, player.GetPlayerCollision()))
            {
                if(!isRobotDead)player.KillPlayer();
            }
        }

        private void AnimateRobotMovement()
        {            
            if(currentRobotFrame > 9 && isOnLeftSide) 
                currentRobotFrame = 7;
            else if(currentRobotFrame > 12 && !isOnLeftSide) 
                currentRobotFrame = 10;

            robotAnimationFrame.x = (float)currentRobotFrame*(float)robotTex.width/13f;
        }

        private void AnimateRobotIdle()
        {
            if(currentRobotFrame > 6) currentRobotFrame = 0;

            robotAnimationFrame.x = (float)currentRobotFrame*(float)robotTex.width/13f;
        }

        private void CheckLastSide()
        {
            if(lastSide != isOnLeftSide && !isOnLeftSide)
            {
                robotAnimationFrame.x = 220f;
            }

            if(lastSide != isOnLeftSide && isOnLeftSide)
            {
                robotAnimationFrame.x = 154f;
            }
        }
    }
}
