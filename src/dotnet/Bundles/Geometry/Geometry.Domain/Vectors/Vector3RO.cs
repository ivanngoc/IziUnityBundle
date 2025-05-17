using System;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace IziHardGames.Geometry.Domain.Vectors
{

    [StructLayout(LayoutKind.Explicit, Size = 12)]
    public readonly struct Vector3RO : IVector3RO
    {
        private static readonly Vector3RO zero = new Vector3RO(0f, 0f, 0f);
        private static readonly Vector3RO one = new Vector3RO(1f, 1f, 1f);
        private static readonly Vector3RO left = new Vector3RO(-1f, 0f, 0f);
        private static readonly Vector3RO right = new Vector3RO(1f, 0f, 0f);
        private static readonly Vector3RO top = new Vector3RO(0f, 1f, 0f);
        private static readonly Vector3RO bot = new Vector3RO(0f, -1f, 0f);
        private static readonly Vector3RO forward = new Vector3RO(0f, 0f, 1f);
        private static readonly Vector3RO back = new Vector3RO(0f, 0f, -1f);
        private static readonly Vector3RO nan = new Vector3RO(float.NaN, float.NaN, float.NaN);
        private static readonly Vector3RO min = new Vector3RO(float.MinValue, float.MinValue, float.MinValue);
        private static readonly Vector3RO max = new Vector3RO(float.MaxValue, float.MaxValue, float.MaxValue);
        public static Vector3RO NaN => nan;
        public static Vector3RO Min => min;
        public static Vector3RO Max => max;
        public static Vector3RO Zero => zero;
        public static Vector3RO One => one;
        public static Vector3RO Left => left;
        public static Vector3RO Right => right;
        public static Vector3RO Top => top;
        public static Vector3RO Bot => bot;
        public static Vector3RO Forward => forward;
        public static Vector3RO Back => back;

        [FieldOffset(0)] public readonly Vector2RO vector2;
        [FieldOffset(0)] public readonly float x;
        [FieldOffset(4)] public readonly float y;
        [FieldOffset(8)] public readonly float z;

        public float X { get => x; }
        public float Y { get => y; }
        public float Z { get => z; }
        public float DistanceSqr => x * x + y * y + z * z;
        public float Distance => MathF.Sqrt(DistanceSqr);
        public Vector3RO Normalized
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Normalize(this);
            }
        }
        public Vector3RO(float x, float y, float z)
        {
            this.vector2 = default;
            this.x = x;
            this.y = y;
            this.z = z;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3RO operator -(Vector3RO left, Vector3RO right)
        {
            return new Vector3RO(left.x - right.x, left.y - right.y, left.z - right.z);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3RO operator +(Vector3RO left, Vector3RO right)
        {
            return new Vector3RO(left.x + right.x, left.y + right.y, left.z + right.z);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3RO operator *(Vector3RO left, float right)
        {
            return new Vector3RO(left.x * right, left.y * right, left.z * right);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3RO operator /(Vector3RO a, float d)
        {
            return new Vector3RO(a.x / d, a.y / d, a.z / d);
        }

        public override string ToString()
        {
            return $"{x};{y};{z}";
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3RO Project(Vector3RO vector, Vector3RO onNormal)
        {
            float num = Dot(onNormal, onNormal);
            if (num < MathfInternal.Epsilon)
            {
                return zero;
            }
            float num2 = Dot(vector, onNormal);
            return new Vector3RO(onNormal.x * num2 / num, onNormal.y * num2 / num, onNormal.z * num2 / num);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3RO ProjectOnPlane(Vector3RO vector, Vector3RO planeNormal)
        {
            float num = Dot(planeNormal, planeNormal);
            if (num < MathfInternal.Epsilon)
            {
                return vector;
            }
            float num2 = Dot(vector, planeNormal);
            return new Vector3RO(vector.x - planeNormal.x * num2 / num, vector.y - planeNormal.y * num2 / num, vector.z - planeNormal.z * num2 / num);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Dot(Vector3RO lhs, Vector3RO rhs)
        {
            return lhs.x * rhs.x + lhs.y * rhs.y + lhs.z * rhs.z;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3RO Cross(Vector3RO lhs, Vector3RO rhs)
        {
            return new Vector3RO(lhs.y * rhs.z - lhs.z * rhs.y, lhs.z * rhs.x - lhs.x * rhs.z, lhs.x * rhs.y - lhs.y * rhs.x);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Angle(Vector3RO from, Vector3RO to)
        {
            float num = (float)Math.Sqrt(from.DistanceSqr * to.DistanceSqr);
            if (num < 1E-15f)
            {
                return 0f;
            }
            float num2 = Mathf.Clamp(Dot(from, to) / num, -1f, 1f);
            return (float)Math.Acos(num2) * 57.29578f;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3RO Normalize(Vector3RO value)
        {
            float num = Magnitude(value);
            if (num > 1E-05f)
            {
                return value / num;
            }

            return zero;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Magnitude(Vector3RO vector)
        {
            return (float)Math.Sqrt(vector.x * vector.x + vector.y * vector.y + vector.z * vector.z);
        }

    }
    public static class MathfInternal
    {
        public static readonly float Epsilon = (MathfInternal.IsFlushToZeroEnabled ? MathfInternal.FloatMinNormal : MathfInternal.FloatMinDenormal);

        public static volatile float FloatMinNormal = 1.17549435E-38f;

        public static volatile float FloatMinDenormal = float.Epsilon;

        public static bool IsFlushToZeroEnabled = FloatMinDenormal == 0f;
    }
}
