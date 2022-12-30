
namespace GrainInterfaces.Model
{
    public interface IGrainEvent<TType>
    {
        void Apply(TType state);
    }

    [GenerateSerializer]
    public abstract class EventJournaledState<TState, TDelta> 
    where TState : class, new()
    where TDelta : class, IGrainEvent<TState>
    {    
    }
}
