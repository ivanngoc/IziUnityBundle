using IziHardGames.EventSourcing.Contracts.Events;

namespace IziHardGames.EventSourcing.Contracts.Handlers
{
    public interface IConsumer<TEvent> : IConsumer
        where TEvent : IEvent
    {
        void Consume(TEvent e);
    }
}
