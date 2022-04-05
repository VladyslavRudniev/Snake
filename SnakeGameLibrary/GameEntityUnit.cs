using System;

namespace SnakeGameLibrary
{
    public struct GameEntityUnit<T> : IEquatable<GameEntityUnit<T>>
    {
        private int x;
        public int X { get => x; set => x = value; }

        private int y;
        public int Y { get => y; set => y = value; }
        
        private int z;
        public int Z { get => z; set => z = value; }
        
        private T material;
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
