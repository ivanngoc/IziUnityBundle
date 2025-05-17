using System;

namespace IziHardGames.UnityApps.Contracts
{
    public interface IIziApp : IIziAppCommon
    {
        public static IIziApp? AppShared { get; set; }
        IServiceProvider ServiceProvider { get; }
        void Run();
    }
}
