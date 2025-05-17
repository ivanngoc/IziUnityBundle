using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace IziHardGames
{
    [RequireComponent(typeof(Camera))]
    public class IziCameraRotateByDrag : MonoBehaviour
    {
        [SerializeField, Range(0.1f, 10f)] private float speed = 0.05f;

        private void Update()
        {
            var mouse = Mouse.current;
            if (mouse.leftButton.ReadValue() != default)
            {
                var delta = mouse.delta.ReadValue();
                if (delta != default)
                {

                }
            }
            throw new NotImplementedException();
        }
    }
}
