namespace IziHardGames.DependencyInjection.Contracts.Promises
{
    /// <summary>
    /// Когда создается инстанс этого объекта, то ищется соответствующий <see cref="IPromise{T}"/>. если не найден, то ждет когда наоборот промис найдент соурс
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IPromiseSource<T>
    {

    }
}
