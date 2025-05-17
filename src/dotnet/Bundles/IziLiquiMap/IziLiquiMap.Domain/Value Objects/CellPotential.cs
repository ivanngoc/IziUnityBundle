using IziHardGames.ValueObjects.Contracts;

namespace IziHardGames.IziLiquiMap.Domain.Value_Objects
{

    public struct CellPotential : IValueObjectOfFloat
    {
        public float Value { get; set; }
    }
}
