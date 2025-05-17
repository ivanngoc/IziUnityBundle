using System;
using System.Runtime.InteropServices;

namespace IziHardGames.Geometry.Domain.Vectors
{
    [StructLayout(LayoutKind.Explicit, Size = 12)]
    public readonly struct Vector3IntRO
    {
        public static readonly Vector3IntRO up;
        public static readonly Vector3IntRO down;
        public static readonly Vector3IntRO left;
        public static readonly Vector3IntRO right;
        public static readonly Vector3IntRO back;
        public static readonly Vector3IntRO forward;

        static Vector3IntRO()
        {
            up = new Vector3IntRO(0, 1, 0);
            down = new Vector3IntRO(0, -1, 0);
            left = new Vector3IntRO(-1, 0, 0);
            right = new Vector3IntRO(1, 0, 0);
            back = new Vector3IntRO(0, 0, -1);
            forward = new Vector3IntRO(0, 0, 1);
        }


        [FieldOffset(0)] public readonly Vector2IntRO vector2Int;
        [FieldOffset(0)] public readonly int x;
        [FieldOffset(4)] public readonly int y;
        [FieldOffset(8)] public readonly int z;

        public Vector3IntRO(int x, int y, int z)
        {
            this.vector2Int = default;
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public override bool Equals(object? obj)
        {
            return obj is Vector3IntRO rO &&
                   x == rO.x &&
                   y == rO.y &&
                   z == rO.z;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(x, y, z);
        }

        public static Vector3IntRO operator +(Vector3IntRO left, Vector3IntRO right)
        {
            return new Vector3IntRO(left.x + right.x, left.y + right.y, left.z + right.z);
        }
        public static Vector3IntRO operator -(Vector3IntRO left, Vector3IntRO right)
        {
            return new Vector3IntRO(left.x - right.x, left.y - right.y, left.z - right.z);
        }
        public static Vector3IntRO operator *(Vector3IntRO left, int right)
        {
            return new Vector3IntRO(left.x * right, left.y * right, left.z * right);
        }
        public static bool operator ==(Vector3IntRO left, Vector3IntRO right)
        {
            return left.x == right.x && left.y == right.y && left.z == right.z;
        }
        public static bool operator !=(Vector3IntRO left, Vector3IntRO right)
        {
            return left.x != right.x || left.y != right.y || left.z != right.z;
        }
        public override string ToString()
        {
            return $"{x};{y};{z}";
        }
    }
}
