using UnityEngine;

namespace IziHardGames.Ecs.Subscenes.ForUnity
{
    /// <summary>
    /// <see cref="Unity.Entities.Baker"/>
    /// </summary>
    internal class ManagerAuthoring : MonoBehaviour
    {

        public void Awake()
        {
            Debug.LogError("Awaked", this);
        }

        public void Test()
        {
            // Baker<ManagerAuthoring>
            // SceneSystem.LoadSceneAsync();
        }
    }
}
