using GrainInterfaces.Model;

public class SnapshotGrain<TState, TDelta> : EventGrain<TState, TDelta> 
    where TState : class, new()
    where TDelta : class, IGrainEvent<TState>
{ 
}

public class EventGrain<TState, TDelta> : Grain
    where TState : class, new()
    where TDelta : class, IGrainEvent<TState>
{

    public TState State { get; set; } = new();

    public TState RaiseEvent(TDelta delta)
    {
        delta.Apply(this.State);
        return this.State;
    }
} 