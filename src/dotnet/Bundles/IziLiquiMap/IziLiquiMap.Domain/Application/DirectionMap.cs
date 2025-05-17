using System;
using System.Collections.Generic;
using IziHardGames.Geometry.Domain.Vectors;
using IziHardGames.Pools.Contracts;

namespace IziHardGames.IziLiquiMap.Domain.Models
{
    public class DirectionMap : MapXZ<CellOfDirecting>
    {
        private readonly IPoolRent<CellOfDirecting> pool;
        private readonly Dictionary<int, CellOfDirecting> cells = new Dictionary<int, CellOfDirecting>();

        public DirectionMap(Vector3RO origin, Vector3RO diagonal, Vector3IntRO bounds, IPoolRent<CellOfDirecting> poolRent, Vector3RO defaultDirection) : base(origin, diagonal, bounds)
        {
            this.pool = poolRent;
        }

        public override CellOfDirecting GetCellByIndex(MapIndex index)
        {
            throw new NotImplementedException();
        }

        public override bool IsValid(in MapPosition position)
        {
            throw new NotImplementedException();
        }
    }
}
