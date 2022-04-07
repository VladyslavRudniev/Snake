using SnakeGameLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SnakeLulu
{
    class View
    {
        Model model;

        #region Singlton <-- Constructor
        static View view;
        protected View() { }
        protected View(Model model)
        {
            this.model = model;
        }
        public static View GetInstance(Model model)
        {
            if (view == null)
            {
                return view = new View(model);
            }
            return view;
        }
        #endregion

        public void ShowLevelInfo()
        {
            Console.SetCursorPosition(0, model.LevelInfo.CoordinateYForOutputLevelTask);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(model.LevelInfo.LevelTask);
            Console.ResetColor();
        }
        public void ShowLevelScore()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.SetCursorPosition(0, model.LevelInfo.CoordinateYForOutputScore);
            Console.WriteLine($"Score: {model.LevelInfo.Score}, ");
            Console.ResetColor();
        }
        public void DrawWalls()
        {
            Console.BackgroundColor = ConsoleColor.White;
            foreach (var el in model.Walls)
            {
                Console.SetCursorPosition(el.X, el.Y);
                Console.Write(' ');
            }
            Console.BackgroundColor = ConsoleColor.Black;
        }
        public void DrawPlayer()
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            for (int i = model.Player.Body.Length - 1; i >= 0; i--)
            {
                Console.SetCursorPosition(model.Player.Body[i].X, model.Player.Body[i].Y);
                Console.Write(model.Player.Body[i].Material);
            }
        }
        public void DrawApples()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            for (int i = 0; i < model.Apples.Count; i++)
            {
                Console.SetCursorPosition(model.Apples[i].X, model.Apples[i].Y);
                Console.Write(model.Apples[i].Material);
            }
        }

        public void ShowGameIntro(object arg)
        {
            CancellationToken cancellationToken = (CancellationToken)arg;
            string textIntro1 = "\u00abLulu snake\u00bb game";
            string[] textIntro2 = new string[]
            {
                ">>A snake named Lulu lives in a dangerous jungle.\n",
                ">>One day Lulu got out of her cozy house and decided to take\n",
                ">>a walk to the edge of the forest. But on the way there was an unexpected trouble.\n",
                ">>The river that flowed nearby overflowed its banks and carried Lulu away from home.\n",
                ">>Lulu needs your help to get back through the dangerous jungle!\n",
            };

            Console.SetCursorPosition(37, 11);
            foreach (var el in textIntro1)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    Console.Clear();
                    cancellationToken.ThrowIfCancellationRequested();
                }
                Console.Write(el);
                Thread.Sleep(100);
            }
            Thread.Sleep(2000);

            Console.SetCursorPosition(0, 13);
            foreach (var item in textIntro2)
            {
                foreach (var el in item)
                {
                    if (cancellationToken.IsCancellationRequested)
                    {
                        Console.Clear();
                        cancellationToken.ThrowIfCancellationRequested();
                    }
                    Console.Write(el);
                    Thread.Sleep(50);
                }
            }
            Console.ReadKey();
            Console.Clear();
        }
        public void ShowEndGame()
        {
            Console.SetCursorPosition(0, model.LevelInfo.CoordinateYForOutputScore);
            Console.WriteLine("                                                  ");
            Console.SetCursorPosition(0, model.LevelInfo.CoordinateYForOutputScore);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Game over!");
        }

        public void ShowMenu(MenuItems selectItem)
        {
            string menuItem0 = "Continue Game";
            string menuItem1 = "New game";
            string menuItem2 = "Load game";
            string menuItem3 = "Exit";
            
            Console.Clear();

            if (model.GameStatus == GameStatus.PausedGame)
            {
                Console.SetCursorPosition(37, 9);
                if (selectItem == MenuItems.ContinueGame)
                {
                    Console.ForegroundColor = ConsoleColor.DarkBlue;
                    Console.WriteLine(">" + menuItem0);
                    Console.ResetColor();
                }
                else
                {
                    Console.WriteLine(menuItem0);
                }
            }


            Console.SetCursorPosition(37, 12);
            if (selectItem == MenuItems.StartGame)
            {
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.WriteLine(">" + menuItem1);
                Console.ResetColor();
            }
            else
            {
                Console.WriteLine(menuItem1);
            }

            Console.SetCursorPosition(37, 15);
            if (selectItem == MenuItems.LoadGame)
            {
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.WriteLine(">" + menuItem2);
                Console.ResetColor();
            }
            else
            {
                Console.WriteLine(menuItem2);
            }

            Console.SetCursorPosition(37, 18);
            if (selectItem == MenuItems.Exit)
            {
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.WriteLine(">" + menuItem3);
                Console.ResetColor();
            }
            else
            {
                Console.WriteLine(menuItem3);
            }
        }
        public void ShowCountdownBefore()
        {
            for (int i = 3; i > 0; i--)
            {
                Console.SetCursorPosition(38, 15);
                Console.Write(i);
                Thread.Sleep(300);
                Console.Write('.');
                Thread.Sleep(300);
                Console.Write('.');
                Thread.Sleep(200);
                Console.Write('.');
                Thread.Sleep(200);
                Console.SetCursorPosition(38, 15);
                Console.Write("    ");
            }
        }
    }
}
