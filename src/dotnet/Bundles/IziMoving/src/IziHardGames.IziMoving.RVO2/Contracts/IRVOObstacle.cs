namespace IziHardGames.IziMoving.RVO2.Contracts
{
    public interface IRVOObstacle
    {
        IRVOObstacle? Previous { get; }
        IRVOObstacle? Next { get; }
        (float x, float y) Direction { get; }
        (float x, float y) Point { get; }
        bool Convex { get; }
    }
}
