using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IziHardGames.UnityApps.Contracts.Apps;
using IziHardGames.UnityApps.Contracts.DependencyInjection;

namespace IziHardGames.Apps.Contracts
{
    /// <summary>
    /// Полученная из пресета схема с ссыдками на префабы и т.д. 
    /// </summary>
    public interface IIziAppScheme
    {
        T GetBuilder<T>(IIziAppScheme scheme) where T : class, IIziAppBuilder;
        IEnumerable<(Type, Type)> GetSingletons();
        IEnumerable<(Type, Type)> GetTransients();
        IEnumerable<Action<object>> GetStartups();
        /// <summary>
        /// Get <see cref="IIziService"/>
        /// </summary>
        /// <returns></returns>
        IEnumerable<Type> GetServices();
    }
}
