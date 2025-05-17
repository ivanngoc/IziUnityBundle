using System.Threading;
using IziHardGames.ValueObjects.Contracts;

namespace IziHardGames.IziLiquiMap.Domain.Value_Objects
{
    public struct CellStacksDynamic : IValueObjectOfInt
    {
        public int Value { get => value; set => this.value = value; }
        public int value;

        public CellStacksDynamic(int value)
        {
            this.value = value;
        }

        public static implicit operator CellStacksDynamic(CellStacks value) => new CellStacksDynamic(value.Value);

        public void Increment()
        {
            Interlocked.Increment(ref this.value);
        }

        public void Decrement()
        {
            Interlocked.Decrement(ref this.value);
        }
    }
}
