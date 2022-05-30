using SnakeGameLibrary;

namespace SnakeLulu
{
    public struct LevelInfo
    {
        public int LevelNumber { get; set; }
        public string LevelTask { get; set; }
        public int Score { get; set; }
        public int MaxCountOfApples { get; set; }
        public int CoordinateYForOutputLevelTask { get; set; }
        public int RequiredScore { get; set; }
        public int CoordinateYForOutputScore { get; set; }
        public int MaxWidthGameField { get; set; }
        public int MaxHeightGameField { get; set; }
        public int CoordinateXStartPlayerPos { get; set; }
        public int CoordinateYStartPlayerPos { get; set; }
        public Direction DirectionStartBodyPlayer { get; set; }
        public int SpeedIncreaseFrequency { get; set; }
        public int MaxCountOfSnakeBots { get; set; }
        public int CoordinateYStartSnakeBotPos { get; set; }
        public int CoordinateXStartSnakeBotPos { get; set; }
        internal ParamForBuildWall[] ParamForBuildWalls { get; set; }
        internal ParamForBuildWall Gate { get; set; }
    }
}
