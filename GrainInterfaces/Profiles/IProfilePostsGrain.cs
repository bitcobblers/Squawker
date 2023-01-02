using GrainInterfaces.States;

namespace GrainInterfaces.Profiles
{
    public interface IProfilePostsGrain : IPostQueryable, IGrainWithGuidKey
    {
        Task PostCreated(Post post);
    }
}