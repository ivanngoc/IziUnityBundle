using IziHardGames.Geometry.Domain.Tilemap;

namespace IziHardGames.IziLiquiMap.Domain.Models
{
    /// <summary>
    /// Valid Tile position at map
    /// </summary>
    public readonly struct MapPosition
    {
        public readonly TilePosition position;

        public MapPosition(int x, int y, int z) : this(new TilePosition(x, y, z))
        {

        }
        public MapPosition(TilePosition position)
        {
            this.position = position;
        }

        public static implicit operator MapPosition(TilePosition val)
        {
            return new MapPosition(val);
        }
    }
}
