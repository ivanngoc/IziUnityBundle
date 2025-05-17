using IziHardGames.IziLiquiMap.Contracts;

namespace IziHardGames.IziLiquiMap.Domain.Models
{
    public abstract class Map<TPosition, TCell> : IMap<TPosition, TCell>
        where TCell : IMapCell
    {
        public abstract TCell GetCell(TPosition position);
        public abstract bool IsValid(in TPosition position);
    }
}
