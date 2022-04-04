using System;
using System.Threading;
using SnakeGameLibrary;

namespace SnakeLulu
{
    class Program
    {
        static void Main(string[] args)
        {
            LevelBuilder levelBuilder = new LevelBuilder();
            levelBuilder.BuildWallAboutGameField(40, 20);
            levelBuilder.DrawWalls();

            levelBuilder.BuildPlayer();

            levelBuilder.BuildApples(2);
            levelBuilder.DrawApples();

            while (true)
            {
                levelBuilder.DrawPlayer();
                levelBuilder.Player.ChangePositionAsync();
                Thread.Sleep(500);
            }

            Console.ReadKey();
        }

    }
}
