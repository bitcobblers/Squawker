using GrainInterfaces.States;

namespace GrainInterfaces.Posts
{
    public interface IPostTrackingGrain : IGrainWithGuidKey
    {
        Task Track(IStatisticsEvent @event);

        Task<Statistics> Get();
    }
}