using System.Collections.Generic;

namespace IziHardGames.IziMoving.Contracts.Domain.Enteties
{
    public interface IRoute<TPos>
    {
        IEnumerable<TPos> Positions { get; }
    }
}
