using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;

namespace IziHardGames.IziMoving.RVO2.Contracts
{
    public interface IRVOAgent<TOrcaLine>
    {
        IEnumerable<TOrcaLine> Lines { get; }
        int CountObstacles { get; }
        int CountNeighbours { get; }
    }
}
