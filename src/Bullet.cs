using System.Numerics;
using Raylib_cs;


namespace berzerk
{
    class Bullet: IEntity
    {
        private Vector2 direction;
        private Rectangle bulletCollider;
        private bool bulletStatus = true;
        
        public Bullet(Vector2 dir, Vector2 startingPos, bool isVertical)
        {
            direction = dir;
            if(!isVertical)bulletCollider = new Rectangle(startingPos.X, startingPos.Y, 10f, 5f);
            else bulletCollider = new Rectangle(startingPos.X, startingPos.Y, 5f, 10f);
        }

        public Rectangle ReturnBulletCollision()
        {
            return bulletCollider;
        }

        public void DestroyBullet()
        {
            bulletStatus = false;
        }

        public bool returnBulletStatus()
        {
            return bulletStatus;
        }

        public void UpdateEntity()
        {
            if(bulletStatus)moveBullet();
        }

        public void DrawEntity()
        {
            if(bulletStatus)Raylib.DrawRectangleRec(bulletCollider, Color.GREEN);
        }

        private void moveBullet()
        {
            bulletCollider.x += (direction.X * 4f);
            bulletCollider.y += (direction.Y * 4f);
        }
        
    }
}
