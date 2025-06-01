using System;

namespace IziHardGames.UnityApps.Contracts.Apps
{
    public interface IIziApp : IIziAppCommon
    {
        public static IIziApp? AppShared { get; set; }
        /// <summary>
        /// Global services
        /// </summary>
        IServiceProvider ServiceProvider { get; }
        void Run();
    }
}
