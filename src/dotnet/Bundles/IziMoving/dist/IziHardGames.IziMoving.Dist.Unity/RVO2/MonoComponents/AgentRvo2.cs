using System;
using System.Runtime.InteropServices;
using UnityEngine;

namespace IziHardGames.IziMoving.RVO2.MonoComponents
{
    [Guid("c045915f-a4fe-413d-bdba-a7d4253344f4")]
    public class AgentRvo2 : ItemRvo2
    {
        public Vector3 PositionSimulated => positionSimulated;
        public Vector3 Position => positionSimulated;
        public Vector3 Destination => destination;
        public bool Reached => reached;
        public float Radius => radius;
        public float RadiusAvoidance => radiusAvoidance;
        public float Speed => speed;
        /// <summary>
        /// Растояние на котором юниты начинают обходить друг друга в окружную
        /// Так как юниты сталкиваются друг с другом то лучше в таких случаях делать значение = 1. Как только коснется юнита
        /// </summary>
        [SerializeField, Min(0.001f)] private float radiusAvoidance = 10;
        [SerializeField, Min(0.001f)] private float radius = 0.5f;
        /// <summary>
        /// на какой дистанции относительно радиуса будет считаться достигнутой цель. По умолчанию квадрат(1/5 от <see cref="radius"/>)
        /// </summary>
        [SerializeField, Min(0.001f)] private float goalToleranceSqr = 0.5f * 0.2f * (0.5f * 0.2f);
        [SerializeField, Min(0.001f)] private float speed = 1;
        [SerializeField] private Vector3 destination;
        [SerializeField] private Vector3 positionSimulated;
        [SerializeField] private bool reached;
        public RVO.Vector2 Velocity { get; private set; }


        public void SetVelocity(RVO.Vector2 velocity)
        {
            this.Velocity = velocity;
        }

        public bool SetPosition(Vector3 vector3)
        {
            if (!reached)
            {
                var delta = destination - vector3;
                reached = delta.sqrMagnitude < goalToleranceSqr;
                positionSimulated = reached ? destination : vector3;
            }
            return reached;
        }

        /// <param name="perc01">Perc of <see cref="Radius"/></param>
        public void SetGoalTolerancePerc(float perc01)
        {
            goalToleranceSqr = radius * perc01 * (radius * perc01);
        }

        public void SetDestination(Vector3 vector3)
        {
            reached = false;
            destination = vector3;
        }

        public void Die()
        {
            system?.RemoveAgent(this);
        }

        public void Dispose()
        {
            Velocity = default;
            system = default;
        }
    }
}
