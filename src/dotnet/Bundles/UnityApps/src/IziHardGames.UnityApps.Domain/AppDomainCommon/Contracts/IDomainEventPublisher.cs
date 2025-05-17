using System.Threading.Tasks;

namespace IziHardGames.CommonDomain.Contracts
{
    public interface IDomainEventPublisher
    {
        public void Publish<T>(T @event) where T : IDomainEvent;
        public Task PublishAsync<T>(T @event) where T : IDomainEvent;
    }
}
