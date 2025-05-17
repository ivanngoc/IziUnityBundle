using System;
using System.Runtime.InteropServices;

namespace IziHardGames.Apps.Abstractions.Lib
{
    [Guid("28add382-3d8d-41db-b779-1c573cf9ea36")]
    public interface IAppEnterPoint
    {
        public void Run(IServiceProvider provider);
    }
}
