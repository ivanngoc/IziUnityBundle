using IziHardGames.EventSourcing.Contracts.Events;
using IziHardGames.Geometry.Domain.Vectors;
using IziHardGames.UserControl.Contracts.Events;

namespace IziHardGames.UserControl.Domain.Events
{
    public class OnClickEvent : IUserControlEvent
    {
        public int Button { get; private set; }
        public Vector2RO ScreenPosition { get; private set; }

        public void Initialize(int button, Vector2RO screenPosition)
        {
            this.Button = button;
            this.ScreenPosition = screenPosition;
        }
    }
}
