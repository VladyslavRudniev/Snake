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

        public void ShowLevelInfo(int levelNumber)
        {
            switch (levelNumber)
            {
                case 1:
                    Console.SetCursorPosition(0, 21);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Score 200 points to open the gate.");
                    Console.SetCursorPosition(0, 22);
                    break;
            }
            Console.WriteLine($"Score: {model.Score}, ");
            Console.ResetColor();
        }
        public void UpdateLevelInfo(int levelNumber)
        {
            switch (levelNumber)
            {
                case 1:
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.SetCursorPosition(0, 22);
                    break;
            }
            Console.WriteLine($"Score: {model.Score}, ");
            Console.ResetColor();
        }
        public void DrawWalls()
        {
            Console.BackgroundColor = ConsoleColor.White;
            foreach (var el in model.WallsForGameField)
            {
                Console.SetCursorPosition(el.X, el.Y);
                Console.Write(' ');
            }
            Console.BackgroundColor = ConsoleColor.Black;
        }
        public void DrawPlayer()
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            for (int i = 0; i < model.Player.Body.Length; i++)
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
        
        public void ShowMenu(MenuItems selectItem)
        {
            string menuItem1 = "Start game";
            string menuItem2 = "Load game";
            string menuItem3 = "Exit";

            Console.Clear();
            Console.SetCursorPosition(37, 12);
            if (selectItem == MenuItems.StartGame)
            {
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.WriteLine("> " + menuItem1);
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
                Console.WriteLine("> " + menuItem2);
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
                Console.WriteLine("> " + menuItem3);
                Console.ResetColor();
            }
            else
            {
                Console.WriteLine(menuItem3);
            }
        }
    }
}
