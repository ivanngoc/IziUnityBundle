using System.Threading.Tasks;
using IziHardGames.EventSourcing.Contracts.Events;

namespace IziHardGames.EventSourcing.Contracts.Infrastracture
{
    public interface IDomainEventPublisher
    {
        void Publish<T>(T @event) where T : IDomainEvent;
        Task PublishAsync<T>(T @event) where T : IDomainEvent;
    }
}
