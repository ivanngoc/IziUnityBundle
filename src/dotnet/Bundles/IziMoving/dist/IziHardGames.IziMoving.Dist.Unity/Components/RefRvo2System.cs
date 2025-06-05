using System;
using System.Collections.Generic;
using RVO;
using UnityEngine;

namespace IziHardGames.IziMoving.Mono.Components
{
    public class RefRvo2System : MonoBehaviour
    {
        [SerializeField] private List<AgentRvo2> agents = new List<AgentRvo2>();
        [SerializeField] private int countChanges;
        [SerializeField] int maxNeighbours = 10;
        /// <summary>
        /// Как рано происходит обход препятствия
        /// </summary>
        [SerializeField] int timeHorizon = 10;
        [SerializeField] int timeHorizonObst = 10;
        [SerializeField] int timeScale = 1;
        [SerializeField] float timeStep = 1;
        private System.Random? random;

        public void Initilize()
        {
            this.random = new System.Random();
            Simulator.Instance.Clear();
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

        private void AddAgentToSimulation(AgentRvo2 agent)
        {
            var pos = agent.Position;
            var id = Simulator.Instance.addAgent(new RVO.Vector2(pos.x, pos.z), agent.RadiusAvoidance, maxNeighbours, timeHorizon, timeHorizonObst, agent.Radius, agent.Speed, default);
            agent.Bind(id, this);
        }

        public void RemoveAgent(AgentRvo2 agent)
        {
            agents.Remove(agent);
            countChanges++;
            agent.Dispose();
        }
        public void Step(float deltaTime)
        {
            if (countChanges > 0)
            {
                RebuildSimulation();
                countChanges = 0;
            }

            Simulator.Instance.setTimeStep(deltaTime);

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
                Simulator.Instance.setAgentPrefVelocity(agent.Id, velPref);

                //Simulator.Instance.setAgentPrefVelocity(agent.Id, velPref);
            }

            Simulator.Instance.doStep();

            foreach (var agent in agents)
            {
                var pos = Simulator.Instance.getAgentPosition(agent.Id);
                if (agent.SetPosition(new Vector3(pos.x(), default, pos.y())))
                {
                    // reached goal but keep in simulation as obstacle
                }
            }
        }

        private void RebuildSimulation()
        {
            Simulator.Instance.Clear();

            foreach (var agent in agents)
            {
                AddAgentToSimulation(agent);
            }
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
