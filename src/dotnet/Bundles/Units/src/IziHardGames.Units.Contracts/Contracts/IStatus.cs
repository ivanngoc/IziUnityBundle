namespace IziHardGames.GameApps.Units.Contracts
{
    /// <summary>
    /// У юнита может быть несколько статусов в один момент времени
    /// </summary>
    public interface IStatus<T>
    {
        T Status { get; set; }
    }
}
