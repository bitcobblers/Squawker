using GrainInterfaces.Model;
using Orleans.EventSourcing;
using Orleans.EventSourcing.CustomStorage;

public class EventJournaledGrain<TState, TDelta> : JournaledGrain<TState, TDelta>
       where TState : class, new()
       where TDelta : class, IGrainEvent<TState>
{
    
}