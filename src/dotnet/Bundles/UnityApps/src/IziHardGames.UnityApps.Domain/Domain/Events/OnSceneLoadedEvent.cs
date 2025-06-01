using IziHardGames.EventSourcing.Contracts.Events;

namespace IziHardGames.UnityApps.Domain.Events
{
    public class OnSceneLoadedEvent : IInMemoryEvent
    {
        public string Name { get; private set; }
        public OnSceneLoadedEvent(string name)
        {
            this.Name = name;
        }
    }
}
