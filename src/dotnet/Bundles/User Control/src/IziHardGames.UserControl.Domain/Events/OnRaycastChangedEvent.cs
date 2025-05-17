using IziHardGames.Geometry.Domain.Vectors;

namespace IziHardGames.UserControl.Domain.Events
{
    public class OnRaycastChangedEvent : UserControlEvent
    {
        public Vector3RO PositionOfCamera { get; set; }
        public Vector3RO DirectionOfCamera { get; set; }
        public Vector3RO Ray { get; set; }
        public Vector3RO RayNormilized { get; set; }
    }
}
