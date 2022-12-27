using GrainInterfaces.States;

namespace GrainInterfaces.Posts
{
    public interface IPostRepliesGrain : IGrainWithGuidKey
    {
        Task Track(IStatisticsEvent @event);
    }
}