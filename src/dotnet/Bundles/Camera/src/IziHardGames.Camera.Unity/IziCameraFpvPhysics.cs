using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace IziHardGames
{
    [RequireComponent(typeof(Camera), typeof(Rigidbody))]
    public class IziCameraFpvPhysics : MonoBehaviour
    {
        [SerializeField, Range(0.01f, 10f)] private float speed = 0.5f;
        [SerializeField] new private Rigidbody rigidbody;

        private void OnValidate()
        {
            rigidbody = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            var w = Keyboard.current[Key.W];
            if (w.ReadValue() != default)
            {
                rigidbody.AddForce(transform.forward * (speed), ForceMode.Force);
            }
            var a = Keyboard.current[Key.A];
            if (a.ReadValue() != default)
            {
                rigidbody.AddForce(transform.right * (-1 * speed), ForceMode.Force);
            }
            var s = Keyboard.current[Key.S];
            if (s.ReadValue() != default)
            {
                rigidbody.AddForce(transform.forward * (-1 * speed), ForceMode.Force);
            }
            var d = Keyboard.current[Key.D];
            if (d.ReadValue() != default)
            {
                rigidbody.AddForce(transform.right * (speed), ForceMode.Force);
            }
        }
    }
}
