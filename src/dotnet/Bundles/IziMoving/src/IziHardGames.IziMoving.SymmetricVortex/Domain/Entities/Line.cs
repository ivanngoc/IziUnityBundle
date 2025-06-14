using IziHardGames.Geometry.Domain.Vectors;
using IziHardGames.IziMoving.Contracts.Domain.Algo;

namespace IziHardGames.IziMoving.SymmetricVortex.Domain.Entities
{
    public struct Line : IOrcaLine
    {
        public Vector2RO point;
        public Vector2RO direction;
    }
}
