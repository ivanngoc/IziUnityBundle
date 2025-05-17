using System;
using System.Linq;
using IziHardGames.CommonDomain.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace IziHardGames.ApplicationLevel
{
    public static class RegistratorForHandler
    {
        public static void Discover(IServiceCollection serviceCollection)
        {
            var asms = AppDomain.CurrentDomain.GetAssemblies();

            foreach (var asm in asms)
            {
                var types = asm.GetTypes();

                foreach (var type in types)
                {
                    if (IsDomainEventHandler(type, out Type eventType))
                    {
                        serviceCollection.AddSingleton(type);
                    }
                }
            }
        }

        public static bool IsDomainEventHandler(Type type, out Type? eventType)
        {
            eventType = null;
            if (type.IsAbstract) return false;
            if (type.BaseType != null && type.BaseType.IsGenericType && type.BaseType.GetGenericTypeDefinition() == typeof(BaseDomainEventHandler<>))
            {
                eventType = type.BaseType.GetGenericArguments().First();
                return true;
            }
            else if (type.IsGenericType)
            {
                var typeOfHandler = typeof(IDomainEventHandler<>);
                var interfaceType = type.GetInterfaces().Where(x => x.IsGenericType).Where(x => x.GetGenericTypeDefinition() == typeOfHandler).FirstOrDefault();
                if (interfaceType != null)
                {
                    var constructedType = typeOfHandler.MakeGenericType(type);
                    eventType = interfaceType.GetGenericArguments().First();
                    return true;
                }
            }
            eventType = null;
            return false;
        }
    }
}