using System;
using System.Collections;
using System.Collections.Generic;
using IziHardGames.DependencyInjection.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace IziHardGames.DependencyInjection.Implementations
{
    public class IziCollectionService : IIziServiceCollection, IEnumerable<AdapterForServiceDescription>
    {
        private readonly AdapterForDependecyInjection adapter;
        internal IziCollectionService(AdapterForDependecyInjection adapter)
        {
            this.adapter = adapter;
        }
        public static IziCollectionService Create<T>() where T : AdapterForDependecyInjection
        {
            var adapter = Activator.CreateInstance<T>();
            return new IziCollectionService(adapter);
        }
        public static IziCollectionService CreateDefault() => Create<AdapterForDependecyInjectionForMicrosoft>();

        public IEnumerator<AdapterForServiceDescription> GetEnumerator()
        {
            return adapter.GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)adapter).GetEnumerator();
        }

        public void AddSingleton<T1, T2>() where T1 : class where T2 : class, T1
        {
            adapter.AddSingleton<T1, T2>();
        }
        public void AddSingleton(Type type, Type implementation)
        {
            adapter.AddSingleton(type, implementation);
        }

        public void AddTransient(Type type, Type implementation)
        {
            adapter.AddTransient(type, implementation);
        }
        public void AddTransient<T1, T2>() where T1 : class where T2 : class, T1
        {
            adapter.AddTransient<T1, T2>();
        }
        public IIziServiceProvider GetServiceProvider() => adapter.GetServiceProvider();

        public void AddSingleton<T1>() where T1 : class
        {
            adapter.AddSingleton<T1>();
        }

        public void AddSingleton(Type type)
        {
            adapter.AddSingleton(type);
        }

        public void AddSingleton<T1>(T1 implementation) where T1 : class
        {
            adapter.AddSingleton(implementation);
        }

        public T GetAdapterAs<T>() where T : AdapterForDependecyInjection
        {
            var casted = adapter as T;
            if (casted is null) throw new InvalidCastException($"Actual type is: {adapter.GetType().AssemblyQualifiedName}");
            return casted;
        }
        public T GetServiceCollectionAs<T>() where T : class
        {
            var casted = adapter.GetServiceCollection() as T;
            if (casted is null) throw new InvalidCastException($"Actual type is: {adapter.GetType().AssemblyQualifiedName}");
            return casted;
        }

        public object GetServiceCollection()
        {
            return adapter.GetServiceCollection();
        }

        public void Add(Type type, Type implementation, EServiceScope scope)
        {
            throw new NotImplementedException();
        }

        public void Add<T, TImpl>(EServiceScope scope) where TImpl : class, T
        {
            throw new NotImplementedException();
        }

        public void Add<T>(T impl, EServiceScope scope)
        {
            throw new NotImplementedException();
        }
    }
}

