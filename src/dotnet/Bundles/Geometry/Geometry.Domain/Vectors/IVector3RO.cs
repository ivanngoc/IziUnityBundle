namespace IziHardGames.Geometry.Domain.Vectors
{
    public interface IVector3RO : IVector2RO
    {
        public float Z { get; }
    }

    public interface IVector2RO : IReadOnly
    {
        public float X { get; }
        public float Y { get; }
    }
}
