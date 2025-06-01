using System;

namespace IziHardGames.UnityApps.Contracts.Apps
{
    /// <summary>
    /// Like Microsoft.Extensions.Hosting.IHostApplicationBuilder
    /// </summary>
    public interface IIziAppBuilder
    {
        object Services { get; }
        object ServicesProvider { get; }
        IIziApp Build(Action<object>? serviceCollectionHandler = null);
    }
}
