using System;
using System.Collections;
using System.Collections.Generic;
using IziHardGames.DependencyInjection.Contracts;

namespace IziHardGames.DependencyInjection.Implementations
{
    public abstract class AdapterForDependecyInjection : IEnumerable<AdapterForServiceDescription>
    {
        public abstract IEnumerator<AdapterForServiceDescription> GetEnumerator();

        public abstract void AddTransient(Type type, Type implementation);
        public abstract void AddTransient<T1, T2>() where T1 : class where T2 : class, T1;

        public abstract void AddSingleton<T>() where T:class;
        public abstract void AddSingleton<T>(T impl) where T:class;
        public abstract void AddSingleton(Type type);
        public abstract void AddSingleton(Type type, Type implementation);
        public abstract void AddSingleton<T1, T2>() where T1 : class where T2 : class, T1;
        public abstract IIziServiceProvider GetServiceProvider();
        public abstract object GetServiceCollection();

        IEnumerator IEnumerable.GetEnumerator()
        {
            //return ((IEnumerable)services).GetEnumerator();
            return ((IEnumerator)this.GetEnumerator());
        }

    }
}
