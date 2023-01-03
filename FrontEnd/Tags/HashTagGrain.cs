using FrontEnd.Store.RelationalData;
using GrainInterfaces;
using GrainInterfaces.Model.Index;
using GrainInterfaces.States;
using GrainInterfaces.Tags;

namespace Grains.Tags
{
    public class HashTagGrain : Grain, IHashTagGrain
    {
        private readonly IRelationalStore store;
        private readonly IClusterClient client;
        private readonly int takeLimit = 1000;
        private readonly FixedSizedQueue<HashTagLink> links;

        public HashTagGrain(IRelationalStore store, IClusterClient client)
        {
            links = new FixedSizedQueue<HashTagLink>(takeLimit);
            //this.store = store;
            this.client = client;
        }

        public override Task OnActivateAsync(CancellationToken cancellationToken)
        {
            foreach (var link in store.Tags
                .Where(n => n.Name == IdentityString)
                .OrderByDescending(n => n.PostIndex.TimeStamp)
                .Take(takeLimit)
                .ToArray())
            {
                links.Enqueue(new HashTagLink() { Name = link.Name, PostId = link.PostIndexId, State = link.State });
            }

            return base.OnActivateAsync(cancellationToken);
        }


        public async Task<HashTagLink> Link(Post post)
        {
            var link = new HashTagLink() { Name = IdentityString, PostId = post.Id };

            // await store.HashTagLinks.AddAsync(link);
            links.Enqueue(link);

            return link;
        }
        
        public Task<Guid[]> Query(IFeedQuery request)
        {
            return Task.FromResult(links.Select(n => n.PostId).ToArray());
        }

    }
}