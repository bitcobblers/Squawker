using GrainInterfaces.Model;

namespace GrainInterfaces.Profiles
{
    public interface IProfilePosts : IGrainWithGuidKey
    {
        Task PostCreated(Post post);
    }
}