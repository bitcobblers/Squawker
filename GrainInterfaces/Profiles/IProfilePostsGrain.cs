using GrainInterfaces.States;

namespace GrainInterfaces.Profiles
{
    public interface IProfilePostsGrain : IPostFeed, IGrainWithGuidKey
    {
        Task PostCreated(Post post);
    }
}