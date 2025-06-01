using System;
using System.Collections.Generic;
using System.Linq;
using IziHardGames.CommonDomain.Contracts;
using IziHardGames.CoreForUnityApp;
using IziHardGames.EventSourcing.Contracts.Events;
using IziHardGames.EventSourcing.Contracts.Handlers;
using IziHardGames.EventSourcing.Contracts.Infrastracture;
using IziHardGames.UnityApps.Application.Infrastracture;
using Microsoft.Extensions.DependencyInjection;
using Task = System.Threading.Tasks.Task;
namespace IziHardGames.ApplicationLevel
{
    public sealed class DomainEventPublisher : IDomainEventPublisher
    {
        private readonly Dictionary<Type, DomainEventPublishing> keyValuePairs = new Dictionary<Type, DomainEventPublishing>();
        private readonly Dictionary<Type, object> classHandlersByEventType = new Dictionary<Type, object>();
        private readonly IServiceProvider provider;

        public DomainEventPublisher(IServiceProvider provider)
        {
            this.provider = provider;
            DomainEventManager.OnSubscription(CreateRouting);
        }

        public void PublishNonGen(Type type, object e)
        {
            var mi = GetType().GetMethod(nameof(Publish));
            var gen = mi.MakeGenericMethod(type);
            gen.Invoke(this, new[] { e });
        }

        public void Publish<T>(T e) where T : IDomainEvent
        {
            if (!keyValuePairs.TryGetValue(typeof(T), out var pub))
            {
                pub = new DomainEventPublishing();
                keyValuePairs.Add(typeof(T), pub);
            }

            /// <see cref="BaseDomainEventHandler{T}"/>
            //var classHandler = GetHandler<T>();
            //if (classHandler != null)
            //{
            //    classHandler.ConsumeEvent(e);
            //}

            if (pub.Subs.Any())
            {
                foreach (var sub in pub.Subs)
                {
                    var obj = sub?.handler ?? throw new NullReferenceException();
                    var handler = obj as Action<T> ?? throw new InvalidCastException(obj.GetType().AssemblyQualifiedName);
                    handler(e);
                    // collection is modified inside this method (remove). 
                    //AfterPublishWork<T>(pub, e);
                }
            }
            else
            {
                DelayPublish<T>(pub);
            }
            pub.lastEvent = e;
        }


        public IDomainEventHandler<T>? GetHandler<T>() where T : IDomainEvent
        {
            if (classHandlersByEventType.TryGetValue(typeof(T), out var finded))
            {
                return (finded as IDomainEventHandler<T>) ?? throw new InvalidCastException(finded.GetType().AssemblyQualifiedName);
            }
            var handler = provider.GetService<IDomainEventHandler<T>>();
            if (handler == null) return null;
            classHandlersByEventType.Add(typeof(T), handler);
            return handler;
        }

        public async Task PublishAsync<T>(T e) where T : IDomainEvent
        {
            if (!keyValuePairs.TryGetValue(typeof(T), out var pub))
            {
                pub = new DomainEventPublishing();
                keyValuePairs.Add(typeof(T), pub);
            }

            if (pub.Subs.Any())
            {
                foreach (var sub in pub.Subs)
                {
                    var obj = sub?.handler ?? throw new NullReferenceException();
                    var handler = obj as Func<T, Task> ?? throw new InvalidCastException(obj.GetType().AssemblyQualifiedName);
                    await handler(e);
                    // collection is modified inside this method (remove). 
                    //AfterPublishWork<T>(pub, e);
                }
            }
            else
            {
                DelayPublish<T>(pub);
            }
            pub.lastEvent = e;
        }

        private void DelayPublish<T>(DomainEventPublishing pub) where T : IDomainEvent
        {

        }

        private void AfterPublishWork<T>(DomainEventPublishing pub, T e) where T : IDomainEvent
        {
            pub.executionCount++;
            for (int i = 0; i < pub.Subs.Count(); i++)
            {
                var sub = pub.Subs.ElementAt(i) ?? throw new NullReferenceException();
                if (sub.subscriptionType == EEventSubscriptionType.RunOnceForSure)
                {
                    pub.Remove(sub);
                }
            }
        }

        /// <summary>
        /// На одно событие может подписаться несколько источников.
        /// </summary>
        /// <param name="subscription"></param>
        private void CreateRouting(DomainEventSubscription subscription)
        {
            if (keyValuePairs.TryGetValue(subscription.eventType, out var pub))
            {
                pub.Add(subscription);
                if (subscription.subscriptionType == EEventSubscriptionType.RunOnceForSure)
                {
                    if (pub.lastEvent != null)
                    {
                        PublishNonGen(subscription.eventType, pub.lastEvent);
                    }
                }
            }
            else
            {
                pub = new DomainEventPublishing()
                {

                };
                pub.Add(subscription);
                keyValuePairs.Add(subscription.eventType, pub);
            }
        }
    }
}