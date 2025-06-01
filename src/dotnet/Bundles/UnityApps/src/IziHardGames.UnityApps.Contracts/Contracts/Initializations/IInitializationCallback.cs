using System;

namespace IziHardGames.UnityApps.Contracts.Initializations
{
    public interface IInitializationCallback
    {
        void OnInitialized(Action action);
    }
}