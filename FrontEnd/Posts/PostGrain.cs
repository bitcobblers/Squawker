using GrainInterfaces.Model;
using GrainInterfaces.Model.Index;
using GrainInterfaces.Posts;
using GrainInterfaces.States;

using Orleans.Providers;

namespace Grains.Posts
{       
    [StorageProvider(ProviderName = "Document")]
    public class PostGrain : EventGrain<Post, IPostEvent>, IPostGrain
    {
        private readonly IClusterClient client;

        public PostGrain(IClusterClient client)
        {
            this.client = client;
        }

        public async Task<Post> Get()
        {
            var statistics = client.GetGrain<IPostTrackingGrain>(this.GetPrimaryKey());
            await statistics.View();
            return State;
        }

        public async Task<Post> Create(CreatePostRequest post)
        {
            return RaiseEvent(post);
        }

        public async Task<Post> Update(UpdatePostRequest post)
        {
            return RaiseEvent(post);
        }

        //public Task ReplyToMessage(Guid source, Guid reply)
        //{
        //    var statistics = client.GetGrain<IPostTrackingGrain>(this.GetPrimaryKey());
        //    statistics.Comment();

        //    RaiseEvent(new Post());
        //    return Task.CompletedTask;
        //}
    }
}