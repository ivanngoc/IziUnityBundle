using System;
using System.Runtime.InteropServices;

namespace IziHardGames.Geometry.Domain.Vectors
{
    [StructLayout(LayoutKind.Explicit, Size = 8)]
    public readonly struct Vector2IntRO
    {
        [FieldOffset(0)] public readonly int x;
        [FieldOffset(4)] public readonly int y;

        public Vector2IntRO(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public override bool Equals(object? obj)
        {
            return obj is Vector2IntRO rO &&
                   x == rO.x &&
                   y == rO.y;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(x, y);
        }

        public static Vector2IntRO operator +(Vector2IntRO left, Vector2IntRO right)
        {
            return new Vector2IntRO(left.x + right.x, left.y + right.y);
        }
        public static Vector2IntRO operator -(Vector2IntRO left, Vector2IntRO right)
        {
            return new Vector2IntRO(left.x - right.x, left.y - right.y);
        }
        public static Vector2IntRO operator *(Vector2IntRO left, int right)
        {
            return new Vector2IntRO(left.x * right, left.y * right);
        }
        public static bool operator ==(Vector2IntRO left, Vector2IntRO right)
        {
            return left.x == right.x && left.y == right.y;
        }
        public static bool operator !=(Vector2IntRO left, Vector2IntRO right)
        {
            return left.x != right.x || left.y != right.y;
        }
    }
}
