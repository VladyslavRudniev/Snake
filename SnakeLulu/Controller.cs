using SnakeGameLibrary;
using System;
using System.IO;
using System.Media;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SnakeLulu
{
    class Controller
    {
        private static Model model;
        private static View view;
        private static CancellationTokenSource cancellation;
        private static Task ShowCountdownBeforeTask;
        private static SoundPlayer soundPlayer;

        static void Main(string[] args)
        {
            model = Model.GetInstance();
            view = View.GetInstance(model);
            model.GameStatus = GameStatus.NewGame;

            SetOptions();

            ShowGameIntro();
            OpenMenu();
        }

        static void SetOptions()
        {
            Console.OutputEncoding = Encoding.Unicode;
            Console.CursorVisible = false;
            Console.WindowWidth = 90;
            Console.WindowHeight = 30;
        }
        static void ShowGameIntro()
        {
            cancellation = new CancellationTokenSource();
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
        }
        static void ShowCountdownBefore()
        {
            cancellation = new CancellationTokenSource();
            CancellationToken cancellationToken = cancellation.Token;
            ShowCountdownBeforeTask = new Task(view.ShowCountdownBefore, cancellationToken);
            ShowCountdownBeforeTask.Start();
            try
            {
                ShowCountdownBeforeTask.Wait();
            }
            catch { }
        }

        static void OpenMenu()
        {
            soundPlayer = new SoundPlayer(@"Resources\Beep-melody.wav");
            if (!model.IsSoundOff)
            {
                soundPlayer.PlayLooping();
            }
            bool isSelected = false;
            int index = 0;
            if (model.GameStatus == GameStatus.NewGame)
            {
                view.ShowMenu(MenuItems.StartGame);
                index = 0;
            }
            if (model.GameStatus == GameStatus.PausedGame)
            {
                view.ShowMenu(MenuItems.ContinueGame);
                index = 3;
            }

            while (!isSelected)
            {
                view.ShowMenu((MenuItems)index);
                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.UpArrow:
                        --index;
                        if (model.GameStatus == GameStatus.NewGame)
                        {
                            if (index == -1)
                                index = 2;
                        }
                        if (model.GameStatus == GameStatus.PausedGame)
                        {
                            if (index == -1)
                                index = 4;
                        }
                        break;
                    case ConsoleKey.DownArrow:
                        ++index;
                        if (model.GameStatus == GameStatus.NewGame)
                        {
                            if (index == 3)
                                index = 0;
                        }
                        if (model.GameStatus == GameStatus.PausedGame)
                        {
                            if (index == 5)
                                index = 0;
                        }
                        break;
                    case ConsoleKey.Enter:
                        isSelected = true;
                        break;
                    case ConsoleKey.Tab:
                        model.IsSoundOff = model.IsSoundOff ? false : true;
                        if (model.IsSoundOff) soundPlayer.Stop();
                        else soundPlayer.PlayLooping();
                        break;
                }
            }

            soundPlayer.Stop();
            Console.Clear();

            switch (index)
            {
                case 0:
                    model.CurrentLevelNumber = 1;
                    CreateNewGameLevel();
                    PlayGameAsync();
                    ControlGame();
                    break;
                case 1:
                    model.DeserializeModel();
                    model.GameStatus = GameStatus.FlowGame;
                    PlayGameAsync();
                    ControlGame();
                    break;
                case 2:
                    Environment.Exit(0);
                    break;
                case 3:
                    model.GameStatus = GameStatus.FlowGame;
                    PlayGameAsync();
                    ControlGame();
                    break;
                case 4:
                    model.SerializeModel();
                    OpenMenu();
                    break;
            }
        }
        static void CreateNewGameLevel()
        {
            model.NewLevelInfo();
            model.ClearModel();

            model.BuildWalls();
            model.BuildGate();

            model.BuildPlayer();
            
            model.BuildApples();
            model.GameStatus = GameStatus.FlowGame;
        }
        static async void PlayGameAsync()
        {
            await Task.Factory.StartNew(PlayGame);
        }
        static void PlayGame()
        {
            Console.Clear();
            soundPlayer = new SoundPlayer(@"Resources\Death-melody.wav");
            view.DrawWalls();
            view.DrawPlayer();
            view.DrawApples();
            view.ShowLevelInfo();
            ShowCountdownBefore();

            if (model.GameStatus == GameStatus.FlowGame)
            {
                view.ShowLevelScore();
                if (model.CheckImplementationLevelTask())
                {
                    view.DrawGate();
                }
            }

            while (model.GameStatus == GameStatus.FlowGame)
            {
                model.MovePlayer();
                view.DrawPlayer();
                if (model.CheckCoordinateApplesForPlayer())
                {
                    if(model.CheckForChangesAfterAddingScore())
                    {
                        view.PutAwayApples();
                        view.DrawGate();
                    }
                    else
                    {
                        view.DrawApples();
                    }
                    view.ShowLevelScore();
                }

                if (model.CheckCoordinateWallsForPlayer())
                {
                    model.GameStatus = GameStatus.OverGame;
                }
                if (model.CheckCoordinateGateForPlayer() && model.LevelInfo.Score == model.LevelInfo.RequiredScore)
                {
                    model.CurrentLevelNumber++;
                    //TODO smooth transition
                    model.GameStatus = GameStatus.NextLevel;
                }

                if (model.CheckCoordinatePlayerForPlayer())
                {
                    model.GameStatus = GameStatus.OverGame;
                }
                Thread.Sleep(300 - (model.Player.Speed * 20));
            }

            if (model.GameStatus == GameStatus.NextLevel)
            {
                view.ShowMessageNextLevel();
            }
        }
        static void ControlGame()
        {
            while (model.GameStatus == GameStatus.FlowGame)
            {
                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.UpArrow:
                        model.Player.Turn(Direction.Forward);
                        break;
                    case ConsoleKey.DownArrow:
                        model.Player.Turn(Direction.Back);
                        break;
                    case ConsoleKey.LeftArrow:
                        model.Player.Turn(Direction.Left);
                        break;
                    case ConsoleKey.RightArrow:
                        model.Player.Turn(Direction.Right);
                        break;
                    case ConsoleKey.Escape:
                        model.GameStatus = GameStatus.PausedGame;
                        if (!ShowCountdownBeforeTask.IsCompleted)
                            cancellation.Cancel();
                        OpenMenu();
                        break;
                    default:
                        if (!ShowCountdownBeforeTask.IsCompleted)
                            cancellation.Cancel();
                        break;
                }
                Thread.Sleep(300 - (model.Player.Speed * 20));
            }
            if (model.GameStatus == GameStatus.OverGame)
            {
                if (!model.IsSoundOff)
                {
                    soundPlayer.Play();
                }
                view.ShowEndGame();
                Console.ReadKey();
                soundPlayer.Stop();
                model.GameStatus = GameStatus.NewGame;
                OpenMenu();
            }
            if (model.GameStatus == GameStatus.NextLevel)
            {
                if (model.CurrentLevelNumber == 5)
                {
                    cancellation = new CancellationTokenSource();
                    CancellationToken cancellationToken = cancellation.Token;
                    Task task = new Task(view.ShowGameСompletion, cancellationToken);
                    task.Start();
                    Console.ReadKey();
                    try
                    {
                        if (!task.IsCompleted)
                            cancellation.Cancel();
                        task.Wait();
                    }
                    catch { }
                    model.GameStatus = GameStatus.NewGame;
                    OpenMenu();
                }
                else
                {
                    CreateNewGameLevel();
                    PlayGameAsync();
                    ControlGame();
                }
            }
        }
    }
}
