using System;
using System.Diagnostics.CodeAnalysis;
using IziHardGames.Geometry.Domain.Tilemap;
using IziHardGames.Geometry.Domain.Vectors;
using IziHardGames.IziLiquiMap.Contracts;

namespace IziHardGames.IziLiquiMap.Domain.Models
{
    public abstract class MapXZ<TCell> : Map<MapPosition, TCell> where TCell : IMapCell
    {
        public Vector3RO origin;
        public Vector3IntRO originAsInt;
        public Vector3IntRO columnsXRows;
        public Vector3IntRO tilePositionMin;
        public Vector3IntRO tilePositionMax;
        /// <summary>
        /// From left bot corner to right top (from min to max)
        /// </summary>
        public Vector3RO diagonal;
        public Vector3RO diagonalOfCell;

        public readonly Vector3IntRO[] offsets;
        public readonly int count;

        public MapXZ(Vector3RO origin, Vector3RO diagonal, Vector3IntRO bounds)
        {
            this.origin = new Vector3RO(origin.x, 0, origin.z);
            this.originAsInt = new Vector3IntRO((int)MathF.Floor(origin.x), 0, (int)MathF.Floor(origin.z));
            this.diagonal = new Vector3RO(diagonal.x, 0, diagonal.y);
            this.columnsXRows = bounds;
            this.diagonalOfCell = new Vector3RO(diagonal.x / bounds.x, 0, diagonal.z / diagonal.z);
            this.tilePositionMin = new Vector3IntRO((int)MathF.Floor(origin.x), 0, (int)MathF.Floor(origin.z));
            this.tilePositionMax = new Vector3IntRO((int)MathF.Floor(origin.x + diagonal.x - diagonalOfCell.x), 0, (int)MathF.Floor(origin.z + diagonal.z - diagonalOfCell.z));
            this.count = bounds.x * bounds.z;
            this.offsets = new Vector3IntRO[]
            {
                // начиная с севера по часовой стрелке.
                // высота не учитывается так как переход по высоте в каждой игре может быть со штрафом на подъем или спуск
                new Vector3IntRO(0,0, 1 ),
                new Vector3IntRO(1,0, 1 ),
                new Vector3IntRO(1,0, 0 ),
                new Vector3IntRO(1,0, -1 ),
                new Vector3IntRO(0,0, -1 ),
                new Vector3IntRO(-1,0, -1 ),
                new Vector3IntRO(-1,0, 0 ),
                new Vector3IntRO(-1,0, 1 ),
            };
        }

        public TileIndex TilePositionToIndexSafe(in TilePosition vector)
        {
            if (vector.x < tilePositionMin.x || tilePositionMax.x < vector.x)
            {
                throw new ArgumentOutOfRangeException($"Max Index: {columnsXRows.x - 1}, but recieved: {vector.x}");
            }
            if (vector.z < tilePositionMin.z || tilePositionMax.z < vector.z)
            {
                throw new ArgumentOutOfRangeException($"Max Index: {columnsXRows.z - 1}, but recieved: {vector.z}");
            }
            return TilePositionToIndex(in vector);
        }

        public TileIndex PositionToIndex(in Vector3IntRO vector)
        {
            var xRelative = vector.x - originAsInt.x;
            var v = new Vector3IntRO(xRelative, 0, vector.z - originAsInt.z);
            var fullRows = v.z * columnsXRows.x;
            var index = fullRows + xRelative;
            return index;
        }

        public TileIndex TilePositionToIndex(in TilePosition position)
        {
            return MapPositionToMapIndex(position).value;
        }

        public MapIndex MapPositionToMapIndex(in MapPosition position)
        {
            var result = position.position.z * columnsXRows.x + position.position.x;
            return result;
        }

        public MapPosition MapIndexToMapPosition(MapIndex index)
        {
            var pos = ToTilePositionXZ(index);
            return pos;
        }

        public bool PositionToMapIndex(Vector3IntRO position, out MapIndex mapIndex)
        {
            var tilePos = PositionToTilePosition(position);
            if (TilePositionToMapPosition(in tilePos, out var mapPos))
            {
                mapIndex = MapPositionToMapIndex(mapPos);
                return true;
            }
            mapIndex = -1;
            return false;
        }

        public bool TileIndexToMapIndex(TileIndex tileIndex, out MapIndex mapIndex)
        {
            mapIndex = tileIndex;
            if (count > tileIndex.value) return true;
            return false;
        }

        public bool TilePositionToMapPosition(in TilePosition position, out MapPosition mapPosition)
        {
            mapPosition = position;
            if (position.x < 0) return false;
            if (position.z < 0) return false;
            if (position.x >= columnsXRows.x) return false;
            if (position.z >= columnsXRows.z) return false;
            return true;
        }

        public TilePosition PositionToTilePosition(in Vector3IntRO vector)
        {
            var pos = vector - tilePositionMin;
            return (TilePosition)pos;
        }

        public TilePosition ToTilePositionXZ(MapIndex index)
        {
            int z = index.value / columnsXRows.x;
            int x = index.value % columnsXRows.x;
            return new TilePosition(x, 0, z);
        }

        public TilePosition ToTilePositionXZ(in Vector3RO position)
        {
            var dir = position - origin;
            var x = (int)MathF.Floor(dir.x / diagonalOfCell.x);
            var z = (int)MathF.Floor(dir.z / diagonalOfCell.z);
            return new TilePosition(x, 0, z);
        }

        public Vector3RO CenterOf(in MapPosition pos)
        {
            var offset = new Vector3RO(pos.position.x * diagonalOfCell.x, 0, pos.position.z * diagonalOfCell.z);
            var center = diagonalOfCell * 0.5f + offset + origin;
            return center;
        }

        public Vector3RO CenterOf(TileIndex target)
        {
            throw new System.NotImplementedException();
        }

        public override TCell GetCell(MapPosition position)
        {
            var index = MapPositionToMapIndex(in position);
            var cell = GetCellByIndex(index);
            return cell;
        }
        public abstract TCell GetCellByIndex(MapIndex index);
    }
}
