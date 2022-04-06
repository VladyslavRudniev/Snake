using SnakeGameLibrary;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SnakeLulu
{
    class Model
    {
        #region <-- Fields and properties
        Random random = new Random();

        private LevelInfo levelInfo;
        internal LevelInfo LevelInfo { get => levelInfo; set => levelInfo = value; }

        private List<GameEntityUnit<char>> wallsForGameField;
        public List<GameEntityUnit<char>> WallsForGameField { get => wallsForGameField; set => wallsForGameField = value; }
        
        private List<GameEntityUnit<char>> wallsOther;
        public List<GameEntityUnit<char>> WallsOther { get => wallsOther; set => wallsOther = value; }
        
        private int levelScore;
        public int LevelScore { get => levelScore; set => levelScore = value; }

        private SnakeForConsole2D player;
        public SnakeForConsole2D Player { get => player; set => player = value; }

        private List<GameEntityUnit<char>> apples;
        public List<GameEntityUnit<char>> Apples { get => apples; set => apples = value; }

        private int widthGameField, heightGameField;
        private int countApples;

        private int score;
        public int Score { get => score; set => score = value; }

        #endregion

        #region Singlton <-- Constructor
        protected Model()
        {
            this.wallsForGameField = new List<GameEntityUnit<char>>();
            this.wallsOther = new List<GameEntityUnit<char>>();
            this.apples = new List<GameEntityUnit<char>>();
        }
        static Model model;
        public static Model GetInstance()
        {
            if (model == null)
            {
                return model = new Model();
            }
            return model;
        }
        #endregion

        public void BuildWall(int x, int y, int length, Direction direction)
        {
            switch (direction)
            {
                case Direction.Forward:
                    for (int i = 0; i < length; i++)
                    {
                        wallsForGameField.Add(new GameEntityUnit<char>(x, y--, 'W'));
                    }
                    break;
                case Direction.Back:
                    for (int i = 0; i < length; i++)
                    {
                        wallsForGameField.Add(new GameEntityUnit<char>(x, y++, 'W'));
                    }
                    break;
                case Direction.Left:
                    for (int i = 0; i < length; i++)
                    {
                        wallsForGameField.Add(new GameEntityUnit<char>(x--, y, 'W'));
                    }
                    break;
                case Direction.Right:
                    for (int i = 0; i < length; i++)
                    {
                        wallsForGameField.Add(new GameEntityUnit<char>(x++, y, 'W'));
                    }
                    break;
            }
        }
        public void BuildWallAboutGameField(int width, int height)
        {
            widthGameField = width;
            heightGameField = height;
            BuildWall(0, 0, width, Direction.Right);
            BuildWall(width, 0, height, Direction.Back);
            BuildWall(width, height, width, Direction.Left);
            BuildWall(0, height, height, Direction.Forward);
        }
        public void BuildPlayer()
        {
            player = new SnakeForConsole2D(13, new GameEntityUnit<char>[]
            {
                new GameEntityUnit<char>(20, 10, '@'),
                new GameEntityUnit<char>(21, 10, '#'),
                new GameEntityUnit<char>(22, 10, '#'),
                new GameEntityUnit<char>(23, 10, '#'),
                new GameEntityUnit<char>(24, 10, ' '),
            }, EntityState.Move, 0, Direction.Left);
        }
        public void BuildApples(int count)
        {
            countApples = count;
            int x, y;
            while (apples.Count != count)
            {
                x = random.Next(1, widthGameField);
                y = random.Next(1, heightGameField);
                if (!CheckCoordinatePlayer(x, y))
                    apples.Add(new GameEntityUnit<char>(x, y, '$'));
            }
        }

        private bool CheckCoordinatePlayer(int x, int y)
        {
            bool result = false;
            Parallel.For(0, player.Body.Length, i =>
            {
                if (player.Body[i].X == x && player.Body[i].Y == y)
                    result = true;
            });
            return result;
        }
        public bool CheckCoordinateWallsGameField()
        {
            bool result = false;
            Parallel.For(0, wallsForGameField.Count, i =>
            {
                if (wallsForGameField[i].X == player.Body[0].X && wallsForGameField[i].Y == player.Body[0].Y)
                    result = true;
            });
            return result;
        }
        public bool CheckCoordinateApplesForPlayer()
        {
            bool result = false;
            Parallel.For(0, apples.Count, i =>
            {
                if (apples[i].X == player.Body[0].X && apples[i].Y == player.Body[0].Y)
                {
                    score += 10;
                    apples.RemoveAt(i);
                    player.ChangeLengthAsync(1);
                    BuildApples(countApples);
                    result = true;
                }
            });
            return result;
        }

        public void MovePlayer()
        {
            player.ChangePositionAsync();
        }

        public void NewLevelInfo()
        {

        }
    }
}
