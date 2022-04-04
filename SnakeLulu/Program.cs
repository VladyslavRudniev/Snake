using System;
using SnakeGameLibrary;

namespace SnakeLulu
{
    class Program
    {
        static void Main(string[] args)
        {
            LevelBuilder levelBuilder = new LevelBuilder();
            levelBuilder.BuildWalls(0, 0, 30, Direction.Right);
            levelBuilder.BuildWalls(30, 0, 30, Direction.Down);
            levelBuilder.BuildWalls(30, 30, 30, Direction.Left);
            levelBuilder.BuildWalls(0, 30, 30, Direction.Up);
            levelBuilder.DrawWalls();
        }

    }
}
