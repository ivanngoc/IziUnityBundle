using IziHardGames.EventSourcing.Contracts.Events;

namespace IziHardGames.EventSourcing.Contracts.Handlers
{
    public interface IDomainEventHandler<T> where T : IDomainEvent
    {
        void ConsumeEvent(T e);
    }
}
