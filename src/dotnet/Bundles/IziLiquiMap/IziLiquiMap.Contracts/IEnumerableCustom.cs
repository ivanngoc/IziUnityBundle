using System.Collections.Generic;

namespace IziHardGames.IziLiquiMap.Contracts
{
    public interface IEnumerableCustom<T, TEtor> where TEtor : IEnumerator<T>
    {
        TEtor GetEnumerator();
    }
}
