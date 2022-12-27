using GrainInterfaces.Model;
using GrainInterfaces.States;

namespace GrainInterfaces.Profiles
{
    public interface IProfilePosts : IGrainWithGuidKey
    {
        Task PostCreated(Post post);
    }
}