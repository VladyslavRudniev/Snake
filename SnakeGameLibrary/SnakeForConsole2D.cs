using System;

namespace SnakeGameLibrary
{
    public class SnakeForConsole2D : GameEntity<char>
    {
        public override void ChangeLength(int value)
        {
            if(value > 0)
            {

            }
            else
            {

            }
        }

        public override void ChangePosition()
        {
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
                case Direction.Up:
                    ++this.Body[0].Z;
                    break;
                case Direction.Down:
                    --this.Body[0].Z;
                    break;
            }
        }

        public override void ChangeSpeed(int value) => this.Speed += value;

        public override bool Turn(Direction direction)
        {
            return TryChangeDirection(direction);
        }
        private bool TryChangeDirection(Direction direction)
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
