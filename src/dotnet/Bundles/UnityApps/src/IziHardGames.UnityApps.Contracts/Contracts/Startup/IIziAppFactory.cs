using System.Threading.Tasks;
using IziHardGames.UnityApps.Contracts.Apps;

namespace IziHardGames.Apps.Abstractions.Lib
{
    public interface IIziAppFactory
    {
        public Task<IIziAppVersion1> CreateAsync(IIziAppBuilder builder);
    }
}
