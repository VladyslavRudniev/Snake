namespace SnakeGameLibrary
{
    public enum EntityState : byte
    {
        Move = 1,
        Wait = 0,
        Eat = 2,
        Acceleration = 3,
        Deceleration = 4,
        Die = 5,
        Alarmed = 6,
    }
    public enum Direction : byte
    {
        Forward = 0,
        Back = 1,
        Left = 2,
        Right = 3,
        Up = 4,
        Down = 5,
    }
    public enum GameStatus : byte
    {
        Play = 0,
        Over = 1,
        Wait = 2,
    }
}
