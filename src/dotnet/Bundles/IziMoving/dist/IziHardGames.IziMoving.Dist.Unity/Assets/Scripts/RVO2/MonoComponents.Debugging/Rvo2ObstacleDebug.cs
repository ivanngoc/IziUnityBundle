using System.Collections.Generic;
using System.Security.Cryptography;
using IziHardGames.IziMoving.RVO2.Contracts;
using IziHardGames.IziMoving.SymmetricVortex.Domain.Entities;
using RVO;
using UnityEngine;
using Vector2 = RVO.Vector2;

namespace IziHardGames
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// Its best not to use obstacle avoidance in RVO. beter use path finding + RVO
    /// RVO know nothing about shortest path
    /// </remarks>
    [ExecuteAlways]
    public class Rvo2ObstacleDebug : MonoBehaviour
    {
        [Header("Setup")]
        [SerializeField] bool isCopyActualPosition;
        [SerializeField] Vector3 position;
        [SerializeField] Vector3 velocity_ = Vector3.right;
        [SerializeField] float timeHorizonObst_ = 10f;
        [SerializeField] float radius_ = 0.5f;
        [SerializeField] public Vector3 corner1 = new Vector3(5, 1);
        [SerializeField] public Vector3 corner2 = new Vector3(5, -1);
        //[SerializeField] bool convex_ = true;
        [Header("Runtime")]
        [SerializeField] public Vector3 relativePosition1;
        [SerializeField] public Vector3 relativePosition2;
        [SerializeField] float distSq1;
        [SerializeField] float distSq2;
        [SerializeField] float radiusSq;
        [SerializeField] public Vector3 obstacleVector;
        [SerializeField] float s;
        [SerializeField] float distSqLine;
        [SerializeField] Vector3 direction1;
        [SerializeField] Vector3 direction2;
        [SerializeField] Vector3 leftLegDirection;
        [SerializeField] Vector3 rightLegDirection;
        [SerializeField] Vector3 orcaDir;
        [SerializeField] Vector3 orcaPoint;
        [SerializeField] Vector3 leftCutOff;
        [SerializeField] Vector3 rightCutOff;
        [SerializeField] Vector3 cutOffVector;
        [SerializeField] private float distSqCutoff;
        [SerializeField] private float distSqLeft;
        [SerializeField] private float distSqRight;

        private void Update()
        {
            if (isCopyActualPosition)
            {
                position = transform.position;
            }
            else
            {
                transform.position = position;
            }
            float invTimeHorizonObst = 1.0f / timeHorizonObst_;

            var sim = new Simulator();
            var id = sim.addObstacle(new List<Vector2>() { new Vector2(corner1.x, corner1.y), new Vector2(corner2.x, corner2.y) });
            var obstacle = sim.GetObstacle(id);

            var obstacle1 = obstacle;
            var obstacle2 = obstacle1.Next;

            //sim.getOb
            //IRVOAgent
            //IRVOObstacle
            var p1 = GetPoint(obstacle1);
            var p2 = GetPoint(obstacle2);

            relativePosition1 = p1 - position;
            relativePosition2 = p2 - position;
            direction1 = (corner2 - corner1).normalized;
            direction2 = (corner1 - corner2).normalized;
            distSq1 = relativePosition1.sqrMagnitude;
            distSq2 = relativePosition2.sqrMagnitude;
            radiusSq = radius_ * radius_;
            obstacleVector = p2 - p1;
            s = (Vector3.Dot(-relativePosition1, obstacleVector)) / obstacleVector.sqrMagnitude;
            distSqLine = (-relativePosition1 - s * obstacleVector).sqrMagnitude;
            var step = Time.deltaTime;
            RVO.Line line = default;

            if (s < 0.0f && distSq1 <= radiusSq)
            {
                /* Collision with left vertex. Ignore if non-convex. */
                if (obstacle1.Convex)
                {
                    var p = new Vector3(0.0f, 0.0f);
                    line.point = new RVO.Vector2(p.x, p.y);
                    var n = new Vector3(-relativePosition1.y, relativePosition1.x).normalized;
                    line.direction = new RVO.Vector2(n.x, n.y);
                }

                goto END;
            }
            else if (s > 1.0f && distSq2 <= radiusSq)
            {
                /*
                 * Collision with right vertex. Ignore if non-convex or if
                 * it will be taken care of by neighboring obstacle.
                 */
                if (obstacle2.Convex && Det(relativePosition2, direction2) >= 0.0f)
                {
                    line.point = new RVO.Vector2(0.0f, 0.0f);
                    var n = new Vector3(-relativePosition2.y, relativePosition2.x).normalized;
                    line.direction = new RVO.Vector2(n.x, n.y);
                }

                goto END;
            }
            else if (s >= 0.0f && s <= 1.0f && distSqLine <= radiusSq)
            {
                /* Collision with obstacle segment. */
                line.point = new RVO.Vector2(0.0f, 0.0f);
                if (distSq1 < distSq2)
                {
                    var d = -GetDirection(obstacle1);
                    line.direction = new RVO.Vector2(d.x, d.y);
                }
                else
                {
                    var d = GetDirection(obstacle1);
                    line.direction = new RVO.Vector2(d.x, d.y);
                }
                goto END;
            }


            if (s < 0.0f && distSqLine <= radiusSq)
            {
                /*
                 * Obstacle viewed obliquely so that left vertex
                 * defines velocity obstacle.
                 */
                if (!obstacle1.Convex)
                {
                    /* Ignore obstacle. */
                    goto END;
                }

                obstacle2 = obstacle1;

                float leg1 = Mathf.Sqrt(distSq1 - radiusSq);
                leftLegDirection = new Vector3(relativePosition1.x * leg1 - relativePosition1.y * radius_, relativePosition1.x * radius_ + relativePosition1.y * leg1) / distSq1;
                rightLegDirection = new Vector3(relativePosition1.x * leg1 + relativePosition1.y * radius_, -relativePosition1.x * radius_ + relativePosition1.y * leg1) / distSq1;
            }
            else if (s > 1.0f && distSqLine <= radiusSq)
            {
                /*
                 * Obstacle viewed obliquely so that
                 * right vertex defines velocity obstacle.
                 */
                if (obstacle2.Convex)
                {
                    /* Ignore obstacle. */
                    goto END;
                }

                obstacle1 = obstacle2;

                float leg2 = Mathf.Sqrt(distSq2 - radiusSq);
                leftLegDirection = new Vector3(relativePosition2.x * leg2 - relativePosition2.y * radius_, relativePosition2.x * radius_ + relativePosition2.y * leg2) / distSq2;
                rightLegDirection = new Vector3(relativePosition2.x * leg2 + relativePosition2.y * radius_, -relativePosition2.x * radius_ + relativePosition2.y * leg2) / distSq2;
            }
            else
            {
                /* Usual situation. */
                if (obstacle1.Convex)
                {
                    float leg1 = Mathf.Sqrt(distSq1 - radiusSq);
                    leftLegDirection = new Vector3(relativePosition1.x * leg1 - relativePosition1.y * radius_, relativePosition1.x * radius_ + relativePosition1.y * leg1) / distSq1;
                }
                else
                {
                    /* Left vertex non-convex; left leg extends cut-off line. */
                    leftLegDirection = -GetDirection(obstacle1);
                }

                if (obstacle2.Convex)
                {
                    float leg2 = Mathf.Sqrt(distSq2 - radiusSq);
                    rightLegDirection = new Vector3(relativePosition2.x * leg2 + relativePosition2.y * radius_, -relativePosition2.x * radius_ + relativePosition2.y * leg2) / distSq2;
                }
                else
                {
                    /* Right vertex non-convex; right leg extends cut-off line. */
                    rightLegDirection = GetDirection(obstacle1);
                }
            }


            var leftNeighbor = obstacle1.Previous;

            bool isLeftLegForeign = false;
            bool isRightLegForeign = false;

            if (obstacle1.Convex && Det(leftLegDirection, -GetDirection(leftNeighbor)) >= 0.0f)
            {
                /* Left leg points into obstacle. */
                leftLegDirection = -GetDirection(leftNeighbor);
                isLeftLegForeign = true;
            }

            if (obstacle2.Convex && Det(rightLegDirection, GetDirection(obstacle2)) <= 0.0f)
            {
                /* Right leg points into obstacle. */
                rightLegDirection = GetDirection(obstacle2);
                isRightLegForeign = true;
            }

            /* Compute cut-off centers. */
            this.leftCutOff = invTimeHorizonObst * (GetPoint(obstacle1) - position);
            this.rightCutOff = invTimeHorizonObst * (GetPoint(obstacle2) - position);
            this.cutOffVector = rightCutOff - leftCutOff;

            /* Project current velocity on velocity obstacle. */

            /* Check if current velocity is projected on cutoff circles. */
            float t = obstacle1 == obstacle2 ? 0.5f : (Vector3.Dot((velocity_ - leftCutOff), cutOffVector)) / cutOffVector.sqrMagnitude;
            float tLeft = Vector3.Dot((velocity_ - leftCutOff), leftLegDirection);
            float tRight = Vector3.Dot((velocity_ - rightCutOff), rightLegDirection);

            if ((t < 0.0f && tLeft < 0.0f) || (obstacle1 == obstacle2 && tLeft < 0.0f && tRight < 0.0f))
            {
                /* Project on left cut-off circle. */
                Vector3 unitW = (velocity_ - leftCutOff).normalized;
                line.direction = new Vector2(unitW.y, -unitW.x);
                line.point = new Vector2(leftCutOff.x, leftCutOff.y) + radius_ * invTimeHorizonObst * new Vector2(unitW.x, unitW.y);
                goto END;
            }
            else if (t > 1.0f && tRight < 0.0f)
            {
                /* Project on right cut-off circle. */
                Vector3 unitW = (velocity_ - rightCutOff).normalized;
                line.direction = new Vector2(unitW.y, -unitW.x);
                line.point = new Vector2(rightCutOff.x, rightCutOff.y) + radius_ * invTimeHorizonObst * new Vector2(unitW.x, unitW.y);
                goto END;
            }


            /*
                 * Project on left leg, right leg, or cut-off line, whichever is
                 * closest to velocity.
                 */
            this.distSqCutoff = (t < 0.0f || t > 1.0f || obstacle1 == obstacle2) ? float.PositiveInfinity : (velocity_ - (leftCutOff + t * cutOffVector)).sqrMagnitude;
            this.distSqLeft = tLeft < 0.0f ? float.PositiveInfinity : (velocity_ - (leftCutOff + tLeft * leftLegDirection)).sqrMagnitude;
            this.distSqRight = tRight < 0.0f ? float.PositiveInfinity : (velocity_ - (rightCutOff + tRight * rightLegDirection)).sqrMagnitude;

            var isInfinityLeft = distSqLeft == float.PositiveInfinity;
            var isInfinityRight = distSqRight == float.PositiveInfinity;

            if (float.IsFinite(distSqCutoff))
            {
                if (isInfinityLeft && isInfinityRight)
                {
                    if (distSq1 < distSq2)
                    {
                        this.distSqLeft = distSqCutoff / 2;
                    }
                    else
                    {
                        this.distSqRight = distSqCutoff / 2;
                    }
                }
                else if (isInfinityLeft)
                {
                    this.distSqRight = distSqCutoff / 2;
                }
                else if (isInfinityRight)
                {
                    this.distSqLeft = distSqCutoff / 2;
                }
            }

            if (distSqCutoff <= distSqLeft && distSqCutoff <= distSqRight)
            {
                /* Project on cut-off line. */
                line.direction = -new Vector2(obstacle1.Direction.x, obstacle1.Direction.y);
                line.point = new Vector2(leftCutOff.x, leftCutOff.y) + radius_ * invTimeHorizonObst * new Vector2(-line.direction.y(), line.direction.x());
                goto END;
            }

            if (distSqLeft <= distSqRight)
            {
                /* Project on left leg. */
                if (isLeftLegForeign)
                {
                    goto END;
                }

                line.direction = new Vector2(leftLegDirection.x, leftLegDirection.y);
                line.point = new Vector2(leftCutOff.x, leftCutOff.y) + radius_ * invTimeHorizonObst * new Vector2(-line.direction.y(), line.direction.x());

                goto END;
            }

            /* Project on right leg. */
            if (isRightLegForeign)
            {
                goto END;
            }

            line.direction = -new Vector2(rightLegDirection.x, rightLegDirection.y);
            line.point = new Vector2(rightCutOff.x, rightCutOff.y) + radius_ * invTimeHorizonObst * new Vector2(-line.direction.y(), line.direction.x());

            END:
            orcaDir = new Vector3(line.direction.x(), line.direction.y());
            orcaPoint = new Vector3(line.point.x(), line.point.y());
        }

        private void OnDrawGizmos()
        {
            Debug.DrawLine(position, corner1, Color.magenta);
            Debug.DrawLine(position, corner2, Color.magenta);
            Debug.DrawLine(position, position + obstacleVector, Color.gray);
            Debug.DrawLine(position, position + leftLegDirection, Color.green);
            Debug.DrawLine(position, position + rightLegDirection, Color.red);
            Debug.DrawLine(position + orcaPoint, position + orcaPoint + (orcaDir * 1000), Color.cyan);
            Debug.DrawLine(position, position + leftCutOff, Color.yellow);
            Debug.DrawLine(position, position + rightCutOff, Color.yellow);
            Debug.DrawLine(position + leftCutOff, position + rightCutOff, Color.yellow);

        }


        public static float Det(UnityEngine.Vector3 a, UnityEngine.Vector3 b)
        {
            return Det((UnityEngine.Vector2)a, (UnityEngine.Vector2)b);
        }

        public static float Det(UnityEngine.Vector2 a, UnityEngine.Vector2 b)
        {
            return a.x * b.y - a.y * b.x;
        }

        public Vector3 GetPoint(IRVOObstacle obstacle)
        {
            return new Vector3(obstacle.Point.x, obstacle.Point.y);
        }

        public Vector3 GetDirection(IRVOObstacle obstacle)
        {
            return new Vector3(obstacle.Direction.x, obstacle.Direction.y);
        }
    }
}
