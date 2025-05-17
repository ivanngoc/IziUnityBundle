namespace IziHardGames.IziLiquiMap.Contracts
{
    public interface IMap
    {

    }

    public interface IMap<TPosition, TCell> : IMap
        where TCell : IMapCell
    {
        TCell GetCell(TPosition position);
    }
}
