using GrainInterfaces;
using GrainInterfaces.Model;
using GrainInterfaces.States;
using Orleans.Concurrency;

namespace Grains
{
    [StatelessWorker]
    public class FeedGrain : Grain, IFeedGrain
    {
        public Task<Post[]> Get(string feedName)
        {
            var user = Guid.NewGuid();
            var result = new Post[] { 
                new SimpleTextRequest("This is a test1", user),
                new SimpleTextRequest("This is a test2", user),    
                new SimpleTextRequest("This is a test3", user),                
            };

            return Task.FromResult(result);
        }
    }
}