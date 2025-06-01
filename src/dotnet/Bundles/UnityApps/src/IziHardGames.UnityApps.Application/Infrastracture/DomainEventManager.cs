using System;
using System.Collections.Concurrent;
using IziHardGames.CommonDomain.Contracts;
using IziHardGames.CoreForUnityApp;
using IziHardGames.EventSourcing.Contracts.Events;

namespace IziHardGames.UnityApps.Application.Infrastracture
{
    public static class DomainEventManager
    {
        private static readonly ConcurrentQueue<DomainEventSubscription> callbacksWithEventTypes = new ConcurrentQueue<DomainEventSubscription>();
        private static Action<DomainEventSubscription> onDomainEventSubscriptionHandler;
        public static void OnSingletonEvent<T>(Action<T> callback) where T : IDomainEvent
        {
            OnEvent<T>(callback, EEventSubscriptionType.RunOnceForSure);
        }
        public static void OnNextEvent<T>(Action<T> callback) where T : INextEvent
        {
            OnEvent<T>(callback, EEventSubscriptionType.Next);
        }

        public static void OnFrameMomentEvent<T>(Action<T> callback) where T : IFrameMomentEvent
        {
            OnEvent<T>(callback, EEventSubscriptionType.FrameMoment);
        }
        public static void OnEvent<T>(Action<T> callback, EEventSubscriptionType subscriptionType) where T : IDomainEvent
        {
            lock (callbacksWithEventTypes)
            {
                var sub = new DomainEventSubscription(typeof(T), callback, subscriptionType);
                if (onDomainEventSubscriptionHandler != null)
                {
                    onDomainEventSubscriptionHandler(sub);
                }
                else
                {
                    callbacksWithEventTypes.Enqueue(sub);
                }
            }
        }

        public static void OnSubscription(Action<DomainEventSubscription> consumer)
        {
            lock (callbacksWithEventTypes)
            {
                while (callbacksWithEventTypes.TryDequeue(out var subscription))
                {
                    consumer(subscription);
                }
                onDomainEventSubscriptionHandler = consumer;
            }
        }
    }
}
