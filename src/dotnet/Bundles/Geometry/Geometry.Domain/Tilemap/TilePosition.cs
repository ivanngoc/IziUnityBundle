using System;
using System.Runtime.InteropServices;
using IziHardGames.Geometry.Domain.Vectors;

namespace IziHardGames.Geometry.Domain.Tilemap
{
    /// <summary>
    /// Мозаичная координата.
    /// Position relative to origin of map
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 12)]
    public readonly struct TilePosition
    {
        [FieldOffset(0)] public readonly Vector3IntRO vector;

        [FieldOffset(0)] public readonly int x;

        [FieldOffset(4)] public readonly int y;

        [FieldOffset(8)] public readonly int z;

        public TilePosition(int x, int y, int z)
        {
            vector = default;
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public override bool Equals(object obj)
        {
            if (obj is TilePosition tp) return tp == this;
            if (obj is Vector3IntRO vector) return vector == this.vector;
            return false;
        }

        public override int GetHashCode()
        {
            return vector.GetHashCode();
        }

        public static TilePosition operator +(TilePosition left, TilePosition right)
        {
            return new TilePosition(left.x + right.x, left.y + right.y, left.z + right.z);
        }
        public static TilePosition operator -(TilePosition left, TilePosition right)
        {
            return new TilePosition(left.x - right.x, left.y - right.y, left.z - right.z);
        }
        public static Vector3IntRO operator +(TilePosition left, Vector3IntRO right)
        {
            return new TilePosition(left.x + right.x, left.y + right.y, left.z + right.z);
        }
        public static Vector3IntRO operator +(Vector3IntRO left, TilePosition right)
        {
            return new TilePosition(left.x + right.x, left.y + right.y, left.z + right.z);
        }
        public static bool operator ==(TilePosition left, TilePosition right)
        {
            return left.x == right.x && left.y == right.y && left.z == right.z;
        }
        public static bool operator !=(TilePosition left, TilePosition right)
        {
            return left.x != right.x || left.y != right.y || left.z != right.z;
        }

        public static implicit operator Vector3IntRO(TilePosition val) => val.vector;
        public static implicit operator TilePosition(Vector3IntRO val) => new TilePosition(val.x, val.y, val.z);
    }
}
