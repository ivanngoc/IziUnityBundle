using RVO;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Vector2 = RVO.Vector2;
using Random = System.Random;
using System;
using System.Linq;
using static UnityEditor.Progress;
namespace IziHardGames.IziMoving.Mono.Components
{
    public class RefRvo2SystemTest : MonoBehaviour
    {
        internal enum EType
        {
            Block,
            Circle,
        }

        [SerializeField] EType type;
        [SerializeField] float neighborDist = 15f;
        [SerializeField] int maxNeighbors = 10;
        [SerializeField] float timeHorizon = 5.0f;
        [SerializeField] float timeHorizonObst = 5.0f;
        [SerializeField] float maxSpeed = 2f;
        [SerializeField] float radius = 1.5f;
        [SerializeField] float timeScale = 1;
        [SerializeField] public GameObject? testPrefabAgent;
        [Header("Circle")]
        [SerializeField] public int countAgents = 250;

        /* Store the goals of the agents. */
        private IList<Vector2>? goals;
        /** Random number generator. */
        readonly Random random = new Random();
        readonly List<Action> actions = new List<Action>();
        readonly List<Vector3[]> obstacles = new List<Vector3[]>();
        private Simulator simulator = null!;

        private void Awake()
        {
            actions.Clear();
            simulator = new Simulator();
            // Specify the default parameters for agents that are subsequently added.

            simulator.setAgentDefaults(neighborDist, maxNeighbors, timeHorizon, timeHorizonObst, radius, maxSpeed, new Vector2(0.0f, 0.0f));
            switch (type)
            {
                case EType.Block:
                    {
                        SetupTestScenarioBlock();
                        break;
                    }
                case EType.Circle:
                    {
                        SetupTestScenarioCircle();
                        break;
                    }
                default:
                    break;
            }
        }

        private void Update()
        {
            simulator.setTimeStep(Time.deltaTime * timeScale);

            switch (type)
            {
                case EType.Block:
                    {
                        /* Perform (and manipulate) the simulation. */

                        break;
                    }
                case EType.Circle: break;
                default: break;
            }

            if (!reachedGoal())
            {
                setPreferredVelocities();
                simulator.doStep();
            }

            foreach (var action in actions)
            {
                action();
            }
        }

        void setPreferredVelocities()
        {
            /*
             * Set the preferred velocity to be a vector of unit magnitude
             * (speed) in the direction of the goal.
             */
            for (int i = 0; i < simulator.getNumAgents(); ++i)
            {
                Vector2 goalVector = goals[i] - simulator.getAgentPosition(i);

                if (RVOMath.absSq(goalVector) > 1.0f)
                {
                    goalVector = RVOMath.normalize(goalVector);
                }

                simulator.setAgentPrefVelocity(i, goalVector);

                ///* Perturb a little to avoid deadlocks due to perfect symmetry. */
                //float angle = (float)random.NextDouble() * 2.0f * MathF.PI;
                //float dist = (float)random.NextDouble() * 0.0001f;

                //simulator.setAgentPrefVelocity(i, simulator.getAgentPrefVelocity(i) + dist * new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle)));
            }
        }

        bool reachedGoal()
        {
            /* Check if all agents have reached their goals. */
            for (int i = 0; i < simulator.getNumAgents(); ++i)
            {
                if (RVOMath.absSq(simulator.getAgentPosition(i) - goals[i]) > Time.deltaTime)
                {
                    return false;
                }
            }

            return true;
        }


        [ContextMenu("Setup Test Scenario - Circle")]
        public void SetupTestScenarioCircle()
        {
            goals = new List<Vector2>();
            /*
            * Add agents, specifying their start position, and store their
            * goals on the opposite side of the environment.
            */
            for (int i = 0; i < countAgents; ++i)
            {
                var id = simulator.addAgent((countAgents * radius) * new Vector2((float)Math.Cos(i * 2.0f * Math.PI / countAgents), (float)Math.Sin(i * 2.0f * Math.PI / countAgents)));
                goals.Add(-simulator.getAgentPosition(i));
                InitGameObject(id);
            }
        }

        [ContextMenu("Setup Test Scenario - Block")]
        public void SetupTestScenarioBlock()
        {
            goals = new List<Vector2>();
            /* Specify the global time step of the simulation. */



            /*
             * Add agents, specifying their start position, and store their
             * goals on the opposite side of the environment.
             */
            for (int i = 0; i < 5; ++i)
            {
                for (int j = 0; j < 5; ++j)
                {
                    var id0 = simulator.addAgent(new Vector2(55.0f + i * 10.0f, 55.0f + j * 10.0f));
                    goals.Add(new Vector2(-75.0f, -75.0f));

                    var id1 = simulator.addAgent(new Vector2(-55.0f - i * 10.0f, 55.0f + j * 10.0f));
                    goals.Add(new Vector2(75.0f, -75.0f));

                    var id2 = simulator.addAgent(new Vector2(55.0f + i * 10.0f, -55.0f - j * 10.0f));
                    goals.Add(new Vector2(-75.0f, 75.0f));

                    var id3 = simulator.addAgent(new Vector2(-55.0f - i * 10.0f, -55.0f - j * 10.0f));
                    goals.Add(new Vector2(75.0f, 75.0f));

                    InitGameObject(id0);
                    InitGameObject(id1);
                    InitGameObject(id2);
                    InitGameObject(id3);
                }
            }

            /*
             * Add (polygonal) obstacles, specifying their vertices in
             * counterclockwise order.
             */
            IList<Vector2> obstacle1 = new List<Vector2>
            {
                new Vector2(-10.0f, 40.0f),
                new Vector2(-40.0f, 40.0f),
                new Vector2(-40.0f, 10.0f),
                new Vector2(-10.0f, 10.0f)
            };
            simulator.addObstacle(obstacle1);

            IList<Vector2> obstacle2 = new List<Vector2>
            {
                new Vector2(10.0f, 40.0f),
                new Vector2(10.0f, 10.0f),
                new Vector2(40.0f, 10.0f),
                new Vector2(40.0f, 40.0f)
            };
            simulator.addObstacle(obstacle2);

            IList<Vector2> obstacle3 = new List<Vector2>
            {
                new Vector2(10.0f, -40.0f),
                new Vector2(40.0f, -40.0f),
                new Vector2(40.0f, -10.0f),
                new Vector2(10.0f, -10.0f)
            };
            simulator.addObstacle(obstacle3);

            IList<Vector2> obstacle4 = new List<Vector2>
            {
                new Vector2(-10.0f, -40.0f),
                new Vector2(-10.0f, -10.0f),
                new Vector2(-40.0f, -10.0f),
                new Vector2(-40.0f, -40.0f)
            };
            simulator.addObstacle(obstacle4);

            InitObstacle(obstacle1);
            InitObstacle(obstacle2);
            InitObstacle(obstacle3);
            InitObstacle(obstacle4);

            /*
             * Process the obstacles so that they are accounted for in the
             * simulation.
             */
            simulator.processObstacles();
        }

        private void InitObstacle(IList<Vector2> vertevies)
        {
            obstacles.Add(vertevies.Select(x => new Vector3(x.x(), default, x.y())).ToArray());
        }
        private void InitGameObject(int id)
        {
            var go = GameObject.Instantiate(testPrefabAgent);
            Action<GameObject, int> action = (go, id) =>
            {
                var pos = simulator.getAgentPosition(id);
                go.transform.position = new Vector3(pos.x(), default, pos.y());
            };
            action(go, id);
            actions.Add(() => action(go, id));
        }

        private void OnDrawGizmos()
        {
            foreach (var obs in obstacles)
            {
                for (int i = 1; i < obs.Length; i++)
                {
                    Debug.DrawLine(obs[i - 1], obs[i], Color.red);
                }
            }
        }
    }
}
