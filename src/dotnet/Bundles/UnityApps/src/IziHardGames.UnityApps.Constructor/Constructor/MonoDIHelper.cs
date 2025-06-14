using System;
using System.Collections.Generic;
using System.Reflection;
using IziHardGames.DependencyInjection.Application.Mono;
using IziHardGames.UnityApps.Contracts.Apps;
using IziHardGames.UnityApps.Contracts.DependencyInjection;
using IziHardGames.UnityApps.Contracts.Worlds;
using Microsoft.Extensions.DependencyInjection;
using UnityEngine;

namespace IziHardGames.UnityApps.Constructor
{
    public static class MonoDIHelper
    {
        private static readonly MethodInfo miInjectServiceGen;
        static MonoDIHelper()
        {
            miInjectServiceGen = typeof(MonoDIHelper).GetMethod(nameof(InjectServiceGen), BindingFlags.Static | BindingFlags.NonPublic);
        }

        public static void AddServices(SceneServices sceneServices, IServiceCollection services)
        {
            foreach (var pair in sceneServices.GetServicesToAddAsSingleton())
            {
                services.AddSingleton(pair.service, pair.instance);
            }
        }

        public static void InjectServicesOfWorld(IEnumerable<MonoBehaviour> monos, IWorld world)
        {
            InjectServicesRequired(monos, world.ServiceProvider);
        }

        public static void InjectServicesOfApp(IEnumerable<MonoBehaviour> monos, IIziApp app)
        {
            InjectServices(monos, app.ServiceProvider);
        }

        public static void InjectServices(IEnumerable<MonoBehaviour> monos, IServiceProvider provider)
        {
            foreach (var mono in monos)
            {
                var type = mono.GetType();
                var ifaces = type.GetInterfaces();
                foreach (var iface in ifaces)
                {
                    if (iface.IsGenericType && iface.GetGenericTypeDefinition() == typeof(IIziInject<>))
                    {
                        var typeToInject = iface.GenericTypeArguments[0];
                        var serv = provider.GetService(typeToInject);
                        if (serv != null)
                        {
                            InjectService(mono, serv, typeToInject);
                        }
                    }
                }
            }
        }

        public static void InjectServicesRequired(IEnumerable<MonoBehaviour> monos, IServiceProvider provider)
        {
            foreach (var mono in monos)
            {
                var type = mono.GetType();
                var ifaces = type.GetInterfaces();
                foreach (var iface in ifaces)
                {
                    if (iface.IsGenericType && iface.GetGenericTypeDefinition() == typeof(IIziInject<>))
                    {
                        var typeToInject = iface.GenericTypeArguments[0];
                        var serv = provider.GetRequiredService(typeToInject);
                        InjectService(mono, serv, typeToInject);
                    }
                }
            }
        }
        private static void InjectService(MonoBehaviour mono, object serv, Type typeToInject)
        {
            var gen = miInjectServiceGen.MakeGenericMethod(typeToInject);
            gen.Invoke(null, new object[] { mono, serv });
        }

        private static void InjectServiceGen<T>(IIziInject<T> target, T item)
        {
            target.Service = item;
        }

        public static void InheritServices(IServiceCollection from, IServiceCollection to)
        {
            foreach (var serviceDescriptor in from)
            {
                to.Add(serviceDescriptor);
            }
        }
    }
}
