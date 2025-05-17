using System;

namespace IziHardGames.DependencyInjection.Contracts.Promises
{
    public interface IPromiseDynamic<T> : IPromise<T>
    {
        void OnChangeValue(Action<T> handler);
    }
}
