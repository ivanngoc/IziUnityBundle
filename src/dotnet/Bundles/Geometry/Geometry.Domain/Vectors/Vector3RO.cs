namespace IziHardGames.Geometry.Domain.Vectors
{
    public readonly struct Vector3RO : IVector3RO
    {
        public readonly float x;
        public readonly float y;
        public readonly float z;

        public float X { get => x; }
        public float Y { get => y; }
        public float Z { get => z; }
    }
}
