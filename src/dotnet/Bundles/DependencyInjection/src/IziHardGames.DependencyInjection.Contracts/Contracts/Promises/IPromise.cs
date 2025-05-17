using System;

namespace IziHardGames.DependencyInjection.Contracts.Promises
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IPromise<out T>
    {
        T Value { get; }
        void OnGetValue(Action<T> handler);
    }
}
