using GrainInterfaces.Model;
using GrainInterfaces.States;

namespace GrainInterfaces.Posts
{
    public interface ICreatePostGrain : IGrainWithIntegerKey
    {
        Task<Post> Create(CreatePostRequest request);
    }
}