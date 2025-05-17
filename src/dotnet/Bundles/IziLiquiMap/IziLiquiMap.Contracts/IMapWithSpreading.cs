using System;

namespace IziHardGames.IziLiquiMap.Contracts
{
    public interface IMapWithSpreading<TPos, TCell> : IMap<TPos, TCell>
        where TCell : IMapCell
    {
        void Spread();
    }
}
