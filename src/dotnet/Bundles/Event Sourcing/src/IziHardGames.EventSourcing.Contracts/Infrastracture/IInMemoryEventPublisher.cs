using IziHardGames.EventSourcing.Contracts.Events;

namespace IziHardGames.EventSourcing.Contracts.Infrastracture
{
    public interface IInMemoryEventPublisher
    {
        void Publish<T>(T e) where T : IInMemoryEvent;
    }
}
