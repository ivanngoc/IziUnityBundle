using System;

namespace IziHardGames.DependencyInjection.Contracts
{
    /// <summary>
    /// <see cref="IServiceGetter"/>
    /// </summary>
    public interface IIziServiceProvider : IServiceGetter
    {
        object? GetService(Type type);
        object? GetService(Guid guid);
        T GetServiceAs<T>();
        T GetServiceAs<T>(Guid guid);

        object GetSingleton(Type type);
        T GetSingletonAs<T>();
        System.IServiceProvider? GetServiceProvider();
    }
}
