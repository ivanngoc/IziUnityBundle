using System;
using System.Collections.Generic;
using System.Linq;
using RVO;
using UnityEngine;

namespace IziHardGames.IziMoving.Mono.Components
{
    public class RefRvo2System : MonoBehaviour
    {
        [SerializeField] private List<AgentRvo2> agents = new List<AgentRvo2>();
        [SerializeField] private List<ObstacleRvo2> obstacles = new List<ObstacleRvo2>();
        [SerializeField] private int countChanges;
        [SerializeField] private int countChangesObstacles;
        [SerializeField] int maxNeighbours = 10;
        /// <summary>
        /// Как рано происходит обход препятствия
        /// </summary>
        [SerializeField] int timeHorizon = 10;
        [SerializeField] int timeHorizonObst = 10;
        [SerializeField] int timeScale = 1;
        [SerializeField] float timeStep = 1;
        private System.Random? random;
        private Simulator simulator = null!;

        private void Awake()
        {
            simulator = new Simulator();
        }

        public void Initilize()
        {
            simulator = new Simulator();
            this.random = new System.Random();
            RebuildSimulation();
        }

        public void AddAgent(UnityEngine.Object o)
        {
            Debug.Log(o.GetType().AssemblyQualifiedName);
        }
        public void AddAgent(GameObject gameObject)
        {
            AddAgent(gameObject.GetComponent<AgentRvo2>());
        }

        public void AddAgent(AgentRvo2 agent)
        {
            countChanges++;
            agents.Add(agent);
        }
        public void AddObstacle(ObstacleRvo2 obstacle)
        {
            countChanges++;
            countChangesObstacles++;
            obstacles.Add(obstacle);
        }

        private void AddToSimulationObstacle(ObstacleRvo2 item)
        {
            var result = simulator.addObstacle(item.shape.Select(x => new RVO.Vector2(x.x + item.Center.x, x.z + item.Center.z)).ToArray());
            if (result < 0)
            {
                throw new InvalidOperationException();
            }
        }

        private void AddToSimulationAgent(AgentRvo2 agent)
        {
            var pos = agent.Position;
            var id = simulator.addAgent(new RVO.Vector2(pos.x, pos.z), agent.RadiusAvoidance, maxNeighbours, timeHorizon, timeHorizonObst, agent.Radius, agent.Speed, default);
            agent.Bind(id, this);
        }


        public void RemoveAgent(AgentRvo2 agent)
        {
            agents.Remove(agent);
            countChanges++;
            agent.Dispose();
        }


        public void RemoveObStacle(ObstacleRvo2 obstacle)
        {
            countChanges++;
            obstacles.Remove(obstacle);
        }

        public void Step(float deltaTime)
        {
            if (countChanges > 0)
            {
                RebuildSimulation();
                countChanges = 0;
            }

            simulator.setTimeStep(deltaTime);

            if (random == null) throw new NullReferenceException();

            foreach (var agent in agents)
            {
                var vel = (agent.Destination - agent.Position);
                var velPref = new RVO.Vector2(vel.x, vel.z);

                var step = vel.sqrMagnitude;
                var stepFact = step * Time.deltaTime;

                if (RVOMath.absSq(velPref) > 1.0f)
                {
                    velPref = RVOMath.normalize(velPref);
                }

                float angle = (float)random.NextDouble() * 2.0f * (float)Math.PI;
                float dist = (float)random.NextDouble() * 0.0001f;
                velPref = velPref + dist * new RVO.Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));
                simulator.setAgentPrefVelocity(agent.Id, velPref);

                //simulator.setAgentPrefVelocity(agent.Id, velPref);
            }

            simulator.doStep();

            foreach (var agent in agents)
            {
                var pos = simulator.getAgentPosition(agent.Id);
                if (agent.SetPosition(new Vector3(pos.x(), default, pos.y())))
                {
                    // reached goal but keep in simulation as obstacle
                }
            }
        }

        private void RebuildSimulation()
        {
            simulator.Clear();

            foreach (var obs in obstacles)
            {
                AddToSimulationObstacle(obs);
            }

            foreach (var agent in agents)
            {
                AddToSimulationAgent(agent);
            }

            //if (countChangesObstacles > 0)
            //{
            simulator.processObstacles();
            //    countChangesObstacles = 0;
            //}
        }
        private void FixedUpdate()
        {
            Step(Time.fixedDeltaTime * timeScale);

            foreach (var agent in agents)
            {
                agent.transform.position = agent.PositionSimulated;
            }
        }
    }
}
