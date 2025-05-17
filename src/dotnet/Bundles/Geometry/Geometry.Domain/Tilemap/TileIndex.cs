using IziHardGames.Geometry.Domain.Vectors;

namespace IziHardGames.Geometry.Domain.Tilemap
{
    public readonly struct TileIndex
    {
        public readonly int value;
        public TileIndex(int value)
        {
            this.value = value;
        }
        public static implicit operator int(TileIndex val) => val.value;
        public static implicit operator TileIndex(int val) => new TileIndex(val);
    }
}
