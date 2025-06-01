using System;

namespace IziHardGames.GameApps.Units.Contracts
{
    public interface IOwned<T> : IOwned
    {
        T Owner { get; }
    }

    public interface IOwned
    {

    }
}
