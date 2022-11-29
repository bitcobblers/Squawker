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
            var result = new[] { 
                new Post() { Content = "This is a test1" },
                new Post() { Content = "This is a test2" },    
                new Post() { Content = "This is a test3" },                
            };

            return Task.FromResult(result);
        }
    }
}