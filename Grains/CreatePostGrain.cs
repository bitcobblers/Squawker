using GrainInterfaces.Model;
using GrainInterfaces.Model.Index;
using GrainInterfaces.Posts;
using GrainInterfaces.Profiles;
using GrainInterfaces.Tags;
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
            var hashTags = post.Content.SelectMany(section => this.hashTags.Matches(section.Body));
            if (!hashTags.Any())
            {
                return;
            }
            
            var postGrain = this.client.GetGrain<IPostGrain>(post.Id);
            var results = new List<Task<HashTagLink>>(); 
            foreach (Match hashTag in hashTags)
            {
                var hasTag = this.client.GetGrain<IHashTagGrain>(hashTag.Value);
                results.Add(hasTag.Link(post));                
            }
            var tags = await Task.WhenAll(results);
            await postGrain.Tag(tags);

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

        public async Task<Post> Create(RequestPost post)
        {
            var postGrain = this.client.GetGrain<IPostGrain>(post.Id);
            var authorGrain = this.client.GetGrain<IProfilePosts>(post.Author);
            var hashtag = this.client.GetGrain<ILi>
            // process for hashtags and notify the hashtag actors.


            var test = await Task.WhenAll(
                postGrain.Post((RequestPost)post),                
                authorGrain.PostCreated(post));
            
            return post;
        }
    }
}