using GrainInterfaces;
using GrainInterfaces.Model;
using GrainInterfaces.State;
using Orleans.Concurrency;

namespace Grains
{

    [StatelessWorker]
    public class CreatePostGrain : Grain, ICreatePostGrain
    {
        private readonly IClusterClient client;

        public CreatePostGrain(IClusterClient client)
        {
            this.client = client;
        }

        public async Task<Post> Create(Post post, Guid author)
        {
            var postGrain = this.client.GetGrain<IPostGrain>(post.Id);
            var authorGrain = this.client.GetGrain<IProfileGrain>(author);

            await Task.WhenAll(
                postGrain.Post(post),
                authorGrain.SetPost(post));
            
            return post;
        }
    }
}