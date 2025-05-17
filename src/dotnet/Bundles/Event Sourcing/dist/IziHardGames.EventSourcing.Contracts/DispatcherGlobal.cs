using System;
using System.Collections.Generic;
using IziHardGames.EventSourcing.Contracts;

namespace IziHardGames.EventSourcing.Dist.Unity
{
    public static class DispatcherGlobal
    {
        private readonly static Dictionary<Type, IConsumer> consumers = new Dictionary<Type, IConsumer>();

        public static void Regist<T>(IConsumer<T> e) where T : IEvent
        {
#if UNITY_EDITOR || DEBUG
            if (consumers.TryGetValue(typeof(T), out var c))
            {
                consumers[typeof(T)] = c;
                return;
            }
#endif
            consumers.Add(typeof(T), e);
        }

        public static void Dispatch<T>(T e) where T : IEvent
        {
            if (consumers.TryGetValue(e.GetType(), out var consumer))
            {
                var casted = (consumer as IConsumer<T>);
                if (casted != null)
                {
                    casted.Consume(e);
                }
                else
                {
                    throw new InvalidCastException($"Fact:{consumer.GetType().AssemblyQualifiedName}. Expected: {typeof(IConsumer<T>).AssemblyQualifiedName}");
                }
            }
        }

        public static void Clear()
        {
            consumers.Clear();
        }
    }
}
