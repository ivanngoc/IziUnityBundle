using UnityEngine;

namespace IziHardGames.Ecs.Subscenes.ForUnity
{
    public class IziEcsBootstrap2 //: ICustomBootstrap
    {
        public bool Initialize(string defaultWorldName)
        {
            Debug.Log($"ICustomBootstrap2 {defaultWorldName}");
            return false;
        }
    }
}
