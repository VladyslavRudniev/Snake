using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

    }
}
