using GrainInterfaces.Model;

namespace GrainInterfaces.Posts
{
    public interface IPostRepliesGrain : IGrainWithGuidKey
    {
        Task Track(IEngamentTrackingEvent @event);
    }
}