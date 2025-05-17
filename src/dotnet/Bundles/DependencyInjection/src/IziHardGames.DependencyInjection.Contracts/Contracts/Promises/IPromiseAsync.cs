using System.Threading.Tasks;

namespace IziHardGames.DependencyInjection.Contracts.Promises
{
    public interface IPromiseAsync<T> : IPromise<T>
    {
        ValueTask<T> GetValueAsync();
    }
}
