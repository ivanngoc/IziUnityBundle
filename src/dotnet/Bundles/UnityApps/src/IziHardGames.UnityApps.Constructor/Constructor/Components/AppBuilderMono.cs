#nullable enable
//using System;
//using System.Linq;
//using IziHardGames.AppConstructor;
//using IziHardGames.ApplicationLevel;
//using IziHardGames.Apps.Abstractions.Lib;
//using IziHardGames.CommonDomain.Contracts;
//using IziHardGames.CommonDomain.Events;
using System;
using System.Linq;
using IziHardGames.DependencyInjection.Application.Mono;
using IziHardGames.DependencyInjection.Contracts;
using IziHardGames.UnityApps.Constructor;
using IziHardGames.UnityApps.Contracts.Apps;
using Microsoft.Extensions.DependencyInjection;
using UnityEngine;

namespace IziHardGames.Apps.ForUnity
{
    public abstract class AppBuilderMono : MonoBehaviour
    {
        private IServiceCollection? Services { get; set; }
        private bool isResolving;
        private bool isResolved;
        private bool isBuildStarted;
        private BuildContinuation? buildContinuation;
        [SerializeField] private MonoBehaviour[] monos = null!;
        [SerializeField] private MonoBehaviour[] addServiceInstances = null!;
        [SerializeField] private MonoBehaviour[] addServiceTypes = null!;

        protected void OnValidate()
        {
            var scene = this.gameObject.scene;
            this.monos = Component.FindObjectsByType<MonoBehaviour>(FindObjectsInactive.Include, FindObjectsSortMode.None).Where(x => x.gameObject.scene == scene).ToArray();
            this.addServiceInstances = monos.Where(x => x is IAddServicesInstances).ToArray();
            this.addServiceTypes = monos.Where(x => x is IAddServicesType).ToArray();
            OnValidateCustom();
        }

        protected void Awake()
        {
            enabled = true;
            CollectServices();
            AwakeCustom();
        }

        protected void Update()
        {
            if (isBuildStarted && isResolving && IsReady())
            {
                isResolving = false;
                enabled = false;
                OnResolving();
                isResolved = true;
            }
        }

        public BuildContinuation Build()
        {
            if (isBuildStarted) throw new InvalidOperationException("Buuild is already started");
            isBuildStarted = true;
            var sc = CreateServiceCollection();
            Services = sc;
            AddServices(sc);
            ConfigureServices(sc);
            var cont = new BuildContinuation();
            buildContinuation = cont;
            cont.Then(x =>
            {
                MonoDIHelper.InjectServicesRequired(monos, x.ServiceProvider);
            });
            return cont;
        }

        protected abstract IServiceCollection CreateServiceCollection();
        protected abstract IIziApp CreateApp(IServiceCollection services);
        protected void CollectServices()
        {

        }
        protected void AddServices(IServiceCollection services)
        {
            foreach (var item in addServiceInstances)
            {
                if (item is IAddServicesInstances add)
                {
                    foreach (var pair in add.GetServiceInstanceToAdd())
                    {
                        services.AddSingleton(pair.service, pair.instance);
                    }
                }
            }
            foreach (var item in addServiceTypes)
            {
                if (item is IAddServicesType add)
                {
                    foreach (var pair in add.GetServiceImplementationTypeToAdd())
                    {
                        services.AddSingleton(pair.service, pair.impl);
                    }
                }
            }
            foreach (var item in monos)
            {
                if (item is SceneServices sceneServices)
                {
                    foreach (var pair in sceneServices.GetServicesToAddAsSingleton())
                    {
                        services.AddSingleton(pair.service, pair.instance);
                    }
                    foreach (var pair in sceneServices.GetInMemoryConsumers())
                    {
                        services.AddSingleton(pair.service, pair.instance);
                    }
                }
            }
        }
        protected virtual void ConfigureServices(IServiceCollection services)
        {

        }
        /// <summary>
        /// Call when 
        /// </summary>
        protected void Resolve()
        {
            if (isResolving) throw new InvalidOperationException("Builder is already resolving");
            if (isResolved) throw new InvalidOperationException("Builder is already resolved");
            isResolving = true;
        }
        protected void OnResolving()
        {
            if (Services == null || buildContinuation == null) throw new InvalidOperationException($"{nameof(Resolve)} invoked before {nameof(Build)}");
            var app = CreateApp(Services);
            buildContinuation.Resolve(app);
            OnResolved(app);
        }
        protected virtual void OnResolved(IIziApp app)
        {

        }
        protected virtual void OnValidateCustom()
        {

        }
        protected virtual void AwakeCustom()
        {

        }
        protected virtual bool IsReady()
        {
            return true;
        }
    }
}