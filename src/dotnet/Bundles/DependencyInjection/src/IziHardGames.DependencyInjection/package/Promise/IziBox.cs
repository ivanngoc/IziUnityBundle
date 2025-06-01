using System;
using System.Threading.Tasks;
using IziHardGames.DependencyInjection.Contracts.Promises;

namespace IziHardGames.DependencyInjection.Promises
{
    public class IziBox<T> : IDisposable, IPromise<T>
        where T : class
    {
        private T? value;
        public T Value => value ?? throw new NullReferenceException();

        public void SetValueWithoutNotify()
        {
            throw new System.NotImplementedException();
        }
        public void SetValue(T value)
        {
            this.value = value;
        }
        public void Dispose()
        {
            throw new NotImplementedException();
        }
        public ValueTask<T> AwaitAsValueTask()
        {
            throw new System.NotImplementedException();
        }
        public Task<T> AwaitAsTask()
        {
            throw new System.NotImplementedException();
        }
        public void OnGetValue(Action<T> handler)
        {
            throw new NotImplementedException();
        }
    }
}
