namespace IziHardGames.UserControl.Contracts
{
    public interface IUserMode
    {
        bool IsEnabled { get; }
        void Enable();
        void Disable();
    }
}
