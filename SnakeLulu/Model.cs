using SnakeGameLibrary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SnakeLulu
{
    public class Model
    {
        #region <-- Fields and properties
        Random random = new Random();

        private bool isSoundOff;
        public bool IsSoundOff { get => isSoundOff; set => isSoundOff = value; }

        private int currentLevelNumber;
        public int CurrentLevelNumber { get => currentLevelNumber; set => currentLevelNumber = value; }

        private LevelInfo levelInfo;
        public LevelInfo LevelInfo { get => levelInfo; set => levelInfo = value; }

        private List<GameEntityUnit<char>> walls;
        public List<GameEntityUnit<char>> Walls { get => walls; set => walls = value; }

        private List<GameEntityUnit<char>> gate;
        public List<GameEntityUnit<char>> Gate { get => gate; set => gate = value; }

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
            this.gate = new List<GameEntityUnit<char>>();
            this.walls = new List<GameEntityUnit<char>>();
            this.apples = new List<GameEntityUnit<char>>();
            this.player = new SnakeForConsole2D();
            this.levelInfo = new LevelInfo();
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
            this.gate = new List<GameEntityUnit<char>>();
            this.walls = new List<GameEntityUnit<char>>();
            this.apples = new List<GameEntityUnit<char>>();
        }

        #region Build methods
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
        public void BuildGate()
        {
            int x = levelInfo.Gate.StartX;
            int y = levelInfo.Gate.StartY;
            switch (levelInfo.Gate.Direction)
            {
                case Direction.Forward:
                    for (int i = 0; i < levelInfo.Gate.Length; i++)
                    {
                        gate.Add(new GameEntityUnit<char>(x, y--, '/'));
                    }
                    break;
                case Direction.Back:
                    for (int i = 0; i < levelInfo.Gate.Length; i++)
                    {
                        gate.Add(new GameEntityUnit<char>(x, y++, '/'));
                    }
                    break;
                case Direction.Left:
                    for (int i = 0; i < levelInfo.Gate.Length; i++)
                    {
                        gate.Add(new GameEntityUnit<char>(x--, y, '/'));
                    }
                    break;
                case Direction.Right:
                    for (int i = 0; i < levelInfo.Gate.Length; i++)
                    {
                        gate.Add(new GameEntityUnit<char>(x++, y, '/'));
                    }
                    break;
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
                    temp[i] = new GameEntityUnit<char>(levelInfo.CoordinateXStartPlayerPos, levelInfo.CoordinateYStartPlayerPos - i, symbol);
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
                    temp[i] = new GameEntityUnit<char>(levelInfo.CoordinateXStartPlayerPos, levelInfo.CoordinateYStartPlayerPos + i, symbol);
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
                    temp[i] = new GameEntityUnit<char>(levelInfo.CoordinateXStartPlayerPos - i, levelInfo.CoordinateYStartPlayerPos, symbol);
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
                    temp[i] = new GameEntityUnit<char>(levelInfo.CoordinateXStartPlayerPos + i, levelInfo.CoordinateYStartPlayerPos, symbol);
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
                if (!CheckCoordinatePlayer(x, y, 0) & !CheckCoordinateWalls(x, y) & !CheckCoordinateApples(x, y))
                {
                    apples.Add(new GameEntityUnit<char>(x, y, '$'));
                }
            }
        }
        #endregion

        #region Checking methods
        private bool CheckCoordinatePlayer(int x, int y, int startIndex)
        {
            bool result = false;
            for (int i = startIndex; i < player.Body.Length; i++)
            {
                if (player.Body[i].X == x && player.Body[i].Y == y)
                    result = true;
            }
            return result;
        }
        public bool CheckCoordinatePlayerForPlayer()
            => CheckCoordinatePlayer(player.Body[0].X, player.Body[0].Y, 3);
        public bool CheckCoordinateWalls(int x, int y)
        {
            bool result = false;
            Parallel.For(0, walls.Count, i =>
            {
                if (walls[i].X == x && walls[i].Y == y)
                    result = true;
            });
            return result;
        }
        public bool CheckCoordinateWallsForPlayer()
    => CheckCoordinateWalls(player.Body[0].X, player.Body[0].Y);
        public bool CheckCoordinateGateForPlayer()
        {
            bool result = false;
            Parallel.For(0, gate.Count, i =>
            {
                if (gate[i].X == player.Body[0].X && gate[i].Y == player.Body[0].Y)
                    result = true;
            });
            return result;
        }
        public bool CheckCoordinateApples(int x, int y)
        {
            bool result = false;
            for (int i = 0; i < apples.Count; i++)
            {
                if (apples[i].X == x && apples[i].Y == y)
                {
                    apples.RemoveAt(i);
                    BuildApples();
                    result = true;
                }
            }
            return result;
        }
        public bool CheckCoordinateApplesForPlayer()
        {
            if(CheckCoordinateApples(player.Body[0].X, player.Body[0].Y))
            {
                player.ChangeLength(1);
                levelInfo.Score += 10;
                return true;
            }
            return false;
        }
        public bool CheckForChangesAfterAddingScore()
        {
            int valueIncreaseSpeed = levelInfo.Score / levelInfo.SpeedIncreaseFrequency;
            player.Speed = valueIncreaseSpeed;

            if (levelInfo.Score == levelInfo.RequiredScore)
            {
                return true;
            }
            return false;
        }
        public bool CheckImplementationLevelTask()
        {
            if (levelInfo.Score == levelInfo.RequiredScore)
            {
                return true;
            }
            return false;
        }
        #endregion

        public void MovePlayer()
        {
            player.ChangePosition();
        }

        public void NewLevelInfo()
        {
            levelInfo = new LevelInfo();
            switch(currentLevelNumber)
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
                    levelInfo.Gate = new ParamForBuildWall(20, 8, 4, Direction.Back);
                    break;
                case 2:
                    levelInfo.Score = 0;
                    levelInfo.RequiredScore = 270;
                    levelInfo.MaxCountOfApples = 2;
                    levelInfo.LevelNumber = 2;
                    levelInfo.LevelTask = "Score 270 points to open next level.";

                    levelInfo.CoordinateYForOutputLevelTask = 16;
                    levelInfo.CoordinateYForOutputScore = 17;
                    levelInfo.MaxHeightGameField = 15;
                    levelInfo.MaxWidthGameField = 40;

                    levelInfo.SpeedIncreaseFrequency = 30;
                    levelInfo.CoordinateXStartPlayerPos = 6;
                    levelInfo.CoordinateYStartPlayerPos = 3;
                    levelInfo.DirectionStartBodyPlayer = Direction.Left;

                    levelInfo.ParamForBuildWalls = new ParamForBuildWall[]
                    {
                        new ParamForBuildWall(0, 0, 40, Direction.Right),
                        new ParamForBuildWall(40, 0, 15, Direction.Back),
                        new ParamForBuildWall(40, 15, 40, Direction.Left),
                        new ParamForBuildWall(0, 15, 15, Direction.Forward),
                        new ParamForBuildWall(19, 14, 7, Direction.Forward),
                        new ParamForBuildWall(29, 1, 7, Direction.Back),
                        new ParamForBuildWall(1, 5, 17, Direction.Right),
                    };
                    levelInfo.Gate = new ParamForBuildWall(40, 6, 4, Direction.Back);
                    break;
                case 3:
                    levelInfo.Score = 0;
                    levelInfo.RequiredScore = 270;
                    levelInfo.MaxCountOfApples = 2;
                    levelInfo.LevelNumber = 3;
                    levelInfo.LevelTask = "Score 270 points to open next level.";

                    levelInfo.CoordinateYForOutputLevelTask = 21;
                    levelInfo.CoordinateYForOutputScore = 22;
                    levelInfo.MaxHeightGameField = 20;
                    levelInfo.MaxWidthGameField = 30;

                    levelInfo.SpeedIncreaseFrequency = 30;
                    levelInfo.CoordinateXStartPlayerPos = 5;
                    levelInfo.CoordinateYStartPlayerPos = 4;
                    levelInfo.DirectionStartBodyPlayer = Direction.Left;

                    levelInfo.ParamForBuildWalls = new ParamForBuildWall[]
                    {
                        new ParamForBuildWall(0, 0, 30, Direction.Right),
                        new ParamForBuildWall(14, 1, 2, Direction.Back),
                        new ParamForBuildWall(30, 0, 20, Direction.Back),
                        new ParamForBuildWall(30, 20, 9, Direction.Left),
                        new ParamForBuildWall(21, 20, 15, Direction.Forward),
                        new ParamForBuildWall(21, 8, 4, Direction.Left),
                        new ParamForBuildWall(15, 8, 9, Direction.Left),
                        new ParamForBuildWall(10, 8, 2, Direction.Back),
                        new ParamForBuildWall(10, 10, 6, Direction.Right),
                        new ParamForBuildWall(16, 10, 10, Direction.Back),
                        new ParamForBuildWall(16, 20, 16, Direction.Left),
                        new ParamForBuildWall(0, 20, 20, Direction.Forward),
                        new ParamForBuildWall(21, 20, 5, Direction.Left),
                    };
                    levelInfo.Gate = new ParamForBuildWall(24, 20, 4, Direction.Right);
                    break;
                case 4:
                    levelInfo.Score = 0;
                    levelInfo.RequiredScore = 360;
                    levelInfo.MaxCountOfApples = 1;
                    levelInfo.MaxCountOfSnakeBots = 0;
                    levelInfo.CoordinateXStartSnakeBotPos = 10;
                    levelInfo.CoordinateYStartSnakeBotPos = 5;
                    levelInfo.LevelNumber = 4;
                    levelInfo.LevelTask = "Score 360 points to open next level.";
                    levelInfo.CoordinateYForOutputLevelTask = 21;
                    levelInfo.CoordinateYForOutputScore = 22;
                    levelInfo.MaxHeightGameField = 20;
                    levelInfo.MaxWidthGameField = 30;
                    levelInfo.SpeedIncreaseFrequency = 30;
                    levelInfo.CoordinateXStartPlayerPos = 10;
                    levelInfo.CoordinateYStartPlayerPos = 14;
                    levelInfo.DirectionStartBodyPlayer = Direction.Back;
                    levelInfo.ParamForBuildWalls = new ParamForBuildWall[]
                    {
                        new ParamForBuildWall(0, 0, 30, Direction.Right),
                        new ParamForBuildWall(30, 0, 20, Direction.Back),
                        new ParamForBuildWall(30, 20, 30, Direction.Left),
                        new ParamForBuildWall(0, 20, 20, Direction.Forward),
                        new ParamForBuildWall(16, 1, 3, Direction.Back),
                        new ParamForBuildWall(16, 7, 4, Direction.Back),
                        new ParamForBuildWall(16, 10, 14, Direction.Right),
                        new ParamForBuildWall(8, 20, 15, Direction.Forward),
                    };
                    levelInfo.Gate = new ParamForBuildWall(30, 8, 4, Direction.Back);
                    break;
            }
        }

        #region Save and load game methods
        public void SerializeModel()
        {
            try
            {
                XmlSerializer serializer;
                FileStream stream;
                Directory.CreateDirectory("Save");

                serializer = new XmlSerializer(typeof(List<GameEntityUnit<char>>));
                File.Delete("Save\\SerializationWalls.xml");
                stream = new FileStream("Save\\SerializationWalls.xml", FileMode.OpenOrCreate, FileAccess.Write, FileShare.Read);
                serializer.Serialize(stream, walls);

                serializer = new XmlSerializer(typeof(List<GameEntityUnit<char>>));
                File.Delete("Save\\SerializationApples.xml");
                stream = new FileStream("Save\\SerializationApples.xml", FileMode.OpenOrCreate, FileAccess.Write, FileShare.Read);
                serializer.Serialize(stream, apples);

                serializer = new XmlSerializer(typeof(SnakeForConsole2D));
                File.Delete("Save\\SerializationPlayer.xml");
                stream = new FileStream("Save\\SerializationPlayer.xml", FileMode.OpenOrCreate, FileAccess.Write, FileShare.Read);
                serializer.Serialize(stream, player);

                serializer = new XmlSerializer(typeof(LevelInfo));
                File.Delete("Save\\SerializationLevelInfo.xml");
                stream = new FileStream("Save\\SerializationLevelInfo.xml", FileMode.OpenOrCreate, FileAccess.Write, FileShare.Read);
                serializer.Serialize(stream, levelInfo);

                serializer = new XmlSerializer(typeof(List<GameEntityUnit<char>>));
                File.Delete("Save\\SerializationGate.xml");
                stream = new FileStream("Save\\SerializationGate.xml", FileMode.OpenOrCreate, FileAccess.Write, FileShare.Read);
                serializer.Serialize(stream, gate);

                serializer = new XmlSerializer(typeof(GameStatus));
                File.Delete("Save\\SerializationGameStatus.xml");
                stream = new FileStream("Save\\SerializationGameStatus.xml", FileMode.OpenOrCreate, FileAccess.Write, FileShare.Read);
                serializer.Serialize(stream, gameStatus);

                serializer = new XmlSerializer(typeof(int));
                File.Delete("Save\\SerializationCurrentLevelNumber.xml");
                stream = new FileStream("Save\\SerializationCurrentLevelNumber.xml", FileMode.OpenOrCreate, FileAccess.Write, FileShare.Read);
                serializer.Serialize(stream, currentLevelNumber);

                stream.Close();
            }
            catch { }
            
        }
        public void DeserializeModel()
        {
            try
            {
                XmlSerializer serializer;
                FileStream stream;

                serializer = new XmlSerializer(typeof(List<GameEntityUnit<char>>));
                stream = new FileStream("Save\\SerializationWalls.xml", FileMode.Open, FileAccess.Read, FileShare.Read);
                walls = serializer.Deserialize(stream) as List<GameEntityUnit<char>>;

                serializer = new XmlSerializer(typeof(List<GameEntityUnit<char>>));
                stream = new FileStream("Save\\SerializationApples.xml", FileMode.Open, FileAccess.Read, FileShare.Read);
                apples = serializer.Deserialize(stream) as List<GameEntityUnit<char>>;

                serializer = new XmlSerializer(typeof(SnakeForConsole2D));
                stream = new FileStream("Save\\SerializationPlayer.xml", FileMode.Open, FileAccess.Read, FileShare.Read);
                player = serializer.Deserialize(stream) as SnakeForConsole2D;

                serializer = new XmlSerializer(typeof(LevelInfo));
                stream = new FileStream("Save\\SerializationLevelInfo.xml", FileMode.Open, FileAccess.Read, FileShare.Read);
                levelInfo = (LevelInfo)serializer.Deserialize(stream);

                serializer = new XmlSerializer(typeof(List<GameEntityUnit<char>>));
                stream = new FileStream("Save\\SerializationGate.xml", FileMode.Open, FileAccess.Read, FileShare.Read);
                gate = serializer.Deserialize(stream) as List<GameEntityUnit<char>>;

                serializer = new XmlSerializer(typeof(GameStatus));
                stream = new FileStream("Save\\SerializationGameStatus.xml", FileMode.Open, FileAccess.Read, FileShare.Read);
                gameStatus = (GameStatus)serializer.Deserialize(stream);

                serializer = new XmlSerializer(typeof(int));
                stream = new FileStream("Save\\SerializationCurrentLevelNumber.xml", FileMode.Open, FileAccess.Read, FileShare.Read);
                currentLevelNumber = (int)serializer.Deserialize(stream);

                stream.Close();
            }
            catch { }
        }
        #endregion
    }
}
