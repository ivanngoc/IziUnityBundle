namespace IziHardGames.CommonDomain.Contracts
{
    public interface IDomainEventHandler<T> where T : IDomainEvent
    {
        void ConsumeEvent(T e);
    }
}
