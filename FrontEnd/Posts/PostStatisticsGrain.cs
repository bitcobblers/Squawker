using GrainInterfaces.Posts;
using GrainInterfaces.States;


namespace Grains.Posts
{
    public class PostStatisticsGrain : EventGrain<Statistics, IStatisticsEvent>, IPostTrackingGrain

    {
        public async Task<Statistics> Get() => this.State;

        public Task Comment()
        {
            this.RaiseEvent(new PostCommentEvent());
            return Task.CompletedTask;            
        }               

        public Task React(IReaction reaction)
        {
            this.RaiseEvent(new PostReactionEvent(reaction));
            return Task.CompletedTask;
        }

        public Task View()
        {
            this.RaiseEvent(new PostViewEvent());
            return Task.CompletedTask;
        }
    }
}