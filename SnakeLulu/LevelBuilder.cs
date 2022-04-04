using System;
using System.Threading.Tasks;
using SnakeGameLibrary;

namespace SnakeLulu
{
    class LevelBuilder
    {
        private GameEntityUnit<char>[] walls;
        public GameEntityUnit<char>[] Walls { get => walls; set => walls = value; }

        private int levelScore;
        public int LevelScore { get => levelScore; set => levelScore = value; }

        public LevelBuilder()
        {
            walls = new GameEntityUnit<char>[0];
        }
        public void BuildWalls(int x, int y, int length, Direction direction)
        {
            GameEntityUnit<char>[] temp = new GameEntityUnit<char>[walls.Length + length];
            walls.CopyTo(temp, 0);
            switch (direction)
            {
                case Direction.Forward:
                    Parallel.For(walls.Length, temp.Length, i => temp[i] = new GameEntityUnit<char>(x, y--, 'W'));
                    break;
                case Direction.Back:
                    Parallel.For(walls.Length, temp.Length, i => temp[i] = new GameEntityUnit<char>(x, y++, 'W'));
                    break;
                case Direction.Left:
                    Parallel.For(walls.Length, temp.Length, i => temp[i] = new GameEntityUnit<char>(x--, y, 'W'));
                    break;
                case Direction.Right:
                    Parallel.For(walls.Length, temp.Length, i => temp[i] = new GameEntityUnit<char>(x++, y, 'W'));
                    break;
            }
            walls = temp;
        }
        public void DrawWalls()
        {
            Console.BackgroundColor = ConsoleColor.White;
            Parallel.ForEach(walls, el =>
            {
                Console.SetCursorPosition(el.X, el.Y);
                Console.Write(' ');
            });
            Console.BackgroundColor = ConsoleColor.Black;
        }
    }
}
