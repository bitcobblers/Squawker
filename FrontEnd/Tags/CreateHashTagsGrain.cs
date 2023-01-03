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
            var hasTags = false;
            var hashTags = post.Content
                .SelectMany(section => this.hashTags.Matches(section.Body))
                .Select(match => match.Value.Replace("#", string.Empty));

            var requests = new[] { string.Empty }.Concat(hashTags)
                .Select(tag => client.GetGrain<IHashTagGrain>(tag).Link(post));
            
            var tags = await Task.WhenAll(requests);
            foreach (var tag in tags.Where(n=>n.Name != string.Empty))
            {
                hasTags = true;
                this.store.Tags.Add(tag);
            }
            
            if (hasTags)
            {
                var postGrain = client.GetGrain<IPostGrain>(post.Id);
                await postGrain.Update(new UpdatePostRequest() { HashTags = tags });                
            }            
        }
    }
}