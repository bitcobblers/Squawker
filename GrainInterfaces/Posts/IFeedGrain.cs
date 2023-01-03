using GrainInterfaces.States;

namespace GrainInterfaces.Posts
{

    public interface IFeedGrain :  IGrainWithIntegerKey
    {        
        Task<Post[]> Get(Guid[] ids);
        
        Task<Post[]> Query(IFeedSelector request);
    }
}