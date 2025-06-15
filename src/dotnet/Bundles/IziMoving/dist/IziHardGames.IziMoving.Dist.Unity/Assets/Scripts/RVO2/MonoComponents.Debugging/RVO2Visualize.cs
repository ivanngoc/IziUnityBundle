#if UNITY_EDITOR
using System.Linq;
#endif
using System;
using System.Runtime.InteropServices;
using UnityEngine;
using Agent = IziHardGames.IziMoving.RVO2.Contracts.IRVOAgent<RVO.Line>;

namespace IziHardGames.IziMoving.RVO2.MonoComponents.Debugging
{
    [Guid("a2386c74-a7e9-4b84-95fa-edd9e56d9c48")]
    public class RVO2Visualize : MonoBehaviour
    {
        [SerializeField] Color colorOrcaLine = Color.black;
        public int countObstacles;
        public int countNeighbours;
        private Agent? agent;
        /// <summary>
        /// <seealso cref="RVO.Agent"/>
        /// </summary>
        public Agent Agent => agent ?? throw new InvalidOperationException();
        public void Initlize(Agent agent)
        {
            this.agent = agent;
        }

        private void LateUpdate()
        {
            if (agent != null)
            {
                countObstacles = agent.CountObstacles;
                countNeighbours = agent.CountNeighbours;
            }
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            if (global::UnityEditor.Selection.Contains(gameObject))
            {
                if (agent != null)
                {
                    var arr = agent.Lines.ToArray();

                    for (int i = 0; i < arr.Length; i++)
                    {
                        var line = arr[i];
                        var dir = new Vector3(line.direction.x(), default, line.direction.y());
                        var dot = new Vector3(line.point.x(), default, line.point.y());
                        Debug.DrawLine(dot + transform.position, transform.position + dot + (dir * 1000f), colorOrcaLine);
                    }
                }
            }
        }
#endif

    }
}
