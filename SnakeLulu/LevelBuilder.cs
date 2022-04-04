using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SnakeGameLibrary;

namespace SnakeLulu
{
    class LevelBuilder
    {
        private List<GameEntityUnit<char>> walls;
        public List<GameEntityUnit<char>> Walls { get => walls; set => walls = value; }

        private int levelScore;
        public int LevelScore { get => levelScore; set => levelScore = value; }
        
        private SnakeForConsole2D player;
        public SnakeForConsole2D Player { get => player; set => player = value; }

        public LevelBuilder()
        {
            walls = new List<GameEntityUnit<char>>();
        }
        public void BuildWall(int x, int y, int length, Direction direction)
        {
            switch (direction)
            {
                case Direction.Forward:
                    Parallel.For(0, length, i => walls.Add(new GameEntityUnit<char>(x, y--, 'W')));
                    break;
                case Direction.Back:
                    Parallel.For(0, length, i => walls.Add(new GameEntityUnit<char>(x, y++, 'W')));
                    break;
                case Direction.Left:
                    Parallel.For(0, length, i => walls.Add(new GameEntityUnit<char>(x--, y, 'W')));
                    break;
                case Direction.Right:
                    Parallel.For(0, length, i => walls.Add(new GameEntityUnit<char>(x++, y, 'W')));
                    break;
            }
        }
        public void BuildWallAboutGameField(int width, int height)
        {
            BuildWall(0, 0, width, Direction.Right);
            BuildWall(width, 0, height, Direction.Back);
            BuildWall(width, height, width, Direction.Left);
            BuildWall(0, height, height, Direction.Forward);
        }
        public void DrawWalls()
        {
            Console.BackgroundColor = ConsoleColor.White;
            foreach (var el in walls)
            {
                Console.SetCursorPosition(el.X, el.Y);
                Console.Write(' ');
            }
            Console.BackgroundColor = ConsoleColor.Black;
        }
        public void BuildPlayer()
        {
            player = new SnakeForConsole2D(13, new GameEntityUnit<char>[]
            {
                new GameEntityUnit<char>(20, 10, '@', "Head"),
                new GameEntityUnit<char>(21, 10, '#', "Body"),
                new GameEntityUnit<char>(22, 10, '#', "Body"),
                new GameEntityUnit<char>(23, 10, '#', "Body"),
                new GameEntityUnit<char>(24, 10, ' ', "PhantomTail"),
            }, EntityState.Move, 0, Direction.Left);
        }
        public void DrawPlayer()
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            for (int i = 0; i < player.Body.Length; i++)
            {
                Console.SetCursorPosition(player.Body[i].X, player.Body[i].Y);
                Console.Write(player.Body[i].Material);
                
            }
        }
    }
}
