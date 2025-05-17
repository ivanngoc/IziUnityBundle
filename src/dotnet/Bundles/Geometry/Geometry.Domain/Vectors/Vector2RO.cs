using System.Runtime.InteropServices;

namespace IziHardGames.Geometry.Domain.Vectors
{
    [StructLayout(LayoutKind.Explicit, Size = 8)]
    public readonly struct Vector2RO : IVector2RO
    {
        [FieldOffset(0)] public readonly float x;
        [FieldOffset(4)] public readonly float y;
        public float X { get => x; }
        public float Y { get => y; }

        public Vector2RO(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        public static Vector2RO operator -(Vector2RO left, Vector2RO right)
        {
            return new Vector2RO(left.x - right.x, left.y - right.y);
        }
        public static Vector2RO operator +(Vector2RO left, Vector2RO right)
        {
            return new Vector2RO(left.x + right.x, left.y + right.y);
        }
        public static Vector2RO operator *(Vector2RO left, float right)
        {
            return new Vector2RO(left.x * right, left.y * right);
        }
    }
}
