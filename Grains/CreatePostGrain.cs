using GrainInterfaces;
using Orleans.Concurrency;

namespace Grains
{
    [StatelessWorker]
    public class CreatePostGrain : Orleans.Grain, ICreatePostGrain
    {
        private readonly IClusterClient client;

        public CreatePostGrain(IClusterClient client)
        {
            this.client = client;
        }

        public async Task<Post> Create(Post post, Guid author)
        {
            var postGrain = this.client.GetGrain<IPostGrain>(post.Id);
            var authorGrain = this.client.GetGrain<IAuthorGrain>(author);

            await Task.WhenAll(
                postGrain.Post(post),
                authorGrain.SetPost(post));
            
            return post;
        }
    }
}