namespace IziHardGames.UnityApps.Domain.Events
{
    public class OnAppStartedEvent<TApp> : OnAppStartedEvent
    {
        public TApp App { get; private set; }
        public OnAppStartedEvent(TApp app)
        {
            this.App = app;
        }
    }
}
