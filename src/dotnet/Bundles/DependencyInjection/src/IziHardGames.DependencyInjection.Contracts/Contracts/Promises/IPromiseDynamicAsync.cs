using System;
using System.Threading.Tasks;

namespace IziHardGames.DependencyInjection.Contracts.Promises
{
    public interface IPromiseDynamicAsync<T> : IPromiseAsync<T>
    {
        void OnChangeValue(Func<T, ValueTask<T>> handler);
    }
}
