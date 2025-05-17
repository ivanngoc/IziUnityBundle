using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace IziHardGames
{
    /// <summary>
    /// Look araound
    /// </summary>
    [RequireComponent(typeof(Camera))]
    public class IziCameraLookAround : MonoBehaviour
    {
        [SerializeField, Range(0.1f, 10f)] private float speed = 0.05f;
        [SerializeField] private bool isInverseHorizontal;
        [SerializeField] private bool isInverseVertical;
        private void Update()
        {
            var mouse = Mouse.current;
            var delta = mouse.delta.ReadValue();
            if (delta != default)
            {
                transform.Rotate(0, (isInverseVertical ? -speed : speed) * delta.x, 0, Space.World);
                transform.Rotate((isInverseHorizontal ? speed : -speed) * delta.y, 0, 0, Space.Self);
            }
        }
    }
}

