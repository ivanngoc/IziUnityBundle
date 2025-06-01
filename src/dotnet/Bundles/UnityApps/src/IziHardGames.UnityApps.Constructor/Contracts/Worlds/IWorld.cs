using System;
using System.Collections.Generic;
using System.Text;

namespace IziHardGames.UnityApps.Contracts.Worlds
{
    public interface IWorld
    {
        /// <remarks>
        /// Пересоздается при обновлении состава сервисов. не стоит сохранять инстанс в переменную
        /// </remarks>
        IServiceProvider ServiceProvider { get; }
    }
}
