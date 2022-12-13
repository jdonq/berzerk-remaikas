using Raylib_cs;


namespace berzerk
{
    class Wall : IEntity
    {
        private Rectangle wallRec;
        private Player player;
        private Color wallClr = Color.BLUE;
        private int wallID = 0;
        private bool isVertical = false;

        public Wall(Player plr, float posX, float posY, bool isHorizontal)
        {
            player = plr;
            InitializeWall(posX, posY, isHorizontal);
        }

        public Wall(Player plr, float posX, float posY, bool isHorizontal, int id)
        {
            player = plr;
            InitializeWall(posX, posY, isHorizontal);
            wallID = id;
        }

        public void UpdateEntity()
        {
            if (Raylib.CheckCollisionRecs(wallRec, player.GetPlayerCollision()))
                player.KillPlayer();
        }

        public void DrawEntity()
        {
            Raylib.DrawRectangleRec(wallRec, wallClr);

        }

        public void SetAsEntryWall()
        {
            wallClr = Color.RED;
        }

        public Rectangle ReturnWallCollision()
        {
            return wallRec;
        }

        public int ReturnWallID()
        {
            return wallID;
        }

        public bool IsWallVertical()
        {
            return isVertical;
        }

        private void InitializeWall(float x, float y, bool isHorizontal)
        {
            if (!isHorizontal)
            {
                wallRec = new Rectangle(x, y, 10f, 140f);
                isVertical = true;
            }
            else
            {
                wallRec = new Rectangle(x, y, 100f, 10f);
            }
        }
    }
}
