using System;
using IziHardGames.Apps.Abstractions.Lib;
using IziHardGames.DependencyInjection.Contracts;
using IziHardGames.UnityApps.Contracts;
using IServiceProvider = System.IServiceProvider;

namespace IziHardGames.AppConstructor
{
    public sealed class IziApp : IIziApp
    {
        private readonly IIziServiceProvider provider;
        public IServiceProvider ServiceProvider { get => provider.GetServiceProvider() ?? throw new NullReferenceException(); }

        public IziApp(IIziServiceProvider provider)
        {
            //IziHardGames.DependencyInjection.Abstractions.IIziInjectable
            this.provider = provider ?? throw new NullReferenceException();
        }

        public void Run()
        {
            //Debug.Log("App is running");
            var interPoint = (provider.GetService(typeof(IAppEnterPoint)) as IAppEnterPoint) ?? throw new NullReferenceException();
            interPoint.Run(provider.GetServiceProvider() ?? throw new NullReferenceException());
        }
    }
}
