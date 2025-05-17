using IziHardGames.CommonDomain.Events;
using IziHardGames.CoreForUnityApp;

namespace IziHardGames.CommonDomain.Contracts
{
    public abstract class BaseDomainEventHandler<T> : IDomainEventHandler<T> where T : IRunOnceEvent
    {
        public BaseDomainEventHandler()
        {
            DomainEventManager.OnSingletonEvent<T>(ConsumeEvent);
        }
        public abstract void ConsumeEvent(T e);
    }
}
