using System.Numerics;
using System;
using System.Collections.Generic;
using Raylib_cs;


namespace berzerk.tests
{
    [TestFixture]
    class Tests
    {
        private Direction[,] cells = new Direction[3, 5] {{0, 0, 0, 0, 0}, {0, 0, 0, 0, 0}, {0, 0, 0, 0, 0}};
        private Random randomSeed = new Random();

        private Vector2 playerPos = new Vector2(1f, 1f);
        private bool isOnLeftSide = false;
        private bool lastSide = false;
        private Rectangle robotCollision = new Rectangle(0f, 0f, 22f, 36f);

        public Tests()
        {
            MoveRobot();
            CheckMaze();
        }

        [Test]
        [Retry(10)]
        public void CheckMaze()
        {
            GenerateMaze(0, 0, Direction.Start);
            foreach(int i in cells)
            {
                Assert.AreNotEqual(i, 0);
            }
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
                }
                else if(destination.X < x)
                {
                    dir = Direction.South;
                }
                else if(destination.Y > y)
                {
                    dir = Direction.West;
                }
                else if(destination.Y < y)
                {
                    dir = Direction.East;
                }

                GenerateMaze((int)destination.X, (int)destination.Y, dir);
            }            
        }

        [Test]
        public void MoveRobot()
        {
            if(playerPos.X - robotCollision.x < 0)
            {
                isOnLeftSide = true;
                robotCollision.x -= 1f;
            }
            if(playerPos.X - robotCollision.x > 0)
            {
                isOnLeftSide = false;
                robotCollision.x += 1f;
            }
            if(playerPos.Y - robotCollision.y < 0)
            {
                robotCollision.y -= 1f;
            }
            if(playerPos.Y - robotCollision.y > 0)
            {
                robotCollision.y += 1f;
            }

            Assert.AreEqual((int)robotCollision.x, (int)playerPos.X, "X are not at that same location");
            Assert.AreEqual((int)robotCollision.y, (int)playerPos.Y, "Y are not at that same location");

            lastSide = isOnLeftSide;
        }
    }
}