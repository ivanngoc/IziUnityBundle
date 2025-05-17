using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using IziHardGames.Geometry.Domain.Tilemap;
using IziHardGames.Geometry.Domain.Vectors;
using IziHardGames.IziLiquiMap.Contracts;
using IziHardGames.Pools.Contracts;

namespace IziHardGames.IziLiquiMap.Domain.Models
{
    public class LiquiMap : MapXZ<Cell>, IMapWithSpreading<MapPosition, Cell>, ICircleProvider<Circle, MapIndex>
    {
        private readonly Dictionary<int, Cell> cells = new Dictionary<int, Cell>();
        public const float WEIGHT_DIRECT = 1;
        public const float WEIGHT_SIDE = 1.4f;
        private readonly IPoolRent<Cell> pool;


        public LiquiMap(Vector3RO origin, Vector3RO diagonal, Vector3IntRO bounds, IPoolRent<Cell> poolRent) : base(origin, diagonal, bounds)
        {
            this.pool = poolRent;
        }

        public void StackAt(in TilePosition position)
        {
            var index = TilePositionToIndex(in position);
            StackAt(index);
        }

        public void StackAt(Cell cell)
        {
            cell.Stack();
        }

        public void UnstackAt(Cell cell)
        {
            cell.Unstack();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void StackAt(MapIndex index)
        {
            cells[index.value].Stack();
        }

        public void UnstackAt(MapIndex index)
        {
            cells[index.value].Unstack();
        }




        public void Begin()
        {
            foreach (var cell in cells.Values)
            {
                cell.Begin();
            }
        }

        public void End()
        {
            foreach (var cell in cells.Values)
            {
                cell.Commit();
            }
        }

        /// <summary>
        /// Растекание идет из самых емких ячеек в наимение 
        /// </summary>
        public void Spread()
        {
            foreach (var cell in cells.Values)
            {
                cell.Spread();
            }
        }

        public void SpreadByStacks()
        {
            foreach (var cell in cells.Values)
            {
                cell.SpreadByStacks();
            }
        }


        public Cell GetCell(MapIndex index)
        {
            if (!cells.TryGetValue(index.value, out var cell))
            {
                cell = pool.Rent();
                var pos = ToTilePositionXZ(index);
                var center = CenterOf(pos);
                cell.Initilize(this, index, pos, center);
            }
            return cell;
        }

        public Cell GetCell(MapIndex index, in MapPosition pos, Cell cell, OctagonIndex octagonIndex)
        {
            var newCell = GetCell(index, in pos);
            cell.BindAs(newCell, octagonIndex);
            return newCell;
        }

        public Cell GetCell(MapIndex index, in MapPosition pos)
        {
            if (!cells.TryGetValue(index.value, out var cell))
            {
                cell = pool.Rent();
                var center = CenterOf(pos);
                cell.Initilize(this, index, pos, center);
                for (int i = 0; i < OctagonIndex.COUNT; i++)
                {
                    Vector3IntRO neighbourPos = pos.position + offsets[i];
                    if (TilePositionToMapPosition(neighbourPos, out var mapPos))
                    {
                        var neighbourIndex = MapPositionToMapIndex(mapPos);
                        if (cells.TryGetValue(neighbourIndex.value, out var neighbour))
                        {
                            cell[new OctagonIndex(i)] = neighbour;
                        }
                    }
                }
                cells.Add(index.value, cell);
            }
            return cell;
        }

        public override Cell GetCellByIndex(MapIndex index)
        {
            return cells[index.value];
        }

        public Circle GetCircle(MapIndex index, int radius)
        {
            var mapPos = MapIndexToMapPosition(index);
            var circle = new Circle(this, mapPos, radius);
            throw new NotImplementedException();
        }

        public override bool IsValid(in MapPosition position)
        {
            throw new NotImplementedException();
        }
    }
}
