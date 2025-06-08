using System;
using System.Collections;
using System.Collections.Generic;

namespace IziHardGames.IziMoving.RVO2.Contracts
{
    public interface IRVOAgent<TOrcaLine>
    {
        IEnumerable<TOrcaLine> Lines { get; }
    }
}
