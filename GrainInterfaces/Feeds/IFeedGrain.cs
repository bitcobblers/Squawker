using GrainInterfaces.States;

namespace GrainInterfaces.Feeds
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
    public interface IFeedGrain : IGrainWithIntegerKey
    {
        Task<Post[]> Get(IFeedRequest request);
        Task<Post[]> Get(Guid[] ids);
    }
}