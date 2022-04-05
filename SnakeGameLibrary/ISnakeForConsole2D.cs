namespace SnakeGameLibrary
{
    public interface ISnakeForConsole2D
    {
        public void ChangeLength(int value);
        public void ChangeLengthAsync(int value);
        public void ChangePosition();
        public void ChangePositionAsync();
        public void ChangeSpeed(int value);
        public bool Turn(Direction direction);
        public void TurnAsync(Direction direction);


    }
}
