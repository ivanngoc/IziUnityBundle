using IziHardGames.Geometry.Domain.Vectors;
using IziHardGames.UserControl.Contracts.Events;

namespace IziHardGames.UserControl.Domain.Events
{
    public class OnPointerChangedEvent: IUserControlEvent
    {
        public Vector2RO ScreenPosition { get; set; }
    }
}
