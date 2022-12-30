using GrainInterfaces.Model;
using GrainInterfaces.Model.Index;
using GrainInterfaces.Posts;
using GrainInterfaces.States;

using Orleans.Providers;

namespace Grains.Posts
{       
    [StorageProvider(ProviderName = "Document")]
    public class PostGrain : EventGrain<Post, IGrainEvent<Post>>, IPostGrain
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

        public Task Tag(HashTagLink[] tags)
        {
            if (tags is null)
            {
                throw new ArgumentNullException(nameof(tags));
            }

            throw new NotImplementedException();
        }


        public async Task<Post> Post(CreatePostRequest post)
        {
            return RaiseEvent(post);
        }
    }
}