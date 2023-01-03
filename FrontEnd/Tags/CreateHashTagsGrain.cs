using FrontEnd.Store.RelationalData;
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
        private readonly Regex hashTags = new Regex(@"(#)((?:[A-Za-z0-9-_]*))");
        private readonly IClusterClient client;
        private readonly IRelationalStore store;

        public CreateHashTagsGrain(IClusterClient client, IRelationalStore store)
        {
            this.client = client;
            this.store = store;
        }

        public async Task Create(Post post)
        {
            var hashTags = post.Content
                .SelectMany(section => this.hashTags.Matches(section.Body))
                .Select(match => match.Value.Replace("#", string.Empty));
            
            var results = new List<Task<HashTagLink>>();
            var defaultTracker = client.GetGrain<IHashTagGrain>(string.Empty);
            results.Add(defaultTracker.Link(post));
                        
            foreach (string tag in hashTags)
            {                
                var tagGrain = client.GetGrain<IHashTagGrain>(tag);
                results.Add(tagGrain.Link(post));
            }
            
            var tags = await Task.WhenAll(results);
            if (hashTags.Any())
            {
                var postGrain = client.GetGrain<IPostGrain>(post.Id);
                await postGrain.Update(new UpdatePostRequest() { HashTags = tags });
            }            
        }
    }
}