using GrainInterfaces.Model;

namespace GrainInterfaces
{
    public interface IFeedGrain: IGrainWithIntegerKey
    {
        Task<Post[]> Get(string feedName);
    }

    public interface ICreatePostGrain: IGrainWithIntegerKey
    {
        Task<Post>  Create(Post post, Guid author);
    }
}