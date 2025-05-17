using IziHardGames.Geometry.Domain.Vectors;
using IziHardGames.IziLiquiMap.Contracts;
using IziHardGames.IziLiquiMap.Domain.Models;

namespace IziHardGames.IziLiquiMap.UnitTests
{
    public class Spreading_UnitTest
    {
        [Fact]
        public void LiquiMap_Radial_Spreading()
        {
            var map = new LiquiMap(new Vector3RO(-50, 0, -50), new Vector3RO(100, 0, 100), new Vector3IntRO(100, 1, 100), new TestPool());
            for (int x = -50; x < 50; x++)
            {
                for (int z = -50; z < 50; z++)
                {
                    if (map.PositionToMapIndex(new Vector3IntRO(x, 0, z), out var index))
                    {
                        var cell = map.GetCell(index);
                    }
                    else
                    {
                        throw new Exception();
                    }
                }
            }
            RadialSpreadingTest(map, map);
        }


        [Fact]
        public void LiquiMap_Directional_Spreading()
        {

        }

        private static void RadialSpreadingTest(IMapWithSpreading<MapPosition, Cell> map, ICircleProvider<Circle, MapIndex> circleProvider)
        {
            var center = map.GetCell(new MapPosition(0, 0, 0));

            for (int i = 0; i < 5; i++)
            {
                map.Spread();
                for (int radius = 0; radius < i; radius++)
                {
                    var circle = circleProvider.GetCircle(center.Index, radius);
                }
            }
        }
    }
}