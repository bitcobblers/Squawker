using GrainInterfaces.Model;
using GrainInterfaces.Model.Index;
using GrainInterfaces.States;

namespace GrainInterfaces.Posts
{

    public interface IPostGrain : IGrainWithGuidKey
    {
        Task<Post> Get();

        Task<Post> Create(CreatePostRequest post);

        Task<Post> Update(UpdatePostRequest post);
    }
}