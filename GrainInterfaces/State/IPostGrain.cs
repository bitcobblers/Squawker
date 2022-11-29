using GrainInterfaces.Model;

namespace GrainInterfaces.State
{

    public interface IPostGrain : IGrainWithGuidKey
    {
        Task<Post> GetContent();

        Task Post(Post post);
    }
}