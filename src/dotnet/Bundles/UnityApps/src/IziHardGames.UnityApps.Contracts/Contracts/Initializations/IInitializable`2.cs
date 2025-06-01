namespace IziHardGames.UnityApps.Contracts.Initializations
{
    public interface IInitializable<T1, T2>
    {
        void Initialize(T1 t1, T2 t2);
    }
}