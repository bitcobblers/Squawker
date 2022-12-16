using GrainInterfaces.Model;
using GrainInterfaces.Posts;
using GrainInterfaces.Profiles;
using GrainInterfaces.Tags;
using Orleans.Concurrency;

namespace Grains.Posts
{

    [StatelessWorker]
    public class CreatePostGrain : Grain, ICreatePostGrain
    {
        private readonly IClusterClient client;

        public CreatePostGrain(IClusterClient client)
        {
            this.client = client;
        }

        public async Task<Post> Create(RequestPost post)
        {
            var createTags = client.GetGrain<ICreateHashTagsGrain>(0);
            var authorGrain = client.GetGrain<IProfilePosts>(post.Author);
            var postGrain = client.GetGrain<IPostGrain>(post.Id);

            var createdPost = await postGrain.Post(post);
            await Task.WhenAll(
                authorGrain.PostCreated(createdPost),
                createTags.Create(createdPost));

            return await postGrain.Post(post);
        }
    }
}