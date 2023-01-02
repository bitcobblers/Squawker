using GrainInterfaces;
using GrainInterfaces.Feeds;
using GrainInterfaces.Model;
using GrainInterfaces.States;
using Orleans.Concurrency;

namespace Grains
{

    public interface IFeedRequest
    {
        Guid UserId { get; }
        string Type { get; }

        DateTime? StartingFrom { get; }
        DateTime? EndingBefor { get; }

        int? PageSize { get; }
        int? PageCount { get; }
    }

    [StatelessWorker]
    public class FeedGrain : Grain, IFeedGrain
    {
        public Task<Post[]> Get(IFeedRequest request)
        {
            var user = Guid.NewGuid();
            var result = new Post[] { 
                new SimpleTextRequest("This is a test1", user),
                new SimpleTextRequest("This is a test2", user),    
                new SimpleTextRequest("This is a test3", user),                
            };

            return Task.FromResult(result);
        }
     
        public Task<Post[]> Get(Guid[] ids)
        {
            throw new NotImplementedException();
        }
    }
}