using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGameLibrary
{
    public class SnakeBotForConsole2D : SnakeForConsole2D
    {
        private Random random;
        private int numberOfMovesToTurn;
        private int maxNumberOfMovesToTurn;

        private int desiredPointX;
        private int desiredPointY;

        private Direction[] pastTurns;

        public int DesiredPointX { get => desiredPointX; set => desiredPointX = value; }
        public int DesiredPointY { get => desiredPointY; set => desiredPointY = value; }
        public Direction[] PastTurns { get => pastTurns; set => pastTurns = value; }

        public SnakeBotForConsole2D(int id, GameEntityUnit<char>[] entityUnits, EntityState state, int speed, Direction direction) 
            : base(id, entityUnits, state, speed, direction) 
        {
            pastTurns = new Direction[3];
            random = new Random();
            numberOfMovesToTurn = 0;
        }

        public void SetDesiredPoint(int x, int y)
        {
            this.desiredPointX = x;
            this.desiredPointY = y;
        }
        public void СalculateNextNumberOfMovesToTurn()
        {
            switch (Direction)
            {
                case Direction.Forward:
                    maxNumberOfMovesToTurn = random.Next(1, Body[0].Y - desiredPointY);
                    break;
                case Direction.Back:
                    maxNumberOfMovesToTurn = random.Next(1, desiredPointY - Body[0].Y);
                    break;
                case Direction.Left:
                    maxNumberOfMovesToTurn = random.Next(1, Body[0].X - desiredPointX);
                    break;
                case Direction.Right:
                    maxNumberOfMovesToTurn = random.Next(1, desiredPointX - Body[0].X);
                    break;
            }
        }
        protected override bool TryChangeDirection(Direction direction)
        {
            switch (this.Direction)
            {
                case Direction.Forward:
                    if (Body[0].X < desiredPointX) { this.Direction = Direction.Right; return true; }
                    if (Body[0].X > desiredPointX) { this.Direction = Direction.Left; return true; }
                    break;
                case Direction.Back:
                    if (Body[0].X < desiredPointX) { this.Direction = Direction.Right; return true; }
                    if (Body[0].X > desiredPointX) { this.Direction = Direction.Left; return true; }
                    break;
                case Direction.Left:
                    if (Body[0].Y < desiredPointY) { this.Direction = Direction.Back; return true; }
                    if (Body[0].Y > desiredPointY) { this.Direction = Direction.Forward; return true; }
                    break;
                case Direction.Right:
                    if (Body[0].Y < desiredPointY) { this.Direction = Direction.Back; return true; }
                    if (Body[0].Y > desiredPointY) { this.Direction = Direction.Forward; return true; }
                    break;
            }
            return false;
        }

        public override void ChangePosition()
        {
            if (numberOfMovesToTurn == maxNumberOfMovesToTurn)
            {
                numberOfMovesToTurn = 0;
                TryChangeDirection(Direction.Forward);
                СalculateNextNumberOfMovesToTurn();
            }
            base.ChangePosition();
            numberOfMovesToTurn++;
        }
    }
}
