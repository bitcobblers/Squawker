using GrainInterfaces.States;

namespace GrainInterfaces.Profiles
{
    public interface IProfilePostsGrain : IGrainWithGuidKey
    {
        Task PostCreated(Post post);
    }
}