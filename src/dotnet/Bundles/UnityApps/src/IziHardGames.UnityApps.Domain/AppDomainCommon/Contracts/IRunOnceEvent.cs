namespace IziHardGames.CommonDomain.Contracts
{
    /// <summary>
    /// Event occurred only once per application runtime.
    /// If consumer/subscriber acess event before it raised, than it scheduled to when this event completes.
    /// If event is already done than consumer's handler will be executed immediatly
    /// </summary>
    public interface IRunOnceEvent : IDomainEvent
    {

    }
}
