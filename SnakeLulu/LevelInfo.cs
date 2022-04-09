using SnakeGameLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeLulu
{
    public struct LevelInfo
    {
        private int levelNumber;
        private ParamForBuildWall[] paramForBuildWalls;
        private string levelTask;
        private int score;
        private int requiredScore;
        private int maxCountOfApples;
        private int maxCountOfSnakeBots;
        private int coordinateXStartSnakeBotPos;
        private int coordinateYStartSnakeBotPos;
        private int coordinateYForOutputLevelTask;
        private int coordinateYForOutputScore;
        private int maxWidthGameField;
        private int maxHeightGameField;
        private int coordinateXStartPlayerPos;
        private int coordinateYStartPlayerPos;
        private Direction directionStartBodyPlayer;
        private int speedIncreaseFrequency;
        private ParamForBuildWall gate;

        public int LevelNumber { get => levelNumber; set => levelNumber = value; }
        public string LevelTask { get => levelTask; set => levelTask = value; }
        public int Score { get => score; set => score = value; }
        public int MaxCountOfApples { get => maxCountOfApples; set => maxCountOfApples = value; }
        public int CoordinateYForOutputLevelTask { get => coordinateYForOutputLevelTask; set => coordinateYForOutputLevelTask = value; }
        public int RequiredScore { get => requiredScore; set => requiredScore = value; }
        public int CoordinateYForOutputScore { get => coordinateYForOutputScore; set => coordinateYForOutputScore = value; }
        public int MaxWidthGameField { get => maxWidthGameField; set => maxWidthGameField = value; }
        public int MaxHeightGameField { get => maxHeightGameField; set => maxHeightGameField = value; }
        public int CoordinateXStartPlayerPos { get => coordinateXStartPlayerPos; set => coordinateXStartPlayerPos = value; }
        public int CoordinateYStartPlayerPos { get => coordinateYStartPlayerPos; set => coordinateYStartPlayerPos = value; }
        public Direction DirectionStartBodyPlayer { get => directionStartBodyPlayer; set => directionStartBodyPlayer = value; }
        public int SpeedIncreaseFrequency { get => speedIncreaseFrequency; set => speedIncreaseFrequency = value; }
        public int MaxCountOfSnakeBots { get => maxCountOfSnakeBots; set => maxCountOfSnakeBots = value; }
        public int CoordinateYStartSnakeBotPos { get => coordinateYStartSnakeBotPos; set => coordinateYStartSnakeBotPos = value; }
        public int CoordinateXStartSnakeBotPos { get => coordinateXStartSnakeBotPos; set => coordinateXStartSnakeBotPos = value; }
        internal ParamForBuildWall[] ParamForBuildWalls { get => paramForBuildWalls; set => paramForBuildWalls = value; }
        internal ParamForBuildWall Gate { get => gate; set => gate = value; }
    }
}
