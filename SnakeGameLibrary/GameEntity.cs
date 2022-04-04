using System.Collections.Generic;

namespace SnakeGameLibrary
{
    public abstract class GameEntity<T>
    {
        private GameEntityUnit<T>[] body;
        public GameEntityUnit<T>[] Body { get => body; set => body = value; }

        private int id;
        public int Id { get => id; set => id = value; }

        private EntityState state;
        public EntityState State { get => state; set => state = value; }

        private Direction direction;
        public Direction Direction { get => direction; set => direction = value; }

        private int speed;
        public int Speed { get => speed; set => speed = value; }

        public GameEntityUnit<T> this[int index]
        {
            get => body[index];
            set => body[index] = value;
        }

        public GameEntity() : this(0, null, EntityState.Wait, 0, Direction.Forward) { }
        public GameEntity(GameEntityUnit<T>[] entityUnits) : this(0, entityUnits, EntityState.Wait, 0, Direction.Forward) { }
        public GameEntity(int id) : this(id, null, EntityState.Wait, 0, Direction.Forward) { }
        public GameEntity(int id, GameEntityUnit<T>[] entityUnits) : this(id, entityUnits, EntityState.Wait, 0, Direction.Forward) { }
        public GameEntity(int id, GameEntityUnit<T>[] entityUnits, EntityState state) : this(id, entityUnits, state, 0, Direction.Forward) { }
        public GameEntity(int id, GameEntityUnit<T>[] entityUnits, EntityState state, int speed) : this(id, entityUnits, state, speed, Direction.Forward) { }
        public GameEntity(int id, GameEntityUnit<T>[] entityUnits, EntityState state, int speed, Direction direction)
        {
            this.id = id;
            if (entityUnits != null) this.body = entityUnits;
            else this.body = new GameEntityUnit<T>[0];
            this.state = state;
            this.speed = speed;
            this.direction = direction;
        }

        public override int GetHashCode() => this.id;
        public override string ToString()
            => $"{this.GetType().Name}: id=[{this.id}] direction=[{this.direction}] state=[{this.state}] speed=[{this.speed}]";

        public abstract void ChangePosition();
        public abstract bool Turn(Direction direction);
        public abstract void ChangeLength(int value);
        public abstract void ChangeSpeed(int value);
    }
}
