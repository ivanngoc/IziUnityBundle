using IziHardGames.CommonDomain.Contracts;
using IziHardGames.UnityApps.Contracts;
using IziHardGames.UnityApps.Contracts.Apps;

namespace IziHardGames.UnityApps.Domain.Events
{
    public class OnAppBuildedEvent : IRunOnceEvent
    {
        public IIziApp App { get; }
        public OnAppBuildedEvent(IIziApp app)
        {
            App = app;
        }
    }
}
