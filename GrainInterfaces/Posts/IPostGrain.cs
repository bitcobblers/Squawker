using GrainInterfaces.Model;
using GrainInterfaces.Model.Index;
using GrainInterfaces.States;

namespace GrainInterfaces.Posts
{

    public interface IPostGrain : IGrainWithGuidKey
    {
        Task<Post> Get();

        Task<Post> Post(CreatePostRequest post);

        Task Tag(HashTagLink[] tags);
    }
}