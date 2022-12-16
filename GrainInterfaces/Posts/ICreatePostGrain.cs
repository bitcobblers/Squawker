using GrainInterfaces.Model;

namespace GrainInterfaces.Posts
{
    public interface ICreatePostGrain : IGrainWithIntegerKey
    {
        Task<Post> Create(RequestPost post);
    }
}