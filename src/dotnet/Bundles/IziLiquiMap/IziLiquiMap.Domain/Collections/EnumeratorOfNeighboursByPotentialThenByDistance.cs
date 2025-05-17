using System;
using System.Collections;
using System.Collections.Generic;
using IziHardGames.Geometry.Domain.Vectors;

namespace IziHardGames.IziLiquiMap.Domain.Models
{
    public struct EnumeratorOfNeighboursByPotentialThenByDistance : IEnumerator<Cell?>
    {
        private Cell cell;
        public Cell? Current { get; private set; }
        public OctagonIndex CurrentIndexOfOctagon { get; private set; }
        object? IEnumerator.Current { get => Current; }

        private int index;
        public readonly Vector3RO position;
        private static readonly Func<EnumeratorOfNeighboursByPotentialThenByDistance, (Cell?, OctagonIndex)>[] actions;
        /// <summary>
        /// <see cref="EOctagonConfiguration"/>
        /// </summary>
        private EOctagonConfiguration configuration;
        static EnumeratorOfNeighboursByPotentialThenByDistance()
        {
            actions = new Func<EnumeratorOfNeighboursByPotentialThenByDistance, (Cell?, OctagonIndex)>[]
            {
                GetClosestWithinConfiguration,
                GetClosestWithinConfiguration,
                GetClosestWithinConfiguration,
                GetClosestWithinConfiguration,
            };
        }
        public EnumeratorOfNeighboursByPotentialThenByDistance(Cell cell, Vector3RO position)
        {
            this.cell = cell;
            Current = null;
            index = 0;
            this.position = new Vector3RO(position.x, 0, position.y);
            configuration = 0;
            CurrentIndexOfOctagon = 0;
        }

        public bool MoveNext()
        {
            if (index < 8)
            {
                bool isAtEast = cell.Center.x <= position.x;
                bool isAtNorth = cell.Center.z <= position.z;
                byte config = 0;
                if (isAtEast)
                {
                    config = 1;
                }
                if (isAtNorth)
                {
                    config |= 1 << 1;
                }
                var closest = actions[config](this);
                Current = closest.Item1;
                CurrentIndexOfOctagon = closest.Item2;
                configuration = (EOctagonConfiguration)((int)configuration | (1 << CurrentIndexOfOctagon.value));
                index++;
                return true;
            }
            return false;
        }

        public void Reset()
        {
            throw new NotImplementedException();
        }


        public void Dispose()
        {
            throw new NotImplementedException();
        }

        private static (Cell?, OctagonIndex) GetClosestWithinConfiguration(EnumeratorOfNeighboursByPotentialThenByDistance etor)
        {
            float minPotential = int.MaxValue;
            float minDistance = float.MaxValue;
            Cell? finded = null;
            OctagonIndex indexClosest = OctagonIndex.None;
            var cell = etor.cell;
            for (int i = 0; i < OctagonIndex.COUNT; i++)
            {
                if (((int)etor.configuration & (1 << i)) == 0)
                {
                    var neihbourCell = cell.octagon[i];
                    var potential = neihbourCell?.Potential ?? 0;
                    var to = cell.Center - etor.position;
                    var distanceSqr = to.DistanceSqr;
                    if (potential < minPotential)
                    {
                        indexClosest = i;
                        finded = neihbourCell;
                        minPotential = potential;
                        minDistance = distanceSqr;
                    }
                    else if (potential == minPotential && distanceSqr < minDistance)
                    {
                        indexClosest = i;
                        finded = neihbourCell;
                        minDistance = distanceSqr;
                    }
                }
            }
            return (finded, indexClosest);
        }
        private static Cell GetNorth()
        {
            throw new System.NotImplementedException();
        }
    }
}
