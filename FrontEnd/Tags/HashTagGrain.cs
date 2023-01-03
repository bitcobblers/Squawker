using GrainInterfaces;
using GrainInterfaces.Model.Index;
using GrainInterfaces.Posts;
using GrainInterfaces.States;
using GrainInterfaces.Tags;
using Grains.RelationalData;

namespace Grains.Tags
{
    public class HashTagGrain : Grain, IHashTagGrain
    {
        private readonly IRelationalStore store;
        private readonly IClusterClient client;
        private readonly int takeLimit = 1000;
        private readonly FixedSizedQueue<HashTagLink> links;

        public HashTagGrain(/*IRelationalStore store,*/ IClusterClient client)
        {
            links = new FixedSizedQueue<HashTagLink>(takeLimit);
            //this.store = store;
            this.client = client;
        }

        public override Task OnActivateAsync(CancellationToken cancellationToken)
        {
            //foreach (var link in store.HashTagLinks
            //    .Where(n => n.Name == IdentityString)
            //    .OrderByDescending(n => n.TimeStamp)
            //    .Take(takeLimit)
            //    .ToArray())
            //{
            //    links.Enqueue(link);
            //}

            return base.OnActivateAsync(cancellationToken);
        }


        public async Task<HashTagLink> Link(Post post)
        {
            var link = new HashTagLink() { Name = IdentityString, Post = post.Id, ProfileId = post.Author };

            // await store.HashTagLinks.AddAsync(link);
            links.Enqueue(link);

            return link;
        }
        
        public Task<Guid[]> Query(IFeedQuery request)
        {
            return Task.FromResult(links.Select(n => n.Post).ToArray());
        }

    }
}