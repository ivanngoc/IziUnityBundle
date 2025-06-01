using UnityEngine;

namespace IziHardGames.Ecs.Subscenes.ForUnity
{
    // Multiple custom ICustomBootstrap specified, ignoring IziHardGames.Ecs.Subscenes.ForUnity.IziEcsBootstrap
    public class IziEcsBootstrap //: ICustomBootstrap
    {
        public bool Initialize(string defaultWorldName)
        {
            // true if the bootstrap has performed initialization, or false if default world initialization should be performed.
            Debug.Log($"ICustomBootstrap {defaultWorldName}");
            return false;
        }
    }
}
