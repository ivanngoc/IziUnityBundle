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
using IziHardGames.CommonDomain.Events;
using IziHardGames.UnityApps.Contracts;
using UnityEngine;

namespace IziHardGames.Apps.ForUnity
{
    [Bootstrap]
    public sealed class BootstrapMono : MonoBehaviour
    {
        [SerializeField] AppBuilderMono? appBuilderMono;

        private void Awake()
        {
            appBuilderMono?.Build().Then<IIziApp>(x =>
            {
                var publisher = x.ServiceProvider.GetService(typeof(IDomainEventPublisher)) as IDomainEventPublisher;
                if (publisher != null)
                {
                    publisher.Publish(new DomainEventOnAppBuilded());

                }
                else
                {
                    throw new NullReferenceException();
                }
            });
        }
    }

    public class BuildContinuation
    {
        public void Then<TApp>(Action<TApp> app)
        {
            throw new NotImplementedException();
        }
    }
}