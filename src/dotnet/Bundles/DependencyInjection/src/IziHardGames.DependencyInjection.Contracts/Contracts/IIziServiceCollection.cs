using System;

namespace IziHardGames.DependencyInjection.Contracts
{
    /// <summary>
    /// <see cref="IServiceGetter"/>
    /// </summary>
    public interface IIziServiceCollection //: global::Microsoft.Extensions.DependencyInjection.IServiceCollection
    {
        void AddSingleton<T1>(T1 implementation) where T1 : class;
        void AddSingleton<T1>() where T1 : class;
        void AddSingleton<T1, T2>() where T1 : class where T2 : class, T1;
        void AddSingleton(Type type);
        void AddSingleton(Type type, Type implementation);
        void Add(Type type, Type implementation, EServiceScope scope);
        void Add<T, TImpl>(EServiceScope scope) where TImpl : class, T;
        void Add<T>(T impl, EServiceScope scope);
        IIziServiceProvider? GetServiceProvider();
        object? GetServiceCollection();
    }

    public enum EServiceScope
    {
        None,
        Transient,
        Scoped,
        Singleton,
    }
}