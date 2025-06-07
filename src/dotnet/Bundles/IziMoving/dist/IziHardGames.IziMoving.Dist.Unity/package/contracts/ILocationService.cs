namespace IziHardGames.Libs.MovingSystem.Contracts
{
    public interface ILocationService<TPos>
    {
        int PositionToKey(TPos position);
    }
}