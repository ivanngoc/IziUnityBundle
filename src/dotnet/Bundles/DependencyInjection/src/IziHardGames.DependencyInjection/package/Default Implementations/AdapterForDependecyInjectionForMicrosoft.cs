using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using IziHardGames.DependencyInjection.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace IziHardGames.DependencyInjection.Implementations
{
    internal class AdapterForDependecyInjectionForMicrosoft : AdapterForDependecyInjection
    {
        private readonly ServiceCollection services;
        public IServiceCollection ServiceCollection => services;

        public AdapterForDependecyInjectionForMicrosoft() : base()
        {
            services = new ServiceCollection();
        }

        public override IEnumerator<AdapterForServiceDescription> GetEnumerator()
        {
            return services.Select(x => new AdapterForServiceDescription(x)).GetEnumerator();
        }

        public override void AddSingleton<T1, T2>()
        {
            services.AddSingleton<T1, T2>();
        }

        public override void AddSingleton(Type type, Type implementation)
        {
            services.AddSingleton(type, implementation);
        }

        public override void AddTransient(Type type, Type implementation)
        {
            services.AddTransient(type, implementation);
        }

        public override void AddTransient<T1, T2>()
        {
            services.AddTransient<T1, T2>();
        }

        public override IIziServiceProvider GetServiceProvider()
        {
            var provider = new AdapterForServiceCollection(services.BuildServiceProvider());
            return provider;
        }

        public override void AddSingleton<T>()
        {
            services.AddSingleton<T>();
        }
        public override void AddSingleton<T1>(T1 item)
        {
            services.AddSingleton(item);
        }

        public override void AddSingleton(Type type)
        {
            services.AddSingleton(type);
        }

        public override object GetServiceCollection()
        {
            return services;
        }
    }

    internal class AdapterForServiceCollection : IIziServiceProvider, IDisposable, IAsyncDisposable
    {
        private readonly Dictionary<Guid, Func<object>> servicesByGuid = new Dictionary<Guid, Func<object>>();
        private System.IServiceProvider serviceProvider;

        public AdapterForServiceCollection(System.IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;

            var serv = serviceProvider.GetRequiredService(typeof(IEnumerable<ServiceDescriptor>));
            var serviceCollection = serv as IEnumerable<ServiceDescriptor> ?? throw new InvalidCastException(serv.GetType().AssemblyQualifiedName);

            foreach (var descriptor in serviceCollection)
            {
                var type = descriptor.ImplementationType ?? throw new NullReferenceException();
                var guidAtr = type.GetCustomAttributes(false).FirstOrDefault(x => x is GuidAttribute) as GuidAttribute;
                if (guidAtr is null) continue;
                if (Guid.TryParse(guidAtr.Value, out var guid))
                {
                    servicesByGuid.Add(guid, () => serviceProvider.GetRequiredService(descriptor.ServiceType));
                }
                else
                {
                    throw new FormatException($"Guid is invalid for :{type.AssemblyQualifiedName}. value:{guidAtr.Value}");
                }
            }
        }

        public object GetService(Type type)
        {
            return serviceProvider.GetService(type);
        }

        public object GetService(Guid guid)
        {
            throw new NotImplementedException();
        }


        public T GetServiceAs<T>() where T : notnull
        {
            return serviceProvider.GetRequiredService<T>();
        }

        public T GetServiceAs<T>(Guid guid)
        {
            throw new NotImplementedException();
        }

        public object GetSingleton(Type type)
        {
            return serviceProvider.GetRequiredService(type);
        }

        public T GetSingletonAs<T>()
        {
            throw new NotImplementedException();
        }

        public ValueTask DisposeAsync()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public T GetService<T>()
        {
            throw new NotImplementedException();
        }

        public T GetService<T>(Guid guid)
        {
            throw new NotImplementedException();
        }

        public T GetService<T>(string key)
        {
            throw new NotImplementedException();
        }

        public T GetService<T>(int key)
        {
            throw new NotImplementedException();
        }

        public T GetService<T>(Type serviceType)
        {
            throw new NotImplementedException();
        }

        public T GetService<T, TKey>(TKey key)
        {
            throw new NotImplementedException();
        }

        public System.IServiceProvider? GetServiceProvider()
        {
            throw new NotImplementedException();
        }
    }
}
