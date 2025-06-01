namespace IziHardGames.UnityApps.Contracts.Identities
{
    /// <summary>
    /// Объект имеет идентификатор. В пределах одного типа может быть несколько экземлпяров
    /// </summary>
    public interface IId<TKey> : IUnique
    {
        TKey Id { get; set; }
    }
}