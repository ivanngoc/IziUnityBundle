using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace IziHardGames
{

    /// <summary>
    /// Перемещение камеры как в шутере. Вперед/Назад - Влево/Вправо
    /// </summary>
    [RequireComponent(typeof(Camera))]
    public class IziCameraFly : MonoBehaviour
    {
        [SerializeField, Range(0.1f, 10f)] private float speed = 0.05f;
        [SerializeField, Range(0.1f, 10f)] private float transitionTime = 0.5f;
        [SerializeField] private float timeLeft;

        //[SerializeField] private Vector3 target;
        [SerializeField] private Vector3 destination;

        private void Update()
        {
            var w = Keyboard.current[Key.W];
            if (w.ReadValue() != default)
            {
                transform.position += transform.forward * (speed);
            }
            var a = Keyboard.current[Key.A];
            if (a.ReadValue() != default)
            {
                transform.position += transform.right * (-1 * speed);
            }
            var s = Keyboard.current[Key.S];
            if (s.ReadValue() != default)
            {
                transform.position += transform.forward * (-1 * speed);
            }
            var d = Keyboard.current[Key.D];
            if (d.ReadValue() != default)
            {
                transform.position += transform.right * (speed);
            }

        }
        private void UpdateV2()
        {
            //target = transform.position;
            var temp = Vector3.zero;
            bool isDelta = false;

            var w = Keyboard.current[Key.W];
            if (w.ReadValue() != default)
            {
                temp += transform.forward * (speed);
                isDelta = true;
            }
            var a = Keyboard.current[Key.A];
            if (a.ReadValue() != default)
            {
                temp += transform.right * (-1 * speed);
                isDelta = true;
            }
            var s = Keyboard.current[Key.S];
            if (s.ReadValue() != default)
            {
                temp += transform.forward * (-1 * speed);
                isDelta = true;
            }
            var d = Keyboard.current[Key.D];
            if (d.ReadValue() != default)
            {
                temp += transform.right * (speed);
                isDelta = true;
            }

            if (isDelta)
            {
                destination = transform.position + temp;
                timeLeft = transitionTime;
            }
            else
            {
                destination = transform.position;
            }

            var length = (transform.position - destination).sqrMagnitude;
            transform.position = Vector3.Lerp(transform.position, destination, 1 - Mathf.Clamp01((timeLeft - Time.deltaTime) / timeLeft));
        }
    }
}
