namespace IziHardGames.Geometry.Domain.Vectors
{
    public interface IVector3RO : IReadOnly
    {
        public float X { get; }
        public float Y { get; }
        public float Z { get; }
    }
}
