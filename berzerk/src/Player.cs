using Raylib_cs;
using System.Numerics;


namespace berzerk
{
    class Player : IEntity
    {
        private const float LeftSideFrame = 154f;

        private Vector2 playerPosition;

        private static Texture2D playerTex = Raylib.LoadTexture("data/player.png");
        private Rectangle playerFrame = new Rectangle(0.0f, 0.0f, (float)playerTex.width / 12f, (float)playerTex.height);
        private Rectangle playerCollider = new Rectangle(0.0f, 0.0f, (float)playerTex.width / 12f, (float)playerTex.height);
        private BulletManager bulletManager;

        private int currentPlayerFrame = 0;
        private int framesCounter = 0;
        private int framesSpeed = 14;

        private bool isOnLeftSide = false;
        private bool isPlayerMoving = false;
        private bool isPlayerDead = false;

        public Player(Vector2 startingPosition)
        {
            playerPosition = startingPosition;
        }

        public void SetBulletManager(BulletManager blt)
        {
            bulletManager = blt;
        }

        public Rectangle GetPlayerCollision()
        {
            return playerCollider;
        }

        public void KillPlayer()
        {
            isPlayerDead = true;
        }

        public bool ReturnPlayerState()
        {
            return isPlayerDead;
        }

        public void UpdateEntity()
        {
            if (!isPlayerDead) MovePlayer();

            if (isPlayerMoving && !isPlayerDead)
            {
                AnimatePlayerMovement();
            }
            else if (isPlayerDead)
            {
                AnimatePlayerDeath();
            }
            else
            {

                Shoot();

                if (!isOnLeftSide) playerFrame.x = 0f;
                else playerFrame.x = LeftSideFrame;
            }
        }

        public void DrawEntity()
        {
            Raylib.DrawTextureRec(playerTex, playerFrame, playerPosition, Color.GREEN);
        }

        private void MovePlayer()
        {
            isPlayerMoving = false;

            if (Raylib.IsKeyDown(KeyboardKey.KEY_RIGHT))
            {
                playerPosition.X += 2.0f;
                isOnLeftSide = false;
                isPlayerMoving = true;
            }
            else if (Raylib.IsKeyDown(KeyboardKey.KEY_LEFT))
            {
                playerPosition.X -= 2.0f;
                isOnLeftSide = true;
                isPlayerMoving = true;
            }

            if (Raylib.IsKeyDown(KeyboardKey.KEY_UP))
            {
                playerPosition.Y -= 2.0f;
                isPlayerMoving = true;
            }
            else if (Raylib.IsKeyDown(KeyboardKey.KEY_DOWN))
            {
                playerPosition.Y += 2.0f;
                isPlayerMoving = true;
            }

            playerCollider.x = playerPosition.X;
            playerCollider.y = playerPosition.Y;
        }

        private void AnimatePlayerMovement()
        {
            if (framesCounter >= (60 / framesSpeed))
            {
                framesCounter = 0;

                if (!isOnLeftSide) currentPlayerFrame++;
                else currentPlayerFrame--;

                if (currentPlayerFrame > 3 && !isOnLeftSide) currentPlayerFrame = 0;
                if (currentPlayerFrame < 4 && isOnLeftSide) currentPlayerFrame = 7;

                playerFrame.x = (float)currentPlayerFrame * (float)playerTex.width / 12f;
            }

            framesCounter++;
        }

        private void AnimatePlayerDeath()
        {
            if (framesCounter >= (60 / framesSpeed))
            {
                framesCounter = 0;

                currentPlayerFrame--;

                if (currentPlayerFrame < 8) currentPlayerFrame = 11;

                playerFrame.x = (float)currentPlayerFrame * (float)playerTex.width / 12f;
            }

            framesCounter++;
        }

        private void Shoot()
        {
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_D))
            {
                Bullet blt = new Bullet(new Vector2(1f, 0f), new Vector2(playerPosition.X + 18f, playerPosition.Y + 20f), false);
                bulletManager.AddBullet(blt);
            }

            if (Raylib.IsKeyPressed(KeyboardKey.KEY_A))
            {
                Bullet blt = new Bullet(new Vector2(-1f, 0f), new Vector2(playerPosition.X - 6f, playerPosition.Y + 20f), false);
                bulletManager.AddBullet(blt);
            }

            if (Raylib.IsKeyPressed(KeyboardKey.KEY_W))
            {
                Bullet blt = new Bullet(new Vector2(0f, -1f), new Vector2(playerPosition.X + 8f, playerPosition.Y - 6f), true);
                bulletManager.AddBullet(blt);
            }

            if (Raylib.IsKeyPressed(KeyboardKey.KEY_S))
            {
                Bullet blt = new Bullet(new Vector2(0f, 1f), new Vector2(playerPosition.X + 8f, playerPosition.Y + 40f), true);
                bulletManager.AddBullet(blt);
            }
        }
    }
}
