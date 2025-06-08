using System;
using System.Collections.Generic;
using UnityEngine;

namespace IziHardGames.IziMoving.RVO2.MonoComponents
{
    public class ObstacleRvo2 : ItemRvo2
    {
        /// <summary>
        /// In Counter clockwise order
        /// </summary>
        [SerializeField] public List<Vector3> shape = new List<Vector3>();
        [SerializeField] private float size = 0.5f;
        [SerializeField] private Vector3 center;
        public Vector3 Center => center;

        public void SetPosition(Vector3 vector)
        {
            center = vector;
        }
        private void OnValidate()
        {
            center = transform.position;
        }

        [ContextMenu("Quad")]
        public void Quad()
        {
            shape.Clear();

            shape.Add(new Vector3(-1, 0, 1) * size);
            shape.Add(new Vector3(-1, 0, -1) * size);
            shape.Add(new Vector3(1, 0, -1) * size);
            shape.Add(new Vector3(1, 0, 1) * size);
        }

        [ContextMenu("Circle-8")]
        public void Circle8()
        {
            shape.Clear();

            int dotCount = 8;
            float radius = size;
            float cx = 0; // center X
            float cy = 0; // center Y

            for (int i = 0; i < dotCount; i++)
            {
                // Angle in radians (360 degrees = 2π radians)
                float angle = -2f * MathF.PI * i / dotCount;

                // Calculate x, y on the circle
                float x = cx + radius * MathF.Cos(angle);
                float y = cy + radius * MathF.Sin(angle);
                shape.Add(new Vector3(x, default, y));
            }
        }

        private void OnDrawGizmos()
        {
            for (int i = 1; i < shape.Count; i++)
            {
                Debug.DrawLine(shape[i - 1] + transform.position, shape[i] + transform.position, Color.magenta);
            }
        }
    }
}
