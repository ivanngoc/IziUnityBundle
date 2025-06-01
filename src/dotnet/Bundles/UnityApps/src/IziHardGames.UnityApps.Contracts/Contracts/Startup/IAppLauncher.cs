using IziHardGames.Apps.Contracts;
using IziHardGames.UnityApps.Contracts.Apps;

namespace IziHardGames.Apps.Abstractions.Lib
{
    public interface IAppLauncher
    {
        IIziAppBuilder Builder { get; }
        void SetScheme(IIziAppScheme scheme);
        bool Execute();
        IIziApp Complete();
    }
}
