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

    public class AuthorGrain : Orleans.Grain<Author>, IAuthorGrain
    {
        public async Task Follow()
        {
            return;
        }

        public async Task Friend()
        {
            return;
        }

        public async Task<Author> Get()
        {
            return this.State;
        }

        public async Task SetPost(Post post)
        {
            return;
        }
    }
}