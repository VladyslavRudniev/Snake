using SnakeGameLibrary;

namespace SnakeLulu
{
    struct ParamForBuildWall
    {
        private int startX;
        private int startY;
        private int length;
        private Direction direction;

        public int StartX { get => startX; set => startX = value; }
        public int StartY { get => startY; set => startY = value; }
        public int Length { get => length; set => length = value; }
        public Direction Direction { get => direction; set => direction = value; }

        public ParamForBuildWall(int startX, int startY, int length, Direction direction)
        {
            this.direction = direction;
            this.startX = startX;
            this.startY = startY;
            this.length = length;
        }
    }
}
