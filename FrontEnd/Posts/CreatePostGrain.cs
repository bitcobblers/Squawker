using GrainInterfaces.Model;
using GrainInterfaces.Posts;
using GrainInterfaces.Profiles;
using GrainInterfaces.States;
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

        public async Task<Post> Create(CreatePostRequest post)
        {
            var createTags = client.GetGrain<ICreateHashTagsGrain>(0);
            var authorGrain = client.GetGrain<IProfilePostsGrain>(post.Author);
            var postGrain = client.GetGrain<IPostGrain>(post.Id);
            var replyFromTask = Task.CompletedTask;
            
            var createdPost = await postGrain.Create(post);
            
            if (post.ReplyTo.HasValue)
            {
                var replyTo = client.GetGrain<IPostGrain>(post.ReplyTo.Value);
                // replyFromTask = replyTo.ReplyFrom(createdPost.Id);
                
            }
                        
            await Task.WhenAll(
                authorGrain.PostCreated(createdPost),
                createTags.Create(createdPost),
                replyFromTask);

            return await postGrain.Create(post);
        }
    }
}