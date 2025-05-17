using System;
using System.Collections;
using System.Collections.Generic;

namespace IziHardGames.IziLiquiMap.Domain.Models
{
    public struct CircleEnumeratorXZOfMap : IEnumerator<MapPosition>
    {
        public MapPosition Current { get; private set; }
        object IEnumerator.Current { get => throw new NotImplementedException(); }

        private Circle circle;
        private CircleEnumeratorXZ etor;

        public CircleEnumeratorXZOfMap(Circle circle)
        {
            etor = new CircleEnumeratorXZ(circle.center.position, circle.radius);
            this.circle = circle;
            Current = default;
        }

        public bool MoveNext()
        {
            while (etor.MoveNext())
            {
                var pos = etor.Current;
                if (circle.map.IsValid(pos)) return true;
            }
            return false;
        }

        public void Reset()
        {
            etor.Reset();
        }

        public void Dispose()
        {
            etor.Dispose();
            circle = default;
        }
    }
}
