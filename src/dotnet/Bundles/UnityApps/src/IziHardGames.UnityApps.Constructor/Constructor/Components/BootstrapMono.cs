#nullable enable
//using System;
//using System.Linq;
//using IziHardGames.AppConstructor;
//using IziHardGames.ApplicationLevel;
//using IziHardGames.Apps.Abstractions.Lib;
//using IziHardGames.CommonDomain.Contracts;
//using IziHardGames.CommonDomain.Events;
using System;
using IziHardGames.Attributes;
using IziHardGames.CommonDomain.Contracts;
using IziHardGames.EventSourcing.Contracts.Infrastracture;
using IziHardGames.UnityApps.Contracts;
using IziHardGames.UnityApps.Contracts.Apps;
using IziHardGames.UnityApps.Domain.Events;
using Microsoft.Extensions.DependencyInjection;
using UnityEngine;

namespace IziHardGames.Apps.ForUnity
{
    [Bootstrap]
    public sealed class BootstrapMono : MonoBehaviour
    {
        [SerializeField] AppBuilderMono? appBuilderMono;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            appBuilderMono?.Build().Then<IIziApp>(x =>
            {
                var serv = x.ServiceProvider.GetRequiredService(typeof(IInMemoryEventPublisher));
                var publisher = serv as IInMemoryEventPublisher;
                if (publisher != null)
                {
                    publisher.Publish(new OnAppBuildedEvent(x));
                }
                else
                {
                    throw new InvalidCastException(serv.GetType().AssemblyQualifiedName);
                }
            });
        }
    }
}