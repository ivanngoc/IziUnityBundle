using System;
using System.Collections.Generic;

namespace IziHardGames.CoreForUnityApp
{
    public class DomainEventPublishing
    {
        public IEnumerable<DomainEventSubscription?> Subs => subs;
        private readonly List<DomainEventSubscription?> subs = new List<DomainEventSubscription?>();
        public object? lastEvent;
        public int executionCount;

        public void Add(DomainEventSubscription sub)
        {
            subs.Add(sub);
        }

        public void Remove(DomainEventSubscription sub)
        {
            subs.Remove(sub);
        }
    }
}
