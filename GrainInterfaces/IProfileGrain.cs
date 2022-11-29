using GrainInterfaces.Model;

namespace GrainInterfaces.State
{
    public interface IProfileGrain : IGrainWithGuidKey
    {
        Task<Profile> Get();

        Task Follow();

        Task Friend();

        Task SetPost(Post post);
    }
}