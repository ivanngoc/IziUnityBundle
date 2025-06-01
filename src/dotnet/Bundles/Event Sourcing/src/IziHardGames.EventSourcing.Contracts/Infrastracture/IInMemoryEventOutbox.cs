namespace IziHardGames.EventSourcing.Contracts.Infrastracture
{
    public interface IInMemoryEventOutbox : IEventOutbox
    {
        void Add(object e);
    }
}
