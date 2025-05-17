using System.Collections.Generic;

namespace IziHardGames.Moving.Contracts
{
    public interface IRoute<TPos>
    {
        IEnumerable<TPos> Positions { get; }
    }
}
