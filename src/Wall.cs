using System.Numerics;
using System.Collections.Generic;

using Raylib_cs;


namespace berzerk
{
    class Wall : IEntity
    {
        private Rectangle wallRec; 
        private Player player;


        public Wall(Player plr, float posX, float posY, bool isHorizontal)
        {
            player = plr;
            if(!isHorizontal)
                wallRec = new Rectangle(posX, posY, 10f, 140f);
            else 
                wallRec = new Rectangle(posX, posY, 100f, 10f);
        }

        public void UpdateEntity()
        {
            if(Raylib.CheckCollisionRecs(wallRec, player.GetPlayerCollision())) 
                player.KillPlayer();
        }

        public void DrawEntity()
        {
            Raylib.DrawRectangleRec(wallRec, Color.BLUE);
            
        }
        
        public Rectangle returnWallCollision()
        {
            return wallRec;
        }

    }
}
