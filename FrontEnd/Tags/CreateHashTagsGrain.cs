using GrainInterfaces.Model;
using GrainInterfaces.Model.Index;
using GrainInterfaces.Posts;
using GrainInterfaces.States;
using GrainInterfaces.Tags;
using Orleans.Concurrency;
using System.Text.RegularExpressions;

namespace Grains.Tags
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

            var postGrain = client.GetGrain<IPostGrain>(post.Id);
            var results = new List<Task<HashTagLink>>();
            foreach (Match hashTag in hashTags)
            {
                var hasTag = client.GetGrain<IHashTagGrain>(hashTag.Value);
                results.Add(hasTag.Link(post));
            }
            var tags = await Task.WhenAll(results);
            await postGrain.Update(new UpdatePostRequest() {  HashTags = tags });

            return;
        }
    }
}