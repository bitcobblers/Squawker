using GrainInterfaces.States;

namespace GrainInterfaces
{
    public interface IFeedSelector : IFeedQuery
    {
        IPostFeed GetFeed(IClusterClient client);                
    }
}