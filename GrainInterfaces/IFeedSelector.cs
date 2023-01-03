using GrainInterfaces.States;

namespace GrainInterfaces
{
    public interface IFeedSelector : IFeedQuery
    {
        IPostFeed GetQueryable(IClusterClient client);                
    }
}