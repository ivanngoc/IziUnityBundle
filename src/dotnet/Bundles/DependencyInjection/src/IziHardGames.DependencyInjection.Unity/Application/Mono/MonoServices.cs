using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace IziHardGames.DependencyInjection.Application.Mono
{
    public class MonoServices //: IServiceCollection
    {
        public object GetService(Type serviceType)
        {
            throw new NotImplementedException();
        }

        public static IEnumerable<(MonoBehaviour mono, Type iface)> FindServices(Type serviceType)
        {
            var monos = GameObject.FindObjectsByType<MonoBehaviour>(FindObjectsInactive.Include, FindObjectsSortMode.None);

            foreach (var mono in monos)
            {
                if (serviceType.IsInterface)
                {
                    if (serviceType.IsGenericTypeDefinition)
                    {
                        var interfacesFiltered = mono.GetType().GetInterfaces().Where(x => x.IsGenericType && x.GetGenericTypeDefinition() == serviceType);
                        foreach (var i in interfacesFiltered)
                        {
                            yield return (mono, i);
                        }
                    }
                    else
                    {
                        var iface = mono.GetType().GetInterfaces().FirstOrDefault(x => serviceType.IsAssignableFrom(x));
                        if (iface != null)
                        {
                            yield return (mono, iface);
                        }
                    }
                }
                else
                {
                    if (serviceType.IsGenericTypeDefinition)
                    {

                    }

                    throw new NotImplementedException();
                }
            }

        }
    }
}
