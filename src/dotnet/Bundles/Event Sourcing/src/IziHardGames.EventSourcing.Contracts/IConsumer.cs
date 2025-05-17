namespace IziHardGames.EventSourcing.Contracts
{
    public interface IConsumer<TEvent> : IConsumer
        where TEvent : IEvent
    {
        void Consume(TEvent e);
    }

    public interface IConsumer
    {

    }
}
