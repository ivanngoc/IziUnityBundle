using IziHardGames.EventSourcing.Contracts.Events;

namespace IziHardGames.EventSourcing.Contracts.Handlers
{
    public interface IInMemoryConsumer<TEvent> where TEvent : IInMemoryEvent
    {
        void Consume(TEvent e);
    }
}
