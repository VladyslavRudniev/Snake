using System.Threading.Tasks;

namespace SnakeGameLibrary
{
    public class SnakeForConsole2D : GameEntity<char>, ISnakeForConsole2D
    {
        public SnakeForConsole2D() : base() { }
        public SnakeForConsole2D(int id, GameEntityUnit<char>[] entityUnits, EntityState state, int speed, Direction direction) : base(id, entityUnits, state, speed, direction) { }
        public override void ChangeLength(int value)
        {
            Body[Body.Length - 1].Material = Body[Body.Length - 2].Material;
            GameEntityUnit<char>[] temp = new GameEntityUnit<char>[Body.Length +1];
            Body.CopyTo(temp, 0);
            Body = temp;
            Body[Body.Length - 1].Material = ' ';
        }
        public virtual async void ChangeLengthAsync(int value)
        {
            await Task.Factory.StartNew(() => ChangeLength(value));
        }

        public override void ChangePosition()
        {
            ChangingPosition();
        }
        protected virtual void ChangingPosition()
        {
            for (int i = this.Body.Length - 1; i > 0; i--)
            {
                Body[i].X = Body[i - 1].X;
                Body[i].Y = Body[i - 1].Y;
            }
            switch (this.Direction)
            {
                case Direction.Forward:
                    --this.Body[0].Y;
                    break;
                case Direction.Back:
                    ++this.Body[0].Y;
                    break;
                case Direction.Left:
                    --this.Body[0].X;
                    break;
                case Direction.Right:
                    ++this.Body[0].X;
                    break;
            }
        }
        public virtual async void ChangePositionAsync()
        {
            await Task.Factory.StartNew(ChangePosition);
        }

        public override void ChangeSpeed(int value) => this.Speed += value;

        public override bool Turn(Direction direction)
        {
            return TryChangeDirection(direction);
        }
        public virtual async void TurnAsync(Direction direction)
        {
            await Task.Factory.StartNew(() => Turn(direction));
        }
        protected virtual bool TryChangeDirection(Direction direction)
        {
            switch (direction)
            {
                case Direction.Forward:
                    if (this.Direction != Direction.Back) { this.Direction = direction; return true; }
                    break;
                case Direction.Back:
                    if (this.Direction != Direction.Forward) { this.Direction = direction; return true; }
                    break;
                case Direction.Left:
                    if (this.Direction != Direction.Right) { this.Direction = direction; return true; }
                    break;
                case Direction.Right:
                    if (this.Direction != Direction.Left) { this.Direction = direction; return true; }
                    break;
                case Direction.Up:
                    if (this.Direction != Direction.Down) { this.Direction = direction; return true; }
                    break;
                case Direction.Down:
                    if (this.Direction != Direction.Up) { this.Direction = direction; return true; }
                    break;
            }
            return false;
        }
    }
}
