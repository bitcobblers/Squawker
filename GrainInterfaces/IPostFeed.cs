using GrainInterfaces.States;

namespace GrainInterfaces
{
    public interface IPostFeed
    {
        Task<Guid[]> Query(IFeedQuery request);
    }
}