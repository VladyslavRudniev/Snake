namespace SnakeGameLibrary
{
    /// <summary>
    /// Snake essence abstraction
    /// </summary>
    /// <typeparam name="T">Snake body unit type</typeparam>
    public abstract class GameEntity<T>
    {
        protected GameEntityUnit<T>[] body;
        /// <summary>
        /// Array of snake body units
        /// </summary>
        public GameEntityUnit<T>[] Body { get => body; set => body = value; }

        protected int id;
        /// <summary>
        /// Unique identifier for the snake
        /// </summary>
        public int Id { get => id; set => id = value; }

        protected EntityState state;
        /// <summary>
        /// Snake state
        /// </summary>
        public EntityState State { get => state; set => state = value; }

        protected Direction direction;
        /// <summary>
        /// Snake direction
        /// </summary>
        public Direction Direction { get => direction; set => direction = value; }

        protected int speed;
        /// <summary>
        /// Snake movement speed
        /// </summary>
        public int Speed { get => speed; set => speed = value; }
        /// <summary>
        /// Get and set methods for accessing the snake body unit by index
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
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
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns>Unique identifier of snake</returns>
        public override int GetHashCode() => this.id;
        public override string ToString()
            => $"{this.GetType().Name}: id=[{this.id}] direction=[{this.direction}] state=[{this.state}] speed=[{this.speed}]";

        /// <summary>
        /// Changes the position of the snake
        /// </summary>
        public abstract void ChangePosition();
        /// <summary>
        /// Changes the direction of the snake
        /// </summary>
        /// <param name="direction"></param>
        /// <returns>Has the direction been changed</returns>
        public abstract bool Turn(Direction direction);
        /// <summary>
        /// Changes the length of the snake
        /// </summary>
        /// <param name="value">Growth size</param>
        public abstract void ChangeLength(int value);
        /// <summary>
        /// Changes the speed of movement the snake
        /// </summary>
        /// <param name="value"></param>
        public abstract void ChangeSpeed(int value);

    }
}
