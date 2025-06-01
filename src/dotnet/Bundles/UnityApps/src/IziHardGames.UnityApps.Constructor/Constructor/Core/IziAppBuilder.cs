using System;
using IziHardGames.Apps.Contracts;
using IziHardGames.DependencyInjection.Contracts;
using IziHardGames.UnityApps.Contracts;
using IziHardGames.UnityApps.Contracts.Apps;
using UnityEngine;
using IServiceProvider = System.IServiceProvider;

namespace IziHardGames.AppConstructor
{
    public sealed class IziAppBuilder : IIziAppBuilder
    {
        private readonly IIziServiceCollection iziCollectionService;
        private readonly IIziAppScheme? scheme;
        private IServiceProvider? servicesProvider;
        public object Services { get => iziCollectionService; }


        public object ServicesProvider { get => servicesProvider ?? throw new NullReferenceException(); }

        public IziAppBuilder(IIziServiceCollection iziCollectionService, IIziAppScheme? scheme)
        {
            this.iziCollectionService = iziCollectionService;
            this.scheme = scheme;
        }
        public IIziApp Build(Action<object>? serviceCollectionHandler = null)
        {
            if (serviceCollectionHandler != null)
            {
                serviceCollectionHandler(iziCollectionService.GetServiceCollection() ?? throw new NullReferenceException());
            }
            var customProvider = iziCollectionService.GetServiceProvider();
            if (customProvider == null) throw new NullReferenceException();
            this.servicesProvider = customProvider.GetServiceProvider();
            return new IziApp(customProvider);
        }

        public bool InitilizeInner()
        {
            try
            {
                if (scheme != null)
                {
                    foreach (var startup in scheme.GetStartups())
                    {
                        startup(iziCollectionService);
                    }

                    foreach (var item in scheme.GetSingletons())
                    {
                        iziCollectionService.AddSingleton(item.Item1, item.Item2);
                    }

                    foreach (var item in scheme.GetTransients())
                    {
                        iziCollectionService.Add(item.Item1, item.Item2, EServiceScope.Transient);
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                Debug.LogException(ex);
                throw;
            }
        }

        public bool InitilizeServices()
        {
            if (scheme != null)
            {
                foreach (var service in scheme.GetServices())
                {
                    iziCollectionService.AddSingleton(service, service);
                }
            }
            return false;
        }

    }
}
