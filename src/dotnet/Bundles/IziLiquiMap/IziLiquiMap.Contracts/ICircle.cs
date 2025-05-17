namespace IziHardGames.IziLiquiMap.Contracts
{
    /// <summary>
    /// Кольцо из ячеек на расстоянии N от целевой ячейки
    /// </summary>
    public interface ICircle
    {
        public int Radius { get; }
    }

    public interface ICircle<TPos> : ICircle
    {
        TPos Center { get; }
    }
}
