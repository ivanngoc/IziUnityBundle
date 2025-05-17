using UnityEngine;

namespace IziHardGames.UserControl.Domain.Events
{

    public interface IOnCameraMoveTo
    {
        void MoveToLeft(Vector3 direction);
    }

    public interface IOnCameraMoveToLeft
    {
        void MoveToLeft(Vector3 amount);
    }
}
