namespace IziHardGames.IziLiquiMap.Contracts
{
    public interface ICircleProvider<T, TMapIndex> where T : ICircle
    {
        T GetCircle(TMapIndex index, int radius);
    }
}
