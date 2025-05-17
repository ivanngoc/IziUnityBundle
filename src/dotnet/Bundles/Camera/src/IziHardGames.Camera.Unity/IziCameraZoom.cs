using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace IziHardGames
{
    [RequireComponent(typeof(Camera))]
    public class IziCameraZoom : MonoBehaviour
    {
        [SerializeField, Range(0.01f, 10f)] private float zoomSpeed = 0.05f;
        private void Update()
        {
            var axis = Mouse.current.scroll.ReadValue();
            if (axis != default)
            {
                transform.position += transform.forward * (axis.y * zoomSpeed);
            }
        }
    }
}
