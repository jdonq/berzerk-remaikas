using System.Numerics;
using System;
using System.Collections.Generic;
using NUnit.Framework;


namespace berzerk 
{
    [TestFixture]
    class Tests
    {
        private Direction[,] cells = new Direction[3, 5] {{0, 0, 0, 0, 0}, {0, 0, 0, 0, 0}, {0, 0, 0, 0, 0}};
        private Random randomSeed = new Random();

        public Tests()
        {
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
    }
}