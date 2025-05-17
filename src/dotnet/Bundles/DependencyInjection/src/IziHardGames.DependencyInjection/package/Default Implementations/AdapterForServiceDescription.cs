using Microsoft.Extensions.DependencyInjection;

namespace IziHardGames.DependencyInjection.Implementations
{
    public struct AdapterForServiceDescription
    {
        internal readonly ServiceDescriptor descriptor;
        internal AdapterForServiceDescription(ServiceDescriptor descriptor)
        {
            this.descriptor = descriptor;
        }
    }
}
