namespace IziHardGames.UnityApps.Domain.Events.Updating
{
    public abstract class BaseUpdateEvent : BaseEventInMemory
    {
        public int TotalFrames { get; private set; }
        public float DeltaTime { get; private set; }

        public void InitNewFrame(float deltaTime)
        {
            TotalFrames++;
            DeltaTime = deltaTime;
        }
    }
}
