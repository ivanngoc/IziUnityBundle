using System.Threading.Tasks;

namespace IziHardGames.UnityApps.Contracts
{
    public interface IIziAppVersion1: IIziApp
    {
        public static bool IsStartupFinished { get; internal set; }
        public static IIziAppVersion1? SharedAppV1 { get; set; }
        public Task StartAsync();
        public T GetSingleton<T>() where T : class;
    }
}
