using System.Numerics;
using IziHardGames.Geometry.Domain.Tilemap;
using IziHardGames.Geometry.Domain.Vectors;
using IziHardGames.IziLiquiMap.Domain.Models;
using IziHardGames.Pools.Contracts;

namespace IziHardGames.IziLiquiMap.UnitTests
{
    public class LiquiMap_UnitTest
    {
        private static LiquiMap Lm_No_Offset() => new LiquiMap(default, new Vector3RO(100f, 0, 100f), new Vector3IntRO(100, 0, 100), new TestPool());
        private static LiquiMap Lm_Offset() => new LiquiMap(new Vector3RO(-100, 0, -100), new Vector3RO(100f, 0, 100f), new Vector3IntRO(100, 1, 100), new TestPool());
        [Fact]
        public void Indexing_Is_Correct()
        {
            var origin = new Vector3RO(0, 0, 0);
            var diagonal = new Vector3RO(100, 100, 100);
            var bounds = new Vector3IntRO(100, 1, 100);
            var diagonalOfCell = new Vector3RO(1f, 0, 1f);
            var lm = new LiquiMap(origin, diagonal, bounds, new TestPool());

            Assert.Equal(origin.x, lm.origin.x);
            Assert.Equal(0, lm.origin.y);
            Assert.Equal(origin.z, lm.origin.z);

            Assert.Equal(diagonal.x, lm.diagonal.x);
            Assert.Equal(0, lm.diagonal.y);
            Assert.Equal(diagonal.z, lm.diagonal.z);

            Assert.Equal(diagonalOfCell.x, lm.diagonalOfCell.x);
            Assert.Equal(diagonalOfCell.y, lm.diagonalOfCell.y);
            Assert.Equal(diagonalOfCell.z, lm.diagonalOfCell.z);

            Assert.Equal(bounds.x, lm.columnsXRows.x);
            Assert.Equal(bounds.y, lm.columnsXRows.y);
            Assert.Equal(bounds.z, lm.columnsXRows.z);

            var index0 = lm.PositionToIndex(new Vector3IntRO(0, 0, 0));
            Assert.Equal(0, index0.value);

            var index1 = lm.PositionToIndex(new Vector3IntRO(1, 0, 0));
            Assert.Equal(1, index1.value);

            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                var index2 = lm.TilePositionToIndexSafe(new Vector3IntRO(100, 0, 0));
            });

            var index3 = lm.PositionToIndex(new Vector3IntRO(0, 0, 1));
            Assert.Equal(100, index3.value);

            var index4 = lm.PositionToIndex(new Vector3IntRO(0, 0, 5));
            Assert.Equal(500, index4.value);

            var index5 = lm.PositionToIndex(new Vector3IntRO(99, 0, 0));
            Assert.Equal(99, index5.value);

            var index6 = lm.PositionToIndex(new Vector3IntRO(99, 0, 99));
            Assert.Equal(9999, index6.value);
        }

        [Fact]
        public void Index_Is_Correct_When_Origin_Shifted()
        {
            var origin = new Vector3RO(-100, 0, -100);
            var diagonal = new Vector3RO(100, 100, 100);
            var bounds = new Vector3IntRO(100, 1, 100);
            var diagonalOfCell = new Vector3RO(1f, 0, 1f);
            var lm = new LiquiMap(origin, diagonal, bounds, new TestPool());

            Assert.Equal(origin.x, lm.origin.x);
            Assert.Equal(0, lm.origin.y);
            Assert.Equal(origin.z, lm.origin.z);

            Assert.Equal(diagonal.x, lm.diagonal.x);
            Assert.Equal(0, lm.diagonal.y);
            Assert.Equal(diagonal.z, lm.diagonal.z);

            Assert.Equal(diagonalOfCell.x, lm.diagonalOfCell.x);
            Assert.Equal(diagonalOfCell.y, lm.diagonalOfCell.y);
            Assert.Equal(diagonalOfCell.z, lm.diagonalOfCell.z);

            Assert.Equal(bounds.x, lm.columnsXRows.x);
            Assert.Equal(bounds.y, lm.columnsXRows.y);
            Assert.Equal(bounds.z, lm.columnsXRows.z);

            var index0 = lm.PositionToIndex(new Vector3IntRO(-100, 0, -100));
            Assert.Equal(0, index0.value);

            var index1 = lm.PositionToIndex(new Vector3IntRO(-99, 0, -100));
            Assert.Equal(1, index1.value);

            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                var index2 = lm.TilePositionToIndexSafe(new Vector3IntRO(1, 0, 0));
            });

            var index3 = lm.PositionToIndex(new Vector3IntRO(-100, 0, -99));
            Assert.Equal(100, index3.value);

            var index4 = lm.PositionToIndex(new Vector3IntRO(-99, 0, -99));
            Assert.Equal(101, index4.value);

            var index5 = lm.PositionToIndex(new Vector3IntRO(-1, 0, -1));
            Assert.Equal(9999, index5.value);
        }

        [Fact]
        public void Get_Cell_By_Index_Is_Correct()
        {
            var lm = Lm_No_Offset();
            var cell0 = lm.GetCell(0);

        }

        [Fact]
        public void Center_Of_Neigbour_Is_Correct_When_Shifted_Origin()
        {
            var lm = Lm_Offset();
            var cell0 = lm.GetCell(0);
            var center0 = new Vector3RO(-99.5f, 0, -99.5f);
            Assert.Equal(center0.x, cell0.Center.x);
            Assert.Equal(center0.y, cell0.Center.y);
            Assert.Equal(center0.z, cell0.Center.z);

            var cell1 = lm.GetCell(1);
            var center1 = new Vector3RO(-98.5f, 0, -99.5f);
            Assert.Equal(center1.x, cell1.Center.x);
            Assert.Equal(center1.y, cell1.Center.y);
            Assert.Equal(center1.z, cell1.Center.z);

            var cell99 = lm.GetCell(99);
            var center99 = new Vector3RO(-0.5f, 0, -99.5f);
            Assert.Equal(center99.x, cell99.Center.x);
            Assert.Equal(center99.y, cell99.Center.y);
            Assert.Equal(center99.z, cell99.Center.z);

            var cell200 = lm.GetCell(200);
            var center200 = new Vector3RO(-99.5f, 0, -97.5f);
            Assert.Equal(center200.x, cell200.Center.x);
            Assert.Equal(center200.y, cell200.Center.y);
            Assert.Equal(center200.z, cell200.Center.z);

            var cell9999 = lm.GetCell(9999);
            var center9999 = new Vector3RO(-0.5f, 0, -0.5f);
            Assert.Equal(center9999.x, cell9999.Center.x);
            Assert.Equal(center9999.y, cell9999.Center.y);
            Assert.Equal(center9999.z, cell9999.Center.z);
        }

        [Fact]
        public void Center_Of_Neigbour_Is_Correct()
        {
            var lm = Lm_No_Offset();
            var cell0 = lm.GetCell(0);
            Assert.Equal(0, cell0.Index.value.value);
            var center0 = new Vector3RO(0.5f, 0, 0.5f);
            Assert.Equal(center0.x, cell0.Center.x);
            Assert.Equal(center0.y, cell0.Center.y);
            Assert.Equal(center0.z, cell0.Center.z);

            var cell1 = lm.GetCell(1);
            Assert.Equal(1, cell1.Index.value.value);
            var center1 = new Vector3RO(1.5f, 0, 0.5f);
            Assert.Equal(center1.x, cell1.Center.x);
            Assert.Equal(center1.y, cell1.Center.y);
            Assert.Equal(center1.z, cell1.Center.z);

            var cell99 = lm.GetCell(99);
            Assert.Equal(99, cell99.Index.value.value);
            var center99 = new Vector3RO(99.5f, 0, 0.5f);
            Assert.Equal(center99.x, cell99.Center.x);
            Assert.Equal(center99.y, cell99.Center.y);
            Assert.Equal(center99.z, cell99.Center.z);

            var cell200 = lm.GetCell(200);
            Assert.Equal(200, cell200.Index.value.value);
            var center200 = new Vector3RO(0.5f, 0, 2.5f);
            Assert.Equal(center200.x, cell200.Center.x);
            Assert.Equal(center200.y, cell200.Center.y);
            Assert.Equal(center200.z, cell200.Center.z);

            var cell9999 = lm.GetCell(9999);
            Assert.Equal(9999, cell9999.Index.value.value);
            var center9999 = new Vector3RO(99.5f, 0, 99.5f);
            Assert.Equal(center9999.x, cell9999.Center.x);
            Assert.Equal(center9999.y, cell9999.Center.y);
            Assert.Equal(center9999.z, cell9999.Center.z);
        }

        [Fact]
        public void To_Tile_Position_Is_Correct()
        {
            var lm = Lm_No_Offset();
            var count = lm.columnsXRows.x * lm.columnsXRows.z;
            for (int i = 0; i < count; i++)
            {
                var index = new MapIndex(i);
                var pos = lm.ToTilePositionXZ(i);
                var indexRe = lm.TilePositionToIndex(pos);
                Assert.Equal(index.value.value, indexRe.value);
            }

            var r0 = lm.ToTilePositionXZ(0);
            Assert.Equal(0, r0.x);
            Assert.Equal(0, r0.z);
            var r1 = lm.ToTilePositionXZ(1);
            Assert.Equal(1, r1.x);
            Assert.Equal(0, r1.z);
            var r99 = lm.ToTilePositionXZ(99);
            Assert.Equal(99, r99.x);
            Assert.Equal(0, r99.z);
            var r100 = lm.ToTilePositionXZ(100);
            Assert.Equal(0, r100.x);
            Assert.Equal(1, r100.z);
            var r200 = lm.ToTilePositionXZ(200);
            Assert.Equal(0, r200.x);
            Assert.Equal(2, r200.z);
            var r9999 = lm.ToTilePositionXZ(9999);
            Assert.Equal(99, r9999.x);
            Assert.Equal(99, r9999.z);
        }

    }
    internal class TestPool : IPoolRent<Cell>
    {
        public Cell Rent()
        {
            return new Cell();
        }
    }
}