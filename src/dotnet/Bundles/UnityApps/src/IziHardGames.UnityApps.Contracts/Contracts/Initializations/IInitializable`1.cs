namespace IziHardGames.UnityApps.Contracts.Initializations
{
    public interface IInitializable<in T>
    {
        void Initialize(T t);
    }
}