using System;
namespace IziHardGames.CoreForUnityApp
{
    public class DomainEventSubscription
    {
        public readonly Type eventType;
        public readonly object handler;
        public readonly EEventSubscriptionType subscriptionType;

        public DomainEventSubscription(Type eventType, object handler, EEventSubscriptionType subscriptionType)
        {
            this.eventType = eventType;
            this.handler = handler;
            this.subscriptionType = subscriptionType;
        }
    }
}
