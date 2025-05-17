using IziHardGames.Geometry.Domain.Tilemap;

namespace IziHardGames.IziLiquiMap.Domain.Models
{
    /// <summary>
    /// Valid tile index at map
    /// </summary>
    public readonly struct MapIndex
    {
        public readonly TileIndex value;

        public MapIndex(TileIndex value)
        {
            this.value = value;
        }

        public static implicit operator MapIndex(int val) => new MapIndex(val);
        public static implicit operator MapIndex(TileIndex val) => new MapIndex(val);
    }
}
