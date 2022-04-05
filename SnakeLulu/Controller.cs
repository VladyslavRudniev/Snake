using SnakeGameLibrary;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SnakeLulu
{
    class Controller
    {
        private static Model model;
        private static View view;
        private static GameStatus gameStatus;
        private static int levelNumber;

        static void Main(string[] args)
        {
            model = Model.GetInstance();
            view = View.GetInstance(model);
            levelNumber = 1;

            SetOptions();

            ShowGameInroAndOpenMenu();     
        }

        static void SetOptions()
        {
            Console.OutputEncoding = Encoding.Unicode;
            Console.CursorVisible = false;
            Console.WindowWidth = 90;
            Console.WindowHeight = 30;
        }
        static void ShowGameInroAndOpenMenu()
        {
            CancellationTokenSource cancellation = new CancellationTokenSource();
            CancellationToken cancellationToken = cancellation.Token;
            Task task = new Task(view.ShowGameIntro, cancellationToken);
            task.Start();
            Console.ReadKey();
            try
            {
                if (!task.IsCompleted)
                    cancellation.Cancel();
                task.Wait();
            }
            catch { }

            view.ShowMenu(MenuItems.StartGame);
            int index = 0;
            bool isSelected = false;
            while (!isSelected)
            {
                view.ShowMenu((MenuItems)index);
                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.UpArrow:
                        --index;
                        if (index == -1)
                            index = 2;
                        break;
                    case ConsoleKey.DownArrow:
                        ++index;
                        if (index == 3)
                            index = 0;
                        break;
                    case ConsoleKey.Enter:
                        isSelected = true;
                        break;
                }
            }
            Console.Clear();
            switch (index)
            {
                case 0:
                    BuildGameModel();
                    PlayGameAsync();
                    ControlSnake();
                    break;
                case 1:

                    //break;
                case 2:
                    Environment.Exit(0);
                    break;
            }
        }
        static void BuildGameModel()
        {
            view.ShowLevelInfo(levelNumber);
            model.BuildWallAboutGameField(40, 20);
            view.DrawWalls();

            model.BuildPlayer();

            model.BuildApples(2);
            view.DrawApples();

            gameStatus = GameStatus.Play;
        }
        static async void PlayGameAsync()
        {
            await Task.Factory.StartNew(PlayGame);
        }
        static void PlayGame()
        {
            while (true)
            {
                model.MovePlayer();
                view.DrawPlayer();

                if (model.CheckCoordinateApplesForPlayer())
                {
                    view.UpdateLevelInfo(levelNumber);
                    view.DrawApples();
                }
                
                if (model.CheckCoordinateWallsGameField())
                {
                    break;
                }
                Thread.Sleep(400);
            }

            gameStatus = GameStatus.Over;
            Console.SetCursorPosition(0, 24);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Game over!!!");
            Console.ReadKey();
        }
        static void ControlSnake()
        {
            while (gameStatus == GameStatus.Play)
            {
                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.UpArrow:
                        model.Player.TurnAsync(Direction.Forward);
                        break;
                    case ConsoleKey.DownArrow:
                        model.Player.TurnAsync(Direction.Back);
                        break;
                    case ConsoleKey.LeftArrow:
                        model.Player.TurnAsync(Direction.Left);
                        break;
                    case ConsoleKey.RightArrow:
                        model.Player.TurnAsync(Direction.Right);
                        break;
                }
                Thread.Sleep(5);
            }
        }

    }
}
