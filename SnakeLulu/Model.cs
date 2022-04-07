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

        private List<GameEntityUnit<char>> walls;
        public List<GameEntityUnit<char>> Walls { get => walls; set => walls = value; }
        
        private int levelScore;
        public int LevelScore { get => levelScore; set => levelScore = value; }

        private SnakeForConsole2D player;
        public SnakeForConsole2D Player { get => player; set => player = value; }

        private List<GameEntityUnit<char>> apples;
        public List<GameEntityUnit<char>> Apples { get => apples; set => apples = value; }

        private GameStatus gameStatus;
        public GameStatus GameStatus { get => gameStatus; set => gameStatus = value; }

        #endregion

        #region Singlton <-- Constructor
        protected Model()
        {
            this.walls = new List<GameEntityUnit<char>>();
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

        public void ClearModel()
        {
            this.walls = new List<GameEntityUnit<char>>();
            this.apples = new List<GameEntityUnit<char>>();
        }
        public void BuildWall(int x, int y, int length, Direction direction)
        {
            switch (direction)
            {
                case Direction.Forward:
                    for (int i = 0; i < length; i++)
                    {
                        walls.Add(new GameEntityUnit<char>(x, y--, 'W'));
                    }
                    break;
                case Direction.Back:
                    for (int i = 0; i < length; i++)
                    {
                        walls.Add(new GameEntityUnit<char>(x, y++, 'W'));
                    }
                    break;
                case Direction.Left:
                    for (int i = 0; i < length; i++)
                    {
                        walls.Add(new GameEntityUnit<char>(x--, y, 'W'));
                    }
                    break;
                case Direction.Right:
                    for (int i = 0; i < length; i++)
                    {
                        walls.Add(new GameEntityUnit<char>(x++, y, 'W'));
                    }
                    break;
            }
        }
        public void BuildWalls()
        {
            foreach (var item in levelInfo.ParamForBuildWalls)
            {
                BuildWall(item.StartX, item.StartY, item.Length, item.Direction);
            }
        }
        public void BuildPlayer()
        {
            player = new SnakeForConsole2D(13, null, EntityState.Move, 0, Direction.Left);
            GameEntityUnit<char>[] temp = new GameEntityUnit<char>[5];
            char symbol;
            if (levelInfo.DirectionStartBodyPlayer == Direction.Forward)
            {
                player.Direction = Direction.Back;
                for (int i = 0; i < temp.Length; i++)
                {
                    if (i == 0)
                    {
                        symbol = '@';
                    }
                    else if (i == temp.Length - 1)
                    {
                        symbol = ' ';
                    }
                    else
                    {
                        symbol = '#';
                    }
                    temp[i] = new GameEntityUnit<char>(levelInfo.CoordinateXStartPlayerPos, levelInfo.CoordinateYStartPlayerPos, symbol);
                }
            }
            else if (levelInfo.DirectionStartBodyPlayer == Direction.Back)
            {
                player.Direction = Direction.Forward;
                for (int i = 0; i < temp.Length; i++)
                {
                    if (i == 0)
                    {
                        symbol = '@';
                    }
                    else if (i == temp.Length - 1)
                    {
                        symbol = ' ';
                    }
                    else
                    {
                        symbol = '#';
                    }
                    temp[i] = new GameEntityUnit<char>(levelInfo.CoordinateXStartPlayerPos, levelInfo.CoordinateYStartPlayerPos, symbol);
                }
            }
            else if (levelInfo.DirectionStartBodyPlayer == Direction.Left)
            {
                player.Direction = Direction.Right;
                for (int i = 0; i < temp.Length; i++)
                {
                    if (i == 0)
                    {
                        symbol = '@';
                    }
                    else if (i == temp.Length - 1)
                    {
                        symbol = ' ';
                    }
                    else
                    {
                        symbol = '#';
                    }
                    temp[i] = new GameEntityUnit<char>(levelInfo.CoordinateXStartPlayerPos, levelInfo.CoordinateYStartPlayerPos, symbol);
                }
            }
            else if (levelInfo.DirectionStartBodyPlayer == Direction.Right)
            {
                player.Direction = Direction.Left;
                for (int i = 0; i < temp.Length; i++)
                {
                    if (i == 0)
                    {
                        symbol = '@';
                    }
                    else if (i == temp.Length - 1)
                    {
                        symbol = ' ';
                    }
                    else
                    {
                        symbol = '#';
                    }
                    temp[i] = new GameEntityUnit<char>(levelInfo.CoordinateXStartPlayerPos, levelInfo.CoordinateYStartPlayerPos, symbol);
                }
            }
            player.Body = temp;
        }
        public void BuildApples()
        {
            int x, y;
            while (apples.Count != levelInfo.MaxCountOfApples)
            {
                x = random.Next(1, levelInfo.MaxWidthGameField - 1);
                y = random.Next(1, levelInfo.MaxHeightGameField - 1);
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
        public bool CheckCoordinatePlayerForPlayer()
        {
            bool result = false;
            Parallel.For(1, player.Body.Length, i =>
            {
                if (player.Body[i].X == player.Body[0].X && player.Body[i].Y == player.Body[0].Y)
                    result = true;
            });
            return result;
        }
        public bool CheckCoordinateWallsGameField()
        {
            bool result = false;
            Parallel.For(0, walls.Count, i =>
            {
                if (walls[i].X == player.Body[0].X && walls[i].Y == player.Body[0].Y)
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
                    levelInfo.Score += 10;
                    apples.RemoveAt(i);
                    BuildApples();
                    player.ChangeLengthAsync(1);
                    CheckForChangesAfterAddingScore();
                    result = true;
                }
            });
            return result;
        }

        public void CheckForChangesAfterAddingScore()
        {
            int valueIncreaseSpeed = levelInfo.Score / levelInfo.SpeedIncreaseFrequency;
            player.Speed = valueIncreaseSpeed;
        }

        public void MovePlayer()
        {
            player.ChangePositionAsync();
        }

        public void NewLevelInfo(int levelNumber)
        {
            levelInfo = new LevelInfo();
            switch(levelNumber)
            {
                case 1:
                    levelInfo.Score = 0;
                    levelInfo.RequiredScore = 300;
                    levelInfo.MaxCountOfApples = 2;
                    levelInfo.LevelNumber = 1;
                    levelInfo.LevelTask = "Score 300 points to open next level.";
                    levelInfo.CoordinateYForOutputLevelTask = 21;
                    levelInfo.CoordinateYForOutputScore = 22;
                    levelInfo.MaxHeightGameField = 20;
                    levelInfo.MaxWidthGameField = 20;
                    levelInfo.SpeedIncreaseFrequency = 30;
                    levelInfo.CoordinateXStartPlayerPos = 10;
                    levelInfo.CoordinateYStartPlayerPos = 10;
                    levelInfo.DirectionStartBodyPlayer = Direction.Back;
                    levelInfo.ParamForBuildWalls = new ParamForBuildWall[]
                    {
                        new ParamForBuildWall(0, 0, 20, Direction.Right),
                        new ParamForBuildWall(20, 0, 20, Direction.Back),
                        new ParamForBuildWall(20, 20, 20, Direction.Left),
                        new ParamForBuildWall(0, 20, 20, Direction.Forward),
            };
                    break;
            }
        }
    }
}
