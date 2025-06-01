using IziHardGames.CommonDomain.Contracts;
using IziHardGames.EventSourcing.Contracts.Events;
using IziHardGames.EventSourcing.Contracts.Handlers;
using IziHardGames.UnityApps.Application.Infrastracture;

namespace IziHardGames.UnityApps.Application.Handlers.Base
{
    public abstract class BaseDomainEventHandler<T> : IDomainEventHandler<T> where T : IDomainEvent
    {
        public BaseDomainEventHandler()
        {
            DomainEventManager.OnSingletonEvent<T>(ConsumeEvent);
        }
        public abstract void ConsumeEvent(T e);
    }
}
