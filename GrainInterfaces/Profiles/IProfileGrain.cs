using GrainInterfaces.Model;

namespace GrainInterfaces.Profiles
{

    public interface IProfileGrain : IGrainWithGuidKey
    {
        Task<Profile> Get();
    }
}