using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

namespace UnityEngine
{
    public static class IziFind
    {
        public static IEnumerable<T> FindComponentsAllAtScene<T>(Scene scene) where T : Component
        {
            return FindComponentsAllAtScene<T>(scene.GetRootGameObjects(), new List<T>());
        }
        public static IEnumerable<T> FindComponentsAllAtScene<T>(IEnumerable<GameObject> roots, List<T> buffer) where T : Component
        {
            buffer.Clear();
            foreach (var gameobject in roots)
            {
                var conponentsOfChilds = FindComponentsIncludeDescendants<T>(gameobject.transform, buffer);

                foreach (var component in conponentsOfChilds)
                {
                    yield return component;
                }
            }
        }

        private static IEnumerable<T> FindComponentsIncludeDescendants<T>(Transform transform, List<T> buffer)
        {
            int countChilds = transform.childCount;

            transform.GetComponents<T>(buffer);

            foreach (var component in buffer)
            {
                yield return component;
            }
            buffer.Clear();
            for (int i = 0; i < countChilds; i++)
            {
                var child = transform.GetChild(i);
                var childComponents = FindComponentsIncludeDescendants<T>(child, buffer);

                foreach (var component in childComponents)
                {
                    yield return component;
                }
                buffer.Clear();
            }
        }
    }

}