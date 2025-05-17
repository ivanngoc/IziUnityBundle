using IziHardGames.Geometry.Domain.Tilemap;
using IziHardGames.IziLiquiMap.Contracts;

namespace IziHardGames.IziLiquiMap.Domain.Models
{
    public readonly struct Circle : ICircle, IEnumerableCustom<MapPosition, CircleEnumeratorXZOfMap>
    {
        public int Radius => radius;
        public readonly int radius;
        public readonly MapPosition center;
        public readonly Map<MapPosition, Cell> map;

        public Circle(LiquiMap liquiMap, MapPosition mapPos, int radius)
        {
            this.radius = radius;
            this.center = mapPos;
            this.map = liquiMap;
        }

        public CircleEnumeratorXZOfMap GetEnumerator()
        {
            return new CircleEnumeratorXZOfMap(this);
        }
    }
}
