using System.Threading.Tasks;

namespace IziHardGames.Apps.Abstractions.Lib
{
    public interface IStartup
    {
        public void ConfigureStartup(object? serviceCollection);
    }
}
