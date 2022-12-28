using GrainInterfaces.States;

namespace GrainInterfaces
{
    public interface IFeedGrain: IGrainWithIntegerKey
    {
        Task<Post[]> Get(string feedName);
    }
}