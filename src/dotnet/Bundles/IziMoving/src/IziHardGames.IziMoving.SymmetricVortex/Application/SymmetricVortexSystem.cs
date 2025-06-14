using System;
using System.Collections.Generic;
using IziHardGames.Geometry.Domain.Vectors;
using IziHardGames.IziMoving.Contracts.Application;
using IziHardGames.IziMoving.SymmetricVortex.Domain.Entities;

namespace IziHardGames.IziMoving.SymmetricVortex.Application
{
    /// <summary>
    /// Non parallel.
    /// Comon case. 
    /// Explicit algo.
    /// No perfomance optimizations
    /// </summary>
    public class SymmetricVortexSystem<TAgent, TObstacle> : IMoveSystem
        where TAgent : SymmetricVortexSystem<TAgent, TObstacle>.IAgent
        where TObstacle : SymmetricVortexSystem<TAgent, TObstacle>.IObstacle
    {
        public interface IObstacle
        {
            IEnumerable<Vector3RO> GetShape();
        }

        public interface IAgent
        {
            float Radius { get; }
            Vector2RO VelocityPrefered { get; }
            /// <summary>
            /// Velocity after simmulation step
            /// </summary>
            Vector2RO VelocityCalculated { get; internal set; }
            /// <summary>
            /// Velocity at begining of calculating. Previous frame velocity
            /// </summary>
            Vector2RO Velocity { get; set; }


            IEnumerable<TAgent> GetNeighbours();
            IEnumerable<TObstacle> GetObstacles();
            void AddLine(Line line);
            void CalculateNewVelocity();
            void Clear();
        }

        public void Execute(IEnumerable<TAgent> agents)
        {
            foreach (var agent in agents)
            {
                agent.Clear();

                var obstacles = agent.GetObstacles();

                foreach (var obs in obstacles)
                {
                    var obsCalc = new ObstacleCalculation(obs, agent);
                    var line = obsCalc.GetOrcaLine();
                    agent.AddLine(line);
                }

                var neighbours = agent.GetNeighbours();
                foreach (var neighbour in neighbours)
                {
                    var calc = new AgentsCalculation(agent, neighbour);
                    var line = calc.GetOrcaLine();
                    agent.AddLine(line);
                }
                agent.CalculateNewVelocity();
            }
        }

        public struct Config
        {
            /// <summary>
            /// step time
            /// </summary>
            public float deltaTime;
        }

        public struct AgentsCalculation
        {
            private readonly TAgent agent;
            private readonly TAgent neighbour;
            public AgentsCalculation(TAgent agent, TAgent neighbour)
            {
                this.agent = agent;
                this.neighbour = neighbour;
            }

            internal Line GetOrcaLine()
            {
                throw new NotImplementedException();
            }
        }

        public struct ObstacleCalculation
        {
            private readonly TObstacle obstacle;
            private readonly TAgent agent;
            public ObstacleCalculation(TObstacle obstacle, TAgent agent)
            {
                this.obstacle = obstacle;
                this.agent = agent;
            }
            internal Line GetOrcaLine()
            {
                var shape = obstacle.GetShape();
                throw new NotImplementedException();
            }
        }
    }
}
