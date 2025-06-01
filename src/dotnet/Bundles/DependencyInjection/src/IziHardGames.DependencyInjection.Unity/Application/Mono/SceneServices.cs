using System;
using System.Collections.Generic;
using System.Linq;
using IziHardGames.DependencyInjection.Contracts;
using IziHardGames.EventSourcing.Contracts.Handlers;
using UnityEngine;

namespace IziHardGames.DependencyInjection.Application.Mono
{
    public class SceneServices : MonoBehaviour, IAddServicesInstances
    {
        [SerializeField] private MonoBehaviour[] monos = Array.Empty<MonoBehaviour>();
        public MonoBehaviour[] MonoBehaviours => monos;
        private void OnValidate()
        {
            monos = UnityEngine.Object.FindObjectsByType<MonoBehaviour>(FindObjectsInactive.Include, FindObjectsSortMode.None).Where(x => x.gameObject.scene == this.gameObject.scene && x.tag != "EditorOnly").Append(this).ToArray();
        }
        private void Awake()
        {

        }

        public IEnumerable<(Type service, MonoBehaviour instance)> GetInMemoryConsumers()
        {
            var pairs = GetServicesToAddByTypeDefenitionT1NotAsignable(typeof(IInMemoryConsumer<>));
            foreach (var pair in pairs)
            {
                yield return (typeof(IInMemoryConsumer<>).MakeGenericType(pair.service), pair.instance as MonoBehaviour ?? throw new InvalidCastException());
            }
        }
        public IEnumerable<(Type service, object instance)> GetServicesToAddAsSingleton()
        {
            return GetServicesToAddByTypeDefenitionT1(typeof(IAddAsSingleton<>));
        }
        public IEnumerable<(Type service, Type impl)> GetServicesToAddAsScoped()
        {
            return GetServicesToAddAsType(typeof(IAddAsScoped<>));
        }
        public IEnumerable<(Type service, object instance)> GetServicesToAddAsTransient()
        {
            return GetServicesToAddByTypeDefenitionT1(typeof(IAddAsTransient<>));
        }
        public IEnumerable<(Type service, Type impl)> GetServicesToAddAsType(Type type)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<(Type service, MonoBehaviour instance)> GetServicesToAddByTypeDefenitionT1NotAsignable(Type type)
        {
            foreach (var mono in monos)
            {
                var ifaces = mono.GetType().GetInterfaces();
                foreach (var iface in ifaces)
                {
                    if (iface.IsGenericType && iface.GetGenericTypeDefinition() == type)
                    {
                        var typeService = iface.GenericTypeArguments[0];
                        if (!mono.GetType().IsAssignableFrom(typeService))
                        {
                            yield return (typeService, mono);
                        }
                    }
                }
            }
        }
        public IEnumerable<(Type service, object instance)> GetServicesToAddByTypeDefenitionT1(Type type)
        {
            foreach (var mono in monos)
            {
                var typeMono = mono.GetType();
                var ifaces = typeMono.GetInterfaces();
                foreach (var iface in ifaces)
                {
                    if (iface.IsGenericType && iface.GetGenericTypeDefinition() == type)
                    {
                        var typeService = iface.GenericTypeArguments[0];
                        if (!typeService.IsAssignableFrom(typeMono))
                        {
                            throw new InvalidOperationException($"Types mismatch. Actual {typeMono.AssemblyQualifiedName}. Impl: {typeService.AssemblyQualifiedName}");
                        }
                        yield return (typeService, mono);
                    }
                }
            }
        }

        public IEnumerable<(Type service, object instance)> GetServiceInstanceToAdd()
        {
            yield return (typeof(SceneServices), this);
        }
    }
}
