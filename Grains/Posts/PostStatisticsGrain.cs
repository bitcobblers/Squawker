using GrainInterfaces.Posts;
using GrainInterfaces.States;


namespace Grains.Posts
{
    public class PostStatisticsGrain : EventGrain<Statistics, IStatisticsEvent>, IPostTrackingGrain
    {
        public Task Track(IStatisticsEvent @event)
        {            
            // some conditions here to make sure that raizing this event makes sense
            this.RaiseEvent(@event); 
            return Task.CompletedTask;            
        }

        public async Task<Statistics> Get() => this.State;
    }
}