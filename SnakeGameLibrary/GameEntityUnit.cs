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

        private string name;
        public string Name { get => name; set => name = value; }

        public GameEntityUnit(int x, int y, T material) : this(x, y, 1, material, string.Empty) { }
        public GameEntityUnit(int x, int y, T material, string name) : this(x, y, 1, material, name) { }
        public GameEntityUnit(int x, int y, int z, T material, string name)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.material = material;
            this.name = name;
        }

        public override int GetHashCode() => x * y * z;
        public bool Equals(GameEntityUnit<T> other) => this.GetHashCode() == other.GetHashCode();
    }
}
