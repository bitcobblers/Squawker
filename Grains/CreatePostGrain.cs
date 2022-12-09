using GrainInterfaces;
using GrainInterfaces.Model;
using GrainInterfaces.State;
using Grains.RelationalData;
using Orleans.Concurrency;
using System.Text.RegularExpressions;

namespace Grains
{
    [StatelessWorker] 
    public class CreateHashTagsGrain : Grain, ICreateHashTagsGrain
    {
        private readonly Regex hashTags = new Regex(@"\B(\#[a-zA-Z]+\b)(?!;)");
        private readonly IClusterClient client;

        public CreateHashTagsGrain(IClusterClient client)
        {
            this.client = client;
        }

        public async Task Create(Post post)
        {
            var hashTags = this.hashTags.Matches(post.Content);
            if (hashTags.Count == 0)
            {
                return;
            }
            
            var postGrain = this.client.GetGrain<IPostGrain>(post.Id);
            var results = new List<Task<HastTagLink>>(); 
            foreach (Match hashTag in hashTags)
            {
                var hasTag = this.client.GetGrain<IHashTagGrain>(hashTag.Value);
                results.Add(hasTag.Link(post));                
            }
            var tags = await Task.WhenAll(results);
            await postGrain.LinkHashTags(tags);

            return;
        }
    }

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
            
            // process for hashtags and notify the hashtag actors.


            await Task.WhenAll(
                postGrain.Post(post),
                authorGrain.SetPost(post));
            
            return post;
        }
    }
}