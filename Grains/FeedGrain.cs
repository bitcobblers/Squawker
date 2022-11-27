using GrainInterfaces;
using GrainInterfaces.Model;
using Orleans.Concurrency;

namespace Grains
{
    [StatelessWorker]
    public class FeedGrain : Grain, IFeedGrain
    {
        public Task<Post[]> Get(string feedName)
        {
            return Task.FromResult(new[] { new Post() {  Content = "This is a test" } } );
        }
    }
}