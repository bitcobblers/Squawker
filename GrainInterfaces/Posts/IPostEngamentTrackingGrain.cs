using GrainInterfaces.Model;

namespace GrainInterfaces.Posts
{
    public interface IPostEngamentTrackingGrain : IGrainWithGuidKey
    {
        Task Track(IEngamentTrackingEvent @event);
    }
}