using System;
using System.Collections.Generic;

namespace IziHardGames.DependencyInjection.Contracts
{
    public interface IAddServicesInstances
    {
        IEnumerable<(Type service, object instance)> GetServiceInstanceToAdd();
    }
}
