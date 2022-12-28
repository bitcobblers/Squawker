using Orleans.EventSourcing.CustomStorage;

namespace GrainInterfaces.Model
{
    public interface IGrainEvent<TType>
    {
        void Apply(TType state);
    }

    public abstract class EventJournaledState<TState, TDelta> : ICustomStorageInterface<TState, TDelta>
    where TState : class, new()
    where TDelta : class, IGrainEvent<TState>
    {
        public Task<bool> ApplyUpdatesToStorage(IReadOnlyList<TDelta> updates, int expectedversion)
        {
            throw new NotImplementedException();
        }

        public Task<KeyValuePair<int, TState>> ReadStateFromStorage()
        {
            throw new NotImplementedException();
        }
    }
}
