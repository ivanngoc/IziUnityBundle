using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using IziHardGames.EventSourcing.Contracts.Handlers;
using Microsoft.Extensions.DependencyInjection;
using IziHardGames.DependencyInjection.Attributes;

namespace IziHardGames.EventSourcing.Application.Infrastracture
{
    public static class Extensions
    {
        public static IServiceCollection AddInMemoryHandlers(this IServiceCollection services, Func<Type, bool>? filter = null)
        {
            var asms = AppDomain.CurrentDomain.GetAssemblies();
            return services.AddInMemoryHandlers(asms, filter);
        }

        public static IServiceCollection AddInMemoryHandlers(this IServiceCollection services, IEnumerable<Assembly> assemblies, Func<Type, bool>? filter)
        {
            foreach (var asm in assemblies)
            {
                var types = asm.GetTypes();
                var filtered = types;
                if (filter != null)
                {
                    filtered = types.Where(t => filter(t)).ToArray();
                }
                foreach (var type in filtered)
                {
                    if (type.IsAbstract) continue;
                    if (type.GetCustomAttributes().Any(x => x.GetType() == typeof(ExcludeAutoDIAttribute))) continue;
                    var interfaces = type.GetInterfaces();
                    var handler = interfaces.FirstOrDefault(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(IInMemoryConsumer<>));
                    if (handler != null)
                    {
                        if (type.IsGenericType)
                        {
                            services.AddSingleton(typeof(IInMemoryConsumer<>), type);
                        }
                        else
                        {
                            var tagetHandlerType = typeof(IInMemoryConsumer<>).MakeGenericType(handler.GenericTypeArguments[0]);
                            services.AddSingleton(tagetHandlerType, type);
                        }
                    }
                }
            }
            return services;
        }
    }
}
