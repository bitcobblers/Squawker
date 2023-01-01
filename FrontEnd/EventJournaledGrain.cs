using GrainInterfaces.Model;

public abstract class EventGrain<TState, TDelta> : Grain<TState>
    where TState : class, new()
    where TDelta : class, IGrainEvent<TState>
{           
    public TState RaiseEvent(TDelta delta)
    {
        delta.Apply(this.State);
        return this.State;
    }
} 