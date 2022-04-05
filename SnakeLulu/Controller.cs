using SnakeGameLibrary;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SnakeLulu
{
    class Controller
    {
        static Model model;
        static View view;

        private static Task playGameTask;
        private static GameStatus gameStatus;
        private static LevelBuilder levelBuilder;
        static void Main(string[] args)
        {
            model = Model.GetInstance();
            view = View.GetInstance(model);


            StartGame();

            playGameTask = new Task(PlayGame);
            playGameTask.Start();

            while (gameStatus == GameStatus.Play)
            {
                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.UpArrow:
                        levelBuilder.Player.TurnAsync(Direction.Forward);
                        break;
                    case ConsoleKey.DownArrow:
                        levelBuilder.Player.TurnAsync(Direction.Back);
                        break;
                    case ConsoleKey.LeftArrow:
                        levelBuilder.Player.TurnAsync(Direction.Left);
                        break;
                    case ConsoleKey.RightArrow:
                        levelBuilder.Player.TurnAsync(Direction.Right);
                        break;
                }
            }
        }

        static void StartGame()
        {
            levelBuilder = new LevelBuilder();

            levelBuilder.BuildWallAboutGameField(40, 20);
            levelBuilder.DrawWalls();

            levelBuilder.BuildPlayer();

            levelBuilder.BuildApples(2);
            levelBuilder.DrawApples();

            gameStatus = GameStatus.Play;
        }
        static void PlayGame()
        {
            bool DrawApples = false;
            while (true)
            {
                if (DrawApples)
                {
                    levelBuilder.DrawApples();
                    DrawApples = false;
                }

                levelBuilder.DrawPlayer();
                levelBuilder.Player.ChangePositionAsync();

                if (levelBuilder.CheckCoordinateApplesForPlayer())
                {
                    DrawApples = true;
                }

                if (levelBuilder.CheckCoordinateWallsGameField(levelBuilder.Player[0].X, levelBuilder.Player[0].Y))
                {
                    break;
                }
                Thread.Sleep(500);
            }

            gameStatus = GameStatus.Over;
            Console.SetCursorPosition(0, 24);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Game over!!!");
            Console.ReadKey();
        }

    }
}
