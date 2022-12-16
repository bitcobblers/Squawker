using GrainInterfaces.Model;
using GrainInterfaces.Model.Index;

namespace GrainInterfaces.Posts
{

    public interface IPostGrain : IGrainWithGuidKey
    {
        Task<Post> Get();

        Task<Post> Post(RequestPost post);

        Task Tag(HashTagLink[] tags);
    }
}