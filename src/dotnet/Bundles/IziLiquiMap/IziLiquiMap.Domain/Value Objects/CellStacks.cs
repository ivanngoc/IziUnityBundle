using IziHardGames.ValueObjects.Contracts;

namespace IziHardGames.IziLiquiMap.Domain.Value_Objects
{
    public struct CellStacks : IValueObjectOfInt
    {
        public int Value { get => value; set => this.value = value; }
        public int value;

        public CellStacks(int value)
        {
            this.value = value;
        }
    }
}
