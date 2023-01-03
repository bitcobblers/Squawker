using GrainInterfaces;
using GrainInterfaces.Model;
using GrainInterfaces.Posts;
using GrainInterfaces.States;
using Microsoft.Extensions.Hosting;
using Orleans.Concurrency;
using System.Reflection.Metadata.Ecma335;

namespace FrontEnd.Posts
{
    [StatelessWorker]
    public class FeedGrain : Grain, IFeedGrain
    {
        private readonly IClusterClient client;

        public FeedGrain(IClusterClient client)
        {
            this.client = client;
        }

        public async Task<Post[]> Query(IFeedSelector request)
        {
            var feed = request.GetFeed(this.client);
            var ids = await feed.Query(request);
            return await this.Get(ids);
        }

        public Task<Post[]> Get(Guid[] ids)
        {
            return Task.WhenAll(ids.Select(n => client.GetGrain<IPostGrain>(n).Get()));
        }
    }
}