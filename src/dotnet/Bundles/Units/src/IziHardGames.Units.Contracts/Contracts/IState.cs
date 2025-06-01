namespace IziHardGames.GameApps.Units.Contracts
{
    /// <summary>
    /// Состояние юнита. В одим момент времени у ожного юнита может быть только одно состояние
    /// </summary>
    public interface IState<T>
    {
        T State { get; set; }
    }
}
