using System;

namespace SnakeGameLibrary
{
    /// <summary>
    /// Entity unit in the game
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public struct GameEntityUnit<T> : IEquatable<GameEntityUnit<T>>
    {
        private int x;
        /// <summary>
        /// X position
        /// </summary>
        public int X { get => x; set => x = value; }

        private int y;
        /// <summary>
        /// Y position
        /// </summary>
        public int Y { get => y; set => y = value; }
        
        private int z;
        /// <summary>
        /// Z position
        /// </summary>
        public int Z { get => z; set => z = value; }
        
        private T material;
        /// <summary>
        /// Designation (view) of entity unit
        /// </summary>
        public T Material { get => material; set => material = value; }

        public GameEntityUnit(int x, int y, T material) : this(x, y, 1, material) { }
        public GameEntityUnit(int x, int y, int z, T material)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.material = material;
        }

        public override int GetHashCode() => x * y * z;
        public bool Equals(GameEntityUnit<T> other) => this.GetHashCode() == other.GetHashCode();
    }
}
