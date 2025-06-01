using System;
using System.Collections.Generic;

namespace IziHardGames.DependencyInjection.Contracts
{
    public interface IAddServicesType
    {
        IEnumerable<(Type service, Type impl)> GetServiceImplementationTypeToAdd();
    }
}
