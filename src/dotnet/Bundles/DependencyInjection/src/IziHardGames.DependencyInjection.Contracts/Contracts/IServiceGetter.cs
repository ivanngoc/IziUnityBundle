using System;

namespace IziHardGames.DependencyInjection.Contracts
{
    /// <summary>
    /// System.IServiceGetter
    /// </summary>
    public interface IServiceGetter
    {
        public T GetService<T>();
        public T GetService<T>(Guid guid);
        public T GetService<T>(string key);
        public T GetService<T>(int key);
        public T GetService<T>(Type serviceType);
        public T GetService<T, TKey>(TKey key);
    }
}
